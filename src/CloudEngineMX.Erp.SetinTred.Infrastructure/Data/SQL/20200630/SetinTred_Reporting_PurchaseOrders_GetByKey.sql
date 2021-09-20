-- =============================================
-- Author:      Lopez Jaime
-- Create date: 30/06/2020
-- Description: Procedure for get Purchase Order
--              by key.
-- =============================================
CREATE PROCEDURE Reporting_PurchaseOrders_GetByKey
	@OrderKey NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		po.CreationDate,
		po.PurchaseOrderCode,
		po.RequisitionCode,
		po.Applicant,
		CONCAT(COALESCE(u.Title, ''), ' ', COALESCE(u.FirstName, ''), ' ', COALESCE(u.LastName , '')) AS ManagedBy,
		s.FiscalName,
		po.[Condition],
		po.SendTo,
		po.RequiresVat,
		po.TotalInLetters,
		po.DigitalSignature,
		LEFT(r.[Application], 60) AS [Application],
		po.Remarks
	FROM PurchaseOrders po
	INNER JOIN Suppliers s ON po.SupplierId = s.Id
	INNER JOIN UserCores u ON po.UserId = u.Id
	INNER JOIN Requisitions r ON po.RequisitionCode = r.RequisitionCode
	WHERE po.[Key] = @OrderKey;

END

GO
