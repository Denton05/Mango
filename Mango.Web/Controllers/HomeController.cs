using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Public Methods

        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}
