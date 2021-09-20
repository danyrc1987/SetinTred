namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    public class RequisitionDetailItemViewModel
    {
        public string DetailKey { get; set; }
        public int Consecutive { get; set; }
        public int Quantity { get; set; }
        public string MeasurementUnit { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public string Review { get; set; }
        public bool IsDispatched { get; set; }
        public bool IsApproved { get; set; }
        public bool IsUrgent { get; set; }
        public string ReasonForRejection { get; set; }
        public int? QuantityDispatched { get; set; }
        public int? QuantityToBuy { get; set; }

        public string Priority => IsUrgent == true ? "Urgente" : "Normal";

        public string ToMail =>
            $"Cantidad:{Quantity}, {Description}, Especificación: {Specification}, Revisión: {Review}";

        public string ToMailPurchase =>
            $"Cantidad:{QuantityToBuy}, {Description}, Especificación: {Specification}, Revisión: {Review}, Prioridad: {Priority}";

        public string ToMailPurchaseCancel =>
            $"Cantidad:{QuantityToBuy}, {Description}, Especificación: {Specification}, Revisión: {Review}, Prioridad: {Priority}, Motivo del Rechazo: {ReasonForRejection}";
    }
}
