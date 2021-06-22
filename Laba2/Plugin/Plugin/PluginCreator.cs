using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin;

namespace MainSahpeClass
{
    public class PluginCreator : IShapesCreator
    {
        public Shapes CreateShape()
        {
            return new Plugins();
        }
    }
}
