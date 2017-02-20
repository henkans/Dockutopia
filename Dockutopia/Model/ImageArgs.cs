using Dockutopia.Foundation;

namespace Dockutopia.Model
{
    public class ImageArgs
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public string ContainerPort { get; set; }
        public string HostPort { get; set; }
        public bool RunIsolated { get; set; }
        public string ExtraInput { get; set; }
        

    }
}
