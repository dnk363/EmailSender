using EmailSender.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.Services
{
    public class FormMessageService : IFormMessageService
    {
        private string _message;
        private List<string> _head = new List<string>();
        private Dictionary<string, string> _row = new Dictionary<string, string>();
        private List<Dictionary<string, string>> _table = new List<Dictionary<string, string>>();

        public string GetMessage(ISiteSettings siteSettings)
        {
            HtmlDocument page = new HtmlDocument();
            page.LoadHtml(GetRequest(siteSettings.SiteUrl));
            HtmlNode table = page.DocumentNode.SelectSingleNode("//table[@class='" + siteSettings.TableClassID + "']");

            HtmlNodeCollection rows = table.SelectNodes("//tr");
            HtmlNodeCollection head = table.SelectNodes("//th");
            HtmlNodeCollection cells;

            foreach (var th in head)
            {
                _head.Add(th.InnerText);
            }

            foreach (var tr in rows)
            {
                _row = new Dictionary<string, string>();
                cells = tr.ChildNodes;
                for (int i = 0; i < cells.Count; i++)
                {
                    _row.Add(_head[i], cells[i].InnerText);
                }

                _table.Add(_row);
            }

            _message = "<!DOCTYPE html><html><head></head><body>" + GetMyTable(_head, _table, siteSettings) + "</body></html>";

            _head.Clear();
            _table.Clear();
            _row.Clear();

            return _message;
        }

        private string GetMyTable(List<string> head, List<Dictionary<string, string>> itemList, ISiteSettings siteSettings)
        {
            StringBuilder table = new StringBuilder();
            table.Append("<TABLE>");
            foreach (var th in head)
            {
                table.Append("<TH>" + th + "</TH>");
            }
            foreach (var row in itemList)
            {
                if (row.GetValueOrDefault(siteSettings.ColumnToCompare) == siteSettings.CompareValue
                    && row.GetValueOrDefault(siteSettings.NotNullColumn) != "")
                {
                    table.Append("<TR>");
                    foreach (var cell in row)
                    {
                        table.Append("<TD>");
                        table.Append(cell.Value);
                        table.Append("</TD>");
                    }
                    table.Append("</TR>");
                }
            }
            table.Append("</TABLE>");

            return table.ToString();
        }

        private string GetRequest(string url)
        {
            var content = new MemoryStream();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.AllowAutoRedirect = false;
                httpWebRequest.Method = "GET";
                using (var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (var stream = httpWebResponse.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream, Encoding.GetEncoding(httpWebResponse.CharacterSet)))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch
            {
                return String.Empty;
            }
        }
    }
}
