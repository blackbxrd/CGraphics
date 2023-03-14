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
        int[,] array4dots;
        int[,] array4line;


        public Form1()
        {
            InitializeComponent();
            this.Width = 900;
            this.Height = 700; 
            int[,] array4dots = new int[bm.Width, bm.Height];
            int[,] array4line = new int[bm.Width, bm.Height];


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
            
            var c = new Point(bm.Width / 2, bm.Height / 2);
            var temp = new Bitmap(bm);
            var gr = Graphics.FromImage(bm);
            gr.Clear(Color.White);
            int angle = 90;

            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);


            
            
            
            
           
                
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
                    arX = e.X;
                    arY = e.Y;
                    g.DrawLine(p, px, py);
                    array4dots[arY, arX] = 1;
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

    

    }
}
