﻿using ApiDotNet.Domain.Validations;

namespace ApiDotNet.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public ICollection<UserPermission> UserPermissions { get; set; } //atributo necessário para fazer o mapeamento
        public User(string email, string password)
        {
            Validation(email, password);
            UserPermissions = new List<UserPermission>();
        }

        public User(int id, string email, string password)
        {
            DomainValidationException.When(id <= 0, "Id deve ser informado!");
            Id = id;
            Validation(email, password);
            UserPermissions = new List<UserPermission>();
        }

        private void Validation(string email, string password)
        {
            DomainValidationException.When(string.IsNullOrEmpty(email), "Email deve ser informado!");
            DomainValidationException.When(string.IsNullOrEmpty(password), "Password deve ser informada!");

            Email = email;
            Password = password;
        }
    }
}
