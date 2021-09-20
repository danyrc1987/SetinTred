namespace CloudEngineMX.Erp.SetinTred.Web.Data
{
    public static class QuoteSerie
    {
        public static string GetQuoteSeries(string normativeData)
        {
            var series = string.Empty;

            return normativeData switch
            {
                "API 6A" => series = "SERIE D",
                "API 7-1" => series = "SERIE C",
                "API 16A" => series = "SERIE E",
                "API 16C" => series = "SERIE F",
                "Varios" => series = "SERIE A",
                _ => series
            };
        }
    }
}
