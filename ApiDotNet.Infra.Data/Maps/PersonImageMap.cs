using ApiDotNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiDotNet.Infra.Data.Maps
{
    public class PersonImageMap : IEntityTypeConfiguration<PersonImage>
    {
        public void Configure(EntityTypeBuilder<PersonImage> builder)
        {
            builder.ToTable("peopleimage");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("idimagem").UseIdentityColumn();
            builder.Property(x => x.PersonId).HasColumnName("idpessoa");
            builder.Property(x => x.ImageBase).HasColumnName("imagembase");
            builder.Property(x => x.ImageUri).HasColumnName("imagemurl");

            builder.HasOne(x => x.Person).WithMany(x => x.PersonImages);
        }
    }
}
