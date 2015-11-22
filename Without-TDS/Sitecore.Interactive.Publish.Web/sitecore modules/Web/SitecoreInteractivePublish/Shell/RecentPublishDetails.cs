using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Shell.Applications.ContentManager.Galleries;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.Interactive.Publish.Web.sitecore_modules.Web.SitecoreInteractivePublish.Shell.Publishing
{
    /// <summary>
    /// This Class manages the gallery button which shows us recent publish jobs in Publish Tab
    /// </summary>
    public class RecentPublishDetails : GalleryForm
    {
        protected Scrollbox Links;

        public override void HandleMessage(Message message)
        {
            Assert.ArgumentNotNull(message, "message");
            Invoke(message, true);
            message.CancelBubble = true;
            message.CancelDispatch = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            Assert.ArgumentNotNull(e, "e");
            base.OnLoad(e);
            if (!Context.ClientPage.IsEvent)
            {
                StringBuilder result = new StringBuilder();
                RenderRecentPublish(result);
                Links.Controls.Add(new LiteralControl(result.ToString()));
            }
        }

        /// <summary>
        /// This method will fill Job details in Gallery of the button
        /// </summary>
        /// <param name="result">Here a null StringBuilder object is required </param>
        private void RenderRecentPublish(StringBuilder result)
        {
            result.Append("<div style=\"font-weight:bold;padding:2px 0px 4px 0px\"> <h3>" + Translate.Text("Recent Publish Details") + " :  </h3> </div>");

            string logPath = Sitecore.Configuration.Settings.DataFolder + "/logs/PublishHistory.txt"; 
            int skip = 0;
            int recentPublishEntries = int.Parse(Settings.GetSetting("RecentPublish"));
            for (int publishJobNumber = 0; publishJobNumber < recentPublishEntries; publishJobNumber++)
            {
                try
                {
                    string[] lines = File.ReadLines(logPath).Reverse().Skip(skip).Take(4).ToArray();
                    if (!(lines == null || lines.Length == 0))
                    {
                        result.Append(string.Concat(new object[] { "<br /><b style=\"padding:2px 0px 2px 0px; \">Publish Handler :  " + lines[3] + "</b>" }));
                        result.Append(string.Concat(new object[] { "<br /><b style=\"padding:2px 0px 2px 0px; \">Publish Target :   </b>" + lines[2] }));
                        result.Append(string.Concat(new object[] { "<br /><b style=\"padding:2px 0px 2px 0px; \">Owner :    </b>" + lines[1] }));
                        result.Append(string.Concat(new object[] { "<br /><b style=\"padding:2px 0px 2px 0px; \">Date & Time :  </b>" + lines[0] }));
                        result.Append(string.Concat(new object[] { "<br /><hr />" }));
                    }
                }
                catch (Exception ex)
                {

                    Log.Error("Publish History : Sitecore Ribbon Error : " + ex.Message, Context.Item);
                    Log.Error("Publish History : Sitecore Ribbon Error : " + ex.StackTrace, Context.Item);

                    result.Append(string.Concat(new object[] { "<b><a href=\"#\"> Publish log might be empty or has been deleted </a></b>" }));
                    break;
                }
                finally
                {
                    skip = skip + 4;
                }

            }

        }
    }
}