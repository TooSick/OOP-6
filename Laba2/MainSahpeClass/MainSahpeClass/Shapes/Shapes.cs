using System.Drawing;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace MainSahpeClass
{
    [DataContract]
    [KnownType(typeof(Line))]
    [KnownType(typeof(Ellipse))]
    [KnownType(typeof(Rectangle))]
    [KnownType(typeof(Polyline))]
    [KnownType(typeof(Polygon))]

    public abstract class Shapes
    {
        protected Pen pen;
        protected SolidBrush brush;

        [DataMember]
        protected int lineSize;

        [DataMember]
        protected Color lineColor;

        [DataMember]
        protected Color shapesBrush;

        abstract public void DrawShapes(Graphics g);
        abstract public void SetFirstPoints(float x1, float y1);
        abstract public void SetSecondPointOrWhidthAndHeight(float x2, float y2, bool drawn);
        abstract public bool PolylineOrPolygonDrawn();

        public void SetPenColorAndLineSize(Pen pen, int lineSize)
        {
            lineColor = pen.Color;
            this.lineSize = lineSize;
        }

        public void SetFillColor(SolidBrush brush)
        {
            shapesBrush = brush.Color;
        }

        public SolidBrush GetFillColor()
        {
            return brush;
        }

        public virtual void PolygonStopDraw()
        {

        }
    }
}
