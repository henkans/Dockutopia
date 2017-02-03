using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Dockutopia.Foundation;
using Dockutopia.Model;
using Dockutopia.Repository;
using Dockutopia.Utils;

namespace Dockutopia.Handler
{
    public class DockerListHandler<T>: NotifyPropertyChangedBase
    {
        public DockerListHandler()
        {
            RunDockerListCommand = new RelayCommand(RunDockerList);
            _dockerEntities = new ObservableCollection<T>();
            TempDockerEntities = new ObservableCollection<T>();
            RunDockerList();
        }
        
        private ObservableCollection<T> _dockerEntities;

        public ObservableCollection<T> DockerEntities
        {
            get { return _dockerEntities; }
            set
            {
                _dockerEntities = value;
                this.OnPropertyChanged();
            }
        }

        private DockerContainer _selectedDockerEntity;
        public DockerContainer SelectedDockerEntity
        {
            get { return _selectedDockerEntity; }
            set
            {
                _selectedDockerEntity = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<T> TempDockerEntities { get; set; }

        // -------------------- List
        public ICommand RunDockerListCommand { get; set; }
        private void RunDockerList()
        {

            try
            {
                TempDockerEntities.Clear();
                // Run command...
                //DockerRepository dockerWrapper = new DockerRepository(DockerContainer.DockerCommand);
                DockerRepository dockerWrapper = new DockerRepository((string)typeof(T).GetProperty("DockerCommand").GetValue(null));
                dockerWrapper.Exited += DockerList_Exited;
                dockerWrapper.OutputDataReceived += DockerList_DataReceived;
                dockerWrapper.ErrorDataReceived += DockerList_DataReceived;
                dockerWrapper.BeginRun();
            }
            catch (Exception exception)
            {
                throw new Exception("Error running RunDockerList().", exception);
            }
        }

        void DockerList_DataReceived(object sender, DataEventArgs e)
        {

            Application.Current.Dispatcher.Invoke((Action)(() =>
            {

                if (e.Data != null && e.Data.Trim() != string.Empty)
                {
                    var result = DockerEntityStringParser.ParseDockerString<T>(e.Data);
                    TempDockerEntities.Add(result);
                }

            }));
        }

        void DockerList_Exited(object sender, EventArgs e)
        {

            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                DockerEntities = new ObservableCollection<T>(TempDockerEntities);
                //TempDockerEntities.Clear();

            }));

        }


    }
}
