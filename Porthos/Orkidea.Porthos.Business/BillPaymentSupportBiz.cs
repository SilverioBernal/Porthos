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
    public class BillPaymentSupportBiz
    {
        /*CRUD Entity*/

        /// <summary>
        /// Retrieve list without parameters
        /// </summary>
        /// <returns></returns>
        public List<BillPaymentSupport> GetBillPaymentSupportList()
        {

            List<BillPaymentSupport> lstBillPaymentSupport = new List<BillPaymentSupport>();

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstBillPaymentSupport = ctx.BillPaymentSupport.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstBillPaymentSupport;
        }

        /// <summary>
        /// Retrieve information based in the primary key
        /// </summary>
        /// <param name="BillPaymentSupportTarget"></param>
        /// <returns></returns>
        public BillPaymentSupport GetBillPaymentSupportByKey(BillPaymentSupport BillPaymentSupportTarget)
        {
            BillPaymentSupport oBillPaymentSupport = new BillPaymentSupport();

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oBillPaymentSupport = ctx.BillPaymentSupport.Where(x => x.id.Equals(BillPaymentSupportTarget.id)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oBillPaymentSupport;
        }

        /// <summary>
        /// Create or update a new record
        /// </summary>
        /// <param name="BillPaymentSupportTarget"></param>
        public void SaveBillPaymentSupport(BillPaymentSupport BillPaymentSupportTarget)
        {

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    //verify if the record exists
                    BillPaymentSupport oBillPaymentSupport = GetBillPaymentSupportByKey(BillPaymentSupportTarget);

                    if (oBillPaymentSupport != null)
                    {
                        // if exists then edit 
                        ctx.BillPaymentSupport.Attach(oBillPaymentSupport);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oBillPaymentSupport, BillPaymentSupportTarget);
                        ctx.SaveChanges();
                    }
                    //else
                    //{
                    //    Cryptography cryptography = new Cryptography();
                    //    // else create
                    //    BillPaymentSupportTarget.usuario = GetNewUserName(BillPaymentSupportTarget);
                    //    BillPaymentSupportTarget.password = cryptography.Encrypt(BillPaymentSupportTarget.documento);
                    //    ctx.BillPaymentSupports.Add(BillPaymentSupportTarget);
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
        /// <param name="BillPaymentSupportTarget"></param>
        public void DeleteBillPaymentSupport(BillPaymentSupport BillPaymentSupportTarget)
        {
            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    //verify if the record exists
                    BillPaymentSupport oBillPaymentSupport = GetBillPaymentSupportByKey(BillPaymentSupportTarget);

                    if (oBillPaymentSupport != null)
                    {
                        // if exists then edit 
                        ctx.BillPaymentSupport.Attach(oBillPaymentSupport);
                        ctx.BillPaymentSupport.Remove(oBillPaymentSupport);
                        ctx.SaveChanges();
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("REFERENCE constraint"))
                {
                    throw new Exception("No se puede eliminar esta BillPaymentSupporta porque existe información asociada a esta.");
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
