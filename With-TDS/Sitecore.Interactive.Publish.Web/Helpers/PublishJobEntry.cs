using Sitecore.Jobs;
using Sitecore.Security.Accounts;

namespace Sitecore.Interactive.Publish.Web.Helpers
{
    /// <summary>
    /// This class acts as a Model for all the Publish Job Details
    /// </summary>
    public class PublishJobEntry
    {
        public PublishJobEntry(Handle jobHandle, string jobName, string category, JobStatus jobStatus, User jobOwner)
        {
            JobHandle = jobHandle.ToString();
            Name = jobName;
            Status = jobStatus;
            Owner = jobOwner;
            Category = category;
        }

        public string JobHandle { get; set; }

        public string Name { get; set; }

        public JobStatus Status { get; set; }

        public string State
        {
            get { return Status != null ? Status.State.ToString() : "Unknown"; }
        }

        public Account Owner { get; set; }

        public string OwnerName { get { return Owner != null ? Owner.Name : "Unknown"; } }

        public string Category { get; set; }

    }


}