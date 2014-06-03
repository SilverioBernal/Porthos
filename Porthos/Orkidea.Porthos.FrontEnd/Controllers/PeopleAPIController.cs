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
    public class PeopleAPIController : ApiController
    {
        PeopleBiz peopleBiz = new PeopleBiz();

        // GET api/people        
        public List<People> Get()
        {
            /*
             * Nota: 
             * tener en cuenta caching en consultas grandes
             */
            List<People> lsPeople = peopleBiz.GetPeopleList();
            //List<People> lsPeople = new List<People>();

            //for (int i = 1; i < 40; i++)
            //{
            //    lsPeople.Add(new People() { id = i });
            //}
            return lsPeople;
        }

        // GET api/people/5
        public People Get(int id)
        {
            People people = peopleBiz.GetPeopleByKey(new People() { id = id });

            if (people == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));

            return people;
        }

        //// GET api/people WITH CACHE MANAGEMENT
        //public HttpResponseMessage GetCache()
        //{
        //    var httpResponseMessage = Request.CreateResponse<List<People>>(HttpStatusCode.OK, peopleBiz.GetPeopleList());
        //    httpResponseMessage.Headers.CacheControl = new CacheControlHeaderValue()
        //    {
        //        MaxAge = TimeSpan.FromMinutes(10)
        //    };

        //    return httpResponseMessage;
        //}

        // POST api/people
        public HttpResponseMessage Post(People people)
        {
            if (ModelState.IsValid)
            {
                peopleBiz.SavePeople(people);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, people);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = people.id }));
                return response;
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        }

        // PUT api/people/5
        public HttpResponseMessage Put(int id, People people)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            if (id != people.id)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                peopleBiz.SavePeople(people);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/people/5
        public HttpResponseMessage Delete(int id)
        {
            People people = peopleBiz.GetPeopleByKey(new People() { id = id });

            try
            {
                peopleBiz.DeletePeople(people);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, people);
        }
    }
}
