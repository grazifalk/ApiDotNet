using ApiDotNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiDotNet.Infra.Data.Maps
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("idproduto").UseIdentityColumn();
            builder.Property(x => x.CodErp).HasColumnName("coderp");
            builder.Property(x => x.Name).HasColumnName("nome");
            builder.Property(x => x.Price).HasColumnName("preco");

            //mapear chave estrangeira
            //a tabela no banco não tem a chave estrangeira, é a tabela de compras que tem a chave estrangeira que referencia o idProduto da chave primária
            //aqui vamos mapear a lista de produto da entidade, 1 produto pode estar em várias compras e 1 compra terá somente 1 produto
            // 1 produto : n compras
            builder.HasMany(x => x.Purchases).WithOne(p => p.Product).HasForeignKey(x => x.ProductId);
        }
    }
}
