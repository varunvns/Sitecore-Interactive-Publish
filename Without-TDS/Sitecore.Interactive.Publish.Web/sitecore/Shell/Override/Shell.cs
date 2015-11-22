using System;
using System.IO;
using System.Linq;
using Sitecore.Interactive.Publish.Web.sitecore.Shell.Applications.Dialogs.Publish;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Shell.Applications;
using Sitecore.Web;
using Sitecore.Web.UI;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.Interactive.Publish.Web.sitecore.Shell.Override
{
    /// <summary>
    /// this class is inheriting ShellForm Class which is sitecore's default class which manages all of the User's desktop activites
    /// </summary>
    public class Shell : ShellForm
    {
        private int Timer = 15000;

        public override void HandleMessage(Sitecore.Web.UI.Sheer.Message message)
        {
            base.HandleMessage(message);
            CheckforPublish();
        }

        protected void ActivateProxies()
        {
            base.ActivateProxies();
            CheckforPublish();

        }

        protected void ChangeDatabase(string databaseName)
        {
            base.ChangeDatabase(databaseName);
            CheckforPublish();

        }

        protected void CreateShortcut()
        {
            base.CreateShortcut();
            CheckforPublish();

        }

        protected void DeactivateProxies()
        {
            base.DeactivateProxies();
            CheckforPublish();

        }

        protected void DropLink(string data)
        {
            base.DropLink(data);
            CheckforPublish();

        }

        protected void ClosePopups()
        {
            base.ClosePopups();
            CheckforPublish();

        }

        protected void Launch()
        {
            base.Launch();
            CheckforPublish();
        }

        protected void Launch(string source)
        {
            base.Launch(source);
            CheckforPublish();
        }

        protected void Launch(ListviewItem listviewItem)
        {
            base.Launch(listviewItem);
            CheckforPublish();
        }

        protected void MinimizeAllWindows()
        {
            base.MinimizeAllWindows();
            CheckforPublish();
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            CheckforPublish();
        }

        /// <summary>
        /// This Method checks for the flag present in CustomPublishDialog class if it true then desktop notification is displayed
        /// </summary>
        private void CheckforPublish()
        {
            if (CustomPublishDialog.PublishFlag == true)
            {
                SheerResponse.SetInnerHtml("lblPublishStatus", "Selected items were published successfully at " + System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToLongTimeString());
                SheerResponse.SetAttribute("lblPublishStatus", "class", "publishNotificationVisible");
                SheerResponse.Timer("HideLable", Timer);
            }
        }

        /// <summary>
        /// This Method will be called when timer is completed, to hide the notifications
        /// </summary>
        private void HideLable()
        {
            SheerResponse.SetAttribute("lblPublishStatus", "class", "publishNotificationHide");
            CustomPublishDialog.PublishFlag = false;
        }
        
        [HandleMessage("link:remove", true)]
        protected void RemoveShortcut(ClientPipelineArgs args)
        {
            base.RemoveShortcut(args);
            CheckforPublish();

        }

        protected void Run()
        {
            base.Run();
            CheckforPublish();

        }

        protected void RunExternal(string url, string parameters, string icon, string header)
        {
            base.RunExternal(url, parameters, icon, header);
            CheckforPublish();

        }

        protected void RunShortcut(string id)
        {
            base.RunShortcut(id);
            CheckforPublish();

        }

        protected void Search()
        {
        }

        protected void ShowContextMenu()
        {
            base.ShowContextMenu();
            CheckforPublish();

        }

        protected void ShowDatabaseName(string visible)
        {
            base.ShowDatabaseName(visible);
            CheckforPublish();
        }

        /// <summary>
        /// This Method is used to display recent Publish from Sitecore Taskbar (Earch Icon besides Database Changing button)
        /// </summary>
        protected void ShowPublishDetails()
        {
            Sitecore.Web.UI.HtmlControls.Menu menu = new Sitecore.Web.UI.HtmlControls.Menu();
            string name = Client.ContentDatabase.Name;
            SheerResponse.DisableOutput();
            try
            {
                if (Client.ContentDatabase.ProxiesEnabled)
                {
                    Sitecore.Web.UI.HtmlControls.MenuItem menuItem = new Sitecore.Web.UI.HtmlControls.MenuItem();
                    menu.Controls.Add(menuItem);
                    if (Context.ProxiesActive)
                    {
                        menuItem.Header = Translate.Text("Deactivate Proxies");
                        menuItem.Icon = "Core/16x16/export_d.png";
                        menuItem.Click = "DeactivateProxies()";
                    }
                    else
                    {
                        menuItem.Header = Translate.Text("Activate Proxies");
                        menuItem.Icon = "Core/16x16/export.png";
                        menuItem.Click = "ActivateProxies()";
                    }
                    menu.Controls.Add(new MenuDivider());
                }
                Sitecore.Web.UI.HtmlControls.MenuItem menuItem2 = new Sitecore.Web.UI.HtmlControls.MenuItem();
                menu.Controls.Add(menuItem2);


                menuItem2.Header = Translate.Text("View Publish History Details");
                menuItem2.Click = "ShowDesktopPublishDialog()";
                menuItem2.Icon = "Network/16x16/environment.png";
                menu.Controls.Add(new MenuDivider());

                #region Kept for future Updates
                //Sitecore.Web.UI.HtmlControls.MenuItem menuItem4 = new Sitecore.Web.UI.HtmlControls.MenuItem();
                //menu.Controls.Add(menuItem4);


                //menuItem4.Header = Translate.Text("Publish Dialog");
                //menuItem4.Click = "ShowPublishDialog()";
                //menuItem4.Icon = "Network/16x16/environment.png";
                //menu.Controls.Add(new MenuDivider());
                #endregion


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
                            Sitecore.Web.UI.HtmlControls.MenuItem PublishHandle = new Sitecore.Web.UI.HtmlControls.MenuItem();
                            menu.Controls.Add(PublishHandle);
                            PublishHandle.Header = "Publish Handle : " + lines[3];
                            PublishHandle.Icon = "apps/16x16/earth.png";

                            Sitecore.Web.UI.HtmlControls.MenuItem PublishTarget = new Sitecore.Web.UI.HtmlControls.MenuItem();
                            menu.Controls.Add(PublishTarget);
                            PublishTarget.Header = "Publish Target : " + lines[2];

                            Sitecore.Web.UI.HtmlControls.MenuItem PublishOwner = new Sitecore.Web.UI.HtmlControls.MenuItem();
                            menu.Controls.Add(PublishOwner);
                            PublishOwner.Header = "Owner : " + lines[1];

                            Sitecore.Web.UI.HtmlControls.MenuItem PublishTime = new Sitecore.Web.UI.HtmlControls.MenuItem();
                            menu.Controls.Add(PublishTime);
                            PublishTime.Header = "Date and Time : " + lines[0];

                            menu.Controls.Add(new MenuDivider());
                        }
                    }
                    catch (Exception ex)
                    {

                        Log.Error("Publish History : Sitecore Desktop Error : " + ex.Message, Context.Item);
                        Log.Error("Publish History : Sitecore Desktop Error : " + ex.StackTrace, Context.Item);

                        Sitecore.Web.UI.HtmlControls.MenuItem PublishDetails = new Sitecore.Web.UI.HtmlControls.MenuItem();
                        menu.Controls.Add(PublishDetails);
                        PublishDetails.Header = "Publish log might be empty or has been deleted";
                        PublishDetails.Icon = "Network/16x16/environment_delete.png";
                        break;
                    }
                    finally
                    {
                        skip = skip + 4;
                    }
                    
                }


            }
            catch(Exception ex)
            {
                Log.Error("Publish History : Sitecore Desktop Error : " + ex.Message, Context.Item);
                Log.Error("Publish History : Sitecore Desktop Error : " + ex.StackTrace, Context.Item);
            }
            finally
            {
                SheerResponse.EnableOutput();
            }
            SheerResponse.ShowContextMenu("DatabaseSelector", "above", menu);

        }

        protected void ShowDesktopPublishDialog()
        {
            Sitecore.Shell.Framework.Windows.RunApplication("Publish History");
            CheckforPublish();
        }

        //protected void ShowPublishDialog()
        //{
        //    //Sitecore.Shell.Framework.Windows.RunApplication("Publish History");
        //    Sitecore.Shell.Framework.Items.Publish();
        //    CheckforPublish();
        //}


        protected void ShowDatabases()
        {
            base.ShowDatabases();
            CheckforPublish();
        }

        [HandleMessage("shell:linkproperties", true)]
        protected void ShowLinkProperties(ClientPipelineArgs args)
        {
            base.ShowLinkProperties(args);
            CheckforPublish();
        }

        protected void ShowStartMenu()
        {
            base.ShowStartMenu();
            CheckforPublish();
        }

        private void BuildTray()
        {
            Item item = Client.CoreDatabase.GetItem("/sitecore/content/Applications/Desktop/Tray");
            if (item == null)
            {
                return;
            }
            foreach (Item item2 in item.Children)
            {
                Toolbutton toolbutton = new Toolbutton();
                this.Tray.Controls.Add(toolbutton);
                toolbutton.LoadFromItem(item2);
                toolbutton.IconSize = ImageDimension.id24x24;
            }
        }

        private static bool ProcessStartPage()
        {
            if (!Context.IsLoggedIn)
            {
                return true;
            }
            if (!string.IsNullOrEmpty(WebUtil.GetQueryString("mo")))
            {
                return true;
            }
            System.Web.HttpContext current = System.Web.HttpContext.Current;
            if (current == null)
            {
                return true;
            }
            System.Web.HttpCookie httpCookie = current.Request.Cookies["sitecore_starturl"];
            if (httpCookie == null)
            {
                return true;
            }
            string value = httpCookie.Value;
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            if (string.Compare(value, "/sitecore/shell/default.aspx", System.StringComparison.OrdinalIgnoreCase) != 0)
            {
                current.Response.Redirect(value);
                return false;
            }
            return true;
        }

        private Listview RefreshLinks()
        {
            this.Links.Controls.Clear();
            Listview result = Shell.RefreshLinks(this.Links);
            SheerResponse.SetInnerHtml(this.Links, this.Links.Controls[0]);
            return result;
        }


        private static Listview RefreshLinks(System.Web.UI.Control parent)
        {
            Assert.ArgumentNotNull(parent, "parent");
            DesktopLinkCollection desktopLinkCollection = new DesktopLinkCollection(Registry.GetString("/Current_User/Desktop/Links"));
            Listview listview = new Listview();
            Context.ClientPage.AddControl(parent, listview);
            listview.ID = "DesktopLinks";
            listview.Height = new System.Web.UI.WebControls.Unit(100.0, System.Web.UI.WebControls.UnitType.Percentage);
            int num = 0;
            string name = Client.ContentDatabase.Name;
            string name2 = Client.CoreDatabase.Name;
            foreach (DesktopLink current in desktopLinkCollection)
            {
                num++;
                string databaseName = current.DatabaseName;
                bool flag = true;
                string header = Shell.GetLinkHeader(current, out flag);
                bool flag2 = UIUtil.IsControlUri(current.Path);
                if (!(databaseName != name) || !(databaseName != "-") || !(databaseName != name2) || flag2)
                {
                    if (!flag2 && databaseName != "-")
                    {
                        Database database = Factory.GetDatabase(databaseName);
                        Item item = database.Items[current.Path];
                        if (item == null)
                        {
                            continue;
                        }
                        if (string.Compare(item.TemplateName, "Application Shortcut", System.StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            Item applicationFromShortcut = UIUtil.GetApplicationFromShortcut(item);
                            if (applicationFromShortcut == null)
                            {
                                continue;
                            }
                        }
                        if (!flag)
                        {
                            header = item.DisplayName;
                        }
                    }
                    ListviewItem listviewItem = new ListviewItem();
                    Context.ClientPage.AddControl(listview, listviewItem);
                    listviewItem.ID = Sitecore.Web.UI.HtmlControls.Control.GetUniqueID("L");
                    listviewItem.Header = header;
                    listviewItem.Icon = current.Icon;
                    listviewItem.ServerProperties["Link"] = current.ToString();
                    listviewItem.ServerProperties["Index"] = num - 1;
                    listviewItem.ClassPrefix = "scDesktop";
                }
            }
            return listview;
        }


        private static string GetLinkHeader(DesktopLink link, out bool containsHeaderInCurrentLanguage)
        {
            Assert.ArgumentNotNull(link, "link");
            containsHeaderInCurrentLanguage = true;
            DesktopLink.LanguageAndHeaderContainer languageAndHeaderContainer = link.LanguagesAndHeaders.FirstOrDefault((DesktopLink.LanguageAndHeaderContainer lh) => lh.Language == Context.Language.ToString());
            if (languageAndHeaderContainer == null)
            {
                containsHeaderInCurrentLanguage = false;
                languageAndHeaderContainer = link.LanguagesAndHeaders.FirstOrDefault((DesktopLink.LanguageAndHeaderContainer lh) => string.IsNullOrEmpty(lh.Language));
            }
            if (languageAndHeaderContainer != null)
            {
                return languageAndHeaderContainer.Header;
            }
            return string.Empty;
        }
    }

}