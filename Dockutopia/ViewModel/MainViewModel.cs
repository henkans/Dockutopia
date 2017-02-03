using System.Reflection;
using System.Windows.Input;
using Dockutopia.Foundation;
using Dockutopia.Handler;
using Dockutopia.Model;

namespace Dockutopia.ViewModel
{
    public class MainNotifyPropertyChanged : NotifyPropertyChangedBase
    {
        
        public MainNotifyPropertyChanged()
        {
            DockerHandler = new DockerHandler();
            DockerContainerListHandler = new DockerListHandler<DockerContainer>();
            DockerImagesListHandler = new DockerListHandler<DockerImage>(); //Temp. make generic with DockerListHandler
            PreviousCommandHandler = new PreviousCommandHandler();

            //To run multiple commands...
            OnEnterPressCommand = new RelayCommand<string>(OnEnterPress);
            RefreshUiCommand = new RelayCommand(RefreshUi);
            RunDockerCommandWithRefreshCommand = new RelayCommand<string>(RunDockerCommandWithRefresh);


        }

        public DockerHandler DockerHandler { get; set; }
        public DockerListHandler<DockerContainer> DockerContainerListHandler { get; set; }
        public DockerListHandler<DockerImage> DockerImagesListHandler { get; set; }
        public PreviousCommandHandler PreviousCommandHandler { get; set; }

        public string Version => "[Version " + Assembly.GetEntryAssembly().GetName().Version.ToString() + "]";

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
            DockerImagesListHandler.RunDockerListCommand.Execute(null);
            DockerContainerListHandler.RunDockerListCommand.Execute(null);
        }

    }
}
