namespace MundiPagg.AppService.Shared
{
    internal static class FormatOrderData
    {
        internal static long ConvertToLong(decimal number)
        {
            var numberText = number.ToString().Replace(".", "").Replace(",", "");
            return long.Parse(numberText);
        }
    }
}
