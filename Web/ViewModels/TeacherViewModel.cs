using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.ViewModels
{
    public class TeacherViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Please Enter Full Name."), Display(Name = "Full Name")]
        public string FullName { get; set; }
        public Sex Sex { get; set; }
        public int Age { get; set; }
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Birth Place")]
        public string BirthPlace { get; set; }
    }
}