using ApiDotNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiDotNet.Infra.Data.Maps
{
    public class UserPermissionMap : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.ToTable("permissionuser");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("idpermissaousuario").UseIdentityColumn();

            builder.Property(x => x.PermissionId).HasColumnName("idpermissao");
            builder.Property(x => x.UserId).HasColumnName("idusuario");

            builder.HasOne(x => x.Permission).WithMany(x => x.UserPermissions);
            builder.HasOne(x => x.User).WithMany(x => x.UserPermissions);
        }
    }
}
