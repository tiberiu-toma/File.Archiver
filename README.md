## File.Archiver

**File.Archiver** is a self-didactic project. It's use is doing lightweigth file archival for user uploaded files.

---
The solution has the following parts:
- **Archiver.MVC** is a web app which lets the user upload a file, and download the resulting archive. Archiver.MVC calls the APIs of the Archiver.WebService.
- **Archiver.WebService** is a console app that is self-hosted using OWIN. It is responsible for the actual file archival, and it stores each archive operation to a local DB, using Dapper.
- **ArchiverDB** is a SQL server DB project which defines the only table in the local database. It also contains a publish profile for publishing the DB to MSSQLLocalDB.
---
In order to run this solution locally, the following steps must be followed:
1. Publish the **ArchiverDB** database using the **ArchiverDB.publish.xml** publish profile.
2. Right-click on the **Archiver.Application** solution and use **Multiple Startup Projects**. Set the action as **Start** for **Archiver.MVC** and **Archiver.WebService**.
---
Known issues and future improvements:
- Find a way to upload large files, since at the moment this feature does not work
- At the moment archives get stored at a local address. Figure out a way to not do this, maybe by saving the archived bytes in the database.
- Since Archiver.WebService is a console app, when running it the console remains open. Replace it with a self-hosted windows service instead.
- Add logging and better exception handling.
- Find a way to remove the files from Content and Scripts, as they shouldn't need to be checked into the repo.
