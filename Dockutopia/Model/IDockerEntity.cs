using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dockutopia.Model
{
    public interface IDockerEntity
    {
        string ID { get; set; }
        string Name { get; set; }

    }
}
