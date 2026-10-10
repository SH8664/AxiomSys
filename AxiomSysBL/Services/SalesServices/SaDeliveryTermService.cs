using AxiomSysBL.IServices;
using AxiomSysBL.IServices.ISalesServices;
using AxiomSysBL.Models.Sales;
using AxiomSysDL.Models.SupplyChain;
using AxiomSysDL.Repositories;

namespace AxiomSysBL.Services.SalesServices
{
    public class SaDeliveryTermService : ISaDeliveryTermService
    {
        IUnitOfWork<SupplyChainContext> _uowSC;
        ISharedService _sharedService;

        public SaDeliveryTermService(IUnitOfWork<SupplyChainContext> uowSC, ISharedService sharedService)
        {
            _uowSC = uowSC;
            _sharedService = sharedService;
        }

        public List<SaDeliveryTermModel> GetSaDeliveryTerms()
        {
            var saDeliveries = _uowSC.GetRepository<SaDeliveryTerm>().GetAll().ToList();
            return saDeliveries.Select(x => new SaDeliveryTermModel
            {
                Code = x.Code,
                SName = x.SName,
                BName = x.BName,
                Days = x.Days,
                Active = x.ActiveFlag != 0
            }).ToList();
        }

        public ResponseModel GetSaDeliveryTermByCode(short code)
        {
            try
            {
                var saDelivery = _uowSC.GetRepository<SaDeliveryTerm>().Find(code);
                if (saDelivery == null)
                    return new ResponseModel
                    {
                        status = false,
                        messageAr = "لم يتم العثور على شرط التسليم",
                        messageEn = "Delivery term not found"
                    };
                else
                    return new ResponseModel
                    {
                        status = true,
                        data = new SaDeliveryTermModel
                        {
                            Code = saDelivery.Code,
                            SName = saDelivery.SName,
                            BName = saDelivery.BName,
                            Days = saDelivery.Days,
                            Active = saDelivery.ActiveFlag != 0
                        },
                        messageAr = "تم العثور على شرط التسليم",
                        messageEn = "Delivery term found"
                    };
            }
            catch (Exception ex)
            {
                return _sharedService.HandleException(ex);
            }
        }

        public ResponseModel AddSaDeliveryTerm(SaDeliveryTermModel model)
        {
            try
            {
                var saDelivery = _uowSC.GetRepository<SaDeliveryTerm>().Find(model.Code);
                if (saDelivery != null)
                    return new ResponseModel
                    {
                        status = false,
                        messageAr = "شرط التسليم موجود بالفعل",
                        messageEn = "Delivery term already exists"
                    };
                else
                {
                    var newSaDelivery = new SaDeliveryTerm
                    {
                        Code = model.Code,
                        SName = model.SName,
                        BName = model.BName,
                        Days = model.Days,
                        ActiveFlag = model.Active == true ? (byte)1 : (byte)0,
                    };
                    _uowSC.GetRepository<SaDeliveryTerm>().Add(newSaDelivery);
                    _uowSC.Commit();
                    return new ResponseModel
                    {
                        status = true,
                        messageAr = "تم إضافة شرط التسليم بنجاح",
                        messageEn = "Delivery term added successfully"
                    };
                }
            }
            catch (Exception ex)
            {
                return _sharedService.HandleException(ex);
            }
        }

        public ResponseModel UpdateSaDeliveryTerm(SaDeliveryTermModel model)
        {
            try
            {
                var saDelivery = _uowSC.GetRepository<SaDeliveryTerm>().Find(model.Code);
                if (saDelivery == null)
                    return new ResponseModel
                    {
                        status = false,
                        messageAr = "لم يتم العثور على شرط التسليم",
                        messageEn = "Delivery term not found"
                    };


                saDelivery.Code = model.Code;
                saDelivery.BName = model.BName ?? saDelivery.BName;
                saDelivery.SName = model.SName ?? saDelivery.SName;
                saDelivery.ActiveFlag = model.Active == true ? (byte)1 : (byte)0;
                saDelivery.ChangeDate = DateTime.Now;
                saDelivery.ChangeUser = 1; // TODO :: Replace with actual user ID
                saDelivery.Days = model.Days ?? saDelivery.Days;

                _uowSC.GetRepository<SaDeliveryTerm>().Update(saDelivery);
                _uowSC.Commit();
                return new ResponseModel
                {
                    status = true,
                    messageAr = "تم تعديل شرط التسليم بنجاح",
                    messageEn = "Delivery term updated successfully"
                };

            }
            catch (Exception ex)
            {
                return _sharedService.HandleException(ex);
            }
        }

        public ResponseModel DeleteSaDeliveryTerm(short code)
        {
            try
            {
                var saDelivery = _uowSC.GetRepository<SaDeliveryTerm>().Find(code);
                if (saDelivery == null)
                    return new ResponseModel
                    {
                        status = false,
                        messageAr = "لم يتم العثور على شرط التسليم",
                        messageEn = "Delivery term not found"
                    };

                _uowSC.GetRepository<SaDeliveryTerm>().Remove(saDelivery);
                _uowSC.Commit();
                return new ResponseModel
                {
                    status = true,
                    messageAr = "تم حذف شرط التسليم بنجاح",
                    messageEn = "Delivery term deleted successfully"
                };

            }
            catch (Exception ex)
            {
                return _sharedService.HandleException(ex);
            }
        }
    }
}
