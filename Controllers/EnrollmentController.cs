using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Class_Room.Models;

namespace Class_Room.Controllers
{
    public class EnrollmentController : Controller
    {
        private Class_RoomEntities3 db = new Class_RoomEntities3();

        public ActionResult Index()
        {
            return View();
        }
        // GET: Enrollment
        public JsonResult IndexEnrollment()
        {
            var Enrollments = db.Enrollments.Include(e => e.Course).Include(e => e.User);
            var ModelList = db.Enrollments.ToList().Select(x => new Enrollment() { EnrollmentID = x.EnrollmentID, CourseID = x.CourseID, UserID = x.UserID, Grade = x.Grade }).ToList();
            return Json(ModelList, JsonRequestBehavior.AllowGet);
        }

        // GET: Course
        [HttpGet]
        public JsonResult GetCourse(int? id)
        {
            Course course = db.Courses.Find(id);
            var ModelList = db.Courses.Select(x => new CourseDropdown {
                CourseId= x.CourseID,
                CourseName =x.Title
            }).ToList();
            return Json(ModelList, JsonRequestBehavior.AllowGet);
        }
        // GET: Student
        public JsonResult GetUser(int? id)
        {
            User user = db.Users.Find(id);
            var ModelList = db.Users.Where(a => a.Type.ToLower() == "student").ToList();
            return Json(ModelList, JsonRequestBehavior.AllowGet);
        }

        // GET: Enrollment/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title");
            ViewBag.UserID = new SelectList(db.Users.Where(x => x.Type.ToLower() == "student"), "ID", "FirstMidName");
            return View();
        }

        // POST: Enrollment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EnrollmentCreate(Enrollment enrollment)
        {

            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewBag.UserID = new SelectList(db.Users.Where(x => x.Type.ToLower() == "student"), "ID", "FirstMidName", enrollment.UserID);
            return Json(enrollment, JsonRequestBehavior.AllowGet);
        }

        // GET: Enrollment/Edit/5
        public ActionResult Edit(int? id)
        {
      
            Enrollment enrollment = db.Enrollments.Find((int)id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewBag.UserID = new SelectList(db.Users.Where(x => x.Type.ToLower() == "student"), "ID", "FirstMidName", enrollment.UserID);
            return View(enrollment);
        }

        // POST: Enrollment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EnrollmentEdit(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "FirstMidName", enrollment.UserID);
            return Json(enrollment, JsonRequestBehavior.AllowGet);
        }

        // POST: Enrollment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(enrollment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
