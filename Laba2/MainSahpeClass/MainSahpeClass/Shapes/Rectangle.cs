using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace MainSahpeClass
{
    [DataContract]
    class Rectangle : Shapes
    {
        [DataMember]
        protected float x1, y1, x2, y2, widthRect, heithRect;

        public override void DrawShapes(Graphics g)
        {
            pen = new Pen(lineColor);
            pen.Width = lineSize;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            brush = new SolidBrush(shapesBrush);

            if (x2 >= x1 && y2 >= y1)
            {
                g.FillRectangle(brush, x1, y1, widthRect, heithRect);
                g.DrawRectangle(pen, x1, y1, widthRect, heithRect);
            }
            else if (x2 >= x1 && y2 < y1)
            {
                g.FillRectangle(brush, x1, y2, widthRect, heithRect);
                g.DrawRectangle(pen, x1, y2, widthRect, heithRect);
            }
            else if (x2 < x1 && y2 < y1)
            {
                g.FillRectangle(brush, x2, y2, widthRect, heithRect);
                g.DrawRectangle(pen, x2, y2, widthRect, heithRect);
            }
            else if (x2 < x1 && y2 >= y1)
            {
                g.FillRectangle(brush, x2, y1, widthRect, heithRect);
                g.DrawRectangle(pen, x2, y1, widthRect, heithRect);
            }
        }

        public override bool PolylineOrPolygonDrawn()
        {
            return false;
        }

        public override void SetFirstPoints(float x1, float y1)
        {
            this.x1 = x1;
            this.y1 = y1;
        }

        public override void SetSecondPointOrWhidthAndHeight(float x2, float y2, bool drawn)
        {
            this.x2 = x2;
            this.y2 = y2;
            widthRect = Math.Abs(x1 - x2);
            heithRect = Math.Abs(y1 - y2);
        }
    }
}
