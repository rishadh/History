using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Class_Room.Models;

namespace Class_Room.Controllers
{
    public class CourseController : Controller
    {
        private Class_RoomEntities3 db = new Class_RoomEntities3();

        public ActionResult Index()
        {
            return View();
        }

        // GET: Course
        public JsonResult IndexCourse()
        {
            var Courses = db.Courses.Include(e => e.User);
            var modelList = db.Courses.ToList().Select(x => new Course() {CourseID = x.CourseID,Title = x.Title, Credit =x.Credit, Lecture =x.Lecture}).ToList();
            return Json(modelList, JsonRequestBehavior.AllowGet);
        }

        // GET: Lecture
        public JsonResult GetLecture(int? id)
        {
            User user = db.Users.Find(id);
            var ModelList = db.Users.Where(a => a.Type.ToLower() == "lecture").ToList();
             return Json(ModelList,JsonRequestBehavior.AllowGet);
        }
        // GET: Course/Create
        public ActionResult Create()
        {
            ViewBag.Lecture = new SelectList(db.Users, "ID", "FirstMidName");
            return View();
        }

        // POST: Course/Create
        [HttpPost]
        public JsonResult CreateCourse(Course course)
        {
            string status = String.Empty;
               db.Courses.Add(course);
               db.SaveChanges();
                status = "success";
            ViewBag.Lecture = new SelectList(db.Users, "ID", "FirstMidName", course.Lecture);
            return Json(status);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            Course course = db.Courses.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.Lecture = new SelectList(db.Users.Where(a => a.Type.ToLower() == "lecture"), "ID", "FirstMidName", course.Lecture);
            return View(course);
        }

        // POST: Course/Edit/5
        [HttpPost]
        public JsonResult EditCourse(Course course)
        {
            string status = String.Empty;
            var courseDetails = db.Courses.Where(a => a.CourseID == course.CourseID).FirstOrDefault();
            if (courseDetails != null)
            {
                courseDetails.Title = course.Title;
                courseDetails.Credit = course.Credit;
                courseDetails.Lecture = course.Lecture;
                db.SaveChanges();
                status = "success";
            }
            else
            {
                status = "failed";
            }
            ViewBag.Lecture = new SelectList(db.Users, "ID", "FirstMidName", course.Lecture);
            return Json(status);
        }

        // POST: Course/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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
