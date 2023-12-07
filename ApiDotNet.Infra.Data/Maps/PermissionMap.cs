using ApiDotNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiDotNet.Infra.Data.Maps
{
    public class PermissionMap : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("permissions");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("idpermissao").UseIdentityColumn();

            builder.Property(x => x.VisualName).HasColumnName("nomevisual");

            builder.Property(x => x.PermissionName).HasColumnName("nomepermissao");

            //criar relacionamento, uma permissão pode ter em muitos usuários. Um usuário tem várias permissões, mas uma permissão só tem um usuário
            builder.HasMany(x => x.UserPermissions).WithOne(p => p.Permission).HasForeignKey(p => p.PermissionId); //da nossa lista acessamos o objeto permissão e através dele falamos o ligamento
        }
    }
}
