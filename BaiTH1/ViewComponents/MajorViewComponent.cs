using BaiTH1.Data;
using Microsoft.AspNetCore.Mvc;
using BaiTH1.Models;

namespace BaiTH1.ViewComponents
{
    public class MajorViewComponent : ViewComponent
    {
        SchoolContext db;
        List<Major> majors;

        public MajorViewComponent(SchoolContext _context)
        {
            db=_context;
            majors=db.Majors.ToList();
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderMajor",majors);
        }
    }
}
