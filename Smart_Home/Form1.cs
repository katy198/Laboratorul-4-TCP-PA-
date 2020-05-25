using Smart_Home.Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smart_Home
{
    public partial class Form1 : Form
    {
        private int portNumber;
        public Form1()
        {
          
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            portNumber = int.Parse(txt_port.Text);
            try
            {
                SocketTCP.GetInstance();
                SocketTCP.client.Connect(txt_ip.Text, portNumber);
                
                MessageBox.Show("Este conectat");
            }
            catch
            {
                MessageBox.Show("Eroare de conectare");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text.StartsWith("Partajează"))
            {
                timer1.Start();
                button2.Text = "Stopează partajarea ecranului";
            }
            else
            {
                timer1.Stop();
                button2.Text = "Partajează ecranul către server";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            while(SocketTCP.client.Connected)
            SendDesktopeImage();
        }


        private static Image GrapDesktop()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
            Graphics graphic = Graphics.FromImage(screenshot);
            graphic.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
            return screenshot;
        }
        private static void SendDesktopeImage()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(SocketTCP.GetStream(), GrapDesktop());
            }
            catch
            {
                MessageBox.Show("Sa deconectat Vizualizatorul");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
