using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Sitecore.Diagnostics;
using Sitecore.Interactive.Publish.Web.Helpers;
using Sitecore.Shell.Applications.Dialogs.Publish;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.Interactive.Publish.Web.sitecore.Shell.Applications.Dialogs.Publish
{
    public class CustomPublishDialog : PublishForm
    {
        public static bool PublishFlag = false;
        //public static string currentPage = String.Empty, previousPage = String.Empty;
        protected Listview JobList;
        protected Scrollbox JobPanel;
        private int Timer = 600;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void ActivePageChanged(string page, string oldPage)
        {
            base.ActivePageChanged(page, oldPage);
            if (page == "Publishing")
            {
                //currentPage = page;
                //previousPage = oldPage;
                Populate();
            }
        }

        protected override bool ActivePageChanging(string page, ref string newpage)
        {
            return base.ActivePageChanging(page, ref newpage);
        }

        protected override void OnNext(object sender, EventArgs formEventArgs)
        {
            base.OnNext(sender, formEventArgs);
        }

        protected override void OnCancel(object sender, EventArgs formEventArgs)
        {
            base.OnCancel(sender, formEventArgs);
        }

        protected void StartTimer()
        {
            Populate();
        }

        protected void Populate()
        {
            FillJobList();
            SheerResponse.Timer("Populate", Timer);
        }

        private void FillJobList()
        {
            try
            {
                bool queuedFlag = false;
                IEnumerable<PublishJobEntry> jobEntries = PublishJobHelper.GetJobs();
                if (((IList)jobEntries).Count == 0)
                {
                    AddEmptyItem();
                }
                else
                {
                    string selectedItemId = "";

                    if (JobList.SelectedItems.Length > 0)
                    {
                        selectedItemId = JobList.SelectedItems[0].ID;
                    }
                    JobList.Controls.Clear();

                    foreach (var job in jobEntries)
                    {
                        if (job.State.Contains("Queued"))
                        {
                            queuedFlag = true;
                            SheerResponse.SetInnerHtml("lblPublishing", "Queued...");

                            break;
                        }
                        else
                        {
                            queuedFlag = false;
                        }
                    }

                    if (queuedFlag)
                    {
                        SheerResponse.SetAttribute("PublishStatusManagerLink", "style", "display:block;");
                        SheerResponse.SetAttribute("PublishingTarget", "style", "display:none;");
                        SheerResponse.SetAttribute("imgSpinner", "style", "margin-top:20px !important;");
                        SheerResponse.SetInnerHtml("lblPublishing", "Queued...");

                        jobEntries = jobEntries.OrderBy(j => j.Status.Job.QueueTime.ToLocalTime());

                        foreach (var job in jobEntries)
                        {
                            if (job.State != "Finished")
                            {
                                ListviewItem listItem = new ListviewItem();
                                listItem.Disabled = true;

                                if (job.State.Contains("Queued") && job.OwnerName.Contains(Membership.GetUser().UserName))
                                {
                                    listItem.Selected = true;
                                }
                                if(job.State.Contains("Running") && job.OwnerName.Contains(Membership.GetUser().UserName))
                                {
                                    SheerResponse.SetInnerHtml("lblPublishing", "Publishing...");
                                }

                                Context.ClientPage.AddControl(JobList, listItem);

                                PopulateListviewItem(listItem, job, selectedItemId);
                            }
                        }
                        SheerResponse.SetInnerHtml("JobPanel", JobList);
                        SheerResponse.SetAttribute("JobPanel", "Disabled", "true");
                    }
                    else
                    {
                        SheerResponse.SetAttribute("PublishStatusManagerLink", "style", "display:none;");
                        SheerResponse.SetAttribute("PublishingTarget", "style", "display:block;");
                        SheerResponse.SetAttribute("imgSpinner", "style", " ");
                        SheerResponse.SetInnerHtml("lblPublishing", "Publishing...");

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Publish History : Sitecore Ribbon Error : " + ex.Message, Context.Item);
                Log.Error("Publish History : Sitecore Ribbon Error : " + ex.StackTrace, Context.Item);
            }
        }


        private void AddEmptyItem()
        {
            JobList.Controls.Clear();
            ListviewItem emptyItem = new ListviewItem();
            emptyItem.Header = Sitecore.Globalization.Translate.Text("There are no publishing jobs to display.");
            emptyItem.Disabled = true;
            Context.ClientPage.AddControl(JobList, emptyItem);
            SheerResponse.SetInnerHtml("JobPanel", JobList);
        }

        protected void PopulateListviewItem(ListviewItem listItem, PublishJobEntry job, string selectedItemId)
        {
            listItem.ID = job.JobHandle;
            listItem.Disabled = true;

            if (job.JobHandle.Equals(selectedItemId))
            {
                listItem.Selected = true;
            }
            listItem.Header = job.Name;
            listItem.ColumnValues["jobName"] = job.Name;
            //listItem.ColumnValues["jobCategory"] = job.Category;
            listItem.ColumnValues["jobState"] = job.State;
            listItem.ColumnValues["jobOwner"] = job.OwnerName;
            listItem.ColumnValues["jobProcessed"] = job.Status.Total > 0
                                                       ? job.Status.Processed + "/" + job.Status.Total
                                                       : job.Status.Processed.ToString();
            // QueueTime get setup whenever the job gets created. 
            //listItem.ColumnValues["jobStarted"] = job.Status.Job.QueueTime.ToLocalTime().ToLongTimeString();
        }
    }
}