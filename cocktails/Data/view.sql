drop view ItemsVw

create view ItemsVw as
	(
	select [Id], [Name], [Price], [Rating], [ImagePath]
	from Items
	)


create view ItemsVw as
(
select [Id], [Name], [Price], [Rating], [ImagePath]
from Items
where 1=0
)

Select * from ItemsVw