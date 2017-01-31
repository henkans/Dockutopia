﻿using System;
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

        public ICommand RunDockerContainerListCommand { get; set; }
        private void RunDockerContainerList()
        {
            DockerContainers.Clear();

            try
            {
                // Run command...
                DockerRepository dockerWrapper = new DockerRepository(DockerContainer.Format);
                //dockerWrapper.Exited += DockerWrapper_Exited;
                dockerWrapper.OutputDataReceived += DockerContainerList_DataReceived;
                dockerWrapper.ErrorDataReceived += DockerContainerList_DataReceived;
                dockerWrapper.BeginRun();
            }
            catch (Exception exception)
            {
                // OutputText = exception.ToString();
            }
        }
        void DockerContainerList_DataReceived(object sender, DataEventArgs e)
        {

            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                if (e.Data != null && e.Data.Trim() != string.Empty)
                {
                    var result = ContainerStringParser.ParseDockerContainerString(e.Data);
                    DockerContainers.Add(result);
                }

            }));

        }
    }
}