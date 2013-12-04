using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AppHarborTest.Models;

namespace AppHarborTest.Controllers
{
    public class StepController : ApiController
    {
        private DefaultContext db = new DefaultContext();

        public object GetSteps()
        {
            return from step in db.Steps.ToList()
                   join section in db.Sections.ToList()
                   on step.SectionID equals section.ID
                   orderby step.OrderNum ascending
                   select new
                   {
                       step = new
                       {
                           number = step.OrderNum,
                           name = step.Name,
                           description = step.Description,
                           tip = step.Tip,
                           section = new
                           {
                               number = section.OrderNum,
                               name = section.Name,
                           },
                       },
                   };
        }

        // GET api/Step/5
        public HttpResponseMessage GetStep(int id)
        {
            Step step = db.Steps.Where(o => o.OrderNum == id).FirstOrDefault();
            if (step == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            Section section = db.Sections.Where(o => o.ID == step.SectionID).Single();
            return Request.CreateResponse(HttpStatusCode.OK, new
                   {
                       //step = new
                       //{
                           number = step.OrderNum,
                           name = step.Name,
                           description = step.Description,
                           tip = step.Tip,
                           section = new
                           {
                               number = section.OrderNum,
                               name = section.Name,
                           },
                       //},
                   });
        }

        // PUT api/Step/5
        public HttpResponseMessage PutStep(int id, Step step)
        {
            return Request.CreateResponse(HttpStatusCode.Forbidden);
        }

        // POST api/Step
        public HttpResponseMessage PostStep(Step step)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new { message = "Post not implemented!" });
        }

        // DELETE api/Step/5
        public HttpResponseMessage DeleteStep(int id)
        {
            return Request.CreateResponse(HttpStatusCode.Forbidden);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}