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
    
    public partial class People
    {
        public People()
        {
            this.Project = new HashSet<Project>();
            this.ProjectDistribution = new HashSet<ProjectDistribution>();
            this.ProjectDistribution1 = new HashSet<ProjectDistribution>();
        }
    
        public int id { get; set; }
        public string nombre1 { get; set; }
        public string nombre2 { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string tipoId { get; set; }
        public string numeroId { get; set; }
        public Nullable<System.DateTime> fechaExpedicion { get; set; }
        public string tel1 { get; set; }
        public string tel2 { get; set; }
        public string email { get; set; }
        public string usuario { get; set; }
        public string contraseña { get; set; }
        public bool usuarioOrkidea { get; set; }
    
        public virtual ICollection<Project> Project { get; set; }
        public virtual ICollection<ProjectDistribution> ProjectDistribution { get; set; }
        public virtual ICollection<ProjectDistribution> ProjectDistribution1 { get; set; }
    }
}
