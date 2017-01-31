using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Dockutopia.Foundation;
using Dockutopia.Model;
using Dockutopia.Repository;
using Dockutopia.Utils;

namespace Dockutopia.Handler
{
    public class DockerImageListHandler:NotifyPropertyChangedBase
    {
        public DockerImageListHandler()
        {
            RunDockerImagesListCommand = new RelayCommand(RunDockerImagesList);
            _dockerImages = new ObservableCollection<DockerImage>();
            TempDockerImages = new ObservableCollection<DockerImage>();
            RunDockerImagesList();
        }


        private ObservableCollection<DockerImage> _dockerImages;

        public ObservableCollection<DockerImage> DockerImages
        {
            get { return _dockerImages; }
            set
            {
                _dockerImages = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<DockerImage> TempDockerImages {get; set; }


        // -------------------- Image List
        public ICommand RunDockerImagesListCommand { get; set; }
        private void RunDockerImagesList()
        {
            try
            {
                TempDockerImages.Clear();
                // Run command...
                DockerRepository dockerWrapper = new DockerRepository(DockerImage.Command);
                dockerWrapper.Exited += DockerImageList_Exited;
                dockerWrapper.OutputDataReceived += DockerImageList_DataReceived;
                dockerWrapper.ErrorDataReceived += DockerImageList_DataReceived;
                dockerWrapper.BeginRun();
            }
            catch (Exception exception)
            {
                throw new Exception("Error running RunDockerImagesList().", exception);
            }
        }
        void DockerImageList_DataReceived(object sender, DataEventArgs e)
        {

            Application.Current.Dispatcher.Invoke((Action)(() =>
            {

                if (e.Data != null && e.Data.Trim() != string.Empty)
                {
                    var result = ContainerStringParser.ParseDockerImageString(e.Data);
                    TempDockerImages.Add(result);
                }

            }));
        }

        void DockerImageList_Exited(object sender, EventArgs e)
        {

            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                DockerImages = new ObservableCollection<DockerImage>(TempDockerImages);
                //TempDockerImages.Clear();

            }));

        }

        


    }
}
