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
    public class ProjectBiz
    {
        /*CRUD Entity*/

        /// <summary>
        /// Retrieve list without parameters
        /// </summary>
        /// <returns></returns>
        public List<Project> GetProjectList()
        {

            List<Project> lstProject = new List<Project>();

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstProject = ctx.Project.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstProject;
        }

        /// <summary>
        /// Retrieve information based in the primary key
        /// </summary>
        /// <param name="ProjectTarget"></param>
        /// <returns></returns>
        public Project GetProjectByKey(Project ProjectTarget)
        {
            Project oProject = new Project();

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oProject = ctx.Project.Where(x => x.id.Equals(ProjectTarget.id)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oProject;
        }

        /// <summary>
        /// Create or update a new record
        /// </summary>
        /// <param name="ProjectTarget"></param>
        public void SaveProject(Project ProjectTarget)
        {

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    //verify if the record exists
                    Project oProject = GetProjectByKey(ProjectTarget);

                    if (oProject != null)
                    {
                        // if exists then edit 
                        ctx.Project.Attach(oProject);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oProject, ProjectTarget);
                        ctx.SaveChanges();
                    }
                    //else
                    //{
                    //    Cryptography cryptography = new Cryptography();
                    //    // else create
                    //    ProjectTarget.usuario = GetNewUserName(ProjectTarget);
                    //    ProjectTarget.password = cryptography.Encrypt(ProjectTarget.documento);
                    //    ctx.Projects.Add(ProjectTarget);
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
        /// <param name="ProjectTarget"></param>
        public void DeleteProject(Project ProjectTarget)
        {
            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    //verify if the record exists
                    Project oProject = GetProjectByKey(ProjectTarget);

                    if (oProject != null)
                    {
                        // if exists then edit 
                        ctx.Project.Attach(oProject);
                        ctx.Project.Remove(oProject);
                        ctx.SaveChanges();
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("REFERENCE constraint"))
                {
                    throw new Exception("No se puede eliminar esta Projecta porque existe información asociada a esta.");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /*Complementary business methods*/

        public void EditProject(Project ProjectTarget) 
        {

        }
    }
}
