using AxiomSysBL.Models.Sales;

namespace AxiomSysBL.IServices.ISalesServices
{
    public interface ISaDeliveryTermService
    {
        public List<SaDeliveryTermModel> GetSaDeliveryTerms();
        public ResponseModel GetSaDeliveryTermByCode(short code);
        public ResponseModel AddSaDeliveryTerm(SaDeliveryTermModel model);
        public ResponseModel UpdateSaDeliveryTerm(SaDeliveryTermModel model);

        public ResponseModel DeleteSaDeliveryTerm(short code);
    }
}
