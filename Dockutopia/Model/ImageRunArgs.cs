using Dockutopia.Foundation;

namespace Dockutopia.Model
{
    public class ImageRunArgs
    {
        
        public string Name { get; set; }
        public string Ip { get; set; }
        public string ContainerPort { get; set; }
        public string HostPort { get; set; }

        public bool RunIsolated { get; set; }
        

    }
}
