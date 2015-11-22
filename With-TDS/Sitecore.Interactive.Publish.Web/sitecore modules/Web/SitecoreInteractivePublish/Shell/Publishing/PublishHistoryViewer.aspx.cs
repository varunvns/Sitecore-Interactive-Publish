using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls;
using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Interactive.Publish.Web.Helpers;
using Sitecore.Web.UI.HtmlControls;


namespace Sitecore.Interactive.Publish.Web.sitecore_modules.Web.SitecoreInteractivePublish.Shell.Publishing
{
    public partial class PublishHistoryViewer : System.Web.UI.Page
    {
        protected Listview JobList;
        static string logPath = Sitecore.Configuration.Settings.DataFolder + "/logs/PublishHistory.txt"; 

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Kept for Future Updates
            //IEnumerable<PublishJobEntry> jobEntries = PublishJobHelper.GetJobs();
            //if (((IList)jobEntries).Count == 0)
            //{
            //    //Add an Empty Item....
            //}
            //else
            //{
            //    string selectedItemId = "";
                
            //    if (JobList.SelectedItems.Length > 0)
            //    {
            //        selectedItemId = JobList.SelectedItems[0].ID;
            //    }
            //    JobList.Controls.Clear();

            //    jobEntries = jobEntries.OrderBy(j => j.Status.Job.QueueTime.ToLocalTime());

            //    foreach (var job in jobEntries)
            //    {
            //        if (job.State != "Finished")
            //        {
            //            ListviewItem listItem = new ListviewItem();
            //            listItem.Disabled = true;

            //            if (job.State.Contains("Queued") && job.OwnerName.Contains(Membership.GetUser().UserName))
            //            {
            //                //listItem.Selected = true;
            //            }
            //            if (job.State.Contains("Running") && job.OwnerName.Contains(Membership.GetUser().UserName))
            //            {
            //                //SheerResponse.SetInnerHtml("lblPublishing", "Publishing...");
            //            }

            //            //Context.ClientPage.AddControl(JobList, listItem);

            //            //PopulateListviewItem(listItem, job, selectedItemId);
            //        }
            //    }
            //}
            #endregion


            //Here we are reading Log File and fetching all entries from it.
            int skip = 0;

            try
            {
                string[] publishLog;
                do
                {
                    publishLog = File.ReadLines(logPath).Reverse().Skip(skip).Take(4).ToArray();

                    TableRow tr = new TableRow();
                    TableCell tcPublishHandle = new TableCell();
                    TableCell tcPublishTarget = new TableCell();
                    TableCell tcOwner = new TableCell();
                    TableCell tcDateTime = new TableCell();

                    tcPublishHandle.Text = publishLog[3].ToString();
                    tcPublishTarget.Text = publishLog[2].ToString();
                    tcOwner.Text = publishLog[1].ToString();
                    tcDateTime.Text = publishLog[0].ToString();

                    tr.Controls.Add(tcPublishHandle);
                    tr.Controls.Add(tcPublishTarget);
                    tr.Controls.Add(tcOwner);
                    tr.Controls.Add(tcDateTime);

                    tblPublishHistory.Controls.Add(tr);
                    skip = skip + 4;
                } while (!(publishLog == null || publishLog.Length == 0));
            }
            catch (Exception ex)
            {
                if (tblPublishHistory.Rows.Count == 0 )
                {
                    Log.Error("Publish History : Sitecore Application Error : " + ex.Message, this);
                    Log.Error("Publish History : Sitecore Application Error : " + ex.StackTrace, this);

                    TableRow tr = new TableRow();
                    TableCell tcInfo = new TableCell();
                    tcInfo.Text = "Publish log might be empty or has been deleted";
                    tcInfo.ColumnSpan = 4;
                    tcInfo.Font.Bold = true;
                    tr.CssClass = "danger";

                    tr.Controls.Add(tcInfo);
                    tblPublishHistory.Controls.Add(tr);    
                }
                
            }
            
        }

        /// <summary>
        /// This Method will be used for deleting or clearing Publish History Log.
        /// This Method is kept for future use as of it is not used in V-1.0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearPublishLog_Click(object sender, EventArgs e)
        {
            using (new Sitecore.SecurityModel.SecurityDisabler())
            {
                MembershipUser currentUser = Membership.GetUser();
                
                currentUser.GetType();
                if (File.Exists(logPath))
                {
                    File.Delete(logPath);
                }    
            }
        }
    }
}