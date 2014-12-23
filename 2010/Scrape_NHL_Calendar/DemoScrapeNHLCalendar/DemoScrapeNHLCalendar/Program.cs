using System.Text;
using System.Linq;

namespace DemoScrapeNHLCalendar
{
    class Program
    {
        static void Main(string[] args)
        {
            var scraper = new NHLScheduleScraper();

            scraper.get_schedule();

        }
    }
}
