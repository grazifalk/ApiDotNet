using ApiDotNet.Domain.Entities;
using ApiDotNet.Domain.Validations;

namespace ApiDotNet.UnitTest.Domain.Entities
{
    public class PersonTest
    {
        [Fact(DisplayName ="Não deve criar pessoa sem documento")]
        public void CriaPessoa_Person_NaoCriaPessoaSemDocumento()
        {
            //passar a nossa exceção e passar o que tem que fazer = criar nova pessoa e passar o que ele espera
            var ex = Assert.Throws<DomainValidationException>(() =>
                        new Person(document: null, name: "Test", phone: "9999999999"));

            //vamos verificar para comparar o que esperamos e o atual
            Assert.Equal("Documento deve ser informado!", ex.Message);
        }

        [Fact(DisplayName = "Não deve criar pessoa sem id")]
        public void CriaPessoa_Person_NaoCriaPessoaSemId()
        {
            var ex = Assert.Throws<DomainValidationException>(() =>
            new Person(id: 0, document: "651561654", name: "Test", phone: "9999999999"));

            Assert.Equal("Id inválido!", ex.Message);
        }

        [Fact(DisplayName = "Deve criar pessoa")]
        public void CriaPessoa_Person_DeveCriarPessoa()
        {
            //criar pessoa
            var person = new Person("564566464", "Teste", "999999999");
            //vamos validar se a pessoa não é nula, não pode ser nula, tem que criar
            Assert.NotNull(person);
        }
    }
}
