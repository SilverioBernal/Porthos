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
    public class ChargeBiz
    {
        /*CRUD Entity*/

        /// <summary>
        /// Retrieve list without parameters
        /// </summary>
        /// <returns></returns>
        public List<Charge> GetChargeList()
        {

            List<Charge> lstCharge = new List<Charge>();

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstCharge = ctx.Charge.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstCharge;
        }

        /// <summary>
        /// Retrieve information based in the primary key
        /// </summary>
        /// <param name="ChargeTarget"></param>
        /// <returns></returns>
        public Charge GetChargeByKey(Charge ChargeTarget)
        {
            Charge oCharge = new Charge();

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oCharge = ctx.Charge.Where(x => x.id.Equals(ChargeTarget.id)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oCharge;
        }

        /// <summary>
        /// Create or update a new record
        /// </summary>
        /// <param name="ChargeTarget"></param>
        public void SaveCharge(Charge ChargeTarget)
        {

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    //verify if the record exists
                    Charge oCharge = GetChargeByKey(ChargeTarget);

                    if (oCharge != null)
                    {
                        // if exists then edit 
                        ctx.Charge.Attach(oCharge);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oCharge, ChargeTarget);
                        ctx.SaveChanges();
                    }
                    //else
                    //{
                    //    Cryptography cryptography = new Cryptography();
                    //    // else create
                    //    ChargeTarget.usuario = GetNewUserName(ChargeTarget);
                    //    ChargeTarget.password = cryptography.Encrypt(ChargeTarget.documento);
                    //    ctx.Charges.Add(ChargeTarget);
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
        /// <param name="ChargeTarget"></param>
        public void DeleteCharge(Charge ChargeTarget)
        {
            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    //verify if the record exists
                    Charge oCharge = GetChargeByKey(ChargeTarget);

                    if (oCharge != null)
                    {
                        // if exists then edit 
                        ctx.Charge.Attach(oCharge);
                        ctx.Charge.Remove(oCharge);
                        ctx.SaveChanges();
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("REFERENCE constraint"))
                {
                    throw new Exception("No se puede eliminar esta Chargea porque existe información asociada a esta.");
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
