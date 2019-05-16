using System;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace DirectoryListing.Models
{
    public class Info
    {
        public static readonly string ZipIcon = IconFromFilePath(".zip");

        public Info(string rootDirectory, string fullName, string name, long length)
        {
            Url = HttpUtility.UrlPathEncode(fullName.Replace(rootDirectory, string.Empty).Replace(Path.DirectorySeparatorChar, '/').TrimStart('/'));
            Name = HttpUtility.HtmlEncode(name);
            Size = BytesToSize(length);
            Icon = IconFromFilePath(fullName);
        }

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

        private static string BytesToSize(long length)
        {
            const int scale = 1024;
            var orders = new string[] { "EiB", "PiB", "TiB", "GiB", "MiB", "KiB", "B" };
            var max = (long)Math.Pow(scale, orders.Length - 1);

            foreach (var order in orders)
            {
                if (length >= max)
                {
                    return string.Format("{0:##.##} {1}", decimal.Divide(length, max), order);
                }
                max /= scale;
            }
            return "0 B";
        }

        private static string IconFromFilePath(string fullName)
        {
            try
            {
                var icon = IconTools.GetIconForExtension(fullName, ShellIconSize.SmallIcon);
                var bitmap = icon.ToBitmap();
                var stream = new MemoryStream();
                bitmap.Save(stream, ImageFormat.Png);
                var bytes = stream.ToArray();
                return Convert.ToBase64String(bytes);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
