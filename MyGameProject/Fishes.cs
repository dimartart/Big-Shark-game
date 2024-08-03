using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameProject
{
    //creating the new class for fishes
    internal class Fishes
    {
        //here creating the public attribute for fish location
        //and also create a special bool variable for creating the logic of fish motion
        bool FishFaceToRight = true;
        public int fishleft;
        public int fishtop;
        //here the speed of our fish, picturebox for it and the inner timer
        private int fishspeed=3;
        public PictureBox Fish = new PictureBox();
        private System.Windows.Forms.Timer FishTimer = new System.Windows.Forms.Timer();
        
        //here is the function with initial values for our object
        public void MakeFish(Form1 form)
        {
            Fish.Tag = "fish_unit";
            Fish.Image = Properties.Resources.fish;
            Fish.Size = new Size(30, 30);
            Fish.Left = fishleft;
            Fish.Top = fishtop;
            Fish.SizeMode = PictureBoxSizeMode.StretchImage;
            Fish.BringToFront();

            form.Controls.Add(Fish);
            //here is logic for our timer
            FishTimer.Interval = fishspeed;
            FishTimer.Tick += new EventHandler(FishEvent);
            FishTimer.Start();
        }
        //here we have the event for our timer
        //here we immitate the motion of our fish
        //when it achieves the right side it returns and goes to the another side
        //also we change the image depending on direction of our fish
        private void FishEvent(object sender, EventArgs e)
        {
            
            if (Fish.Left < 714 && FishFaceToRight==true)
            {
                Fish.Left += fishspeed;
            }
            else if (Fish.Left >= 714)
            {
                Fish.Image=Properties.Resources.fishReserve;
                FishFaceToRight = false;
                Fish.Left = 713;
            }
            else if (FishFaceToRight == false && Fish.Left>0)
            {
                Fish.Left-= fishspeed;  
            }
            else if (Fish.Left<=0)
            {
                Fish.Image=Properties.Resources.fish;
                FishFaceToRight = true;
                Fish.Left = 1;
            }
           
        }
    }
}
