using ApiDotNet.Domain.Validations;

namespace ApiDotNet.Domain.Entities
{
    public sealed class Permission
    {
        public Permission(string visualName, string permissionName)
        {
            Validation(visualName, permissionName);
            UserPermissions = new List<UserPermission>(); // vamos precisar criar na tabela usuário também
        }

        public int Id { get; private set; }
        public string VisualName { get; private set; }
        public string PermissionName { get; private set; }

        //CRIAR LISTA DE USUÁRIOS
        public ICollection<UserPermission> UserPermissions { get; set; }
            
        private void Validation(string visualName, string permissionName)
        {
            DomainValidationException.When(string.IsNullOrEmpty(visualName), "Nome visual deve ser informado!");
            DomainValidationException.When(string.IsNullOrEmpty(permissionName), "Nome permissão deve ser informado!");

            VisualName = visualName;
            PermissionName = permissionName;
        }
    }
}
