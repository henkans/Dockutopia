namespace Dockutopia.Model
{
    public class DockerContainer: IDockerEntity
    {
        public static string DockerCommand => @"ps -a --format {{.ID}}\t{{.Image}}\t{{.Command}}\t{{.RunningFor}}\t{{.Status}}\t{{.Ports}}\t{{.Names}}";
        // ALL - https://github.com/Shopify/docker/tree/master/docs/reference/commandline
        // .ID Container ID
        // .Image Image ID
        // .Command Quoted command
        // .CreatedAt Time when the container was created.
        // .RunningFor Elapsed time since the container was started.
        // .Ports  Exposed ports.
        // .Status Container status.
        // .Size Container disk size.
        // .Names Container names.
        // .Labels All labels assigned to the container.
        // .Label Value of a specific label for this container.For example '{{.Label "com.docker.swarm.cpu"}}'
        // .Mounts Names of the volumes mounted in this container.

        //public DockerContainer()
        //{
            
        //}

        public DockerContainer(string[] input)
        {
            ID = input[0];
            Name = input[6];

            ContainerId = input[0];
            Image = input[1];
            Command = input[2];
            RunningFor = input[3];
            Status = input[4];
            Ports = input[5];
            Names = input[6];
        }

        public DockerContainer(params object[] input)
        {
            ID = (string) input[0];
            Name = (string)input[6];

            ContainerId = (string)input[0];
            Image = (string)input[1];
            Command = (string)input[2];
            RunningFor = (string)input[3];
            Status = (string)input[4];
            Ports = (string)input[5];
            Names = (string)input[6];
        }


        public string ID { get; set; }
        public string Name { get; set; }


        public string ContainerId { get; set; }
        public string Image { get; set; }
        public string Command { get; set; }
        public string RunningFor { get; set; }
        public string Status { get; set; }
        public string Ports { get; set; }
        public string Names { get; set; }

    }
}
