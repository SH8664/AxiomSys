using AxiomSysBL.IServices;
using AxiomSysBL.Models.Sales;
namespace AxiomSysBL.Services
{
    public class SharedService : ISharedService
    {
        public ResponseModel HandleException(Exception ex)
        {
            return new ResponseModel
            {
                status = false,
                data = null,
                messageAr = "حدث خطأ أثناء معالجة الطلب.",
                messageEn = "An error occurred while processing the request."
            };
        }

    }
}
