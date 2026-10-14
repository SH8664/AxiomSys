using AxiomSysBL.Models.Sales;

namespace AxiomSysBL.IServices
{
    public interface ISharedService
    {
        ResponseModel HandleException(Exception ex, string? msgAr, string? msgEn);
    }
}
