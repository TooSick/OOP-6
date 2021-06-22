using System.Collections.Generic;
using System.Drawing;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace MainSahpeClass
{
    [DataContract]
    class Polyline : Shapes
    {
        [DataMember]
        protected List<PointF> pointFsList;
        [DataMember]
        protected PointF[] pointF;

        public Polyline()
        {
            pointFsList = new List<PointF>();
            pointF = new PointF[2];
            for (int i = 0; i < 2; i++)
                pointF[i] = new PointF();
        }

        public override void SetFirstPoints(float x1, float y1)
        {
            if (pointFsList.Count == 0)
            {
                pointF[0].X = x1;
                pointF[0].Y = y1;
            }
            else
            {
                pointF[0].X = pointFsList[pointFsList.Count - 1].X;
                pointF[0].Y = pointFsList[pointFsList.Count - 1].Y;
            }
        }

        public override void DrawShapes(Graphics g)
        {
            pen = new Pen(lineColor);
            pen.Width = lineSize;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;

            if (pointFsList.Count != 0)
                for (int i = 0; i < pointFsList.Count - 1; i++)
                    g.DrawLine(pen, pointFsList[i].X, pointFsList[i].Y, pointFsList[i + 1].X, pointFsList[i + 1].Y);
            g.DrawLine(pen, pointF[0].X, pointF[0].Y, pointF[1].X, pointF[1].Y);
        }

        public override void SetSecondPointOrWhidthAndHeight(float x2, float y2, bool drawn)
        {
            pointF[1].X = x2;
            pointF[1].Y = y2;
            if (drawn)
                AddPointsInPointsList();
        }

        public void AddPointsInPointsList()
        {
            for (int i = 0; i < 2; i++)
                pointFsList.Add(pointF[i]);
        }

        public override bool PolylineOrPolygonDrawn()
        {
            return true;
        }
    }
}
