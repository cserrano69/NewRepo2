using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PreFinalOp.Models;

namespace PreFinalOp.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        //add
        /*[HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Login iList)
        {
            if (ModelState.IsValid)
            {
                DBHandler dBHandler = new DBHandler();
                if (dBHandler.InsertItem(iList))
                {
                    ViewBag.Message = "User Successfully Registered";
                    ModelState.Clear();
                }
            }
            return View();
        
    */
    }
}