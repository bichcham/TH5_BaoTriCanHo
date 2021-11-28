using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TH5.Models;

namespace TH5.Controllers
{
    public class NhanVienController : Controller
    {

        public IActionResult Index()
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(TH5.Models.DataContext)) as DataContext;
            return View(context.getTenNhanVien());
        }

        public IActionResult ListTB(string id)
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(TH5.Models.DataContext)) as DataContext;
            return View(context.getListTB(id));
        }

        public IActionResult Edit(string id, string id1, string id2, int id3)
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(TH5.Models.DataContext)) as DataContext;
            ViewData["NV_BT"] = context.getNVBT(id, id1, id2, id3);
            return View(context.getNVBT(id, id1, id2, id3));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, string id1, string id2, int id3, NV_BT nv)
        {

            DataContext context = HttpContext.RequestServices.GetService(typeof(TH5.Models.DataContext)) as DataContext;
            if (context.Update(nv) != 0)
            {
                return Redirect("/NhanVien/ListTB?id=" + nv.MaNhanVien);
            }
            return Redirect("/NhanVien/ListTB?id=" + nv.MaNhanVien);
        }

        public IActionResult Delete(string id, string id1, string id2, int id3)
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(TH5.Models.DataContext)) as DataContext;
            if (context.Delete(id, id1, id2, id3) != 0)
            {
                return Redirect("/NhanVien/ListTB?id=" + id);
            }
            return Redirect("/NhanVien/ListTB?id=" + id);
        }

        public IActionResult Search(int id)
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(TH5.Models.DataContext)) as DataContext;
            return View(context.getNhanVien(id));
        }
    }
}