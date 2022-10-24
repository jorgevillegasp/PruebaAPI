namespace PruebaAPI.Models.Respuestas
{
    public class Response
    {
        public bool Complete { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public Response()
        {
            Complete = false;
        }
    }
}
