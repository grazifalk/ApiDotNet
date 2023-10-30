using ApiDotNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;

namespace ApiDotNet.Infra.Data.Maps
{
    public class PersonMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("people");
            builder.HasKey(x => x.Id); //chave primária

            //mapear atributos e a qual coluna do banco se referem
            builder.Property(x => x.Id).HasColumnName("idpessoa").UseIdentityColumn();
            builder.Property(x => x.Document).HasColumnName("documento");
            builder.Property(x => x.Name).HasColumnName("nome");
            builder.Property(x => x.Phone).HasColumnName("celular");

            //mapear as relações - a tabela pessoa é chave primária da tabela compras
            // 1 pra N - a tabela de pessoa tem uma lista de compras, onde essa lista de compras tem um atributo virtual Person e a ligação é feita através de idPessoa, mas 1 compra é referente a 1 pessoa
            builder.HasMany(x => x.Purchases).WithOne(p => p.Person).HasForeignKey(x => x.PersonId);

        }
    }
}
