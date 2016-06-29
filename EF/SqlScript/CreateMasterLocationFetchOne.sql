
create procedure MasterLocationFetchOne
(
	@ParamLocationId		bigint
)
as
begin
	select	ml.LocationId,
			ml.InstitutionId,
			ml.LocationCode,
			ml.LocationName,
			ml.LocationTypeId,
			mlt.LocationTypeCode,
			mlt.LocationTypeDescription,
			ml.Node.ToString() 'Node',
			convert(int, ml.NodeLevel) 'NodeLevel',
			(select LocationId from dbo.MasterLocation where Node = ml.Node.GetAncestor(1)) 'ParentLocationId',
			ml.IsActive,
			ml.CreateBy,
			ml.CreatedDate,
			ml.LastUpdatedBy,
			ml.LastUpdatedDate
	from	dbo.MasterLocation ml
			inner join dbo.MasterLocationType mlt on (ml.LocationTypeId = mlt.LocationTypeId)
	where	LocationId = @ParamLocationId
end