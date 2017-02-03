namespace Dockutopia.Model
{
    public class DockerImage
    {
        public static string DockerCommand => @"images --format {{.Repository}}\t{{.Tag}}\t{{.ID}}\t{{.Size}}\t{{.CreatedSince}}";

        // ALL - https://github.com/Shopify/docker/tree/master/docs/reference/commandline
        // .ID Image ID
        // .Repository Image repository
        // .Tag Image tag
        // .Digest Image digest
        // .CreatedSince Elapsed time since the image was created.
        // .CreatedAt  Time when the image was created.
        // .Size Image disk size.

        public DockerImage(string[] input)
        {
            Repository = input[0];
            Tag = input[1];
            ImageId = input[2];
            Size = input[3];
            Created = input[4] + " ago";
        }

        public DockerImage(params object[] input)
        {
            Repository = (string)input[0];
            Tag = (string)input[1];
            ImageId = (string)input[2];
            Size = (string)input[3];
            Created = (string)input[4] + " ago";
        }


        public string Repository { get; set; }
        public string Tag { get; set; }
        public string ImageId { get; set; }
        public string Created { get; set; }
        public string Size { get; set; }

    }
}
