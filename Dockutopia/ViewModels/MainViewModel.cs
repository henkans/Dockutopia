using System.Windows.Input;
using Dockutopia.Foundation;
using Dockutopia.Handlers;

namespace Dockutopia.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        
        public MainViewModel()
        {
            DockerHandler = new DockerHandler();
            DockerContainerListHandler = new DockerListHandler();
            DockerImagesListHandler = new DockerListHandlerImg(); //Temp. make generic with DockerListHandler
            PreviousCommandHandler = new PreviousCommandHandler();

            //To run multiple commands...
            OnEnterPressCommand = new RelayCommand<string>(OnEnterPress);
            RefreshUiCommand = new RelayCommand(RefreshUi);
            
        }

        public DockerHandler DockerHandler { get; set; }
        public DockerListHandler DockerContainerListHandler { get; set; }
        public DockerListHandlerImg DockerImagesListHandler { get; set; }
        public PreviousCommandHandler PreviousCommandHandler { get; set; }

        public ICommand OnEnterPressCommand { get; set; }
        private void OnEnterPress(object command)
        {
            //To run multiple commands
            DockerHandler.RunDockerCommand.Execute(command);
            PreviousCommandHandler.AddInputCommand.Execute(command);
        }

        public ICommand RefreshUiCommand { get; set; }
        private void RefreshUi()
        {
            DockerImagesListHandler.RunDockerImagesListCommand.Execute(null);
            DockerContainerListHandler.RunDockerContainerListCommand.Execute(null);
        }

    }
}
