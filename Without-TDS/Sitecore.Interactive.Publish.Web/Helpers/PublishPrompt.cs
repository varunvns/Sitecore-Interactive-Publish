using Sitecore.Interactive.Publish.Web.sitecore.Shell.Applications.Dialogs.Publish;

namespace Sitecore.Interactive.Publish.Web.Helpers
{
    public class PublishPrompt
    {
        /// <summary>
        /// This method will Toggle a flag which will be managing visiblity of the Desktop Notification which is shown after Publish.
        /// This method is executed using commands which is excuted after publish
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnPublishEnd(object sender, System.EventArgs args)
        {
            CustomPublishDialog.PublishFlag = true;
        }

    }
}