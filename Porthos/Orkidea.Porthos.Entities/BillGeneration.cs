//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Orkidea.Porthos.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class BillGeneration
    {
        public BillGeneration()
        {
            this.PropertyBill = new HashSet<PropertyBill>();
        }
    
        public int idProyecto { get; set; }
        public int id { get; set; }
        public System.DateTime fechaInicioLiquidacion { get; set; }
        public System.DateTime fechaFinLiquidacion { get; set; }
        public System.DateTime fechaCorte { get; set; }
        public System.DateTime fechaLimitePago1 { get; set; }
        public Nullable<System.DateTime> fechaLimitePago2 { get; set; }
    
        public virtual Project Project { get; set; }
        public virtual ICollection<PropertyBill> PropertyBill { get; set; }
    }
}
