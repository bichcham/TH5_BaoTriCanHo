using Microsoft.AspNetCore.Mvc;
using TH5.Models;

namespace TH5.Controllers
{
    public class CanHoController:Controller
    {
        public IActionResult ThemCanHo()
        {
            return View();
        }

        [HttpPost]
        public string AddCH(CanHoModel canho)
        {
            int count;
            DataContext context = HttpContext.RequestServices.GetService(typeof(TH5.Models.DataContext)) as DataContext;
            count = context.Create(canho);
            if(count == 1)
            {
                return "Thêm Thành Công!";
            }
            return "Thêm thất bại";
        }
    }
}
