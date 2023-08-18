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
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext context = new ApplicationDbContext();
        public List<CourseViewModel> Courses { get; set; }
        public List<Teacher> Teachers { get; set; }

        // GET: Course
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Teachers = context.Teachers.ToList();
            return View();
        }

        public ActionResult CreatePost(CourseViewModel courseViewModel)
        {
            var course = new Course()
            {
                Title = courseViewModel.Title,
                TeacherId = courseViewModel.TeacherId
            };
            context.Courses.Add(course);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}