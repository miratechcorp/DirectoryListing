using System;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DirectoryListing.Controllers
{
    public class HomeController : Controller
    {
        private static readonly string rootDirectory = Path.GetFullPath(Path.Combine(ConfigurationManager.AppSettings["RootDirectory"], string.Empty));

        public ActionResult Index(string path)
        {
            var fullPath = Path.GetFullPath(Path.Combine(rootDirectory, path ?? string.Empty));
            if (!fullPath.StartsWith(rootDirectory, StringComparison.Ordinal))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (System.IO.File.Exists(fullPath))
            {
                return new FilePathResult(fullPath, MimeMapping.GetMimeMapping(fullPath));
            }
            if (!Directory.Exists(fullPath))
            {
                return HttpNotFound();
            }

            return View(new Models.Directory(rootDirectory, fullPath));
        }

        public ActionResult Zip(string path)
        {
            var fullPath = Path.GetFullPath(Path.Combine(rootDirectory, path ?? string.Empty));
            if (!fullPath.StartsWith(rootDirectory, StringComparison.Ordinal))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (System.IO.File.Exists(fullPath))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (!Directory.Exists(fullPath))
            {
                return HttpNotFound();
            }
            var zipFullPath = HttpContext.Server.MapPath(Path.Combine("~/App_Data/Zip", path.TrimEnd('/') + ".zip"));
            var zipFileInfo = new FileInfo(zipFullPath);
            if (!System.IO.File.Exists(zipFullPath))
            {
                zipFileInfo.Directory.Create();
                ZipFile.CreateFromDirectory(fullPath, zipFullPath, CompressionLevel.Fastest, false);
            }

            return File(zipFullPath, "application/zip", zipFileInfo.Name);
        }
    }
}
