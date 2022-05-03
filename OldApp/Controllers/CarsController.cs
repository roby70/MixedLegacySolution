using System.Web.Mvc;
using Antlr.Runtime.Misc;
using MLS.Models;

namespace MLS.OldApp.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {
        [HttpGet]
        public ActionResult Get()
        {
            var cars = new CarModel[]
            {
                new CarModel { Manufacturer = "Fiat", Name = "1100 Musone", Year = 1937, WikipediaUrl = "https://it.wikipedia.org/wiki/Fiat_1100_Musone" },
                new CarModel { Manufacturer = "Citroën", Name = "DS", Year = 1955, WikipediaUrl = "https://it.wikipedia.org/wiki/Citro%C3%ABn_DS" },
            };
            return Json(cars, JsonRequestBehavior.AllowGet);
        }
    }
}