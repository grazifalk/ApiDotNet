using ApiDotNet.Domain.Integrations;

namespace ApiDotNet.Infra.Data.Integrations
{
    public class SavePersonImage : ISavePersonImage
    {
        //declarar a variável
        private readonly string _filePath;
        public SavePersonImage()
        {
            _filePath = "C:/Users/missf/Desktop"; //colocar caminho de onde vamos salvar o arquivo
        }

        //essa classe concreta vai salvar a imagem local, assim podemos ter uma ideia de como salvar a imagem no servidor
        public string Save(string imageBase64)
        {
            //interpretar o base64 para pegar qualquer extensão
            var fileExt = imageBase64.Substring(imageBase64.IndexOf("/") + 1, imageBase64.IndexOf(";") - imageBase64.IndexOf("/")-1); // png ou jpeg...

            //vamos remover o "data" e pegar só a parte do base64
            var base64Code = imageBase64.Substring(imageBase64.IndexOf(",") + 1);

            //vamos pegar a parte do base64 e converter para byte
            var imgBytes = Convert.FromBase64String(base64Code);

            //nomear a imagem adicionando a extensão 'fileExt'
            var fileName = Guid.NewGuid().ToString() + "." + fileExt;

            //usar FileStream para criar o arquivo, aqui vamos salvar o arquivo
            using(var imageFile = new FileStream(_filePath+"/"+fileName, FileMode.Create))
            {
                imageFile.Write(imgBytes,0,imgBytes.Length); //vamos passar o byte, a position 0 que é onde vai começar e até onde vai .length
                imageFile.Flush(); //limpar
            }
            return _filePath + "/" + fileName; //retornar o caminho do nosso arquivo que é o caminho que vamos salvar no banco
        }
    }
}
