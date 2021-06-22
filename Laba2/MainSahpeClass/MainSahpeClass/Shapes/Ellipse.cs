using System.Drawing;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace MainSahpeClass
{
    [DataContract]
    class Ellipse : Rectangle
    {
        public override void DrawShapes(Graphics g)
        {
            pen = new Pen(lineColor);
            pen.Width = lineSize;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            brush = new SolidBrush(shapesBrush);

            if (x2 >= x1 && y2 >= y1)
            {
                g.FillEllipse(brush, x1, y1, widthRect, heithRect);
                g.DrawEllipse(pen, x1, y1, widthRect, heithRect);
            }
            else if (x2 >= x1 && y2 < y1)
            {
                g.FillEllipse(brush, x1, y2, widthRect, heithRect);
                g.DrawEllipse(pen, x1, y2, widthRect, heithRect);
            }
            else if (x2 < x1 && y2 < y1)
            {
                g.FillEllipse(brush, x2, y2, widthRect, heithRect);
                g.DrawEllipse(pen, x2, y2, widthRect, heithRect);
            }
            else if (x2 < x1 && y2 >= y1)
            {
                g.FillEllipse(brush, x2, y1, widthRect, heithRect);
                g.DrawEllipse(pen, x2, y1, widthRect, heithRect);
            }
        }
    }
}
