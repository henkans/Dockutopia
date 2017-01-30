using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Dockutopia.Foundation;
using Dockutopia.Model;
using Dockutopia.Utils;
using Dockutopia.ViewModels;
using Dockutopia.Wrapper;

namespace Dockutopia.Handlers
{
    public class DockerHandler : ViewModelBase
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

        //private void RunDockerContainerList()
        //{
        //    DockerContainers.Clear();

        //    try
        //    {
        //        // Run command...
        //        DockerWrapper dockerWrapper = new DockerWrapper(DockerContainer.Format);
        //        dockerWrapper.Exited += DockerWrapper_Exited;
        //        dockerWrapper.OutputDataReceived += DockerContainerList_DataReceived;
        //        dockerWrapper.ErrorDataReceived += DockerContainerList_DataReceived;
        //        dockerWrapper.BeginRun();
        //    }
        //    catch (Exception exception)
        //    {
        //        OutputText = exception.ToString();
        //    }
        //}


        //Run docker command

        private void RunCommand(object inputCommand)
        {
            var command = inputCommand as string;
            command = StringHelper.RemoveDockerFirstOccurrence(command);

            //input handlar

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
        //Run docker command with format output



    }
}
