using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainSahpeClass;

namespace Plugin
{
    public class Plugins : Shapes
    {
        private PointF[] pointsF;
        private PointF tempPoint;

        public Plugins()
        {
            pointsF = new PointF[4];
            for (int i = 0; i < 4; i++)
                pointsF[i] = new PointF();
            tempPoint = new PointF();
        }

        public override void DrawShapes(Graphics g)
        {
            pen = new Pen(lineColor);
            pen.Width = lineSize;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            brush = new SolidBrush(shapesBrush);

            pointsF[1].X = pointsF[0].X + pointsF[2].Y - pointsF[0].Y;
            pointsF[1].Y = pointsF[2].Y;
            pointsF[3].X = pointsF[2].X + pointsF[2].Y - pointsF[0].Y;
            pointsF[3].Y = pointsF[0].Y;

            g.FillPolygon(brush, pointsF);
            g.DrawPolygon(pen, pointsF);
        }

        public override bool PolylineOrPolygonDrawn()
        {
            return false;
        }

        public override void SetFirstPoints(float x1, float y1)
        {
            pointsF[0].X = x1;
            pointsF[0].Y = y1;
        }

        public override void SetSecondPointOrWhidthAndHeight(float x2, float y2, bool drawn)
        {
            pointsF[2].X = x2;
            pointsF[2].Y = y2;
        }
    }
}
