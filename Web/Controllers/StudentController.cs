using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.DAL;
using Web.Models;
using Web.ViewModels;
using WebGrease.Activities;

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

        public PartialViewResult GetStudents(SizeIndex sizeIndex = SizeIndex.All)
        {
            var studentContext = context.Students;
            var studentList = studentContext.ToList();
            if (sizeIndex != SizeIndex.All)
            {
                studentList = studentContext.Take((int)sizeIndex).ToList();
            }
            var studenViewModels = new List<StudentViewModel>();
            foreach (var student in studentList)
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
            return PartialView("_GetStudents", studenViewModels);
        }

        public ActionResult GetStudent(SizeIndex sizeIndex = SizeIndex.All)
        {
            return View(sizeIndex);
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

        public ActionResult Edit(long? id)
        {
            if (id < 1) return RedirectToAction("Index");
            var findStudent = context.Students.FirstOrDefault(s => s.Id == id);
            if (findStudent == null) return RedirectToAction("Index");
            var studentModel = new StudentViewModel()
            {
                Id = findStudent.Id,
                FullName = findStudent.FullName,
                Sex = findStudent.Sex,
                Age = findStudent.Age,
                BirthDate = findStudent.BirthDate,
                BirthPlace = findStudent.BirthPlace
            };
            return View(studentModel);
        }

        [HttpPost]
        public ActionResult EditPost(StudentViewModel studentModel)
        {
            if (studentModel == null) return RedirectToAction("Index");
            var findStudent = context.Students.FirstOrDefault(s => s.Id == studentModel.Id);
            if (findStudent == null) return RedirectToAction("Index");
            findStudent.Id = studentModel.Id;
            findStudent.FullName = studentModel.FullName;
            findStudent.Sex = studentModel.Sex;
            findStudent.Age = studentModel.Age;
            findStudent.BirthDate = studentModel.BirthDate;
            findStudent.BirthPlace = studentModel.BirthPlace;
            context.Students.AddOrUpdate(findStudent);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Override route (~), harus mengaktifkan attribute route di RouteConfig.cs
        [Route("~/Murid/Hapus/{id}")]
        public ActionResult Delete(long? id)
        {
            if (id < 1) return RedirectToAction("Index");
            var findStudent = context.Students.FirstOrDefault(s => s.Id == id);
            if (findStudent == null) return RedirectToAction("Index");
            context.Students.Remove(findStudent);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}