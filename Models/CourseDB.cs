using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Class_Room.Models;

namespace Class_Room.Models
{
    public class CourseDB
    {
        private Class_RoomEntities3 db;

        public CourseDB(Class_RoomEntities3 context)
        {
            db = context;
        }

        //Get all Course method
        public List<Course> ListAll()
        {
            var Courses = db.Courses;
            var List = db.Courses.ToList();

            return List;
        }
    }
}