using AxiomSysBL.Models.Sales;

namespace AxiomSysBL.IServices.ISalesServices
{
    public interface ISaDeliveryTermService
    {
        public ResponseModel GetSaDeliveryTerms();
        public ResponseModel GetSaDeliveryTermByCode(short code);
        public ResponseModel AddSaDeliveryTerm(SaDeliveryTermModel model, string userId);
        public ResponseModel UpdateSaDeliveryTerm(SaDeliveryTermModel model, string userId);

        public ResponseModel DeleteSaDeliveryTerm(short code);
    }
}
