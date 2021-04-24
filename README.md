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
