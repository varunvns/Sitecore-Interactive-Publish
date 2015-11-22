# Challenge:
When our Sitecore Instance is live, we have many content editor users, page editor users and publishers working day in and day out to keep the content up to date as well as making the new content available as soon as possible. <br />
Now, lets consider a case when there are multiple users publishing simultaneously. Obviously, as Publishing is a pipeline, one user's publishing would be going on while the rest of them are waiting for their turn to come. They get a queued message in the publishing dialog. Now if the users are say in one geographical location, still thing is OK, they may talk to each other and perform publishing but think of a case where the users publishing are managing two different websites in two different geographical locations, but on the same Sitecore instance. <br />
Now, as the users have other critical tasks too, they do not want to sit and wait for their publishing to be started and get completed, but rather close the publish dialog and continue with their work and somewhere, get the information that their publishing is completed.


***
# Solution

The solution of all the above cases in different combinations is just one - Sitecore Interactive Publish.

So, when this Sitecore module is installed, we get the following functionalities:<br />
1. Information of why the publishing is queued, and where does my publishing stand in the queue<br />
2. If the user closes the publishing dialog, still he gets the notification on Desktop.<br />
3. In case the user is so busy he/she misses that notification, still nothing to worry about. Sitecore Interactive Publish has added a new chunk of Publish History in Sitecore Desktop as well as Content Editor, where the user can go anytime, and quickly check whether his/her publishing is completed or not.


***
# Quick Tour

When publishing is going on, only by one user in Sitecore, the Publish Dialog looks same as it normally looks, no change at all. It looks like below:

![Normal Publishing Scenario - Single User Publishing](https://varunvns.files.wordpress.com/2015/11/normal-publishing-dialog-if-only-one-publishing-job.png)

Now, the moment a second user publishes items in Sitecore, when a Publish is in progress, this is what seen by the user whose publishing is going on.

![A table showing Publishing is running for him, and queued for the second user](https://varunvns.files.wordpress.com/2015/11/publishing-running-for-current-user-with-information-if-there-is-other-publishing-in-queued.png)

And this is what the second users sees on his/her screen:

![Publishing Queued for the second user.](https://varunvns.files.wordpress.com/2015/11/publishing-queued-for-other-user-with-information.png)

From the above screenshots, we can see that the Publish dialog by default shows the Publish Job selected user wise - as well as the Owner and message is available so he/she can get a clear picture of whether his/her items are queued or in publishing. ALso, the status keeps on updating regularly. So the user gets a clear picture of what's going on in the system.

Next, say the user is so frustrated, that he/she closes the Publish Dialog and started working on other things in Sitecore.
In that case, the user can check for the following notification in Sitecore Desktop:

![Notification in Desktop](https://varunvns.files.wordpress.com/2015/11/notification-on-desktop-that-items-got-published.png)

Next, lets consider a case, the user is extremely busy and missed the notification. Oh, nevermind, we have a simple way of showing him/her that their publishing was done and successful. We call that part of Sitecore Interactive Publish, as the Publish History. 
The Publish History, can be easily accessed from besides the Switch to another database icon on Sitecore Desktop.

![Recent Publish History in Sitecore Desktop](https://varunvns.files.wordpress.com/2015/11/sitecore-desktop-additional-icon-for-publish-history-besides-database-switch.png)

This shows recent history and the number of items shown here, can be controlled form the Configuration file - Sitecore.Interactive.Publish.config
The Recent Publish History looks like this in the Sitecore Desktop:

![Recent Publish History in Sitecore Desktop](https://varunvns.files.wordpress.com/2015/11/recent-publishing-history-in-sitecore-desktop.png)

The Recent Publish History is also available from the Content Editor in the Publish Tab

![Recent Publish History from Content Editor](https://varunvns.files.wordpress.com/2015/11/recent-publish-history-in-content-editor.png)

Next, if we want to see the complete Publish History, we have a detail page to show that too. Also, its accesible from 3 places:

* Sitecore Desktop:

![Publish History Detail from Sitecore Desktop](https://varunvns.files.wordpress.com/2015/11/recent-publishing-history-in-sitecore-desktop-opens-detail-page.png)

* Sitecore Start Menu

![Publish History Detail from Sitecore Start Menu](https://varunvns.files.wordpress.com/2015/11/publish-history-detail-page-from-start-menu.png)

* Sitecore Content Editor

![Publish History Detail from Sitecore Content Editor](https://varunvns.files.wordpress.com/2015/11/publish-history-detail-page-from-content-editor.png)

And this is how the Publish History Detail looks like

![Publish History Detail Page](https://varunvns.files.wordpress.com/2015/11/publish-history-details.png)


***
## Advantages of the Sitecore Module 
### From the Business User's Perspective:
* Makes it easy for the user to understand why his/her publishing is queued.
* Recent Publish History is easily accessible from Sitecore Desktop as well as Content Editor and gives the required information of what publishing a user did.
* In case the user is interested in knowing more about the publishing, he/she can go the Publishing Detail Page, from Sitecore Desktop, Sitecore Start Menu or from the Content Editor and check it out.

### From the Developer's Perspective:
* Quick and Easy installation as a Sitecore Package.
* No modifications are required, you can directly install and enjoy the module!
* In case you want to check the code we have the projects (with and without TDS) available on Github. [https://github.com/varunvns/Sitecore-Interactive-Publish](Sitecore-Interactive-Publish)
* In case you like the TDS way, we have a TDS Project for you, take those items in your project and sync them with your instance and enjoy the module!


## Drawbacks of this Sitecore Module
* As the module contains an assembly and a config file, it will recycle the application pool of Sitecore Application, making it to restart.
* Sitecore Interactive Publish cannot be used, if you have a Sitecore Publishing Instance configured (i.e. say, if you have an additional Sitecore instance configured, which takes care of Publishing Jobs for your Sitecore environment, then, in that case, this module wont be useful!)
* The Publish History shows history of Publishing of only the publishing done after this module is installed. Also, if the PublishHistory.txt file is deleted from the data folder, the publish history will not be available (We have added a new txt file to the logs folder, and adding information of publishing jobs into it, which serves as a source of Publish History for the users. Hence, the publishes done before installing this module will not be available in the Publish History. Also, this file cannot be deleted by Cleanup Agent, but if someone deletes this file, the Publish History will be lost!)


***
## Future Enhancements
* We are thinking add a new functionality to the Publish dialog that of Hide it. So when the user hides it, and then with a shortcut of Show Publish Dialog - shows the same publish dialog back, showing the current status of the Publish Job.
* We are thinking of making use of the History Tables and taking some information and showing it in the Publish History.
* We are thinking including the paths of items published and whether individually or with Subitems would be a useful information, we have still not explored this, but we would do it in sometime moving forward.


*** 
## Contribute to the Sitecore Module
Well, if you wish to contribute to this Sitecore Module, then you need to take the following steps: <br/>
1. Fork the Development Branch of the Module on Github. <br/>
2. Add the code that you wish to.<br/>
3. Create a Pull request, so that we can understand that you have added some functionality.<br/>
4. You also need to update the readme and tell us why you added it and which files were updated.<br/>
5. We will test the code on our side and will include it in the main dev branch and then the master branch if we don't find anything malicious. Also, its not like if you send a code we will add it, if it doesn't go with the concept or if it contains some malicious code, we might reject the code too. So sorry if that happens with you.