using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
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
            RunDockerContainerListCommand = new RelayCommand(RunDockerContainerList);
            RunDockerImagesListCommand = new RelayCommand(RunDockerImagesList);
            _inputStack = new Stack<string>();
            _dockerContainers = new ObservableCollection<DockerContainer>();
            _dockerImages = new ObservableCollection<DockerImage>();
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


        public DockerContainer SelectedDockerContainer { get; set; }

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
        

        // -------------------- Image List
        public ICommand RunDockerImagesListCommand { get; set; }
        private void RunDockerImagesList()
        {
            DockerImages.Clear();

            try
            {
                // Run command...
                DockerWrapper dockerWrapper = new DockerWrapper(DockerImage.Command);
                dockerWrapper.Exited += DockerWrapper_Exited;
                dockerWrapper.OutputDataReceived += DockerImageList_DataReceived;
                dockerWrapper.ErrorDataReceived += DockerImageList_DataReceived;
                dockerWrapper.BeginRun();
            }
            catch (Exception exception)
            {
                OutputText = exception.ToString();
            }
        }
        void DockerImageList_DataReceived(object sender, DataEventArgs e)
        {

            Application.Current.Dispatcher.Invoke((Action)(() =>
            {

                if (e.Data != null && e.Data.Trim() != string.Empty)
                {
                    var result = ContainerStringParser.ParseDockerImageString(e.Data);
                    DockerImages.Add(result);
                }

            }));
        }

        // -------------------- Container List
        public ICommand RunDockerContainerListCommand { get; set; }
        private void RunDockerContainerList()
        {
            DockerContainers.Clear();

            try
            {
                // Run command...
                DockerWrapper dockerWrapper = new DockerWrapper(DockerContainer.Format);
                dockerWrapper.Exited += DockerWrapper_Exited;
                dockerWrapper.OutputDataReceived += DockerContainerList_DataReceived;
                dockerWrapper.ErrorDataReceived += DockerContainerList_DataReceived;
                dockerWrapper.BeginRun();
            }
            catch (Exception exception)
            {
                OutputText = exception.ToString();
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

        public ICommand RunDockerCommand { get; set; }

        private void RunDocker(object inputCommand)
        {
            var command = inputCommand as string;
            command = StringHelper.RemoveDockerFirstOccurrence(command);

            if (_inputStack.Count == 0 || _inputStack.Peek() != command)
            {
                _inputStack.Push(command);
            }
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
