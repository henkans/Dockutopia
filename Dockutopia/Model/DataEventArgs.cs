using System;

namespace Dockutopia.Model
{
    public class DataEventArgs : EventArgs
    {
        public string Data { get; private set; }

        public DataEventArgs(string data)
        {
            Data = data;
        }
    }
}
