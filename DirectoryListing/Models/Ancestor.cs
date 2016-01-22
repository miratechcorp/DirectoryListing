using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace DirectoryListing.Models
{
    public class Ancestor
    {
        #region Properties

        #region Public Properties

        public string Name
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

        public Ancestor(string rootDirectory, string fullName, string name)
        {
            Url = HttpUtility.UrlPathEncode(fullName.Replace(rootDirectory, string.Empty).Replace(Path.DirectorySeparatorChar, '/').TrimStart('/'));
            Name = HttpUtility.HtmlEncode(name);
        }

        #endregion Constructors
    }
}