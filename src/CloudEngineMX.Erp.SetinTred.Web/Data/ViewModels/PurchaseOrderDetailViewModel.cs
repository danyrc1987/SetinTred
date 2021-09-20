namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    public class PurchaseOrderDetailViewModel
    {
        public string PurchaseOrderDetailKey { get; set; }
        public int Consecutive { get; set; }
        public int Quantity { get; set; }
        public int? OriginalQuantity { get; set; }
        public string Unit { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsApproved { get; set; }
        public bool IsUrgent { get; set; }

        public decimal TotalLine => Quantity * UnitPrice;

        public string Priority => IsUrgent == true ? "Urgente" : "Normal";

        public string ToMail =>
            $"Cantidad: {Quantity} / Unidad: {Unit} / Descripcion: {Description} / Precio: {TotalLine:C2} / Prioridad: {Priority}";
    }
}
