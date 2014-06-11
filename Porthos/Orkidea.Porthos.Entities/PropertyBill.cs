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
    
    public partial class PropertyBill
    {
        public PropertyBill()
        {
            this.BillPaymentSupport = new HashSet<BillPaymentSupport>();
            this.Charge = new HashSet<Charge>();
        }
    
        public int id { get; set; }
        public int idLiquidacion { get; set; }
        public int idPropiedad { get; set; }
        public int fechaPago { get; set; }
        public decimal valorLiquidado { get; set; }
        public Nullable<decimal> valorPagado { get; set; }
        public bool pagado { get; set; }
        public string Observaciones { get; set; }
    
        public virtual BillGeneration BillGeneration { get; set; }
        public virtual ICollection<BillPaymentSupport> BillPaymentSupport { get; set; }
        public virtual ICollection<Charge> Charge { get; set; }
    }
}
