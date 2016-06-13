using System;
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
using Gcc.Web.ViewsModel;

namespace Gcc.Web.Controllers
{
    [InitializeSimpleMembership]
    public class EnqueteController : Controller
    {
        private GccContext db = new GccContext();

        public PartialViewResult AdicionarAlternativa()
        {
            var model = new Alternativa();

            return PartialView("_CriarAlternativa", model);
        }

        public PartialViewResult Card()
        {
            var model = new Enquete();

            return PartialView("Card", model);
        }

        [Authorize]
        public ActionResult Criar(int id)
        {
            Grupo grupo = db.Grupoes.Where(g => g.GrupoID == id).FirstOrDefault();

            Enquete enquete = new Enquete();
            enquete.GrupoID = grupo.GrupoID;
            enquete.Grupo = grupo;

            return View(enquete);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar(Enquete enquete)
        {
            if (ModelState.IsValid)
            {
                db.Enquetes.Add(enquete);
                db.SaveChanges();

                return RedirectToAction("Editar", "Grupo", new { id = enquete.GrupoID });
            }

            return View(enquete);
        }

        [Authorize]
        public ActionResult Editar(long id = 0)
        {
            Enquete enquete = db.Enquetes.Find(id);
            if (enquete == null)
            {
                return HttpNotFound();
            }
            return View(enquete);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Enquete enquete)
        {
            if (ModelState.IsValid)
            {
                foreach (Alternativa a in enquete.Alternativas)
                {
                    a.EnqueteID = enquete.EnqueteID;

                    if (a.AlternativaID == 0)
                    {
                        db.Alternativas.Add(a);
                    }
                    else
                    {
                        db.Entry(a).State = EntityState.Modified;
                    }
                }

                db.Entry(enquete).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Editar", "Grupo", new { id = enquete.GrupoID });
            }
            return View(enquete);
        }

        //aqui: criar classe


        [Authorize]
        public ActionResult Votar(long id = 0)
        {
            Enquete enquete = db.Enquetes.Find(id);

            if (enquete == null)
            {
                return HttpNotFound();
            }

            int userID = WebSecurity.GetUserId(User.Identity.Name);
            Cliente cliente = db.Clientes.Where(c => c.UserId == userID).FirstOrDefault();

            List<Alternativa> alternativas = enquete.Alternativas.ToList();
            List<AlternativaViewModel> alternatviasVM = new List<AlternativaViewModel>();

            foreach (Alternativa a in alternativas)
            {
                Voto voto = db.Votoes.Where(v => v.ClienteID == cliente.ClienteID && v.AlternativaID == a.AlternativaID).FirstOrDefault();

                if (voto != null)
                    alternatviasVM.Add(new AlternativaViewModel(a, true));
                else
                    alternatviasVM.Add(new AlternativaViewModel(a, false));
            }

            return View(alternatviasVM);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Votar(List<AlternativaViewModel> alternativasVM)
        {
            if (ModelState.IsValid)
            {
                int userID = WebSecurity.GetUserId(User.Identity.Name);
                Cliente cliente = db.Clientes.Where(c => c.UserId == userID).FirstOrDefault();

                foreach (AlternativaViewModel avm in alternativasVM)
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM VOTO WHERE ClienteID= {0} AND AlternativaID= {1}", cliente.ClienteID, avm.Alternativa.AlternativaID);
                }

                foreach (AlternativaViewModel avm in alternativasVM)
                {
                    if (avm.Votada)
                    {
                        Voto voto = new Voto();
                        voto.AlternativaID = avm.Alternativa.AlternativaID;
                        voto.EnqueteID = avm.Alternativa.EnqueteID;
                        voto.ClienteID = cliente.ClienteID;

                        db.Votoes.Add(voto);
                    }                    
                }
                
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(alternativasVM);
            //return RedirectToAction("Detalhes", "Grupo", new { id = .GrupoID });

        }

        [Authorize]
        public ActionResult Detalhes(long id = 0)
        {
            Enquete enquete = db.Enquetes.Find(id);

            if (enquete == null)
            {
                return HttpNotFound();
            }
            return View(enquete);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}