using Newtonsoft.Json;

namespace ChequerWorkspace.Models
{
    public class ErrorResponse
    {
        [JsonIgnore]
        public object Message { get; set; }
        
        [JsonIgnore]
        public string Code { get; set; }

        public object Error => new
        {
            Message,
            Code,
        };

        public ErrorResponse(string message, string code)
        {
            Message = message;
            Code = code;
        }

    }
}