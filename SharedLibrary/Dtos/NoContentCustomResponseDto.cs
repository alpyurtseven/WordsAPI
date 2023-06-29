using System.Text.Json.Serialization;


namespace SharedLibrary.Dtos
{
    public class NoContentCustomResponseDto
    {
        public List<string> Errors { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }

        public NoContentCustomResponseDto(List<string> errors, int statusCode)
        {
            Errors = errors;
            StatusCode = statusCode;
        }
    }
}
