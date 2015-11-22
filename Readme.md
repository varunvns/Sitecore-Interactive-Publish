 **Want to know what is Sitecore Interactive Publish module, and why to use it?**
Check this link: [Sitecore Interactive Publish Documentation](http://varunvns.github.io/Sitecore-Interactive-Publish/) <br/>

## How to Configure the Module:

###Important Note
1. The Module supports Sitecore 8.0 and above. <br/>
2. Its good not to install the module directly in your live environment. Check it out on your development boxes and test environments first and only after you feel satisfied, move it to the live environments. <br/>

###For Basic Users:
1. Go to the Sitecore-Package Folder in Master Branch. <br/>
2. Take the latest Sitecore Package present at the location. Versions are managed well, so it should not be difficult to spot the latest package zip. The zip contains required items as well as the code files of the module. <br/> 
3. Simply install it like any other Sitecore Package. <br/>
4. Make sure you select Overwrite if asked for files and items <br/>
5. Once the package installation completes, select the checkboxes Restart the Sitecore Client as well as Sitecore Server on the last page of Installation wizard. <br/>

###For Advanced Users:
Choose the project solution based on your personal choice - with TDS or without TDS. <br/>

####Without TDS

1. Install the Sitecore package following the basic user steps. <br/>
2. Update the Publish Profile as per your environment. <br/>
3. Publish the Project and check it goes to the right destination. <br/>
4. Make the required changes and Publish again and check that your changes are available in the Sitecore Website. <br/>

####With TDS
1. Configure TDS Core Database related configurations and Sync the items with Sitecore. <br/>
2. If you have disabled the File deployment from TDS follow steps 2 thorough 4 above.

For Advanced users: If you make some changes to the module, don't forget to add it back to the module! We would be really grateful for that!

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
