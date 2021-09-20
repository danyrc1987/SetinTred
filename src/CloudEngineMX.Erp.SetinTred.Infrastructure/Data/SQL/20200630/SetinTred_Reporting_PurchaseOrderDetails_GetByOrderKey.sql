-- =============================================
-- Author:      Lopez Jaime
-- Create date: 30/06/2020
-- Description: Procedure for get Purchase Order
--              Details by Oorder key.
-- =============================================
CREATE PROCEDURE Reporting_PurchaseOrderDetails_GetByOrderKey
	@OrderKey NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		pod.Consecutive,
		pod.Quantity,
		pod.Unit,
		pod.PartNumber,
		pod.Description,
		pod.UnitPrice,
		pod.UnitPrice * pod.Quantity AS Amount
	FROM PurchaseOrderDetails pod
	INNER JOIN PurchaseOrders po ON pod.PurchaseOrderId = po.Id
	WHERE po.[Key] = @OrderKey
	ORDER BY pod.Consecutive;

END

GO

