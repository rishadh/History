//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Class_Room.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Course
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int? Credit { get; set; }
        public int? Lecture { get; set; }

        public virtual User User { get; set; }
        public virtual List<Enrollment> Enrollments { get; set; }
    }

    public class CourseDropdown {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }
}
