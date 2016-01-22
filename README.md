# DirectoryListing

An ASP.NET MVC Directory Listing application. Replace the boring default directory listings from IIS with a completely customizable version.

# Setup

Use a new or existing Web Application in IIS set to use .NET Framework 4.0 and publish the project to that folder. Update the Web.config and change the `RootDirectory` value in the app settings to the folder which has the files you wish to show in the directory listing. Browse to the site and browse the directories.

# Customization

The following files can be updated to change the look of the directory listing before publishing:

- `Views/Shared/_Layout.cshtml`
- `Views/Home/Index.cshtml`
- `_static/Site.scss`
- `_static/logo-short.png`
