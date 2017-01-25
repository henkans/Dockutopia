using System;
using System.Windows.Input;
using Dockutopia.Foundation;
using Dockutopia.Model;
using Dockutopia.Utils;
using Dockutopia.Wrapper;

namespace Dockutopia.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel()
        {
            RunDockerCommand = new RelayCommand<object>(RunDocker);
        }

        private string _outputText;

        public string OutputText
        {
            get { return _outputText; }
            set
            {
                _outputText = value;
                this.OnPropertyChanged();
            }
        }

        public ICommand RunDockerCommand { get; set; }

        private void RunDocker(object inputCommand)
        {
            var command = inputCommand as string;
            command = StringHelper.RemoveDockerFirstOccurrence(command);
            OutputText += ">>> docker " + command + Environment.NewLine + Environment.NewLine;

            try
            {
                // Run command...
                DockerWrapper dockerWrapper = new DockerWrapper(command);
                dockerWrapper.Exited += DockerWrapper_Exited;
                dockerWrapper.OutputDataReceived += DockerWrapper_DataReceived;
                dockerWrapper.ErrorDataReceived += DockerWrapper_DataReceived;
                dockerWrapper.BeginRun();
            }
            catch (Exception exception)
            {
                OutputText = exception.ToString();
            }
        }

        void DockerWrapper_DataReceived(object sender, DataEventArgs e)
        {
            OutputText += (e.Data + Environment.NewLine);
        }

        void DockerWrapper_Exited(object sender, EventArgs e)
        {
           // OutputText += ("Command has finished executing." + Environment.NewLine);
        }

    }
}
