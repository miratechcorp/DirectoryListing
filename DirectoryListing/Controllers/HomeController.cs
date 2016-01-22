using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DirectoryListing.Library;
using Ionic.Zip;

namespace DirectoryListing.Controllers
{
    public class HomeController : Controller
    {
        #region Constants

        private static readonly string rootDirectory = Path.GetFullPath(Path.Combine(ConfigurationManager.AppSettings["RootDirectory"], string.Empty));

        #endregion Constants

        #region Methods

        #region Public Methods

        public ActionResult Index(string path)
        {
            string fullPath = Path.GetFullPath(Path.Combine(rootDirectory, path ?? string.Empty));
            if (!fullPath.StartsWith(rootDirectory, StringComparison.Ordinal)) {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (System.IO.File.Exists(fullPath)) {
                return new FilePathResult(fullPath, MimeMapping.GetMimeMapping(fullPath));
            }
            if (!Directory.Exists(fullPath)) {
                return HttpNotFound();
            }

            return View(new DirectoryListing.Models.Directory(rootDirectory, fullPath));
        }

        public ActionResult Zip(string path)
        {
            string fullPath = Path.GetFullPath(Path.Combine(rootDirectory, path ?? string.Empty));
            if (!fullPath.StartsWith(rootDirectory, StringComparison.Ordinal)) {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (System.IO.File.Exists(fullPath)) {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (!Directory.Exists(fullPath)) {
                return HttpNotFound();
            }
            var stream = new MemoryStream();
            var zip = new ZipFile();
            zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestSpeed;
            zip.AddDirectory(fullPath);
            zip.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/zip", new DirectoryInfo(fullPath).Name + ".zip");
        }

        #endregion Public Methods

        #endregion Methods
    }
}