using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Graphics g;
        

        SolidBrush black = new SolidBrush(Color.White);

        int x, y, a, b;

        void putpixel(int x, int y, SolidBrush color) // Рисование пикселя
        {
            g.FillRectangle(color, x, y, 1, 1);
        }
        void pixel4(int x, int y, int x2, int y2, SolidBrush color) // Рисование пикселя для первого квадранта, и, симметрично, для остальных
        {
            putpixel(x + x2, y + y2, color);
            putpixel(x + x2, y - y2, color);
            putpixel(x - x2, y - y2, color);
            putpixel(x - x2, y + y2, color);
        }

        void draw_ellipse(int x, int y, int a, int b, SolidBrush color)
        {
            
            int x2 = 0; // Компонента x
            int y2 = b; // Компонента y
            int a_sqr = a * a; // a^2, a - большая полуось
            int b_sqr = b * b; // b^2, b - малая полуось
            int delta = 4 * b_sqr * ((x2 + 1) * (x2 + 1)) + a_sqr * ((2 * y2 - 1) * (2 * y2 - 1)) - 4 * a_sqr * b_sqr; // Функция координат точки (x+1, y-1/2)
            while (a_sqr * (2 * y2 - 1) > 2 * b_sqr * (x2 + 1)) // Первая часть дуги
            {
                pixel4(x, y, x2, y2, color);
                if (delta < 0) // Переход по горизонтали
                {
                    x2++;
                    delta += 4 * b_sqr * (2 * x2 + 3);
                }
                else // Переход по диагонали
                {
                    x2++;
                    delta = delta - 8 * a_sqr * (y2 - 1) + 4 * b_sqr * (2 * x2 + 3);
                    y2--;
                }
            }
            delta = b_sqr * ((2 * x2 + 1) * (2 * x2 + 1)) + 4 * a_sqr * ((y2 + 1) * (y2 + 1)) - 4 * a_sqr * b_sqr; // Функция координат точки (x+1/2, y-1)
            while (y2 + 1 != 0) // Вторая часть дуги, если не выполняется условие первого цикла, значит выполняется a^2(2y - 1) <= 2b^2(x + 1)
            {
                pixel4(x, y, x2, y2, color);
                if (delta < 0) // Переход по вертикали
                {
                    y2--;
                    delta += 4 * a_sqr * (2 * y2 + 3);
                }
                else // Переход по диагонали
                {
                    y2--;
                    delta = delta - 8 * b_sqr * (x2 + 1) + 4 * a_sqr * (2 * y2 + 3);
                    x2++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g = Graphics.FromHwnd(pictureBox1.Handle);

           
                x = pictureBox1.Width / 2;

            
                y = pictureBox1.Height / 2;

            if (textBox3.Text != "")
                a = Convert.ToInt32(textBox3.Text);
            else
                a = 100;

            if (textBox4.Text != "")
                b = Convert.ToInt32(textBox4.Text);
            else
                b = 100;

            draw_ellipse(x, y, a, b, black);
        }
    }
}
