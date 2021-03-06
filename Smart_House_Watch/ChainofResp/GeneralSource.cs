﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Smart_House_Watch.ChainofResp
{
    public class GeneralSource : SourceHandler
    {
        private  TcpClient client;
        private  TcpListener server;

        public override void HandleRequest(int point)
        {
            if (point == 0)
            {
                client = new TcpClient();
                server = new TcpListener(6600);
                if (client.Connected == false)
                {
                    server.Start();
                    client = server.AcceptTcpClient();
                }
                
            }
            else
                Successor.HandleRequest(point);
        }

        public TcpClient GetClient()
        {
            return client;
        }

        public void StopServer()
        {
            server.Stop();
        }

    }
}
