using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petrol.Controls
{
    public partial class Login : UserControl
    {
        private Timer timer;
        private double opacityStep;
        public Login()
        {
            InitializeComponent();
            opacityStep = 0.02; // The increment for opacity per tick (0 to 1 range)
            timer = new Timer
            {
                Interval = 15 // Time for each tick (speed of animation)
            };
            timer.Tick += Timer_Tick;

            // Start the animation
            StartAnimation();
        }
        private void StartAnimation()
        {
            guna2Panel1.Visible = true;
            label4.Visible = true;

            // Initialize opacity to 0 (fully transparent)
            guna2Panel1.FillColor = System.Drawing.Color.Transparent;
            label4.ForeColor = System.Drawing.Color.Transparent;

            // Start the timer for the animation
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Increment the opacity of the panel and label
            if (guna2Panel1.FillColor.A < 255)
            {
                int newAlpha = (int)(guna2Panel1.FillColor.A + (opacityStep * 255));
                guna2Panel1.FillColor = System.Drawing.Color.FromArgb(newAlpha, 255, 255, 255);
                label4.ForeColor = System.Drawing.Color.FromArgb(newAlpha, 255, 255, 255);
            }
            else
            {
                // Stop the animation once opacity reaches 100%
                timer.Stop();
            }
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            Form1 mainForm = (Form1)this.ParentForm;
          
                mainForm.ShowHome();
            
        }
    }
}
