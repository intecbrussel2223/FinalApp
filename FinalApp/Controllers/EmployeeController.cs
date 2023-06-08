using FinalApp.Data;
using FinalApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace FinalApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageHelper _imageHelper;
        public EmployeeController(ApplicationDbContext context,
            IImageHelper imageHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.Include(d => d.Department).ToListAsync();
            return View(employees);
        }

        public async Task<IActionResult> Create()
        {
            //var departments = _context.Departments.ToListAsync();
            ViewBag.Departments = new SelectList(await _context.Departments.ToListAsync(), "DepartmentId", "Name");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Departments = new SelectList(await _context.Departments.ToListAsync(), "DepartmentId", "Name");
            var employee = _context.Employees.FirstOrDefault(employee => employee.EmployeeId == id);
            if (employee.UrlToPicture != null)
            {
                string imageUrl = _imageHelper.GetImageUrl(employee.UrlToPicture);
                ViewBag.ImageUrl = imageUrl;
            }
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee, IFormFile file)
        {
            string filename = _imageHelper.StoreImage(file);
            employee.UrlToPicture = filename;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee, IFormFile file)
        {
            //Add selected picture Id to Db
            string filename = _imageHelper.StoreImage(file);
            employee.UrlToPicture = filename;

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();  
            return RedirectToAction(nameof(Index));
        }
       

        


    }
}
