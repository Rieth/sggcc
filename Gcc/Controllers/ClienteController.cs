﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gcc.Models;
using Gcc.Data.DataLayerEntityFramework;
using WebMatrix.WebData;
using Gcc.Filters;

namespace Gcc.Web.Controllers
{
    [InitializeSimpleMembership]
    public class ClienteController : Controller
    {
        private GccContext db = new GccContext();

        //
        // GET: /Cliente/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index(long grupoID)
        {
            return View(db.ParticipanteGrupoes.Where(pg => pg.GrupoID == grupoID).Select(c => c.Cliente).ToList());
        }

        //
        // GET: /Cliente/Details/5

        public ActionResult Details(long id = 0)
        {
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        //
        // GET: /Cliente/Create

        [Authorize]
        public ActionResult Perfil()
        {
            Cliente cliente = db.Clientes.Where(c => c.UserId == WebSecurity.CurrentUserId).FirstOrDefault();

            if (cliente != null)
            {
                return View(cliente);
            }

            return View();
        }

        //
        // POST: /Cliente/Create

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Perfil(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente.Endereco).State = EntityState.Modified;
                db.Entry(cliente).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index", "Grupo");
            }
            return View(cliente);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}