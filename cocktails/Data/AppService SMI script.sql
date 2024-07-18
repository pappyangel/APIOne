-- set db to the application database
USE dbItems

-- SMI name is the name of the App Service
CREATE USER [Cocktail-API] FROM EXTERNAL PROVIDER

-- Set the minimal permissions for the App Service
ALTER ROLE db_datareader ADD MEMBER [Cocktail-API]
ALTER ROLE db_datawriter ADD MEMBER [Cocktail-API]

-- test the above
SELECT r.name AS role_principal_name,
    m.name AS member_principal_name
FROM sys.database_role_members rm
    JOIN sys.database_principals r ON rm.role_principal_id = r.principal_id
    JOIN sys.database_principals m ON rm.member_principal_id = m.principal_id
WHERE m.name = 'Cocktail-API' -- Replace 'YourUserName' with the actual user name