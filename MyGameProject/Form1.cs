using System;
using System.Security.AccessControl;

namespace MyGameProject
{
    public partial class Form1 : Form
    {
        //add the random module for creating random location of new fishes
        //add the rectangle class for purpose of imitation the opening mouth of shark
        //add size value, we will use it like a scores
        Random randnum=new Random();
        Rectangle Rect= new Rectangle();
        int size;
        public Form1()
        {
            InitializeComponent();
        }

        //here add the possibility for user to control the shark
        //and also make the borders for our shark unit
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                if (SharkUnit.Left > 0)
                { SharkUnit.Left += -10; };
            }
            if (e.KeyCode == Keys.Right)
            {
                if (SharkUnit.Right < 800)
                { SharkUnit.Left += 10; };
            }
            if (e.KeyCode == Keys.Up)
            {
                if (SharkUnit.Top > 167)
                { SharkUnit.Top += -10; };
            }

            if (e.KeyCode == Keys.Down)
            {
                if (SharkUnit.Bottom < 536)
                { SharkUnit.Top += 10; };

            }

        }
        //here the timer for our game
        //here the main logic
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //here every moment check if player achieve the final size
            //if not - continue the logic, if yes - end the game
            if (size<150)
            {
                //in this cycle check if shark unit intersects with
                //fish unit, if it is happend, we increase the size of our shark,
                //remove the fish and increase the "size-score" on the screen
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox && (string)x.Tag == "fish_unit")
                    {
                        if (SharkUnit.Bounds.IntersectsWith(x.Bounds))
                        {
                            this.Controls.Remove(x);
                            SharkUnit.Width += 3;
                            SharkUnit.Height += 3;
                            //we return the initial image of shark
                            SharkUnit.Image = Properties.Resources.SharkPicture;

                            size += 3;
                            SizeLabel.Text = Convert.ToString(size);
                            if (size<=147)
                            {
                                AddFish();
                            }
                        }
                    }
                }
                //in this cycle we immitate the opening mouth of shark
                //when player goes near the fish unit
                //for this purpose we create the invisible rectangle
                //which is following the fish each seconds
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox && (string)x.Tag == "fish_unit")
                    {
                        Rect.Size = x.Bounds.Size * 4;
                        Rect.X = x.Location.X - (x.Width * 2);
                        Rect.Y = x.Location.Y - (x.Height * 2);
                        if (SharkUnit.Bounds.IntersectsWith(Rect))
                        {  
                            SharkUnit.Image = Properties.Resources.shark_open;
                        }
                        else
                        {
                            SharkUnit.Image = Properties.Resources.SharkPicture;
                        }
                    }
                }
            }
            //here the logic if player achieve the 150 scores
            //we stop the game and offer to the playes start game again
            else
            {
                this.GameTimer.Enabled = false; 
                this.StartButton.Enabled = true;
                MenuLabel.Text = "Congratulations! Now you the biggest \n shark at " +
                "the world! Press start to repeat.";
                size = 0;
            }
        }

        //function for interaction with fish class
        private void AddFish()
        {
            Fishes AddFish = new Fishes();//create new instance
            //assign random values to the spawnn location of fishes
            AddFish.fishtop= randnum.Next(158,500);
            AddFish.fishleft = randnum.Next(0,822);
            AddFish.MakeFish(this); 
        }

        //here is the logic for start button
        private void StartButton_Click(object sender, EventArgs e)
        {
            AddFish();
            this.GameTimer.Enabled = true;
            this.StartButton.Enabled = false;
            MenuLabel.Text = "Welcome to the game. " +
                "The main goal is to feed\r\nyour shark and become the king of the seas!" +
                "\r\nTry to achieve size 150\r\n";
            SharkUnit.Width = 69;
            SharkUnit.Height = 57;
        }
        //logic for exit button
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}