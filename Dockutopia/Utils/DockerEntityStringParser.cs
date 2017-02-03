using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dockutopia.Model;

namespace Dockutopia.Utils
{
    public class DockerEntityStringParser
    {
        public static T ParseDockerString<T>(string input)
        {
            var splittedString = input.Split('\t');
            return (T)Activator.CreateInstance(typeof(T), splittedString);
        }
    }
}
