-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[items]', 'U') IS NOT NULL
DROP TABLE [dbo].[items]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Items] (
    [Id]        INT             IDENTITY (1001, 1) NOT NULL,
    [Name]      VARCHAR (20)    NULL,
    [Price]     NUMERIC (10, 2) NULL,
    [Rating]    NUMERIC (10, 2) NULL,
    [ImagePath] VARCHAR (255)   CONSTRAINT [DEFAULT_Items_ImagePath] DEFAULT ('NoImage.jpg') NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

drop view ItemsVw
go
-- this wil test for all items returned
create view ItemsVw as
	(
	select [Id], [Name], [Price], [Rating], [ImagePath]
	from Items
	)

go



-- sample queries
update items
set name = 'boo', price = 25.55, rating = 3.2
where id = 1006

update i 
set i.name = 'hoo', i.price = 25.55, i.rating = 3.2 
from items i
where i.id = 1006

update i 
set i.ImagePath = ''
from items i
where i.ImagePath IS NULL

Select id, name, price, rating, coalesce(imagepath,'', imagepath) 
from items

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
    