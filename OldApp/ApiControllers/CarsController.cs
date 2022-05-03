using System.Web.Http;
using MLS.Models;

namespace MLS.OldApp.ApiControllers
{
    [Authorize]
    public class CarsController:ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            var cars = new[]
            {
                new CarModel { Manufacturer = "Kia", Name = "Cee'd", Year = 2006, WikipediaUrl = "https://it.wikipedia.org/wiki/Kia_Cee%27d" }
            };
            return Json(cars);
        }
    }
}