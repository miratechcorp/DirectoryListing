using System.IO;
using System.Web;

namespace DirectoryListing.Models
{
    public class Ancestor
    {
        public Ancestor(string rootDirectory, string fullName, string name)
        {
            Url = HttpUtility.UrlPathEncode(fullName.Replace(rootDirectory, string.Empty).Replace(Path.DirectorySeparatorChar, '/').TrimStart('/'));
            Name = HttpUtility.HtmlEncode(name);
        }

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
    }
}
