using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.DAL;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext context = new ApplicationDbContext();

        public List<StudentViewModel> Students { get; set; }

        public ActionResult Index()
        {
            var students = context.Students.ToList();
            var studenViewModels = new List<StudentViewModel>();
            foreach (var student in students)
            {
                var studentViewModel = new StudentViewModel()
                {
                    Id = student.Id,
                    FullName = student.FullName,
                    Sex = student.Sex,
                    Age = student.Age,
                    BirthDate = student.BirthDate,
                    BirthPlace = student.BirthPlace
                };
                studenViewModels.Add(studentViewModel);
            }
            Students = studenViewModels;
            return View(Students);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost(StudentViewModel studentModel)
        {
            var student = new Student()
            {
                FullName = studentModel.FullName,
                Sex = studentModel.Sex,
                Age = studentModel.Age,
                BirthDate = studentModel.BirthDate,
                BirthPlace = studentModel.BirthPlace
            };
            context.Students.Add(student);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}