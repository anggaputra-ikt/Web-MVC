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
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext context = new ApplicationDbContext();
        public List<TeacherViewModel> Teachers { get; set; }

        // GET: Teacher
        public ActionResult Index()
        {
            var teachers = context.Teachers.ToList();
            var teacherViewModels = new List<TeacherViewModel>();
            foreach (var teacher in teachers)
            {
                var teacherViewModel = new TeacherViewModel()
                {
                    Id = teacher.Id,
                    FullName = teacher.FullName,
                    Sex = teacher.Sex,
                    Age = teacher.Age,
                    BirthDate = teacher.BirthDate,
                    BirthPlace = teacher.BirthPlace
                };
                teacherViewModels.Add(teacherViewModel);
            }
            Teachers = teacherViewModels;
            return View(Teachers);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost(TeacherViewModel teacherViewModel)
        {
            var teacher = new Teacher()
            {
                FullName = teacherViewModel.FullName,
                Sex = teacherViewModel.Sex,
                Age = teacherViewModel.Age,
                BirthDate = teacherViewModel.BirthDate,
                BirthPlace = teacherViewModel.BirthPlace
            };
            context.Teachers.Add(teacher);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}