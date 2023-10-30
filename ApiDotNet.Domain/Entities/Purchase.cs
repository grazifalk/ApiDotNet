using ApiDotNet.Domain.Validations;
using System.Diagnostics;
using System.Xml.Linq;

namespace ApiDotNet.Domain.Entities
{
    public class Purchase
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public int PersonId { get; private set; }
        public DateTime Date { get; private set; }

        //vamos declarar as classes virtuais (pessoa e produto, para depois fazer mapeamento das classes e fazer o relacionamento de 1 : n )
        public Person Person { get; set; }
        public Product Product { get; set; }

        public Purchase(int productId, int personId)
        {
            Validation(productId, personId);
        }

        //controle
        public Purchase(int id, int productId, int personId)
        {
            DomainValidationException.When(id <= 0, "Id deve ser informado!");
            Id = id;
            Validation(productId, personId);
        }

        //criar método de editar
        //não podemos usar o controle para editar, pois na hora de chamar o controle damos um "new" e se fizermos isso perdemos o rastreio que fizemos no método e daria erro
        public void Edit(int id, int productId, int personId)
        {
            DomainValidationException.When(id <= 0, "Id deve ser informado!");
            Id = id;
            Validation(productId, personId);
        }

        private void Validation(int productId, int personId)
        {
            DomainValidationException.When(productId <= 0, "Id produto deve ser informado!");
            DomainValidationException.When(personId <= 0, "Id pessoa deve ser informado!");

            ProductId = productId;
            PersonId = personId;
            Date = DateTime.Now;
        }
    }
}
