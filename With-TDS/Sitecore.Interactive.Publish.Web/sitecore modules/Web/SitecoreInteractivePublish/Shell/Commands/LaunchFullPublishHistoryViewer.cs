using Sitecore.Shell.Framework.Commands;

namespace Sitecore.Interactive.Publish.Web.sitecore_modules.Web.SitecoreInteractivePublish.Shell.Commands
{
    public class LaunchFullPublishHistoryViewer : Command
    {
        public override void Execute(CommandContext context)
        {
            Sitecore.Shell.Framework.Windows.RunApplication("Publish History");
        }
    }
}