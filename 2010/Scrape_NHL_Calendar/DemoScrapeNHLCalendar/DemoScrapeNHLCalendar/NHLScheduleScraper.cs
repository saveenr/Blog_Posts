using System.Linq;

namespace DemoScrapeNHLCalendar
{
    class NHLScheduleScraper
    {
        public string strip_excessive_whitespace(string s)
        {
            char[] seps = { '\n', '\t', ' ' };
            string[] pieces = Enumerable.ToArray<string>(s.Split().Where(x => x.Length > 0));
            string r = System.String.Join(" ", pieces);
            return r;
        }
        public string clean_text(string s)
        {
            string r = s;
            r = s.Replace("&nbsp;", " ");
            r = strip_excessive_whitespace(r);
            r = r.Trim();
            return r;

        }


        public void get_schedule()
        {
            string local_fname = "nhl-2010-2011.htm";

            var schedule_doc = new HtmlAgilityPack.HtmlDocument();
            schedule_doc.Load(local_fname);

            // identify all the td nodes that directly contain the text "Date"
            var row_nodes = schedule_doc.DocumentNode.DescendantNodes()
                .Where(n => n.Name == "div")
                .Where(n => n.GetAttributeValue("class", null) == "skedDataRow");


            foreach (var row_node in row_nodes)
            {
                var div_nodes = row_node.Elements("div").ToList();
                if (div_nodes.Count != 7)
                {
                    throw new System.Exception();
                }


                var date_node = div_nodes.Where(n => n.GetAttributeValue("class", null) == "skedDataRow date").FirstOrDefault();
                var team_nodes = div_nodes.Where(n => n.GetAttributeValue("class", null) == "skedDataRow team").ToList();
                var starttime_node = div_nodes.Where(n => n.GetAttributeValue("class", null) == "skedDataRow time").FirstOrDefault();
                var starttimeest_node = starttime_node.Elements("div").Where( n => n.GetAttributeValue("id", null) == "skedStartTimeEST").FirstOrDefault();
 
                string date = date_node != null ? date_node.InnerText : "NO DATE";
                date = clean_text(date);

                if (team_nodes.Count != 2)
                {
                    throw new System.Exception();
                }
                var teams = team_nodes.Select(n => clean_text(n.InnerText)).ToList();

                var startimeest = starttimeest_node != null ? starttimeest_node.InnerText : "NO START TIME";
                startimeest = clean_text(startimeest);

                System.Console.WriteLine(" {0} | {1} @ {2} | {3}", date, teams[0], teams[1], startimeest);
            }

            System.Console.ReadKey();
        }

        public void download_file(string url, string output_fname)
        {
            string fname = System.IO.Path.GetTempFileName();
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.Credentials = System.Net.CredentialCache.DefaultCredentials;
            wc.DownloadFile(url, output_fname);
        }
    }
}