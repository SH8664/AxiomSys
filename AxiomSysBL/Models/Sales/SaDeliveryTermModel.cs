namespace AxiomSysBL.Models.Sales
{
    public class SaDeliveryTermModel
    {
        public short Code { get; set; }
        public string SName { get; set; } = "";
        public string BName { get; set; } = "";
        public short? Days { get; set; }
        public bool Active { get; set; }

    }
}
