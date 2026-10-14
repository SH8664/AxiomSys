using AxiomSysBL.IServices;
using AxiomSysBL.Models.Sales;
namespace AxiomSysBL.Services
{
    public class SharedService : ISharedService
    {
        public ResponseModel HandleException(Exception ex, string? msgAr, string? msgEn)
        {
            return new ResponseModel
            {
                status = false,
                data = null,
                messageAr = msgAr ?? "حدث خطأ أثناء معالجة الطلب.",
                messageEn = msgEn ?? "An error occurred while processing the request." //+ " Error: " + ex.Message
            };
        }

    }
}
