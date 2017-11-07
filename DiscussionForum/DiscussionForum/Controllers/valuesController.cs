using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DiscussionForum.Models;

namespace DiscussionForum.Controllers
{
    public class valuesController : ApiController
    {
        public HttpResponseMessage Get()
        {
            try
            {
                using (SampleDBContext db = new SampleDBContext())
                {
                    var data = db.POSTs.ToList();

                    if (data!=null)
                    {
                        db.Configuration.ProxyCreationEnabled = false; 
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, $"No record found in databse !!");
                    }
                   }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadGateway, ex.ToString());
            }
        }
    }
}
