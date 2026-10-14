using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace AxiomSysDL.Models.SupplyChain
{
    public class SupplyChainContext : IdentityDbContext<IdentityUser>
    {
        public SupplyChainContext(DbContextOptions<SupplyChainContext> options) : base(options) { }
        public DbSet<SaDeliveryTerm> SaDeliveryTerms => Set<SaDeliveryTerm>();

    }
}
