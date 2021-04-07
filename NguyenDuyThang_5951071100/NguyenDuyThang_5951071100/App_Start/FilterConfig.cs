using System.Web;
using System.Web.Mvc;

namespace NguyenDuyThang_5951071100
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
