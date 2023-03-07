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
        int x, y, cX, cY;
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
            /*            g.TranslateTransform((float)bm.Width / 2, (float)bm.Height / 2);


                            g.RotateTransform(30);

                            g.TranslateTransform(-(float)bm.Width / 2, -(float)bm.Height / 2);

                            //set the InterpolationMode to HighQualityBicubic so to ensure a high
                            //quality image once it is transformed to the specified size
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                            //now draw our new image onto the graphics object
                            g.DrawImage(pic.Image, new Point(0, 0));

                            //dispose of our Graphics object
                            g.Dispose();

            */
            var c = new Point(bm.Width / 2, bm.Height / 2);
            var temp = new Bitmap(bm);
            var gr = Graphics.FromImage(bm);
            
            int angle=90;
                             
            var m = new Matrix();
            m.RotateAt(angle, c);
            gr.Transform = m;
                
            gr.DrawImage(temp, Point.Empty);
            


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

            

            if(index==2)
            {
                g.DrawLine(p, cX, cY, x, y);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            index = 1;

        }

    

    }
}
