using System.Collections.ObjectModel;
using System.Windows.Input;
using Dockutopia.Model;

namespace Dockutopia.Handler
{
    public interface IDockerListHandler
    {
        ObservableCollection<DockerContainer> DockerContainers { get; set; }
        DockerContainer SelectedDockerContainer { get; set; }
        ICommand RunDockerContainerListCommand { get; set; }
    }
}