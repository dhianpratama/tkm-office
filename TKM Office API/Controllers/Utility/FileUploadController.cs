using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace TKM_Office_API.Controllers.Utility
{
    public class FileUploadController : BaseController
    {
        public const string TransactionImageFolder = "Content/Transaction";

        public class TransactionImage
        {
            public long TransactionId { get; set; }
            public HttpPostedFileBase Blob { get; set; }
        }

        public async Task<HttpResponseMessage> UploadTransactionImage()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var TransactionImage = new TransactionImage();
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                var imageDirPath = HostingEnvironment.MapPath("~/" + TransactionImageFolder);
                if (imageDirPath == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Image Folder not valid");


                await Request.Content.ReadAsMultipartAsync(provider);

                var filename = "";
                var localFilename = "";
                foreach (MultipartFileData file in provider.FileData)
                {
                    filename = file.Headers.ContentDisposition.FileName.Replace("\"", ""); ;
                    localFilename = file.LocalFileName;
                }

                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        if (key == "TransactionId")
                        {
                            TransactionImage.TransactionId = long.Parse(val);
                        }
                    }
                }

                if (String.IsNullOrEmpty(filename)) return Request.CreateResponse(HttpStatusCode.BadRequest,"Filename not valid");
                
                if (TransactionImage.TransactionId == 0)
                {
                    var path = Path.Combine(imageDirPath, "temp", filename);
                    if (!Directory.Exists(Path.Combine(imageDirPath, "temp")))
                    {
                        Directory.CreateDirectory(Path.Combine(imageDirPath, "temp"));
                    }
                    File.Copy(localFilename, path, true);
                }
                else
                {
                    var path = Path.Combine(imageDirPath, TransactionImage.TransactionId.ToString(), filename);
                    if (!Directory.Exists(Path.Combine(imageDirPath, TransactionImage.TransactionId.ToString())))
                    {
                        Directory.CreateDirectory(Path.Combine(imageDirPath, TransactionImage.TransactionId.ToString()));
                    }
                    File.Copy(localFilename, path, true);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

    }
}