using System.Collections.ObjectModel;
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

            ImageRunPopupCommand = new RelayCommand<string>(ImageRunPopup);
            ImageRunCommand = new RelayCommand(ImageRun);


            //--Kill
            RunDockerKillCommand =new RelayCommand(RunDockerKill);

            //To run multiple commands...
            OnEnterPressCommand = new RelayCommand<string>(OnEnterPress);
            RefreshUiCommand = new RelayCommand(RefreshUi);
            RunDockerCommandWithRefreshCommand = new RelayCommand<string>(RunDockerCommandWithRefresh);

            RunImageArgs = new ImageRunArgs();
        }


        public ICommand ImageRunPopupCommand { get; set; }
        private void ImageRunPopup(object command)
        {
            RunImageIdPopUp = command as string;
        }

        private string _runImageIdPopUp;
        public string RunImageIdPopUp 
        {
            get { return _runImageIdPopUp; }
            set
            {
                _runImageIdPopUp = value;
                this.OnPropertyChanged();
            }
        }


        public ImageRunArgs RunImageArgs { get; set; }
        public ICommand ImageRunCommand { get; set; }
        private void ImageRun()
        {
            var command = $"run -it -d --name {RunImageArgs.Name} {RunImageIdPopUp} ";
            if (RunImageArgs.ContainerPort != null && RunImageArgs.HostPort != null)
            {
                command += $"-p {RunImageArgs.ContainerPort}:{RunImageArgs.HostPort}";
            }

            DockerHandler.RunDockerCommand.Execute(command);

            RunImageArgs = new ImageRunArgs();
            RunImageIdPopUp = null;
        }




        public ICommand RunDockerKillCommand { get; set; }
        private void RunDockerKill()
        {
            //To run multiple commands
            DockerHandler.KillCommand.Execute(null);
        }




        public DockerHandler DockerHandler { get; set; }
        public DockerListHandler<DockerContainer> DockerContainerListHandler { get; set; }
        public DockerListHandler<DockerImage> DockerImagesListHandler { get; set; }
        public PreviousCommandHandler PreviousCommandHandler { get; set; }

        public ICommand OnEnterPressCommand { get; set; }
        private void OnEnterPress(object command)
        {
            //To run multiple commands
            if (DockerHandler.IsEnabled)
            {
                DockerHandler.RunDockerCommand.Execute(command);
                PreviousCommandHandler.AddInputCommand.Execute(command);
            }
            
        }


        public ICommand RunDockerCommandWithRefreshCommand { get; set; }
        private void RunDockerCommandWithRefresh(object command)
        {
            //To run multiple commands
            DockerHandler.RunDockerCommand.Execute(command);
            PreviousCommandHandler.AddInputCommand.Execute(command);
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
