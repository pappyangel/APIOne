
# Release 3.0

Moved to Managed Identity vs. using SQL Server auth with a login and password
with passowrd being stored as a secret in KeyVault

Updated System.Data.SQLClient to Microsoft.Data.SQLClient to allow Authentication keyword in Connection String
Still reference System.Data for some parts of SQL processing

Built support for running in development mode by changing RunAs parameter


# APIOne

This is a repo with multiple apps and solutions.  it is a showcase for many app dev techniques.

### API App

API App the responds to CRUD HTTP commands
Writes data to SQL Azure
Writes data to File system via a custom filedb class
Calls to SQL Azure are using Async 

### Razor Web App

Basic web app to call API
Shows all records and performs insert/update/delete operations
Includes ability to add and display images
Uses dependency injection techniques to perform class object serilization 

### Codespaces

Uses a custom .devcontainer to add specific extensions

### GitHub Actions

Two Action scripts to look for changes in the specific projects above and trigger deployment to App Service if Pull Request is approved
