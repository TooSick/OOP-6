using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace MainSahpeClass
{
    [DataContract]
    class Polygon : Polyline
    {
        [DataMember]
        private bool PolygonDrawn;

        public Polygon()
        {
            PolygonDrawn = false;
        }

        public override void PolygonStopDraw()
        {
            DrawLastLineInPolygon();
        }

        public override void SetSecondPointOrWhidthAndHeight(float x2, float y2, bool drawn)
        {
            pointF[1].X = x2;
            pointF[1].Y = y2;
            if (drawn)
            {
                AddPointsInPointsList();
                AddPointsToPointFs();
            }
        }

        public void DrawLastLineInPolygon()
        {
            if (pointFsList.Count > 1)
            {
                SetFirstPoints(pointFsList[pointFsList.Count - 1].X, pointFsList[pointFsList.Count - 1].Y);
                SetSecondPointOrWhidthAndHeight(pointFsList[0].X, pointFsList[0].Y, true);
                PolygonDrawn = true;
            }
        }

        public void AddPointsToPointFs()
        {
            Array.Resize<PointF>(ref pointF, pointFsList.Count);
            for (int i = 0; i < pointFsList.Count; i++)
            {
                pointF[i].X = pointFsList[i].X;
                pointF[i].Y = pointFsList[i].Y;
            }
        }

        public override void DrawShapes(Graphics g)
        {
            pen = new Pen(lineColor);
            pen.Width = lineSize;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            brush = new SolidBrush(shapesBrush);

            if (PolygonDrawn)
            {
                g.FillPolygon(brush, pointF);
                g.DrawPolygon(pen, pointF);
            }
            else
            {
                if (pointFsList.Count != 0)
                    for (int i = 0; i < pointFsList.Count - 1; i++)
                        g.DrawLine(pen, pointFsList[i].X, pointFsList[i].Y, pointFsList[i + 1].X, pointFsList[i + 1].Y);
                g.DrawLine(pen, pointF[0].X, pointF[0].Y, pointF[1].X, pointF[1].Y);
            }
        }
    }
}
