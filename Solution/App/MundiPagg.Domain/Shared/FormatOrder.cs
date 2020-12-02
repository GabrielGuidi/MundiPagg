using System;
using System.Globalization;

namespace MundiPagg.Domain.Shared
{
    public static class FormatOrder
    {
        public static string ToStringDate(DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.GetCultureInfo("pt-BR"));
        }
    }
}
