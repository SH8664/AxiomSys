using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AxiomSysDL.Models.SupplyChain
{
    [Table("SA_DELIVERY_TERMS")]
    public class SaDeliveryTerm
    {
        [Key]
        [Column("CODE")]
        public short Code { get; set; }

        [Column("S_NAME")]
        public string SName { get; set; } = "";

        [Column("B_NAME")]
        public string BName { get; set; } = "";

        [Column("DAYS")]
        public short? Days { get; set; }

        [Column("ACTIVE_FLAG")]
        public byte? ActiveFlag { get; set; }

        [Column("ENTRY_USER")]
        public int EntryUser { get; set; }

        [Column("ENTRY_DATE")]
        public DateTime EntryDate { get; set; }

        [Column("CHANGE_USER")]
        public int? ChangeUser { get; set; }

        [Column("CHANGE_DATE")]
        public DateTime? ChangeDate { get; set; }
    }
}
