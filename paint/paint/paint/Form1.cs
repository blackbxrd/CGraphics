using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace paint
{


    public partial class Form1 : Form
    {
      

        Bitmap bm;
        Graphics g;
        bool paint = false;
        Point px, py;
        Pen p = new Pen(Color.Black, 1);
        int index;
        int x, y, cX, cY, arX, arY;
        


        public Form1()
        {
            InitializeComponent();
            this.Width = 900;
            this.Height = 700; 
            


            bm = new Bitmap(pic.Width, pic.Height);
            
            this.DoubleBuffered = true;
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            pic.Image = bm;



        }

        





        private void button3_Click(object sender, EventArgs e)
        {
            index = 2;
        }

        private void pic_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (paint)
            {
                if (index == 2)
                {
                    g.DrawLine(p, cX, cY, x, y);
                }
            }
           
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
          //  var c = new Point(bm.Width / 2, bm.Height / 2);
          //  var temp = new Bitmap(bm);
          //  var gr = Graphics.FromImage(bm);
         //   gr.Clear(Color.White);
          //  int angle = 90;

          //  double sin = Math.Sin(angle);
           // double cos = Math.Cos(angle);


            
            
            
            
           
                
           // gr.DrawImage(temp, Point.Empty);
            RotatePixels(45);


        }

        private void pic_Click(object sender, EventArgs e)
        {

        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            py = e.Location;

            cX = e.X;
            cY = e.Y;

        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if(paint)
            {
                if(index==1)
                {
                    px = e.Location;
                    arX = e.X;
                    arY = e.Y;
                    g.DrawLine(p, px, py);
                    
                    py = px;
                }
            }
            pic.Refresh();

            x = e.X;
            y = e.Y;
            
       
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;

            x = e.X;
            y = e.Y;

            if (index==2)
            {
                g.DrawLine(p, cX, cY, x, y);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            index = 1;

        }

        private void RotatePixels(double angle)
        {
            
            int width = pic.Width;
            int height = pic.Height;

            
            Bitmap rotatedBitmap = new Bitmap(width, height);

            
            double centerX = width / 2.0;
            double centerY = height / 2.0;

            
            double radians = angle * Math.PI / 180.0;

           
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);

            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Вычисляем координаты пикселя относительно центра PictureBox
                    double translatedX = x - centerX;
                    double translatedY = y - centerY;

                    // Вычисляем новые координаты пикселя после поворота
                    double rotatedX = translatedX * cos - translatedY * sin + centerX;
                    double rotatedY = translatedX * sin + translatedY * cos + centerY;

                    // Проверяем, что новые координаты находятся в пределах PictureBox
                    if (rotatedX >= 0 && rotatedX < width && rotatedY >= 0 && rotatedY < height)
                    {
                        
                        Color color = ((Bitmap)pic.Image).GetPixel(x, y);

                        
                        rotatedBitmap.SetPixel((int)rotatedX, (int)rotatedY, color);
                    }
                }
            }

            
            pic.Image = rotatedBitmap;
        }


    }
}
