using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenSaver
{
    public partial class formScreenSaver : Form
    {
        List<Image> BGImages = new List<Image>();
        List<BritPic> Britpics = new List<BritPic>();
        Random rand = new Random();

        //holds position of picture
        class BritPic
        {
            public int PicNum;
            public float X;
            public float Y;
            public float Speed;
        }
        public formScreenSaver()
        {
            InitializeComponent();
        }

        //exits program by touching any key
        private void formScreenSaver_KeyDown(object sender, KeyEventArgs e)
        {
            Close();
        }

        private void formScreenSaver_Load(object sender, EventArgs e)
        {
            //get list of pics into images array
            string[] images = System.IO.Directory.GetFiles("pics");
            //loop through images
            foreach (string image in images)
            {
                //creates new bitmap picture and add to BGImages array
                BGImages.Add(new Bitmap(image));
            }

            //looks for images in directory and keep adding up to 50
            for (int i = 0; i < 50; ++i)
            {
                BritPic np = new BritPic();
                np.PicNum = i % BGImages.Count;
                np.X = rand.Next(0, Width);
                np.Y = rand.Next(0, Height);

                Britpics.Add(np);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //enables screen refresh for interval length
            Refresh();
        }

        private void formScreenSaver_Paint(object sender, PaintEventArgs e)
        {
            foreach(BritPic bp in Britpics)
            {
                //draws images on x,y axis
                e.Graphics.DrawImage(BGImages[bp.PicNum], bp.X, bp.Y);
                bp.X -= 2;

                //if object moves off screen, move it to right of screen
                if(bp.X < -250)
                {
                    bp.X = Width + rand.Next(20, 100);
                }
            }
        }
    }
}
