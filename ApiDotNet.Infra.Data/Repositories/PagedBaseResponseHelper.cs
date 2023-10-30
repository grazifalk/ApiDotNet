using ApiDotNet.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiDotNet.Infra.Data.Repositories
{
    public static class PagedBaseResponseHelper
    {
        //vamos criar um método que vai ser o método que vai retornar
        //esse método vai passar uma resposta dinâmica e uma entrada dinâmica
        public static async Task<TResponse> GetResponseAsync<TResponse,T>(IQueryable<T> query, PagedBaseRequest request)
            where TResponse : PagedBaseResponse<T>, new()
        {
            var response = new TResponse();
            var count = await query.CountAsync();
            response.TotalPages = (int)Math.Abs((double)count / request.PageSize); //total de linhas dividido pelo total de páginas
            response.TotalRegisters = count; //total da página
            if (string.IsNullOrEmpty(request.OrderByProperty))
                response.Data = await query.ToListAsync();
            else
                response.Data = query.OrderByDynamic(request.OrderByProperty)
                    .Skip((request.Page -1) * request.PageSize) //quantidade de linhas que vou pular
                    .Take(request.PageSize) //quantidade de linhas que vou pegar
                    .ToList(); 

            return response;
        }

        //método para ordenação
        private static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> query, string propertyName)
        {
            return query.OrderBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null));
        }
    }
}
