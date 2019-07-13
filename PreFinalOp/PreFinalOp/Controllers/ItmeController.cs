using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PreFinalOp.Models;
using System.Data.SqlClient;
using PreFinalOp.App_Code;

namespace PreFinalOp.Controllers
{
    public class ItmeController : Controller
    {
        // GET: Itme
        public ActionResult Index()
        {
            ViewBag.MF = "MF";
            DBHandler dBHandler = new DBHandler();
            ModelState.Clear();
            return View(dBHandler.GetPeople());
        }
/*
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                DBHandler dBHandler = new DBHandler();
                if (dBHandler.InsertItem(person))
                {
                    ViewBag.Message = "K";
                    ModelState.Clear();
                }
            }
            return View();
        }
 */
        [HttpGet]
        public ActionResult Edit(int id)
        {
            DBHandler dBHandler = new DBHandler();
            return View(dBHandler.GetPeople().Find(Person => Person.ID == id));
        }
        [HttpPost]
        public ActionResult Edit(int id, Person personz)
        {
            try
            {
                DBHandler dBHandler = new DBHandler();
                dBHandler.UpdatePerson(personz);
                return RedirectToAction("Index");
            }
            catch { return View(); }

        }

        public ActionResult Details(int id)
        {
            DBHandler dBHandler = new DBHandler();
            return View(dBHandler.GetPeople().Find(Person => Person.ID == id));
        }

        public List<SelectListItem> GetMF()
        {
            List<SelectListItem> MF= new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT ID, Type FROM MutualFundType ORDER BY Type";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            MF.Add(new SelectListItem
                            {
                                Text = data["Type"].ToString(),
                                Value = data["ID"].ToString()
                            });
                        }
                    }
                }
            }
            return MF;
        }

        public ActionResult Add()
        {
            Person person = new Person();
            person.SelectMF = GetMF();
            return View(person);
        }

        [HttpPost]
        public ActionResult Add(Person person)
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"INSERT INTO Person VALUES (@Name, @Amount, @MF)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", person.Name);
                    cmd.Parameters.AddWithValue("@Amount", person.Amount);
                    cmd.Parameters.AddWithValue("@MF", person.MF);
                    cmd.ExecuteNonQuery();
                    return RedirectToAction("Index");
                }
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                DBHandler dBHandler = new DBHandler();
                if (dBHandler.DeletePerson(id))
                {
                    ViewBag.AlertMsg = "Deleted";
                }
                return RedirectToAction("Index");
            }
            catch { return View(); }
        }
    }
}