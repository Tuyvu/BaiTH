﻿using BaiTH1.Data;
using BaiTH1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using System;

namespace BaiTH1.Controllers
{
    [Route("Admin/Student")]
    public class StudentController : Controller
    {
        private List<Student> listStudents = new List<Student>();
        public StudentController()
        {

            //Tạo danh sách sinh viên với 4 dữ liệu mẫu
            listStudents = new List<Student>()
                {
                new Student() { Id = 101, Name = "Hải Nam", Branch = Branch.IT,
                Gender = Gender.Male, IsRegular=true,
                Address = "A1-2018", Email = "nam@g.com" },

                new Student() { Id = 102, Name = "Minh Tú", Branch = Branch.BE,
                Gender = Gender.Female, IsRegular=true,
                Address = "A1-2019", Email = "tu@g.com" },

                new Student() { Id = 103, Name = "Hoàng Phong", Branch = Branch.CE,
                Gender = Gender.Male, IsRegular=false,
                Address = "A1-2020", Email = "phong@g.com" },

                new Student() { Id = 104, Name = "Xuân Mai", Branch = Branch.EE,
                Gender = Gender.Female, IsRegular = false,
                Address = "A1-2021", Email = "mai@g.com" }

                };
        }
        [HttpGet("List")]
        //[Route("Admin/Student/list")]
        public IActionResult Index()
        {
            return View(listStudents);
        }
      
        [HttpGet("Add")]
        public IActionResult Create()
        {

          

            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
                ViewBag.AllBranches = new List<SelectListItem>()
               {
                new SelectListItem { Text = "IT", Value = "1" },
                new SelectListItem { Text = "BE", Value = "2" },
                new SelectListItem { Text = "CE", Value = "3" },
                new SelectListItem { Text = "EE", Value = "4" }
             };
            return View();
            
            
        }
        /*public void ConfigureServices(IServiceProvider serviceProvider)
        {
            // ...
            using (var context = new SchoolContext(serviceProvider.GetRequiredService<DbContextOptions<SchoolContext>>())) ;
            // ...
        }*/
        [HttpPost("Add")]
        /* public IActionResult Create(Student s, IFormFile? imageFile , [FromServices] SchoolContext dbContext)*/
        public IActionResult Create(Student s, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                s.Id = listStudents.Last<Student>().Id + 1;
                listStudents.Add(s);
                return View("Index", listStudents);
            }
                 /* dbContext.Learners.Add(s);*/
            // Xử lý và lưu trữ ảnh đại diện (imageFile) ở đây
            if (imageFile != null && imageFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        imageFile.CopyTo(stream);
                        s.StudentImage = new StudentImage
                        {
                            ImageData = stream.ToArray(),
                            ImageMimeType = imageFile.ContentType
                        };
                    }
                }
                ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
                ViewBag.AllBranches = new List<SelectListItem>()
             {
                new SelectListItem { Text = "IT", Value = "1" },
                new SelectListItem { Text = "BE", Value = "2" },
                new SelectListItem { Text = "CE", Value = "3" },
                new SelectListItem { Text = "EE", Value = "4" }
             };
                return View();
            
           
        }
    }
}
