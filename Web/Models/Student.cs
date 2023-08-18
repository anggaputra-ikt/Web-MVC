using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Identity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    }
    public interface IPerson
    {
        string FullName { get; set; }
        Sex Sex { get; set; }
        int Age { get; set; }
        DateTime BirthDate { get; set; }
        string BirthPlace { get; set; }
    }
    public class Student : Identity, IPerson
    {
        public string FullName { get; set; }
        public Sex Sex { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
    }

    public class Teacher : Identity, IPerson
    {
        public string FullName { get; set; }
        public Sex Sex { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
    }

    public class Course : Identity
    {
        public string Title { get; set; }
        public long TeacherId { get; set; }

        [ForeignKey(nameof(TeacherId))]
        public Teacher Teacher { get; set; }
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