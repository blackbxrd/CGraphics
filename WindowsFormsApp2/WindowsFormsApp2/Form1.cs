using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private bool mouseDown;
        private Point lastLocation;
        private int rotationX;
        private int rotationY;
        private int rotationZ;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rotationX = 0;
            rotationY = 0;
            rotationZ = 0;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                int dx = e.Location.X - lastLocation.X;
                int dy = e.Location.Y - lastLocation.Y;

                if (Math.Abs(dx) > Math.Abs(dy))
                {
                    rotationY += dx;
                }
                else
                {
                    rotationX += dy;
                }

                lastLocation = e.Location;
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            Point center = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);
            int size = 100;

            // куб рисуем
            Point3D[] points = new Point3D[8];
            points[0] = new Point3D(-size, -size, -size);
            points[1] = new Point3D(-size, size, -size);
            points[2] = new Point3D(size, size, -size);
            points[3] = new Point3D(size, -size, -size);
            points[4] = new Point3D(-size, -size, size);
            points[5] = new Point3D(-size, size, size);
            points[6] = new Point3D(size, size, size);
            points[7] = new Point3D(size, -size, size);

            Point[] screenPoints = new Point[8];

            for (int i = 0; i < 8; i++)
            {
                Point3D rotatedPoint = RotatePoint(points[i], rotationX, rotationY, rotationZ);
                screenPoints[i] = ProjectPoint(rotatedPoint, center);
            }

            Pen pen = new Pen(Color.Black);

            e.Graphics.DrawLine(pen, screenPoints[0], screenPoints[1]);
            e.Graphics.DrawLine(pen, screenPoints[1], screenPoints[2]);
            e.Graphics.DrawLine(pen, screenPoints[2], screenPoints[3]);
            e.Graphics.DrawLine(pen, screenPoints[3], screenPoints[0]);

            e.Graphics.DrawLine(pen, screenPoints[0], screenPoints[4]);
            e.Graphics.DrawLine(pen, screenPoints[1], screenPoints[5]);
            e.Graphics.DrawLine(pen, screenPoints[2], screenPoints[6]);
            e.Graphics.DrawLine(pen, screenPoints[3], screenPoints[7]);

            e.Graphics.DrawLine(pen, screenPoints[4], screenPoints[5]);
            e.Graphics.DrawLine(pen, screenPoints[5], screenPoints[6]);
            e.Graphics.DrawLine(pen, screenPoints[6], screenPoints[7]);
            e.Graphics.DrawLine(pen, screenPoints[7], screenPoints[4]);
        }

        private Point3D RotatePoint(Point3D point, int angleX, int angleY, int angleZ)
        {
            double radiansX = angleX * Math.PI / 180.0;
            double radiansY = angleY * Math.PI / 180.0;
            double radiansZ = angleZ * Math.PI / 180.0;

            double cosX = Math.Cos(radiansX);
            double sinX = Math.Sin(radiansX);
            double cosY = Math.Cos(radiansY);
            double sinY = Math.Sin(radiansY);
            double cosZ = Math.Cos(radiansZ);
            double sinZ = Math.Sin(radiansZ);

            double x = point.X;
            double y = point.Y;
            double z = point.Z;

            // поворот по оси X
            double newX = x;
            double newY = cosX * y - sinX * z;
            double newZ = sinX * y + cosX * z;

            x = newX;
            y = newY;
            z = newZ;

            // поворот по оси Y
            // 
            newX = cosY * x + sinY * z;
            newY = y;
            newZ = -sinY * x + cosY * z;

            x = newX;
            y = newY;
            z = newZ;

            // поворот по оси Z
            // 
            newX = cosZ * x - sinZ * y;
            newY = sinZ * x + cosZ * y;
            newZ = z;

            return new Point3D(newX, newY, newZ);
        }

        private Point ProjectPoint(Point3D point, Point center)
        {
            double x = point.X ;
            double y = point.Y ;

            return new Point(center.X + (int)x, center.Y - (int)y);
        }

        public class Point3D
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }

            public Point3D(double x, double y, double z)
            {
                X = x;
                Y = y;
                Z = z;
            }
        }
    }
}

