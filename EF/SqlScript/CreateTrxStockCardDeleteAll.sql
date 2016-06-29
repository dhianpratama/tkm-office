
create procedure TrxStockCardDeleteAll
(
	@ParamInstitutionId			bigint
)
as
begin
	delete	TrxStockCard
	where	InstitutionId = @ParamInstitutionId
end
