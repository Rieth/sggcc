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
    
    public partial class Enquete
    {
        public Enquete()
        {
            this.Alternativas = new HashSet<Alternativa>();
        }
    
        public long EnqueteID { get; set; }
        public Nullable<long> GrupoID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    
        public virtual ICollection<Alternativa> Alternativas { get; set; }
        public virtual Grupo Grupo { get; set; }
    }
}