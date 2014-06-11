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
    public class PropertyBillBiz
    {
        /*CRUD Entity*/

        /// <summary>
        /// Retrieve list without parameters
        /// </summary>
        /// <returns></returns>
        public List<PropertyBill> GetPropertyBillList()
        {

            List<PropertyBill> lstPropertyBill = new List<PropertyBill>();

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstPropertyBill = ctx.PropertyBill.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstPropertyBill;
        }

        /// <summary>
        /// Retrieve information based in the primary key
        /// </summary>
        /// <param name="PropertyBillTarget"></param>
        /// <returns></returns>
        public PropertyBill GetPropertyBillByKey(PropertyBill PropertyBillTarget)
        {
            PropertyBill oPropertyBill = new PropertyBill();

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oPropertyBill = ctx.PropertyBill.Where(x => x.id.Equals(PropertyBillTarget.id)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oPropertyBill;
        }

        /// <summary>
        /// Create or update a new record
        /// </summary>
        /// <param name="PropertyBillTarget"></param>
        public void SavePropertyBill(PropertyBill PropertyBillTarget)
        {

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    //verify if the record exists
                    PropertyBill oPropertyBill = GetPropertyBillByKey(PropertyBillTarget);

                    if (oPropertyBill != null)
                    {
                        // if exists then edit 
                        ctx.PropertyBill.Attach(oPropertyBill);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oPropertyBill, PropertyBillTarget);
                        ctx.SaveChanges();
                    }
                    //else
                    //{
                    //    Cryptography cryptography = new Cryptography();
                    //    // else create
                    //    PropertyBillTarget.usuario = GetNewUserName(PropertyBillTarget);
                    //    PropertyBillTarget.password = cryptography.Encrypt(PropertyBillTarget.documento);
                    //    ctx.PropertyBills.Add(PropertyBillTarget);
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
        /// <param name="PropertyBillTarget"></param>
        public void DeletePropertyBill(PropertyBill PropertyBillTarget)
        {
            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    //verify if the record exists
                    PropertyBill oPropertyBill = GetPropertyBillByKey(PropertyBillTarget);

                    if (oPropertyBill != null)
                    {
                        // if exists then edit 
                        ctx.PropertyBill.Attach(oPropertyBill);
                        ctx.PropertyBill.Remove(oPropertyBill);
                        ctx.SaveChanges();
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("REFERENCE constraint"))
                {
                    throw new Exception("No se puede eliminar esta PropertyBilla porque existe información asociada a esta.");
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
