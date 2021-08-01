using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BeCoreApp.Extensions;
using BeCoreApp.Utilities.Dtos;
using BeCoreApp.Utilities.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeCoreApp.Areas.Admin.Controllers
{
    public class UploadController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public UploadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task UploadImageForCKEditor(IList<IFormFile> upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            DateTime now = DateTime.Now;
            if (upload.Count == 0)
            {
                await HttpContext.Response.WriteAsync("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ",'','Please choose an image');</script>");
            }
            else
            {
                var file = upload[0];

                var result = IsValidImage(file);

                if (!result.Success)
                {
                    await HttpContext.Response.WriteAsync("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ",'','" + result.Message + "');</script>");
                }
                else
                {
                    var imageFolder = $@"\uploaded\images\{now.ToString("yyyyMMdd")}";

                    var fileName = ProcessUpdatingImage(imageFolder, file);

                    await HttpContext.Response.WriteAsync("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", '" + Path.Combine(imageFolder, fileName).Replace(@"\", @"/") + "');</script>");
                }
            }
        }

        /// <summary>
        /// Upload image for form
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadImage()
        {
            DateTime now = DateTime.Now;
            var files = Request.Form.Files;
            if (files.Count == 0)
            {
                return new BadRequestObjectResult(files);
            }

            var file = files.FirstOrDefault();

            var result = IsValidImage(file);

            if (!result.Success)
                return new BadRequestObjectResult(result.Message);

            var userName = User.GetSpecificClaim("UserName");

            var imageFolder = $@"\uploaded\images\{userName}\{now.ToString("yyyyMMddHHmmss")}";

            var fileName = ProcessUpdatingImage(imageFolder, file);

            return new OkObjectResult(Path.Combine(imageFolder, fileName).Replace(@"\", @"/"));
        }

        private string ProcessUpdatingImage(string imageFolder, IFormFile file)
        {
            var filename = ContentDispositionHeaderValue
                                    .Parse(file.ContentDisposition)
                                    .FileName
                                    .Trim('"');
            string folder = _hostingEnvironment.WebRootPath + imageFolder;

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string filePath = Path.Combine(folder, filename);
            using (FileStream fs = System.IO.File.Create(filePath))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            return filename;
        }

        private GenericResult IsValidImage(IFormFile file)
        {
            if (file.Length > 3000000)
                return GenericResult.ToFail("Please upload smaller image 3MB");

            var fileExtension = Path.GetExtension(file.FileName);

            if (!fileExtension.Equals(".jpg", ".png", ".PNG", ".jpeg") && !file.ContentType.Equals("image/jpg", "image/jpeg", "image/pjpeg", "image/png", "image/x-png"))
                return GenericResult.ToFail("Only images of jpg hoặc png");

            return GenericResult.ToSuccess();
        }
    }
}
