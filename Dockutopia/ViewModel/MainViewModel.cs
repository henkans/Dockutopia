using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;
using Dockutopia.Foundation;
using Dockutopia.Handler;
using Dockutopia.Model;
using Dockutopia.Utils;

namespace Dockutopia.ViewModel
{
    public class MainViewModel : NotifyPropertyChangedBase
    {
        
        public MainViewModel()
        {
            if (DockerHelpers.IsSoftwareInstalled("Docker"))
            {
                Debug.WriteLine("Docker is installed");
            }
            else
            {
                { Debug.WriteLine("Docker is NOT installed"); }
            }

            DockerHandler = new DockerHandler();
            DockerContainerListHandler = new DockerListHandler<DockerContainer>();
            DockerImagesListHandler = new DockerListHandler<DockerImage>(); //Temp. make generic with DockerListHandler
            PreviousCommandHandler = new PreviousCommandHandler();
            
            // Build complex image commands
            ImagePopupCommand = new RelayCommand<string>(ImagePopup);
            ImageRunCommand = new RelayCommand(ImageRun);
            ImageArgs = new ImageArgs();
            
            //--Kill
            RunDockerKillCommand =new RelayCommand(RunDockerKill);

            //To run multiple commands...
            OnEnterPressCommand = new RelayCommand<string>(OnEnterPress);
            RefreshUiCommand = new RelayCommand(RefreshUi);
            RunDockerCommandWithRefreshCommand = new RelayCommand<string>(RunDockerCommandWithRefresh);
            
        }


        public ICommand ImagePopupCommand { get; set; }
        private void ImagePopup(object param)
        {
            RunImageIdPopUp = param as string;
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


        public ImageArgs ImageArgs { get; set; }
        public ICommand ImageRunCommand { get; set; }
        private void ImageRun()
        {
            var command = CommandBuilder.RunImage(ImageArgs);
            DockerHandler.RunDockerCommand.Execute(command);
            ImageArgs = new ImageArgs();
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
