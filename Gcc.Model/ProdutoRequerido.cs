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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    
    public partial class ProdutoRequerido
    {
        public long ProdutoRequeridoID { get; set; }
        public Nullable<long> ProdutoID { get; set; }
        public Nullable<long> ClienteID { get; set; }
        public Nullable<long> GrupoID { get; set; }
        [Required(ErrorMessage = "Quantidade � um campo de preenchimento obrigat�rio.")]
        public int Quantidade { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual Grupo Grupo { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
