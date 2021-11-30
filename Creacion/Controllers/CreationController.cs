using Creacion.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Creacion.Controllers
{
    public class CreationController : Controller
    {

        // GET: Creation
        public ActionResult Index()
        {
            return View();
        }

        // GET: Creation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Creation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Creation/Create
        [HttpPost]
        public ActionResult Create(CreationModel creationModel)
        {
            if (ModelState.IsValid)
            {
                string constr = ConfigurationManager.AppSettings["connectionString"];
                var Client = new MongoClient(constr);
                var DB = Client.GetDatabase("creation_db");
                var collection = DB.GetCollection<CreationModel>("creation_db");
                collection.InsertOneAsync(creationModel);
                return RedirectToAction("Create", creationModel);
            }
            return View();

        }

        // GET: Creation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Creation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Creation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Creation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
