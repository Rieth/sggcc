//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gcc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Alternativa
    {
        public Alternativa()
        {
            this.Votoes = new HashSet<Voto>();
        }
    
        public long AlternativaID { get; set; }
        public Nullable<long> EnqueteID { get; set; }    
        public string Nome { get; set; }
        public string Valor { get; set; }
    
        public virtual Enquete Enquete { get; set; }
        public virtual ICollection<Voto> Votoes { get; set; }
    }
}