using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemMVC.Services;

namespace SchoolManagementSystemMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IStudentService _studentService;

        public AccountController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (await _studentService.ValidateLoginAsync(username, password))
            {
                return RedirectToAction("List", "Student");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }
    }
}