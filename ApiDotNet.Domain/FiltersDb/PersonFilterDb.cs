using ApiDotNet.Domain.Repositories;

namespace ApiDotNet.Domain.FiltersDb
{
    public class PersonFilterDb : PagedBaseRequest
    {
        public string? Name { get; set; } //campo que vamos usar para fazer o filtro
    }
}
