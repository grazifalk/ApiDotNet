namespace ApiDotNet.Domain.Repositories
{
    public class PagedBaseResponse<T> //vai receber tipo T que é a lista que vamos retornar
    {
        //com essa classe vamos conseguir retornar os dados que foram paginados, o total de páginas que deu e o total de registros que tem na tabela
        public List<T> Data { get; set; } //nossos dados, cada classe terá seu tipo específico
        public int TotalPages { get; set; } // 
        public int TotalRegisters { get; set; }

    }
}
