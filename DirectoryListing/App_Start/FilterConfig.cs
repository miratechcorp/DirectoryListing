using System.Web;
using System.Web.Mvc;

namespace DirectoryListing
{
    public class FilterConfig
    {
        #region Static Methods

        #region Public Static Methods

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        #endregion Public Static Methods

        #endregion Static Methods
    }
}