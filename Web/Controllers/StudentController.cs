using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<ActionResult> Index()
        {
            var students = await context.Students.ToListAsync();
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

        [HttpPost]
        public async Task<ActionResult> GetStudentAsync(string searchIndex, SizeIndex sizeIndex = SizeIndex.All)
        {
            // Dapatkan context student
            var studentContext = context.Students;
            // Konversi list
            var studentList = await studentContext.ToListAsync();
            // Inisiasi search student dengan tipe queryable student
            IQueryable<Student> studentSearch = null;
            // Jika search tidak kosong
            if (!string.IsNullOrEmpty(searchIndex))
            {
                // Masukan context dengan filter name sesuai search
                studentSearch = studentContext.Where(student => student.FullName.Contains(searchIndex));
            }
            // Jika size index dipilih
            if (sizeIndex != SizeIndex.All)
            {
                // Jika search tidak kosong
                if (studentSearch != null)
                {
                    // Ambil data dengan filter search dan jumlah sesuai size index
                    studentList = await studentSearch.Take((int)sizeIndex).ToListAsync();
                }
                else
                {
                    // Ambil data sesuai size index
                    studentList = await studentContext.Take((int)sizeIndex).ToListAsync();
                }
            }
            // Jika search tidak kosong
            if (studentSearch != null)
            {
                // Konversi list
                studentList = await studentSearch.ToListAsync();
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
            return PartialView("_GetStudent", studenViewModels);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost(StudentViewModel studentViewModel)
        {
            var student = new Student()
            {
                FullName = studentViewModel.FullName,
                Sex = studentViewModel.Sex,
                Age = studentViewModel.Age,
                BirthDate = studentViewModel.BirthDate,
                BirthPlace = studentViewModel.BirthPlace
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

        public ActionResult Details(long? id)
        {
            if (id < 1) return RedirectToAction("Index");
            var findStudent = context.Students.FirstOrDefault(s => s.Id == id);
            if (findStudent == null) return RedirectToAction("Index");
            var studentViewModel = new StudentViewModel()
            {
                Id = findStudent.Id,
                Age = findStudent.Age,
                BirthDate = findStudent.BirthDate,
                BirthPlace = findStudent.BirthPlace,
                FullName = findStudent.FullName,
                Sex = findStudent.Sex
            };
            return View(studentViewModel);
        }
    }
}