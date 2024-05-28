using System.Text.Json.Serialization;

namespace DallE
{
    public class RequestBody
    {
        public RequestBody(string texto)
        {
            Texto = string.IsNullOrEmpty(texto.Trim()) ? texto : "programador futurista";
        }

        [JsonPropertyName("model")]
        public string Modelo => "dall-e-3"; //lento - dall-e-2 |rapido - dall-e-3

        [JsonPropertyName("prompt")]
        public string Texto { get; private set; } // texto fornecido

        [JsonPropertyName("n")]
        public int Quantidade => 1; //10 - dall-e-2 | 1 - dall-e-3 

        [JsonPropertyName("size")]
        public string size => "1024x1024"; //Tamanho Imagem - 1024×1024 | 1024×1792 | 1792×1024

        [JsonPropertyName("quality")]
        public string quality => "hd"; //Qualidade | se não definida padrão 'standard'
    }
}
