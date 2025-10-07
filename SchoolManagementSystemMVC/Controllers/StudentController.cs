using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemMVC.Models;
using SchoolManagementSystemMVC.Services;

namespace SchoolManagementSystemMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Student student)
        {

            // FIX: Clear validation errors for navigation properties
            var keysToRemove = ModelState.Keys
                .Where(k => k.Contains("Qualifications[") && k.Contains(".Student"))
                .ToList();

            foreach (var key in keysToRemove)
            {
                ModelState.Remove(key);
            }

            if (ModelState.IsValid)
            {
                await _studentService.CreateStudentAsync(student);
                TempData["Success"] = $"Student {student.FirstName} {student.LastName} registered successfully with ID: {student.StudentId}!";
                return RedirectToAction("Register");
            }
            return View(student);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return View(students);
        }
    }
}