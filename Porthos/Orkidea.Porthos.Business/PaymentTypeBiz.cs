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
    public class PaymentTypeBiz
    {
        /*CRUD Entity*/

        /// <summary>
        /// Retrieve list without parameters
        /// </summary>
        /// <returns></returns>
        public List<PaymentType> GetPaymentTypeList()
        {

            List<PaymentType> lstPaymentType = new List<PaymentType>();

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstPaymentType = ctx.PaymentType.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstPaymentType;
        }

        /// <summary>
        /// Retrieve information based in the primary key
        /// </summary>
        /// <param name="PaymentTypeTarget"></param>
        /// <returns></returns>
        public PaymentType GetPaymentTypeByKey(PaymentType PaymentTypeTarget)
        {
            PaymentType oPaymentType = new PaymentType();

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oPaymentType = ctx.PaymentType.Where(x => x.id.Equals(PaymentTypeTarget.id)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oPaymentType;
        }

        /// <summary>
        /// Create or update a new record
        /// </summary>
        /// <param name="PaymentTypeTarget"></param>
        public void SavePaymentType(PaymentType PaymentTypeTarget)
        {

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    //verify if the record exists
                    PaymentType oPaymentType = GetPaymentTypeByKey(PaymentTypeTarget);

                    if (oPaymentType != null)
                    {
                        // if exists then edit 
                        ctx.PaymentType.Attach(oPaymentType);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oPaymentType, PaymentTypeTarget);
                        ctx.SaveChanges();
                    }
                    //else
                    //{
                    //    Cryptography cryptography = new Cryptography();
                    //    // else create
                    //    PaymentTypeTarget.usuario = GetNewUserName(PaymentTypeTarget);
                    //    PaymentTypeTarget.password = cryptography.Encrypt(PaymentTypeTarget.documento);
                    //    ctx.PaymentTypes.Add(PaymentTypeTarget);
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
        /// <param name="PaymentTypeTarget"></param>
        public void DeletePaymentType(PaymentType PaymentTypeTarget)
        {
            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    //verify if the record exists
                    PaymentType oPaymentType = GetPaymentTypeByKey(PaymentTypeTarget);

                    if (oPaymentType != null)
                    {
                        // if exists then edit 
                        ctx.PaymentType.Attach(oPaymentType);
                        ctx.PaymentType.Remove(oPaymentType);
                        ctx.SaveChanges();
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("REFERENCE constraint"))
                {
                    throw new Exception("No se puede eliminar esta PaymentTypea porque existe información asociada a esta.");
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
