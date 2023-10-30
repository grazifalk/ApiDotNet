using ApiDotNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiDotNet.Infra.Data.Maps
{
    public class PurchaseMap : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("purchases");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("idcompra").UseIdentityColumn();

            builder.Property(x => x.PersonId).HasColumnName("idpessoa");
            builder.Property(x => x.ProductId).HasColumnName("idproduto");
            builder.Property(x => x.Date).HasColumnType("date").HasColumnName("datacompra");

            //1 compra pode ter 1 pessoa | 1 pessoa pode ter várias compras = N pra 1
            //1 pessoa n compras | 1 produto N compras
            builder.HasOne(x => x.Person).WithMany(x => x.Purchases);
            builder.HasOne(x => x.Product).WithMany(x => x.Purchases);
        }
    }
}
