using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using TravelAlberta.Exercise.Domain;
using TravelAlberta.Exercise.Domain.Parser;
using TravelAlberta.Exercise.Services;
using TravelAlberta.Exercise.WebApi.Client;

namespace TravelAlberta.Exercise.Controllers
{
    public class ListController : Controller
    {
        private readonly ICsvContentClient csvClient;
        private readonly ICsvParser csvParser;
        private readonly IDomainMapper<PlacesToStay> mapper;

        public ListController(IConfigInfo configInfo, ICsvParser parser, IDomainMapper<PlacesToStay> domainMapper)
        {
            csvClient = new CsvContentClient(configInfo.CsvReadUrl, string.Empty);
            this.csvParser = parser;
            this.mapper = domainMapper;
        }

        public async Task<JsonResult> Index()
        {
            string csvContent = await csvClient.GetCsvContent().ConfigureAwait(false);
            IEnumerable<IEnumerable<string>> placesToStayRawLines = this.csvParser.Parse(csvContent);
            List<PlacesToStay> placesToStay = this.mapper.Map(placesToStayRawLines);

            return Json(placesToStay, JsonRequestBehavior.AllowGet);
        }
    }
}
