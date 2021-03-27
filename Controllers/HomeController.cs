using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationProductModule.Models;

namespace WebApplicationProductModule.Controllers
{
    public class HomeController : Controller
    {
        ProductDb productDb = new ProductDb();

        // GET: Home
        public ActionResult Index()
        {
            return View(productDb.ListAll().ToList());
        }

        
        public JsonResult GetSearchingData(string SearchValue)
        {

            return Json(productDb.GetListByName(SearchValue),JsonRequestBehavior.AllowGet);
        }

        public JsonResult List()
        {
            return Json(productDb.ListAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(Product product)
        {
            return Json(productDb.Add(product), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetById(int Id)
        {
            var Product = productDb.ListAll().Find(x => x.ProductId.Equals(Id));
            return Json(Product, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(Product product)
        {
            return Json(productDb.Update(product), JsonRequestBehavior.AllowGet);
        }



    }
}