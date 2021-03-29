using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using Content_Upload_Model.Models;

namespace Content_Upload_Model.Controllers
{
    public class ContentController : ApiController
    {
        // GET api/<controller>

        public HttpResponseMessage Get()
        {

            try
            {
                Content c = new Content();
                List<Content> contentsList = c.getcontents();
                return Request.CreateResponse(HttpStatusCode.OK, contentsList);
            }
            catch (Exception ex)
            {
                if (ex.Message == "failed to connect to the server")
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }

        }



        //POST api/<controller>
        public HttpResponseMessage Post([FromBody] Content content)
        {
            try
            {
                string Cname = content.Insert();
                return Request.CreateResponse(HttpStatusCode.OK, Cname);
            }
            catch (Exception ex)
            {
                if (ex.Message == "failed to connect to the server")
                {
                    //500
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);

                }
                else if (ex.Message == "no content was inserted")
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
                    //throw (ex);
                }
                else return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);

            }

        }

        //upload video
        [HttpPost]
        [Route("api/Content/uploadVideo")]
        public HttpResponseMessage Post()
        {
            List<string> VideoLinks = new List<string>();
            var httpContext = HttpContext.Current;

            // Check for any uploaded file  
            if (httpContext.Request.Files.Count > 0)
            {
                //Loop through uploaded files  
                for (int i = 0; i < httpContext.Request.Files.Count; i++)
                {
                    HttpPostedFile httpPostedFile = httpContext.Request.Files[i];

                    // this is an example of how you can extract addional values from the Ajax call
                    string name = httpContext.Request.Form["name"];

                    if (httpPostedFile != null)
                    {
                        // Construct file save path  
                        //var fileSavePath = Path.Combine(HostingEnvironment.MapPath(ConfigurationManager.AppSettings["fileUploadFolder"]), httpPostedFile.FileName);
                        //string fname = httpPostedFile.FileName.Split('\\').Last();
                        string timeStamp = (DateTime.Now).ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss");

                        string typePic = httpPostedFile.FileName.Split('.').Last();

                        string strToimg = name + "-" + timeStamp + "." + typePic;
                        var fileSavePath = Path.Combine(HostingEnvironment.MapPath("~/video"), strToimg);
                        // Save the uploaded file  
                        httpPostedFile.SaveAs(fileSavePath);
                        VideoLinks.Add("video/" + strToimg);
                    }
                }
            }

            // Return status code  
            return Request.CreateResponse(HttpStatusCode.Created, VideoLinks);
        }




        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        //// POST api/<controller>
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}