namespace ApiDotNet.Domain.Repositories
{
    public class PagedBaseRequest
    {
        public int Page { get; set; } //página
        public int PageSize { get; set; } //tamanho da página
        public string OrderByProperty { get; set; } //ordenação

        //inicializar as variáveis por padrão, para caso ao usar a api não informar nada, ter um valor padrão
        public PagedBaseRequest()
        {
            Page = 1;
            PageSize = 10;
            OrderByProperty = "Id";
        }
    }
}
