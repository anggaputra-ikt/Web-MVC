using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public string FullName { get; set; }
        public Sex Sex { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
    }

    public enum Sex
    {
        Male,
        Female
    }

    public enum SizeIndex
    {
        All = 0,
        Three = 3,
        Five = 5,
        Ten = 10
    }
}