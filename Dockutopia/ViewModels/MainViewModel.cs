using System;
using System.Collections.Generic;
using System.Linq;
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
            GetPreviousInputCommand = new RelayCommand(GetPreviousInput);
            GetNextInputCommand = new RelayCommand(GetNextInput);
            _inputStack = new Stack<string>();
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

        private string _commandText;
        public string CommandText
        {
            get { return _commandText; }
            set
            {
                _commandText = value;
                this.OnPropertyChanged();
            }
        }


        private int inputIndex = -1;
        private static Stack<string> _inputStack;
        public ICommand GetPreviousInputCommand { get; set; }
        private void GetPreviousInput()
        {
            if (_inputStack.Count > (inputIndex + 1))
            {
                CommandText = _inputStack.ElementAt(inputIndex + 1);
                inputIndex++;
            }
        }

        public ICommand GetNextInputCommand { get; set; }
        private void GetNextInput()
        {
            if (0 < inputIndex)
            {
                CommandText = _inputStack.ElementAt(inputIndex - 1);
                inputIndex--;
            }
        }


        public ICommand RunDockerCommand { get; set; }

        private void RunDocker(object inputCommand)
        {
            var command = inputCommand as string;
            command = StringHelper.RemoveDockerFirstOccurrence(command);

            _inputStack.Push(command);
            CommandText = string.Empty;
            inputIndex = -1;

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
