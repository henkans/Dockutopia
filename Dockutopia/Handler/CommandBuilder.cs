using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Dockutopia.Foundation;
using Dockutopia.Model;

namespace Dockutopia.Handler
{
    public static class CommandBuilder 
    {

        

        private static string RunImage(ImageRunArgs imageRunArgs)
        {
            var command = $"run -it -d --name {imageRunArgs.Name} {imageRunArgs.ID} ";
            if (imageRunArgs.ContainerPort != null && imageRunArgs.HostPort != null)
            {
                command += $"-p {imageRunArgs.ContainerPort}:{imageRunArgs.HostPort}";
            }
            return command;
        }


        private static string Run(ImageRunArgs imageRunArgs, string command)
        {
            var result = $"{command} -it -d --name {imageRunArgs.Name} {imageRunArgs.ID} ";
            if (imageRunArgs.ContainerPort != null && imageRunArgs.HostPort != null)
            {
                result += $"-p {imageRunArgs.ContainerPort}:{imageRunArgs.HostPort}";
            }
            return result;
        }



    }
}
