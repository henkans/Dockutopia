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

        public static string RunImage(ImageArgs imageArgs)
        {
            var command = $"run -it -d --name {imageArgs.Name} {imageArgs.ID} ";
            if (imageArgs.ContainerPort != null && imageArgs.HostPort != null)
            {
                command += $" -p {imageArgs.ContainerPort}:{imageArgs.HostPort}";
            }

            if (imageArgs.Ip != null)
            {
                command += $" -ip {imageArgs.Ip} ";
            }

            if (imageArgs.RunIsolated)
            {
                // if linux container run --isolation
                command += " --isolation=hyperv ";
            }

            if (imageArgs.ExtraInput != null)
            {
                command += $" {imageArgs.ExtraInput}";
            }

            return command;
        }


        private static string Run(ImageArgs imageArgs, string command)
        {
            var result = $"{command} -it -d --name {imageArgs.Name} {imageArgs.ID} ";
            if (imageArgs.ContainerPort != null && imageArgs.HostPort != null)
            {
                result += $"-p {imageArgs.ContainerPort}:{imageArgs.HostPort}";
            }
            return result;
        }



    }
}
