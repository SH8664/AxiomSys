using Microsoft.EntityFrameworkCore;


namespace AxiomSysDL.Models.SupplyChain
{
    public class SupplyChainContext : DbContext
    {
        public SupplyChainContext(DbContextOptions<SupplyChainContext> options) : base(options) { }
        public DbSet<SaDeliveryTerm> SaDeliveryTerms => Set<SaDeliveryTerm>();

    }
}
