namespace Dockutopia.Model
{
    public class DockerResponse
    {
        public DockerResponse()
        {
            
        }

        public DockerResponse(string message, string color)
        {
            Message = message;
            Color = color;
        }
        public string Message { get; set; }
        public string Color { get; set; }
    }
}
