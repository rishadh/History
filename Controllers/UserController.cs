using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Class_Room.Models;
using Login = Class_Room.Models.Login;

namespace Class_Room.Controllers
{
    public class UserController : Controller
    {
        private Class_RoomEntities3 db = new Class_RoomEntities3();
        //GET: User
        public ActionResult Index()
        {
            
            return View();
        }

        //GET: User
        public JsonResult IndexUser()
        {
            var sessionTypeInfo = "";
            sessionTypeInfo = Session["Type"] as string;
            sessionTypeInfo = sessionTypeInfo != null ? sessionTypeInfo.Trim().ToLower() : sessionTypeInfo;
            var modelList = new UserDB(db).ListAll(sessionTypeInfo);
            return Json(modelList, JsonRequestBehavior.AllowGet);
        }

        // GET: User/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: User/Register
        [HttpPost]
        public JsonResult RegisterUser(User user)
        {
            var status = "failed";
            if (ModelState.IsValid)
            {
                 status = new UserDB(db).AddUser(user);
            }
            return Json(status);
        }


        //Log In
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //LogIn POST
        [HttpPost]
        public JsonResult Login(Login login)
        {
            var status = new UserDB(db).Login(login);
            return Json(status);
        }
            
            
        // GET: User/Edit/5
        public ActionResult Edit()
        {
            var userId = Session["UserId"];
            User user = db.Users.Find((int)userId);

            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                return View(user);
            }
            
        }

        // POST: User/Edit/5
        [HttpPost]
        public JsonResult EditUser(User user)
        {
            var status = "failed";
            if (ModelState.IsValid)
                {
                status = new UserDB(db).EditUser(user);
                }
            else
                {
                status = "failed";
                }
            return Json(status);

        }

        [HttpGet]
        public new ActionResult Profile()
        {
           
            return View();
        }

        // GET: User/Profile/5
        [HttpGet]
        public JsonResult ProfileUser()
        {
            var userId = (int)Session["UserId"];
            var modelList = new UserDB(db).ProfileUser(userId);
            return Json(modelList, JsonRequestBehavior.AllowGet);
        }

        // POST: User/Delete/5
        [HttpPost]
        public JsonResult Delete(int? ID)
        {
             string status = string.Empty;
             status = new UserDB(db).DeleteUser(ID);
            
            return Json(status);
        }

        //Logout 
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["UserId"] = null;
            Session.Abandon();
            Session.Clear();
            HttpContext.Response.Cache.SetAllowResponseInBrowserHistory(false);
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Redirect("");

            return RedirectToAction("Login", "User");
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
