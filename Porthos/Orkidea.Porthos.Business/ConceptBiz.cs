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
    public class ConceptBiz
    {
        /*CRUD Entity*/

        /// <summary>
        /// Retrieve list without parameters
        /// </summary>
        /// <returns></returns>
        public List<Concept> GetConceptList()
        {

            List<Concept> lstConcept = new List<Concept>();

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstConcept = ctx.Concept.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstConcept;
        }

        /// <summary>
        /// Retrieve information based in the primary key
        /// </summary>
        /// <param name="ConceptTarget"></param>
        /// <returns></returns>
        public Concept GetConceptByKey(Concept ConceptTarget)
        {
            Concept oConcept = new Concept();

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oConcept = ctx.Concept.Where(x => x.id.Equals(ConceptTarget.id)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oConcept;
        }

        /// <summary>
        /// Create or update a new record
        /// </summary>
        /// <param name="ConceptTarget"></param>
        public void SaveConcept(Concept ConceptTarget)
        {

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    //verify if the record exists
                    Concept oConcept = GetConceptByKey(ConceptTarget);

                    if (oConcept != null)
                    {
                        // if exists then edit 
                        ctx.Concept.Attach(oConcept);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oConcept, ConceptTarget);
                        ctx.SaveChanges();
                    }
                    //else
                    //{
                    //    Cryptography cryptography = new Cryptography();
                    //    // else create
                    //    ConceptTarget.usuario = GetNewUserName(ConceptTarget);
                    //    ConceptTarget.password = cryptography.Encrypt(ConceptTarget.documento);
                    //    ctx.Concepts.Add(ConceptTarget);
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
        /// <param name="ConceptTarget"></param>
        public void DeleteConcept(Concept ConceptTarget)
        {
            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    //verify if the record exists
                    Concept oConcept = GetConceptByKey(ConceptTarget);

                    if (oConcept != null)
                    {
                        // if exists then edit 
                        ctx.Concept.Attach(oConcept);
                        ctx.Concept.Remove(oConcept);
                        ctx.SaveChanges();
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("REFERENCE constraint"))
                {
                    throw new Exception("No se puede eliminar esta Concepta porque existe información asociada a esta.");
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
