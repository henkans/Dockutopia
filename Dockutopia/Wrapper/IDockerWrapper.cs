using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dockutopia.Wrapper
{
    interface IDockerWrapper
    {
        void BeginRun();
        void WriteToStandardInput(string command);
    }
}
