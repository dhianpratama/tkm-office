create procedure MasterLocationFetchAllForDropdownList
(
	@ParamInstitutionId		bigint
)
as
begin
	select	LocationId ,
	        InstitutionId ,
	        LocationCode ,
	        LocationName ,
			replicate('--', convert(int, NodeLevel - 1)) + ' [' + LocationCode +'] ' + LocationName 'Description',
	        LocationTypeId ,
	        Node.ToString() 'Node' ,
	        convert(int, NodeLevel) 'NodeLevel',
	        IsActive ,
	        CreateBy ,
	        CreatedDate ,
	        LastUpdatedBy ,
	        LastUpdatedDate
	from	dbo.MasterLocation
	where	InstitutionId = @ParamInstitutionId
	and		IsActive = 1
	order by Node
end