using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Dockutopia.Foundation;
using Dockutopia.Model;
using Dockutopia.Repository;
using Dockutopia.Utils;

namespace Dockutopia.Handler
{

    public class DockerHandler : NotifyPropertyChangedBase
    {
  
        private DockerRepository dockerWrapper;

        public DockerHandler()
        {
            RunDockerCommand = new RelayCommand<object>(RunCommand);
            WriteToStandardInputCommand = new RelayCommand<object>(WriteToStandardInput);
            KillCommand = new RelayCommand(Kill);

            _outputDockerResponses = new ObservableCollection<DockerResponse>();
            OutputDockerResponses.Add(new DockerResponse(Version, "CadetBlue"));
        }
        

        public string Version => "Dockutopia [Version " + Assembly.GetEntryAssembly().GetName().Version.ToString() + "]" + Environment.NewLine;

        private ObservableCollection<DockerResponse> _outputDockerResponses;
        public ObservableCollection<DockerResponse> OutputDockerResponses
        {
            get { return _outputDockerResponses; }
            set
            {
                _outputDockerResponses = value;
                this.OnPropertyChanged();
            }
        }

        
        public ICommand RunDockerCommand { get; set; }

        private void RunCommand(object inputCommand)
        {
            var command = inputCommand as string;
            if (command == null) return;
            command = StringHelper.RemoveDockerFirstOccurrence(command);
            
            OutputDockerResponses.Add(new DockerResponse("> docker " + command + Environment.NewLine, "CadetBlue"));

            try
            {
                // Run command...
                dockerWrapper = new DockerRepository(command);
                dockerWrapper.Exited += DockerWrapper_Exited;
                dockerWrapper.OutputDataReceived += DockerWrapper_DataReceived;
                dockerWrapper.ErrorDataReceived += DockerWrapper_ErrorReceived;
                IsEnabled = false;
                dockerWrapper.BeginRun();
            }
            catch (Exception exception)
            {
                OutputDockerResponses.Add(new DockerResponse(exception.ToString(), "Coral"));
            }
        }

        public void DockerWrapper_DataReceived(object sender, DataEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (e.Data != null) {
                    OutputDockerResponses.Add(new DockerResponse(e.Data, "White"));
                }
            }));
        }

        public void DockerWrapper_ErrorReceived(object sender, DataEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (e.Data != null)
                {
                    OutputDockerResponses.Add(new DockerResponse(e.Data, "Coral"));
                }
            }));
        }

        public void DockerWrapper_Exited(object sender, EventArgs e)
        {
            IsEnabled = true;
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                OutputDockerResponses.Add(new DockerResponse(Environment.NewLine + "Debug: Command has finished executing." + Environment.NewLine, "Gray"));
            }));
        }
        
        public ICommand WriteToStandardInputCommand { get; set; }
        public void WriteToStandardInput(object command)
        {
            dockerWrapper.WriteToStandardInput(command as string);
        }

        public ICommand KillCommand { get; set; }
        public void Kill()
        {
            try
            {
                dockerWrapper.Kill();
            }
            catch (Exception) { }
        }

        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                this.OnPropertyChanged();
            }
        }

    }
}
