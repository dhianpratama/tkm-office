create procedure MasterLocationUpdate
(
	@ParamLocationId			bigint,
	@ParamLocationCode			nvarchar(max),
	@ParamLocationName			nvarchar(max),
	@ParamLocationTypeId		bigint,
	@ParamCreatedBy				nvarchar(max),
	@ParamCreatedDate			datetime
)
as
begin
	update	dbo.MasterLocation
	set		LocationCode = @ParamLocationCode,
			LocationName = @ParamLocationName,
			LocationTypeId = @ParamLocationId,
			LastUpdatedBy = @ParamCreatedBy,
			LastUpdatedDate = @ParamCreatedDate
	where	LocationId = @ParamLocationId
end
