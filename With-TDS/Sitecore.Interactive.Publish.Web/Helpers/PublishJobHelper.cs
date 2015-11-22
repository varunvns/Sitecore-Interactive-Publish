using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sitecore.Configuration;
using Sitecore.Jobs;

namespace Sitecore.Interactive.Publish.Web.Helpers
{
    /// <summary>
    /// This class contains various helper methods for Publish Jobs.
    /// </summary>
    public class PublishJobHelper
    {
        /// <summary>
        /// This method fetches all publishing jobs which are currently active and adds those jobs to the list and returns it.
        /// </summary>
        /// <returns>IEnumerable<PublishJobEntry> : List of PublishJobEntry </returns>
        public static IEnumerable<PublishJobEntry> GetJobs()
        {
            var jobs = JobManager.GetJobs();
            var publishJobs =
               jobs.Where(job => job.Category.StartsWith("publish", StringComparison.InvariantCultureIgnoreCase)).Select(
                  job => new PublishJobEntry(job.Handle, job.Name, job.Category, job.Status, job.Options.ContextUser));
            List<PublishJobEntry> jobList = new List<PublishJobEntry>(publishJobs);

            LogJob(jobList);

            return jobList;

        }

        /// <summary>
        /// This method will fetch all publishing jobs which are currently active and adds those jobs to the list and returns it.
        /// </summary>
        /// <param name="state">This Method is accepting Sitecore.Jobs.JobState enum as a parameter.</param>
        /// <returns>IEnumerable<PublishJobEntry> : List of PublishJobEntry </returns>
        public static IEnumerable<PublishJobEntry> GetJobs(JobState state)
        {
            var jobs = JobManager.GetJobs();
            var publishJobs =
               jobs.Where(job => job.Category.Equals("publish", StringComparison.InvariantCultureIgnoreCase) && job.Status.State == state).Select(
                  job => new PublishJobEntry(job.Handle, job.Name, job.Category, job.Status, job.Options.ContextUser));
            List<PublishJobEntry> jobList = new List<PublishJobEntry>(publishJobs);

            LogJob(jobList);

            return jobList;
        }

        /// <summary>
        /// This method will return a selected Job from currently running jobs.
        /// This Method is kept for future use as of it is not used in V-1.0
        /// </summary>
        /// <param name="jobContainerId">This method will require a job's container Id in string form.</param>
        /// <returns>This Method returns a Sitecore.Jobs.Job Object</returns>
        public static Job GetSelectedJob(string jobContainerId)
        {
            Sitecore.Web.UI.HtmlControls.Listview jobList =
               Context.ClientPage.FindSubControl(jobContainerId) as Sitecore.Web.UI.HtmlControls.Listview;
            if (jobList != null && jobList.SelectedItems.Length > 0)
            {
                string jobHandle = jobList.SelectedItems[0].ID;
                return JobManager.GetJob(Handle.Parse(jobHandle));
            }
            return null;
        }

        /// <summary>
        /// This method is used for adding publish job details to our custom log file.
        /// </summary>
        /// <param name="jobList">This method requires a list of PublishJobEntry Class as a parameter.</param>
        public static void LogJob(List<PublishJobEntry> jobList)
        {
            string logPath = Sitecore.Configuration.Settings.DataFolder + "/logs/PublishHistory.txt"; 
            string logContent = String.Empty;
            if (!File.Exists(logPath))
            {
                File.Create(logPath).Close();
                
                using (StreamWriter streamWriter = new StreamWriter(logPath, true))
                {
                    foreach (PublishJobEntry job in jobList)
                    {
                        if (!logContent.Contains(job.JobHandle))
                        {
                            if (job.State == "Finished" && job.Category != "PublishManager")
                            {
                                streamWriter.WriteLine(job.JobHandle);
                                streamWriter.WriteLine(job.Name);
                                streamWriter.WriteLine(job.OwnerName);
                                streamWriter.WriteLine(job.Status.Job.QueueTime.ToLocalTime().ToString());
                            }
                            
                        }

                    }
                    streamWriter.Close();
                }
                   
            }
            else if (File.Exists(logPath))
            {
                //Taking whole content in string variable for checking for duplicate entries
                using (StreamReader streamReader = new StreamReader(logPath))
                {
                    logContent = streamReader.ReadToEnd();
                    streamReader.Close();
                }

                using (StreamWriter streamWriter = new StreamWriter(logPath, true))
                {
                    foreach (PublishJobEntry job in jobList)
                    {

                        if (!logContent.Contains(job.JobHandle))
                        {
                            if (job.State == "Finished" && job.Category != "PublishManager")
                            {
                                streamWriter.WriteLine(job.JobHandle);
                                streamWriter.WriteLine(job.Name);
                                streamWriter.WriteLine(job.OwnerName);
                                streamWriter.WriteLine(job.Status.Job.QueueTime.ToLocalTime().ToString());
                            }
                        }

                    }
                    streamWriter.Close();
                }
            }
        }
    }
}