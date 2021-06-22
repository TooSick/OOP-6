using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using MainSahpeClass;

namespace Laba2
{
    public partial class Form1 : Form
    {
        private DataContractJsonSerializer jsonSerializer;
        private List<Shapes> allShapes;
        private Shapes shapes;
        private IShapesCreator shapesCreator;
        private Graphics g;
        private bool buttonIsPressed, drawn, polylineOrPolygonStartDraw;
        private Pen pen;
        private SolidBrush brush;
        private Button shapesButtons;
        private int countAllShapes;

        public Form1()
        {
            InitializeComponent();
            allShapes = new List<Shapes>();
            drawn = buttonIsPressed = polylineOrPolygonStartDraw = false;
            pen = new Pen(Color.Black);
            brush = new SolidBrush(Color.White);
            countAllShapes = 0;
            jsonSerializer = new DataContractJsonSerializer(typeof(List<Shapes>));
        }

        private void lineButton_Click(object sender, EventArgs e)
        {
            if (buttonIsPressed == false || shapesButtons.Name != lineButton.Name)
            {
                shapes = new LineCreator().CreateShape();
                shapesCreator = new LineCreator();

                buttonIsPressed = true;
                shapesButtons = (Button)sender;
            }
            else if (buttonIsPressed && shapesButtons.Name == lineButton.Name)
                buttonIsPressed = false;
        }

        private void rectangleButton_Click(object sender, EventArgs e)
        {
            if (buttonIsPressed == false || shapesButtons.Name != rectangleButton.Name)
            {
                shapes = new RectangleCreator().CreateShape();
                shapesCreator = new RectangleCreator();
                buttonIsPressed = true;
                shapesButtons = (Button)sender;
            }
            else if (buttonIsPressed && shapesButtons.Name == rectangleButton.Name)
                buttonIsPressed = false;
        }

        private void ellipseButton_Click(object sender, EventArgs e)
        {
            if (buttonIsPressed == false || shapesButtons.Name != ellipseButton.Name)
            {
                shapes = new EllipseCreator().CreateShape();
                shapesCreator = new EllipseCreator();
                buttonIsPressed = true;
                shapesButtons = (Button)sender;
            }
            else if (buttonIsPressed && shapesButtons.Name == ellipseButton.Name)
                buttonIsPressed = false;
        }

        private void polygonButton_Click(object sender, EventArgs e)
        {
            if (buttonIsPressed == false || shapesButtons.Name != polygonButton.Name)
            {
                shapes = new PolygonCreator().CreateShape();
                shapesCreator = new PolygonCreator();
                buttonIsPressed = true;
                polylineOrPolygonStartDraw = false;
                shapesButtons = (Button)sender;
            }
            else if (buttonIsPressed && shapesButtons.Name == polygonButton.Name)
                buttonIsPressed = false;
        }

        private void polylineButton_Click(object sender, EventArgs e)
        {
            if (buttonIsPressed == false || shapesButtons.Name != polylineButton.Name)
            {
                shapes = new PolylineCreator().CreateShape();
                shapesCreator = new PolylineCreator();
                buttonIsPressed = true;
                polylineOrPolygonStartDraw = false;
                shapesButtons = (Button)sender;
            }
            else if (buttonIsPressed && shapesButtons.Name == polylineButton.Name)
                buttonIsPressed = false;
        }

        private void pluginButton_Click(object sender, EventArgs e)
        {
            if (buttonIsPressed == false || shapesButtons.Name != pluginButton.Name)
            {
                shapes = new PluginCreator().CreateShape();
                shapesCreator = new PluginCreator();
                buttonIsPressed = true;
                polylineOrPolygonStartDraw = false;
                shapesButtons = (Button)sender;
            }
            else if (buttonIsPressed && shapesButtons.Name == pluginButton.Name)
                buttonIsPressed = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (buttonIsPressed && e.Button == MouseButtons.Left)
            {
                if (countAllShapes != allShapes.Count)
                    for (int i = allShapes.Count - 1; i >= countAllShapes; --i)
                        allShapes.RemoveAt(i);
                shapes.SetFirstPoints(e.X, e.Y);
                drawn = true;
                shapes.SetPenColorAndLineSize(pen, lineSize.Value);
                shapes.SetFillColor(brush);
            }
            else if (buttonIsPressed && e.Button == MouseButtons.Right)
            {
                drawn = polylineOrPolygonStartDraw = false;
                shapes.PolygonStopDraw();
                shapes = shapesCreator.CreateShape();
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (buttonIsPressed && e.Button == MouseButtons.Left)
            {
                drawn = false;
                shapes.SetSecondPointOrWhidthAndHeight(e.X, e.Y, true);
                pictureBox1.Invalidate();
                if (shapes.PolylineOrPolygonDrawn() == false)
                {
                    allShapes.Add(shapes);
                    ++countAllShapes;
                    shapes = shapesCreator.CreateShape();
                }
                else if (polylineOrPolygonStartDraw == false)
                {
                    polylineOrPolygonStartDraw = true;
                    allShapes.Add(shapes);
                    ++countAllShapes;
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawn)
            {
                shapes.SetSecondPointOrWhidthAndHeight(e.X, e.Y, false);
                pictureBox1.Invalidate();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            allShapes.Clear();
            countAllShapes = 0;
            pictureBox1.Invalidate();
            buttonIsPressed = polylineOrPolygonStartDraw = false;
        }

        private void colorsButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            colorLineButton.BackColor = button.BackColor;
            pen = new Pen(colorLineButton.BackColor, lineSize.Value);
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            if (countAllShapes != 0)
            {
                --countAllShapes;
                pictureBox1.Invalidate();
            }
            buttonIsPressed = false;
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            if (countAllShapes != allShapes.Count && allShapes.Count != 0)
            {
                ++countAllShapes;
                pictureBox1.Invalidate();
            }
            buttonIsPressed = false;
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FileStream fs = new FileStream("shapes.json", FileMode.Create))
            {
                jsonSerializer.WriteObject(fs, allShapes);
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (FileStream fs = new FileStream("shapes.json", FileMode.Open))
                {
                    if (allShapes.Count == 0)
                        allShapes = (List<Shapes>)jsonSerializer.ReadObject(fs);
                    else
                    {
                        List<Shapes> tempList = new List<Shapes>();
                        tempList = (List<Shapes>)jsonSerializer.ReadObject(fs);
                        for (int i = 0; i < tempList.Count; i++)
                            allShapes.Add(tempList[i]);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка: нет сохранений!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                countAllShapes = allShapes.Count;
                pictureBox1.Invalidate();
            }
        }

        private void fillButtons_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            colorFillButton.BackColor = button.BackColor;
            brush = new SolidBrush(button.BackColor);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            for (int i = 0; i < countAllShapes; i++)
            {
                allShapes[i].DrawShapes(g);
            }
            if (drawn)
            {
                shapes.DrawShapes(g);
            }
        }
    }
}
