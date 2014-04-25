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
    public class PeopleBiz
    {
        /*CRUD Entity*/

        /// <summary>
        /// Retrieve list without parameters
        /// </summary>
        /// <returns></returns>
        public List<People> GetPeopleList()
        {

            List<People> lstPeople = new List<People>();

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstPeople = ctx.People.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstPeople;
        }

        /// <summary>
        /// Retrieve information based in the primary key
        /// </summary>
        /// <param name="PeopleTarget"></param>
        /// <returns></returns>
        public People GetPeopleByKey(People PeopleTarget)
        {
            People oPeople = new People();

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oPeople = ctx.People.Where(x => x.id.Equals(PeopleTarget.id)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oPeople;
        }

        /// <summary>
        /// Create or update a new record
        /// </summary>
        /// <param name="PeopleTarget"></param>
        public void SavePeople(People PeopleTarget)
        {

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    //verify if the record exists
                    People oPeople = GetPeopleByKey(PeopleTarget);

                    if (oPeople != null)
                    {
                        // if exists then edit 
                        ctx.People.Attach(oPeople);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oPeople, PeopleTarget);
                        ctx.SaveChanges();
                    }
                    //else
                    //{
                    //    Cryptography cryptography = new Cryptography();
                    //    // else create
                    //    PeopleTarget.usuario = GetNewUserName(PeopleTarget);
                    //    PeopleTarget.password = cryptography.Encrypt(PeopleTarget.documento);
                    //    ctx.Peoples.Add(PeopleTarget);
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
        /// <param name="PeopleTarget"></param>
        public void DeletePeople(People PeopleTarget)
        {
            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    //verify if the record exists
                    People oPeople = GetPeopleByKey(PeopleTarget);

                    if (oPeople != null)
                    {
                        // if exists then edit 
                        ctx.People.Attach(oPeople);
                        ctx.People.Remove(oPeople);
                        ctx.SaveChanges();
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("REFERENCE constraint"))
                {
                    throw new Exception("No se puede eliminar esta Peoplea porque existe información asociada a esta.");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /*Complementary business methods*/

        /// <summary>
        /// Retrieve information based in Username
        /// </summary>
        /// <param name="PeopleTarget"></param>
        /// <returns></returns>
        public People GetPeopleByUsername(People PeopleTarget)
        {
            People oPeople = new People();

            try
            {
                using (var ctx = new PropiedadHorizontalEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oPeople = ctx.People.Where(x => x.usuario == PeopleTarget.usuario).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oPeople;
        }

    }
}
