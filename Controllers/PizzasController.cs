﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kendoui.Data;
using kendoui.Models;

namespace kendoui.Controllers
{
    public class PizzasController : Controller
    {
        private PizzashopsContext db = new PizzashopsContext();

        // GET: Pizzas
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Filter()
        {
            var pizzas = from m in db.Pizzas select m;
            return Json(pizzas, JsonRequestBehavior.AllowGet);
        }
        // GET: Pizzas/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizzas.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // GET: Pizzas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pizzas/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "pizzaid,pizzaname,rate")] Pizza pizza)
        {
            var pizzas = from m in db.Pizzas where m.pizzaname == pizza.pizzaname select m.pizzaname;
            if(pizzas.Count()>0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                db.Pizzas.Add(pizza);
                db.SaveChanges();
                return Json(pizza);
            }
            return View(pizza);
        }
        // GET: Pizzas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizzas.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // POST: Pizzas/Edit/5
       [HttpPost]
        public ActionResult Edit([Bind(Include = "pizzaid,pizzaname,rate")] Pizza pizza)
        {
            var pizzas = from m in db.Pizzas where m.pizzaname == pizza.pizzaname select m.pizzaname;
            if (pizzas.Count() > 0)
            {
                  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                db.Entry(pizza).State = EntityState.Modified;
                db.SaveChanges();
                return Json(pizza);
            }
            return View(pizza);
        }

        // GET: Pizzas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizzas.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // POST: Pizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Pizza pizza = db.Pizzas.Find(id);
            db.Pizzas.Remove(pizza);
            db.SaveChanges();
            return Json(pizza);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
