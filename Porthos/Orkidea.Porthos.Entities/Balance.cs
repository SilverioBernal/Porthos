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
    
    public partial class Balance
    {
        public Balance()
        {
            this.BalanceDetail = new HashSet<BalanceDetail>();
            this.BalancePaymentSupport = new HashSet<BalancePaymentSupport>();
        }
    
        public int idProyecto { get; set; }
        public int id { get; set; }
        public System.DateTime desde { get; set; }
        public System.DateTime hasta { get; set; }
        public bool pagado { get; set; }
    
        public virtual Project Project { get; set; }
        public virtual ICollection<BalanceDetail> BalanceDetail { get; set; }
        public virtual ICollection<BalancePaymentSupport> BalancePaymentSupport { get; set; }
    }
}
