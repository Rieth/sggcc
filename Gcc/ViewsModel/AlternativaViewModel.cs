using Gcc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gcc.Web.ViewsModel
{
    public class AlternativaViewModel
    {
        public Alternativa Alternativa { get; set; }
        public bool Votada { get; set; }

        public AlternativaViewModel()
        {

        }

        public AlternativaViewModel(Alternativa alternativa, bool votada)
        {
            this.Alternativa = alternativa;
            this.Votada = votada;
        }
    }
}