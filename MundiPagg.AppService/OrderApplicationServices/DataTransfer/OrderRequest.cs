using MundiPagg.AppService.OrderApplicationServices.ValueObjects;

namespace MundiPagg.AppService.DataTransfer
{
    public class OrderRequest
    {
        public string Order { get; set; }
        public SumulateCardValueEnum EventSimulate { get; set; }
        public OrderContentFormatEnum OrderContent { get; set; }
    }
}
