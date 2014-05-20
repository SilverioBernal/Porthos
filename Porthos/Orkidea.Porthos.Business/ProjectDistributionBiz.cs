using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orkidea.Porthos.DAL;
using Orkidea.Porthos.Entities;
using Orkidea.Porthos.Utilities;

namespace Orkidea.Porthos.Business
{
    public class ProjectDistributionBiz
    {
        /*CRUD Entity*/

        /// <summary>
        /// Retrieve list without parameters
        /// </summary>
        /// <returns></returns>
        public List<ProjectDistribution> GetProjectDistributionList()
        {

            List<ProjectDistribution> lstProjectDistribution = new List<ProjectDistribution>();

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstProjectDistribution = ctx.ProjectDistribution.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstProjectDistribution;
        }

        /// <summary>
        /// Retrieve information based in the primary key
        /// </summary>
        /// <param name="ProjectDistributionTarget"></param>
        /// <returns></returns>
        public ProjectDistribution GetProjectDistributionByKey(ProjectDistribution ProjectDistributionTarget)
        {
            ProjectDistribution oProjectDistribution = new ProjectDistribution();

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oProjectDistribution = ctx.ProjectDistribution.Where(x => x.id.Equals(ProjectDistributionTarget.id)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oProjectDistribution;
        }

        /// <summary>
        /// Create or update a new record
        /// </summary>
        /// <param name="ProjectDistributionTarget"></param>
        public void SaveProjectDistribution(ProjectDistribution ProjectDistributionTarget)
        {

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    //verify if the record exists
                    ProjectDistribution oProjectDistribution = GetProjectDistributionByKey(ProjectDistributionTarget);

                    if (oProjectDistribution != null)
                    {
                        // if exists then edit 
                        ctx.ProjectDistribution.Attach(oProjectDistribution);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oProjectDistribution, ProjectDistributionTarget);
                        ctx.SaveChanges();
                    }
                    //else
                    //{
                    //    Cryptography cryptography = new Cryptography();
                    //    // else create
                    //    ProjectDistributionTarget.usuario = GetNewUserName(ProjectDistributionTarget);
                    //    ProjectDistributionTarget.password = cryptography.Encrypt(ProjectDistributionTarget.documento);
                    //    ctx.ProjectDistributions.Add(ProjectDistributionTarget);
                    //    ctx.SaveChanges();
                    //}
                }

            }
            catch (DbEntityValidationException e)
            {
                StringBuilder oError = new StringBuilder();
                foreach (var eve in e.EntityValidationErrors)
                {
                    oError.AppendLine(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));

                    foreach (var ve in eve.ValidationErrors)
                    {
                        oError.AppendLine(string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
                string msg = oError.ToString();
                throw new Exception(msg);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="ProjectDistributionTarget"></param>
        public void DeleteProjectDistribution(ProjectDistribution ProjectDistributionTarget)
        {
            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    //verify if the record exists
                    ProjectDistribution oProjectDistribution = GetProjectDistributionByKey(ProjectDistributionTarget);

                    if (oProjectDistribution != null)
                    {
                        // if exists then edit 
                        ctx.ProjectDistribution.Attach(oProjectDistribution);
                        ctx.ProjectDistribution.Remove(oProjectDistribution);
                        ctx.SaveChanges();
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("REFERENCE constraint"))
                {
                    throw new Exception("No se puede eliminar esta ProjectDistributiona porque existe información asociada a esta.");
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
