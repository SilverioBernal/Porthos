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
    public class BillGenerationBiz
    {
        /*CRUD Entity*/

        /// <summary>
        /// Retrieve list without parameters
        /// </summary>
        /// <returns></returns>
        public List<BillGeneration> GetBillGenerationList()
        {

            List<BillGeneration> lstBillGeneration = new List<BillGeneration>();

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstBillGeneration = ctx.BillGeneration.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstBillGeneration;
        }

        /// <summary>
        /// Retrieve information based in the primary key
        /// </summary>
        /// <param name="BillGenerationTarget"></param>
        /// <returns></returns>
        public BillGeneration GetBillGenerationByKey(BillGeneration BillGenerationTarget)
        {
            BillGeneration oBillGeneration = new BillGeneration();

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oBillGeneration = ctx.BillGeneration.Where(x => x.id.Equals(BillGenerationTarget.id)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oBillGeneration;
        }

        /// <summary>
        /// Create or update a new record
        /// </summary>
        /// <param name="BillGenerationTarget"></param>
        public void SaveBillGeneration(BillGeneration BillGenerationTarget)
        {

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    //verify if the record exists
                    BillGeneration oBillGeneration = GetBillGenerationByKey(BillGenerationTarget);

                    if (oBillGeneration != null)
                    {
                        // if exists then edit 
                        ctx.BillGeneration.Attach(oBillGeneration);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oBillGeneration, BillGenerationTarget);
                        ctx.SaveChanges();
                    }
                    //else
                    //{
                    //    Cryptography cryptography = new Cryptography();
                    //    // else create
                    //    BillGenerationTarget.usuario = GetNewUserName(BillGenerationTarget);
                    //    BillGenerationTarget.password = cryptography.Encrypt(BillGenerationTarget.documento);
                    //    ctx.BillGenerations.Add(BillGenerationTarget);
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
        /// <param name="BillGenerationTarget"></param>
        public void DeleteBillGeneration(BillGeneration BillGenerationTarget)
        {
            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    //verify if the record exists
                    BillGeneration oBillGeneration = GetBillGenerationByKey(BillGenerationTarget);

                    if (oBillGeneration != null)
                    {
                        // if exists then edit 
                        ctx.BillGeneration.Attach(oBillGeneration);
                        ctx.BillGeneration.Remove(oBillGeneration);
                        ctx.SaveChanges();
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("REFERENCE constraint"))
                {
                    throw new Exception("No se puede eliminar esta BillGenerationa porque existe información asociada a esta.");
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
