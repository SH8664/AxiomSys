namespace AxiomSysDL.Models.SupplyChain
{
    public class SaDeliveryTerm
    {
        public short Code { get; set; }
        public string SName { get; set; } = "";
        public string BName { get; set; } = "";
        public short? Days { get; set; }
        public byte? ActiveFlag { get; set; }
        public int EntryUser { get; set; }
        public DateTime EntryDate { get; set; }
        public int? ChangeUser { get; set; }
        public DateTime? ChangeDate { get; set; }
    }
}
