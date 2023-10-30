namespace ApiDotNet.Application.DTOs
{
    public class PagedBaseResponseDTO<T>
    {
        public int TotalRegisters { get; private set; }
        public List<T> Data { get; private set; }

        //toda vez que usarmos essa classe vamos forçar a passar os dados pra ela: a lista e a quantidade de registro (um inteiro e um objeto lista)
        public PagedBaseResponseDTO(int totalRegisters, List<T> data)
        {
            TotalRegisters = totalRegisters;
            Data = data;
        }
    }
}
