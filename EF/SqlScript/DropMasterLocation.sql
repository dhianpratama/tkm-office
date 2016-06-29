if exists (	select * 
				from	information_schema.tables 
				where	table_schema = 'dbo' 
                and		table_name = 'MasterLocation')
begin
	drop table dbo.MasterLocation
end
