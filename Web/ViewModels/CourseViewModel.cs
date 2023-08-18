using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class CourseViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        [Display(Name = "Teacher")]
        public long TeacherId { get; set; }
    }
}