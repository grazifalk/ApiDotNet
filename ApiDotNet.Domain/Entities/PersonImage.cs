using ApiDotNet.Domain.Validations;

namespace ApiDotNet.Domain.Entities
{
    public class PersonImage
    {
        public int Id { get; private set; }
        public int PersonId { get; private set; }
        public string? ImageUri { get; private set; }
        public string? ImageBase { get; private set; }

        public Person Person { get; set; }

        public PersonImage(int personId, string? imageUri, string? imageBase)
        {
            Validation(personId);
            ImageUri = imageUri;
            ImageBase = imageBase;
        }

        private void Validation(int personId)
        {
            DomainValidationException.When(personId == 0, "Id pessoa deve ser informado!");
            PersonId = personId;
        }
    }
}
