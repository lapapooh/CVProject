using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace INTRANET.Controllers.Api
{
    public class BaseApiController : ApiController
    {
        // Please add here common methods/properties for all controllers

        protected BaseApiController()
        {
            // prevent redirect to login page for unauthorized webapi calls            
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
