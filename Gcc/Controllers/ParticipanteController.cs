using Gcc.Data.DataLayerEntityFramework;
using Gcc.Filters;
using Gcc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gcc.Web.Controllers
{
    [InitializeSimpleMembership]
    public class ParticipanteController : Controller
    {

        private GccContext db = new GccContext();

        [Authorize]
        public ActionResult Detalhes(long id = 0)
        {
            ParticipanteGrupo participante = db.ParticipanteGrupoes.Find(id);

            if (participante == null)
            {
                return HttpNotFound();
            }

            return View(participante);
        }
    }
}
