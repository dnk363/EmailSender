using EmailSender.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace EmailSender.Services
{
    public class FormMessageService
    {
        private string _message;
        private List<string> _row = new List<string>();
        private List<string> _head = new List<string>();
        private List<List<string>> _table = new List<List<string>>();
        
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
                _row = new List<string>();
                cells = tr.ChildNodes;
                foreach (var td in cells)
                {
                    _row.Add(td.InnerText);
                }

                _table.Add(_row);
            }

            _message = "<!DOCTYPE html><html><head></head><body>" + GetMyTable(_head, _table, siteSettings.CompareValue) + "</body></html>";

            _head.Clear();
            _table.Clear();
            _row.Clear();

            return _message;
        }

        private string GetRequest(string url)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.AllowAutoRedirect = false;
                httpWebRequest.Method = "GET";
                httpWebRequest.Referer = "http://google.com";
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

        private static string GetMyTable(List<string> head, List<List<string>> itemList, string valueToCompare)
        {
            StringBuilder table = new StringBuilder();
            table.Append("<TABLE>");
            foreach (var th in head)
            {
                table.Append("<TH>" + th + "</TH>");
            }
            foreach (var row in itemList)
            {
                if (row.Exists(x => x == valueToCompare))
                {
                    table.Append("<TR>");
                    foreach (var cell in row)
                    {
                        table.Append("<TD>");
                        table.Append(cell);
                        table.Append("</TD>");
                    }
                    table.Append("</TR>");
                }
            }
            table.Append("</TABLE>");

            return table.ToString();
        }
    }
}
