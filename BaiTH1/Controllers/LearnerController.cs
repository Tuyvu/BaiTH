using BaiTH1.Data;
using BaiTH1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DevExpress.Utils.Design;
using System.Drawing.Printing;

namespace BaiTH1.Controllers
{
    public class LearnerController : Controller
    {
        private SchoolContext db;
        public LearnerController(SchoolContext context)
        {
            db = context;
        }
        //them action loc
       
       /* public IActionResult LearnerByPage(int? page, int? pageSize)
        {
            var _pageSize = ipageSize ?? 2;
            var pageIndex = page ?? 1;
            var 


        }*/

        public IActionResult Create()
        {
            //dùng 1 trong 2 cách để tạo SelectList gửi về View qua ViewBag để
            //hiển thị danh sách chuyên ngành (Majors)
            var majors = new List<SelectListItem>(); //cách 1
            foreach (var item in db.Majors)
            {
                majors.Add(new SelectListItem
                {
                    Text = item.MajorName,

                    Value = item.MajorID.ToString()
                });

            }
            ViewBag.MajorID = majors;
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName"); //cách 2
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstMidName,LastName,MajorID,EnrollmentDate")]
Learner learner)
        {
            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            //lại dùng 1 trong 2 cách tạo SelectList gửi về View để hiển thị danh sách Majors
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View();
        }
        //index
        /*  public IActionResult Index(int? mid,int? page)
          {
              var query = db.Learners.Include(m => m.Major).AsQueryable();

              if (mid != null)
              {
                  query = query.Where(l => l.MajorID == mid);
              }

              return View(query);
          }*/
        public IActionResult LearnerByMajorID(int? mid, int? page, string? search)
        {
            int pageNumber = page.HasValue ? page.Value : 1; // Trang mặc định nếu không được xác định
            int pageSize = 10;
            if (mid == null && string.IsNullOrEmpty(search))
            {
                var learners = db.Learners
                .Include(m => m.Major).ToPagedList(pageNumber, pageSize);
                 return PartialView("LearnerTable", learners);
            
            }
            else 
            {
                if (mid != null)
                {
                 var learners = db.Learners.Where(l => l.MajorID == mid)
                 .Include(m => m.Major).ToPagedList(pageNumber, pageSize);
                  return PartialView("LearnerTable", learners);
                }
                else
                { 
                    var results = db.Learners.Where(x => x.LastName.ToLower().Contains(search.Trim().ToLower())).Include(m => m.Major).ToPagedList(pageNumber, pageSize);
                    return PartialView("LearnerTable", results);
                }
                   
            }
            
        }

     /*   public IActionResult LearnerBySearch(string search, int? page)
        {

            int pageNumber = page.HasValue ? page.Value : 1; // Trang mặc định nếu không được xác định
            int pageSize = 10;
            
            if (!string.IsNullOrEmpty(search))  
            { 
               var results = db.Learners.Where(x => x.LastName.ToLower().Contains(search.Trim().ToLower())).Include(m => m.Major).ToPagedList(pageNumber, pageSize);
                return PartialView("LearnerTable", results);
            }
            else
            {
               var results = db.Learners.Include(m => m.Major).ToPagedList(pageNumber, pageSize);
                return PartialView("LearnerTable", results);

                }

           }*/
           


        
        // index pagelist
        public IActionResult Index(int? mid, int? page)
        {
            int pageNumber = page.HasValue ? page.Value : 1; // Trang mặc định nếu không được xác định
            int pageSize = 10; // Số lượng phần tử trên mỗi trang
            if (mid == null)
            {
                var learners = db.Learners.Include(m => m.Major).ToPagedList(pageNumber, pageSize);
                return View(learners);
            }
            else
            {
                var learners = db.Learners.Where(l => l.MajorID == mid).Include(m => m.Major).ToPagedList(pageNumber, pageSize);
                return View(learners);
            }
        }
        /*public IActionResult Index(int? mid, int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;

            var query = db.Learners.Include(m => m.Major).AsQueryable();

            if (mid != null)
            {
                query = query.Where(l => l.MajorID == mid);
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var learners = query.ToPagedList(pageNumber, pageSize);
                return PartialView("_LearnersListPartial", learners);
            }

            return View(query.ToPagedList(pageNumber, pageSize));
        }*/
        //thêm mới 2 action edit
        public IActionResult Edit(int id)
        {
            if(id<=0 || db.Learners == null)
            {
                return NotFound();
            }
            var learner = db.Learners.Find(id);
            if(learner == null)
            {
                return NotFound();
            }
            ViewBag.MajorId = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,
            [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if(id != learner.LearnerID)
            {
                return NotFound(); 
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(learner);
                    db.SaveChanges();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!LearnerExit(learner.LearnerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MajorId = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }
        private bool LearnerExit(int id)
        {
            return (db.Learners?.Any(e => e.LearnerID == id)).GetValueOrDefault();
        }
        // thêm 2 action delete
        public IActionResult Delete(int id)
        {
            if(id==null || db.Learners == null)
            {
                return NotFound();
            }
            var learner =db.Learners.Include(l=>l.Major).Include(e=>e.Enrollments).FirstOrDefault(m=>m.LearnerID==id);
            if(learner == null)
            {
                return NotFound();
            }
            if (learner.Enrollments.Count() > 0)
            {
                return Content("This learner has some errollment ,can't delete");
            }
            return View(learner);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if(db.Learners == null)
            {
                return Problem("Entity set 'learners' is null");
            }
            var learner = db.Learners.Find(id);
            if(learner != null) {
                db.Learners.Remove(learner);
            }
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
       
    }
}
