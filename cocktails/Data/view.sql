drop view ItemsVw

-- this wil test for all items returned
create view ItemsVw as
	(
	select [Id], [Name], [Price], [Rating], [ImagePath]
	from Items
	)

-- this will test for null data returned
create view ItemsVw as
(
select [Id], [Name], [Price], [Rating], [ImagePath]
from Items
where 1=0
)

Select * from ItemsVw
