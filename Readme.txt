How to set up the project - New
=========================
1. Get latest version of project.
2. delete the dbo.Transaction table from your database.
3. run the sql script CreateNWBA_additional.sql (I will included it in the main script later).
4. run the sql script sample_data.sql to insert sample data into transaction type and transaction category tables.
5. change the connection string in web.config if it has changed.
6. compile and run the project.
7. I am here to help with the set up (errors, etc) to get it working.

How to set up the project
=========================
1. Get latest version of WDTAssignment2NWBA from team service.
2. In the Web.config, depending on how you set up your database, the Data Source for your connection string should point to
   your database. For example, mine is localhost.
   <connectionStrings>
    <!--
    <add name="DefaultConnection" connectionString="Data Source=(LocalDb)\v11.0;Initial Catalog=aspnet-WDTAssignment2NWBA-20130927174151;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|\aspnet-WDTAssignment2NWBA-20130927174151.mdf" providerName="System.Data.SqlClient" />
    -->
    <clear />
    <add name="SqlRoleManagerConnection" providerName="System.Data.SqlClient" connectionString="Data Source=localhost;Initial Catalog=WDTAssignment2NWBA;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False" />
    <add name="WDTAssignment2NWBAEntities" connectionString="metadata=res://*/DataAccessLayer.NWBA.csdl|res://*/DataAccessLayer.NWBA.ssdl|res://*/DataAccessLayer.NWBA.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=localhost;initial catalog=WDTAssignment2NWBA;integrated security=True;connect timeout=15;encrypt=False;trustservercertificate=False;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
  </connectionStrings>
3. In the AccountController, uncomment the filter [AllowAnonymous] and comment out the filter [Authorize(Roles = "Admin")] for the Register method 
   like below:

   // GET: /Account/Register

   [AllowAnonymous]
   //[Authorize(Roles = "Admin")]
   public ActionResult Register()
   {
		...
   }

   //
   // POST: /Account/Register

   [HttpPost]
   [AllowAnonymous]
   //[Authorize(Roles = "Admin")]
   [ValidateAntiForgeryToken]
   public ActionResult Register(RegisterModel model)
   {
		...        
   }

4. Delete all the current NWBA tables from your WDTAssignment2NWBA database.
5. If you haven't, then run the aspnet_regsql.exe tool (as shown in the lecture) to install the asp.net default
   membership provider tables into the WDTAssignment2NWBA database. (If you have done it, then its fine.)
6. Open the CreateNWBA_modified2.sql under the Sql directory in management studio and run it.
7. Build and run the application.
8. When the application runs successfully, type http://localhost:<your port number>/Register in the browser and add some users.
9. Enter some roles in the dbo.webpages_Roles table.
10. Assign users from the dbo.UserProfile to roles in the dbo.webpages_UsersInRoles table.
11. If everything works fine, then go to step 3 and restore back the [Authorize(Roles = "Admin")] filter. (i.e., comment out
    the [AllowAnonymous] filter and uncomment the [Authorize(Roles = "Admin")] filter.
12. You should be running the application normally now.


Information on the Entity Framework created for NWBA
====================================================

Connection string = WDTAssignment2NWBAEntities

Model Namespace = WDTAssignment2NWBAModel