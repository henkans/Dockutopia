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
    public class DockerContainerListHandler : NotifyPropertyChangedBase, IDockerListHandler
    {
        public DockerContainerListHandler()
        {
            RunDockerContainerListCommand = new RelayCommand(RunDockerContainerList);
            _dockerContainers = new ObservableCollection<DockerContainer>();
            TempDockerContainers = new ObservableCollection<DockerContainer>();
            RunDockerContainerList();
        }

        private ObservableCollection<DockerContainer> _dockerContainers;
        public ObservableCollection<DockerContainer> DockerContainers
        {
            get { return _dockerContainers; }
            set
            {
                _dockerContainers = value;
                this.OnPropertyChanged();
            }
        }

        public DockerContainer SelectedDockerContainer { get; set; }


        private ObservableCollection<DockerContainer> TempDockerContainers { get; set; }
    

        public ICommand RunDockerContainerListCommand { get; set; }
        private void RunDockerContainerList()
        {
           
            try
            {
                // Run command...
                DockerRepository dockerWrapper = new DockerRepository(DockerContainer.Format);
                dockerWrapper.Exited += DockerContainerList_Exited;
                dockerWrapper.OutputDataReceived += DockerContainerList_DataReceived;
                dockerWrapper.ErrorDataReceived += DockerContainerList_DataReceived;
                dockerWrapper.BeginRun();
            }
            catch (Exception exception)
            {
                throw new Exception("Error running RunDockerContainerList().", exception);
            }
        }
        void DockerContainerList_DataReceived(object sender, DataEventArgs e)
        {

            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                if (e.Data != null && e.Data.Trim() != string.Empty)
                {
                    var result = ContainerStringParser.ParseDockerContainerString(e.Data);
                    TempDockerContainers.Add(result);
                }

            }));

        }

        void DockerContainerList_Exited(object sender, EventArgs e)
        {

            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                DockerContainers = new ObservableCollection<DockerContainer>(TempDockerContainers);
                TempDockerContainers.Clear();

            }));

        }
    }
}
