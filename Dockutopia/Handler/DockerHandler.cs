using System;
using System.Windows.Input;
using Dockutopia.Foundation;
using Dockutopia.Model;
using Dockutopia.Repository;
using Dockutopia.Utils;

namespace Dockutopia.Handler
{

    public class DockerHandler : NotifyPropertyChangedBase, IDockerHandler
    {
        private string _outputText;

        public DockerHandler()
        {
            RunDockerCommand = new RelayCommand<object>(RunCommand);
        }
        
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

        private void RunCommand(object inputCommand)
        {
            var command = inputCommand as string;
            command = StringHelper.RemoveDockerFirstOccurrence(command);

            OutputText += ">>> docker " + command + Environment.NewLine + Environment.NewLine;

            try
            {
                // Run command...
                DockerRepository dockerWrapper = new DockerRepository(command);
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

        public void DockerWrapper_DataReceived(object sender, DataEventArgs e)
        {
            OutputText += (e.Data + Environment.NewLine);
        }

        public void DockerWrapper_Exited(object sender, EventArgs e)
        {
            // OutputText += ("Command has finished executing." + Environment.NewLine);
        }
        //Run docker command with format output



    }
}
