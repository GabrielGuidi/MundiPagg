using System.ComponentModel;

namespace MundiPagg.AppService.OrderApplicationServices.ValueObjects
{
    public enum SumulateCardValueEnum
    {
        [Description("4000000000000010")]
        Success = 1,

        [Description("4000000000000028")]
        Fail = 2,

        [Description("4000000000000036")]
        ProcessingThanSuccess = 3,

        [Description("4000000000000044")]
        ProcessingThanFail = 4,

        [Description("4000000000000077")]
        ErrorInSecondOperation = 5,

        [Description("4000000000000051")]
        SuccessThanPurchaserFailure = 6,

        [Description("4000000000000051")]
        ProcessingThanCanceled = 7,

        [Description("0123456789123456")]
        Other = 8
    }
}
