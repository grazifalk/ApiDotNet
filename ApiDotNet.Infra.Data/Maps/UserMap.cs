using ApiDotNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiDotNet.Infra.Data.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("idusuario");
            builder.Property(x => x.Email).HasColumnName("email");
            builder.Property(x => x.Password).HasColumnName("senha");
        }
    }
}
