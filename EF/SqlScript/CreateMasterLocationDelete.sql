
create procedure MasterLocationDelete
(
	@ParamLocationId		bigint
)
as
begin
	declare	@current_node	hierarchyid
  
	select	@current_node = Node
	from	dbo.MasterLocation
	where	LocationId = @ParamLocationId

	delete	dbo.MasterLocation
	where	Node.IsDescendantOf(@current_node) = 1
end
