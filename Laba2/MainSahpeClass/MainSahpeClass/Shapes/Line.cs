using System.Drawing;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace MainSahpeClass
{
    [DataContract]
    class Line : Shapes
    {
        [DataMember]
        private float x1, y1, x2, y2;

        public override void DrawShapes(Graphics g)
        {
            pen = new Pen(lineColor);
            pen.Width = lineSize;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            g.DrawLine(pen, x1, y1, x2, y2);
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
        }
    }
}
