
# dev branch notes for MCAPS Deployment 7-2024
Added default NoImage.jpg to ddl in Database items table
```Code
CREATE TABLE [dbo].[Items] (
    [Id]        INT             IDENTITY (1001, 1) NOT NULL,
    [Name]      VARCHAR (20)    NULL,
    [Price]     NUMERIC (10, 2) NULL,
    [Rating]    NUMERIC (10, 2) NULL,
    [ImagePath] VARCHAR (255)   CONSTRAINT [DEFAULT_Items_ImagePath] DEFAULT ('NoImage.jpg') NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
```

### App Service process
- create new app services and plan - Free plan should work
    - add AppInsights
- To let API AppService access KeyVault
    - turn on SMI under Settings, Identity
    - Then assign that SMI, Key Vault Secrets User - RBAC Permissions
- to let API App Service access SQL
    - use the SMI enabled above
    - log into SQL as an AAD/EntraID user
    - create user and assign permissions
        - see script in script folder 

### Code changes for move to production app service 
- In the Frontend
    - update appsettings and change to production url of API
    - We were using a hard coded url in Program.cs.  This was needed during development but will break production if left in.
    - Technically, this should not be done in code at all, but rather via the launchSettings.json file in Properties folder.
- In the API
    - set the RunAsParm to SMI
    - update SQL connection string to use production
    - We add a Password paramter to the SQL connection string during development.  This will break production which uses SMI.  This line of code needs to be commented out.

### Additional notes

- During the process of deploying and verifying success, it was very helpful to use the App Services, Developet Tools, Advanced Tools - specifically Log stream and site wwwroot.

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
