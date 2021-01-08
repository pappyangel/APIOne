--create -- Create a new table called '[TableName]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[items]', 'U') IS NOT NULL
DROP TABLE [dbo].[items]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Items]
(
      [Id] INT identity (1001,1) NOT NULL PRIMARY KEY -- Primary Key column
    , [Name]  VARCHAR(20) 
    , [Price] numeric(10,2)
    , Rating  numeric(10,2)
    -- Specify more columns here
);
GO



select *
from items

insert into items
    (Name, Price ,Rating)
values

     ('JimTini' , 8.50  , 4.5)
    ,('Bloody'  , 7.75  , 3.0)
    ,('Scotch'  , 7.75  , 5.0)
    ,('Beer'    , 5.75  , 3.0)
    ,('Wine'    , 10.75 , 3.5)
    
