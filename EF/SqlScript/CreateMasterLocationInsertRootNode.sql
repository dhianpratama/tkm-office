create procedure MasterLocationInsertRootNode
(
	@ParamInstitutionId			bigint,
	@ParamLocationCode			nvarchar(max),
	@ParamLocationName			nvarchar(max),
	@ParamLocationTypeId		bigint,
	@ParamCreatedBy				nvarchar(max),
	@ParamCreatedDate			datetime
)
as
begin
		declare	@parent_node				hierarchyid,
				@max_current_child_node		hierarchyid,
				@new_node					hierarchyid,
				@root_node					hierarchyid
	set transaction isolation level serializable
	begin transaction
		select	@parent_node = hierarchyid::GetRoot()

		select	@max_current_child_node = max(Node) 
		from	dbo.MasterLocation
		where	NodeLevel = 1

		insert into dbo.MasterLocation
		(	InstitutionId ,
			LocationCode ,
			LocationName ,
			LocationTypeId,
			Node ,
			IsActive ,
			CreateBy ,
			CreatedDate ,
			LastUpdatedBy ,
			LastUpdatedDate
		)
		values  (	@ParamInstitutionId, -- InstitutionId - bigint
					@ParamLocationCode, -- LocationCode - nvarchar(max)
					@ParamLocationName, -- LocationName - nvarchar(max)
					@ParamLocationTypeId,
					@parent_node.GetDescendant(@max_current_child_node, NULL) , -- Node - hierarchyid
					1 , -- IsActive - bit
					@ParamCreatedBy, -- CreateBy - nvarchar(max)
					@ParamCreatedDate, -- CreatedDate - datetime
					@ParamCreatedBy, -- LastUpdatedBy - nvarchar(max)
					@ParamCreatedDate -- LastUpdatedDate - datetime
				)  
  
	commit
end