using BaiTH1.Data;
using BaiTH1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaiTH1.Controllers
{
    public class CourseController : Controller
    {
        private SchoolContext db;
        public CourseController(SchoolContext context)
        {
            db=context;
        }

        public IActionResult Index()
        {
            IEnumerable<Course> courses = db.Courses.ToList();
            return View(courses);
        }
        //create
        
        public IActionResult Create([Bind("CourseID ,Title ,Credits")] Course course)
        {
             if(ModelState.IsValid)
            {
                db.Courses.Add(course); 
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
             return View();
        }
    }
}
