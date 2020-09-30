using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Class_Room.Models
{
    public class UserDB
    {
        private Class_RoomEntities3 db;

        public UserDB(Class_RoomEntities3 context)
        {
            db = context;
        }
        //Get All User Details
        public List<User> ListAll(string sessionTypeInfo)
        {
            var type = sessionTypeInfo == "lecture" ? "student" : "lecture";
            var list = db.Users.Where(a => a.Type == type).ToList().Select(x=>new User{
                FirstMidName = x.FirstMidName,
                LastName = x.LastName,
                EnrollmentDate = x.EnrollmentDate,
                ID = x.ID,
                ImagePath = x.ImagePath,
                Type = x.Type
            }).ToList();
            return list;
        }

        //Register Method
        public string AddUser(User user)
        {
            string status = String.Empty;
            #region //username is Already Exist!
            var IsExist = IsUsernameExist(user.FirstMidName);
            if (IsExist)
            {
                status = "failed";
                return status;
            }
            #endregion

            #region //Password Hashing

            user.Password = Crypto.Hash(user.Password);
            user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
            #endregion

            //upload avatar
            string fileName = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
            string extension = Path.GetExtension(user.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            user.ImagePath = "~/Avatar/" + fileName;
            fileName = Path.Combine(HttpContext.Current.Server.MapPath("~/Avatar/"), fileName);
            user.ImageFile.SaveAs(fileName);

            #region //save to database
            db.Users.Add(user);
            db.SaveChanges();
            #endregion

            status = "success";
        
            return status;
        }

        //Edit method
        public string EditUser(User user)
        {
            string status = String.Empty;
            try
            {
                #region 
                //username is Already Exist!
                var IsExist = IsUsernameExist(user.FirstMidName);
                if (IsExist)
                {
                    status = "failed";
                    return status;
                }
                #endregion
                //upload avatar
                string fileName = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
                string extension = Path.GetExtension(user.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                user.ImagePath = "~/Avatar/" + fileName;
                fileName = Path.Combine(HttpContext.Current.Server.MapPath("~/Avatar/"), fileName);
                user.ImageFile.SaveAs(fileName);

                var userDetails = db.Users.Where(a => a.ID == user.ID).FirstOrDefault();
                if (userDetails != null)
                {
                    userDetails.FirstMidName = user.FirstMidName;
                    userDetails.LastName = user.LastName;
                    userDetails.ImagePath = user.ImagePath;
                    db.SaveChanges();
                    status = "success";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return status;
        }

        //Login Method
        public string Login(Login login)
        {
            string status = String.Empty;
            var userDetails = db.Users.Where(a => a.FirstMidName == login.UserName).FirstOrDefault();
            if (userDetails != null)
            {
                if (string.Compare(Crypto.Hash(login.Password), userDetails.Password) == 0)
                {

                    int timeout = login.RememberMe ? 1000 : 120;
                    var ticket = new FormsAuthenticationTicket(login.UserName, login.RememberMe, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    cookie.HttpOnly = true;
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    HttpContext.Current.Session["LoggedIn"] = true;
                    HttpContext.Current.Session["Type"] = userDetails.Type;
                    HttpContext.Current.Session["UserId"] = userDetails.ID;
                    status = "success";
                }
                else
                {
                    status = "failed";
                }
            }
            else
            {
                status = "failed";
            }

            return status;
        }

        //Profile Method
        public User ProfileUser(int userId)
        {
            var data = db.Users.Where(a => a.ID == userId).FirstOrDefault();
            return data;
        }

        //Delete Method
        public string DeleteUser(int? ID)
        {
            string status = String.Empty;
            var userDetail = db.Users.Where(a => a.ID == ID).FirstOrDefault();
            //User user = db.Users.Find(id);
            db.Users.Remove(userDetail);
            db.SaveChanges();
            status = "success";
            return status;
        }

        public bool IsUsernameExist(string username)
        {

            var v = db.Users.Where(a => a.FirstMidName == username).FirstOrDefault();
            return v != null;

        }

    }
}