if not exists (	select * 
				from	information_schema.tables 
				where	table_schema = 'dbo' 
                and		table_name = 'MasterLocation')
begin
	create table MasterLocation
	(
		LocationId				bigint primary key identity,
		InstitutionId			bigint foreign key references dbo.MasterInstitution (InstitutionId),
		LocationCode			nvarchar(max),
		LocationName			nvarchar(max),
		LocationTypeId			bigint foreign key references dbo.MasterLocationType(LocationTypeId),
		Node					hierarchyid,
		NodeLevel				as Node.GetLevel(),
		IsActive				bit default 1,
		CreateBy				nvarchar(max) null,
		CreatedDate				datetime null,
		LastUpdatedBy			nvarchar(max) null,
		LastUpdatedDate			datetime null
	)
end
