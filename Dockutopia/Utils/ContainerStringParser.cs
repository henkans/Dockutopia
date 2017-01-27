using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dockutopia.Model;

namespace Dockutopia.Utils
{
    public class ContainerStringParser
    {

        public static DockerContainer ParseDockerContainerString(string input)
        {
            //var splittedString = System.Text.RegularExpressions.Regex.Split(input, @"\s{2,}");

            var splittedString = input.Split('\t');
            return new DockerContainer(splittedString);
        }


        public static DockerImage ParseDockerImageString(string input)
        {
            //var splittedString = System.Text.RegularExpressions.Regex.Split(input, @"\s{2,}");
            var splittedString = input.Split('\t');
            return new DockerImage(splittedString);
        }
    }
}
