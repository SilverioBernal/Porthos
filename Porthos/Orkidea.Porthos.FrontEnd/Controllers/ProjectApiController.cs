using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Orkidea.Porthos.Business;
using Orkidea.Porthos.Entities;

namespace Orkidea.Porthos.FrontEnd.Controllers
{
    public class ProjectApiController : ApiController
    {
        ProjectBiz projectBiz = new ProjectBiz();

        // GET api/projectapi
        public List<Project> Get()
        {
            List<Project> lsProject = projectBiz.GetProjectList();
            return lsProject;
        }

        // GET api/projectapi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/projectapi
        public void Post([FromBody]string value)
        {
        }

        // PUT api/projectapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/projectapi/5
        public void Delete(int id)
        {
        }
    }
}
