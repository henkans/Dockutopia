using System;
using System.Windows.Input;
using Dockutopia.Model;

namespace Dockutopia.Handler
{
    public interface IDockerHandler
    {
        string OutputText { get; set; }
        ICommand RunDockerCommand { get; set; }
        void DockerWrapper_DataReceived(object sender, DataEventArgs e);
        void DockerWrapper_Exited(object sender, EventArgs e);
    }
}