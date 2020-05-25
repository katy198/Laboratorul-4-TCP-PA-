using Smart_House_Watch.ChainofResp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smart_House_Watch
{
    public partial class Form1 : Form
    {
        Bitmap Bitmap;
        private  Thread Listening;
        private  Thread GetImage;
        private int point;
        SecondHandler secondHandler;
        GeneralSource generalSource;
        TcpClient client;
        NetworkStream mainstream;
        public Form1()
        {
            InitializeComponent();

            Listening = new Thread(StartListening);
            GetImage = new Thread(ReceiveImage);
        }
       
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            if (Listening.IsAlive) StopListening();
            
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Este necesar sa alegeti sursa");
            }
            else if (comboBox1.SelectedItem == "Sursa Primară")
            {
                point = 0;
                generalSource.HandleRequest(point);
            }
            else
            {
                point = 1;
                generalSource.HandleRequest(point);
            }

            if (Listening.IsAlive == false)
            {
                Listening = new Thread(StartListening);
                Listening.Start();
                GetImage = new Thread(ReceiveImage);
                GetImage.Start();
            }
            
            


        }

        private void StartListening()
        {
            client = null;
            if (point == 0)
            {
                client = generalSource.GetClient();
            }
            else
                client = secondHandler.GetClient();
            
        }


        private async void ReceiveImage()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            
            while (client.Connected)
            {
                mainstream = client.GetStream();
                Bitmap = new Bitmap((Image)formatter.Deserialize(mainstream), new Size(564, 483));

                pictureBox1.Image = Bitmap;


            }
        }
        
        private void StopListening()
        {
            Listening.Interrupt();
            GetImage.Interrupt();
            if (GetImage.IsAlive) GetImage.Abort();
            if (point == 0)
            {
                generalSource.StopServer();
            }
            else
                
            {
                secondHandler.StopServer();
            }
            client.Close();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            StopListening();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           secondHandler = new SecondHandler();
           generalSource = new GeneralSource();
           generalSource.Successor = secondHandler;
           
        }
    }
}
