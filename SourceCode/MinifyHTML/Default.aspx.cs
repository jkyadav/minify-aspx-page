using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinifyHTML
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private static readonly Regex RegexBetweenTags = new Regex(@">(?! )\s+", RegexOptions.Compiled);
        private static readonly Regex RegexLineBreaks = new Regex(@"([\n\s])+?(?<= {2,})<", RegexOptions.Compiled);

        protected override void Render(HtmlTextWriter writer)
        {
            using (HtmlTextWriter htmlwriter = new HtmlTextWriter(new System.IO.StringWriter()))
            {
                base.Render(htmlwriter);
                string html = htmlwriter.InnerWriter.ToString();
                html = RemoveWhitespaceFromHtmlPage(html);
                writer.Write(html);
            }
        }

        public string RemoveWhitespaceFromHtmlPage(string html)
        {
            html = RegexBetweenTags.Replace(html, ">");
            html = RegexLineBreaks.Replace(html, "<");
            return html.Trim();
        }
    }
}