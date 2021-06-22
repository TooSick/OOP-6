using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSahpeClass
{
    public class EllipseCreator : IShapesCreator
    {
        public Shapes CreateShape()
        {
            return new Ellipse();
        }
    }
}
