using System.Windows.Input;
using Dockutopia.Foundation;
using Dockutopia.Handler;

namespace Dockutopia.ViewModel
{
    public class MainNotifyPropertyChanged : NotifyPropertyChangedBase
    {
        
        public MainNotifyPropertyChanged()
        {
            DockerHandler = new DockerHandler();
            DockerContainerListHandler = new DockerContainerListHandler();
            DockerImagesListHandler = new DockerImageListHandler(); //Temp. make generic with DockerListHandler
            PreviousCommandHandler = new PreviousCommandHandler();

            //To run multiple commands...
            OnEnterPressCommand = new RelayCommand<string>(OnEnterPress);
            RefreshUiCommand = new RelayCommand(RefreshUi);
            RunDockerCommandWithRefreshCommand = new RelayCommand<string>(RunDockerCommandWithRefresh);


            //set initial
            //RefreshUiCommand.Execute(null);
            //RefreshUi();
            //DockerImagesListHandler.RunDockerImagesListCommand.Execute(null);
            //DockerContainerListHandler.RunDockerContainerListCommand.Execute(null);

        }

        public DockerHandler DockerHandler { get; set; }
        public DockerContainerListHandler DockerContainerListHandler { get; set; }
        public DockerImageListHandler DockerImagesListHandler { get; set; }
        public PreviousCommandHandler PreviousCommandHandler { get; set; }

        public ICommand OnEnterPressCommand { get; set; }
        private void OnEnterPress(object command)
        {
            //To run multiple commands
            DockerHandler.RunDockerCommand.Execute(command);
            PreviousCommandHandler.AddInputCommand.Execute(command);
        }


        public ICommand RunDockerCommandWithRefreshCommand { get; set; }
        private void RunDockerCommandWithRefresh(object command)
        {
            //To run multiple commands
            DockerHandler.RunDockerCommand.Execute(command);
            RefreshUiCommand.Execute(command);
        }


        public ICommand RefreshUiCommand { get; set; }
        private void RefreshUi()
        {
            DockerImagesListHandler.RunDockerImagesListCommand.Execute(null);
            DockerContainerListHandler.RunDockerContainerListCommand.Execute(null);
        }

    }
}
