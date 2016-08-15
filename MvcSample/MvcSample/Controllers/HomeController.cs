using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSample.Controllers
{
	public class HomeController : Controller
	{
	  private const string ViewCountName = "ViewCount";


	  public ActionResult Index()
	  {
	    int? savedCount = Session[ViewCountName] as int?;
	    if (savedCount == null)
	    {
	      Session[ViewCountName] = 1;
	    }
	    else
	    {
	      Session[ViewCountName] = savedCount + 1;
	    }
	    if (savedCount == 3)
	    {
	      return Redirect("/home/about");
	    }
	    return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}