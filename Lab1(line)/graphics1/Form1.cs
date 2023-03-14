using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphics1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private Graphics g = null;
        private Graphics g1 = null;
        private int pixelSize = 5;
        void PutPixel(Graphics g, int x, int y, int size, Color c, int alpha = 255)
        {
           

            SolidBrush myBrush = new SolidBrush(Color.FromArgb(alpha,c));
            g.FillRectangle(myBrush, new Rectangle(new Point() { X = x, Y = y }, new Size() {  Width = size, Height = size }));
            
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            long pointX1, pointY1, pointX2, pointY2;
            pointX1 = Convert.ToInt64(textBox1.Text);
            pointY1 = Convert.ToInt64(textBox2.Text);
            pointX2 = Convert.ToInt64(textBox3.Text);
            pointY2 = Convert.ToInt64(textBox4.Text);
            
            if (g != null)
                g.Clear(Color.White);
            if (g1 != null)
                g1.Clear(Color.White);

            g = this.pictureBoxMain.CreateGraphics();
            g1 = this.pictureBox1.CreateGraphics();
            pixelSize = Convert.ToInt16(this.numericUpDown1.Text);
            if (pixelSize > 0)
            {
                
                NonSymDDALine(g, (int) pointX1, (int)pointY1, (int)pointX2, (int)pointY2, pixelSize, Color.Red);
                BrezengamLine(g1, (int) pointX1, (int)pointY1, (int)pointX2, (int)pointY2, pixelSize, Color.Blue);
                
                


            }
        }

        private int RoundToPixelSize(double d, int size)
        {
            int i = Convert.ToInt16(d);
            if (i % size != 0) i = (i / size) * size + size;
            return i;
        }

    

        private void NonSymDDALine(Graphics g, int x1, int y1, int x2, int y2, int size, Color col)
        {
             x1 = x1 * size;
             y1 = y1 * size;
             x2 = x2 * size;
             y2 = y2 * size;

            int dx;
            int dy;
            int s;
            if (x1 > x2)
            {
                s = x1; x1 = x2; x2 = s;
                s = y1; y1 = y2; y2 = s;
            }
            dx = x2 - x1; dy = y2 - y1;
            PutPixel(g, (int)x1, (int)y1, size, col);
            if (dx == 0 && dy == 0) return;

            /* Вычисление количества позиций по X и Y */
            dx = dx + size; dy = dy + size;

            if (dy == dx)
            {                 /* Наклон == 45 градусов */
                while (x1 < x2)
                {
                    x1 = x1 + size;
                    y1 = y1 + size;
                    PutPixel(g, RoundToPixelSize(x1, size), RoundToPixelSize(y1, size), size, col);
                }
            }
            else if (dx > dy)
            {           /* Наклон <  45 градусов */
                s = 0;
                while (x1 < x2)
                {
                    x1 = x1 + size;
                    s = s + dy;
                    if (s >= dx) { s = s - dx; y1 = y1 + size; }
                    PutPixel(g, RoundToPixelSize(x1, size), RoundToPixelSize(y1, size), size, col);
                }
            }
            else
            {                        /* Наклон >  45 градусов */
                s = 0;
                while (y1 < y2)
                {
                    y1 = y1 + size;
                    s = s + dx;
                    if (s >= dy) { s = s - dy; x1 = x1 + size; }
                    PutPixel(g, RoundToPixelSize(x1, size), RoundToPixelSize(y1, size), size, col);
                }
            }
        }

        private void BrezengamLine(Graphics g1, int x1, int y1, int x2, int y2, int size, Color col)
        {
             x1 = x1 * size;
             y1 = y1 * size;
             x2 = x2 * size;
             y2 = y2 * size;

            int dx, dy, s, sx, sy, kl, swap, incr1, incr2;
            sx = 0;
            /* Вычисление приращений и шагов */
            if ((dx = x2 - x1) < 0) { dx = -dx; sx -= size; } else if (dx > 0) sx += size;
            sy = 0;
            if ((dy = y2 - y1) < 0) { dy = -dy; sy -= size; } else if (dy > 0) sy += size;

            /* Учет наклона */
            swap = 0;
            if ((kl = dx) < (s = dy))
            {
                dx = s; dy = kl; kl = s; ++swap;
            }
            s = (incr1 = 2 * dy) - dx; /* incr1 - констан. перевычисления */
            /* разности если текущее s < 0  и  */
            /* s - начальное значение разности */
            incr2 = 2 * dx;         /* Константа для перевычисления    */
            /* разности если текущее s >= 0    */
            /* Первый  пиксел вектора       */
            PutPixel(g1, RoundToPixelSize(x1, size), RoundToPixelSize(y1, size), size, col);
            while ((kl -= size) >= 0)
            {
                if (s >= 0)
                {
                    if (swap > 0) x1 += sx; else y1 += sy;
                    s -= incr2;
                }
                if (swap > 0) y1 += sy; else x1 += sx;
                s += incr1;
                PutPixel(g1, RoundToPixelSize(x1, size), RoundToPixelSize(y1, size), size, col); /* Текущая  точка  вектора   */
            }
        }

      



       

    }
}
