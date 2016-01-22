using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace DirectoryListing.Models
{
    public class Info
    {
        #region Constants

        public static readonly string ZipIcon = IconFromFilePath(".zip");

        #endregion Constants

        #region Properties

        #region Public Properties

        public string Icon
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public string Size
        {
            get;
            private set;
        }

        public string Url
        {
            get;
            private set;
        }

        #endregion Public Properties

        #endregion Properties

        #region Constructors

        public Info(string rootDirectory, string fullName, string name, long length)
        {
            Url = HttpUtility.UrlPathEncode(fullName.Replace(rootDirectory, string.Empty).Replace(Path.DirectorySeparatorChar, '/').TrimStart('/'));
            Name = HttpUtility.HtmlEncode(name);
            Size = BytesToSize(length);
            Icon = IconFromFilePath(fullName);
        }

        #endregion Constructors

        #region Static Methods

        #region Private Static Methods

        private static string BytesToSize(long length)
        {
            const int scale = 1024;
            string[] orders = new string[] { "EiB", "PiB", "TiB", "GiB", "MiB", "KiB", "B" };
            long max = (long)Math.Pow(scale, orders.Length - 1);

            foreach (string order in orders) {
                if (length >= max) {
                    return string.Format("{0:##.##} {1}", decimal.Divide(length, max), order);
                }
                max /= scale;
            }
            return "0 B";
        }

        private static string IconFromFilePath(string fullName)
        {
            try {
                Icon icon = IconTools.GetIconForExtension(fullName, ShellIconSize.SmallIcon);
                Bitmap bitmap = icon.ToBitmap();
                MemoryStream stream = new MemoryStream();
                bitmap.Save(stream, ImageFormat.Png);
                byte[] bytes = stream.ToArray();
                return Convert.ToBase64String(bytes);
            } catch {
                return string.Empty;
            }
        }

        #endregion Private Static Methods

        #endregion Static Methods
    }
}