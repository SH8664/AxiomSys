namespace AxiomSysPortal.Models
{

    public class ResponseModel<T>
    {
        public bool status { get; set; }
        public T? data { get; set; }
        public string messageAr { get; set; } = "";
        public string messageEn { get; set; } = "";
    }
}
