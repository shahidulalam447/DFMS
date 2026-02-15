using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FirmWebApp.Controllers.Employee
{
    internal class HttpStatusCodeResult : ActionResult
    {
        private HttpStatusCode badRequest;

        public HttpStatusCodeResult(HttpStatusCode badRequest)
        {
            this.badRequest = badRequest;
        }
    }
}