using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Dockutopia.Foundation;

namespace Dockutopia.Handler
{
    public class PreviousCommandHandler : NotifyPropertyChangedBase

{
        public PreviousCommandHandler()
        {
            GetPreviousInputCommand = new RelayCommand(GetPreviousInput);
            GetNextInputCommand = new RelayCommand(GetNextInput);
            AddInputCommand = new RelayCommand<string>(AddInput);
            _inputStack = new Stack<string>();
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


        public ICommand AddInputCommand { get; set; }

        private void AddInput(object input)
        {
            var command = input as string;
            // lastinputhandlar!!!!!
            if (_inputStack.Count == 0 || _inputStack.Peek() != command)
            {
                _inputStack.Push(command);
            }
            CommandText = string.Empty;
            inputIndex = -1;
        }

    }
}
