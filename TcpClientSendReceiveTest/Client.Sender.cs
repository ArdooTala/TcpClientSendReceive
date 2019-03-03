﻿using Serilog;
using System;
using System.Net.Sockets;
using System.Threading;
using TcpClientSendReceiveTest.Helpers;

namespace TcpClientProgram
{
    internal sealed partial class Client
    {
        private sealed class Sender
        {
            internal GenericResult<bool> SendData(byte[] data)
            {
                try
                {
                    // transition the data to the thread and send it...
                    _stream.Write(data, 0, data.Length);

                    return new GenericResult<bool>(true);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error sending data to server in Client.Sender.{SendData}()", nameof(SendData));
                    return new GenericResult<bool>(ex);
                }
            }

            internal Sender(NetworkStream stream)
            {
                _stream = stream;
                _thread = new Thread(Run);
                _thread.Start();
            }

            private void Run()
            {
                // main thread loop for sending data...
            }

            private NetworkStream _stream;
            private Thread _thread;
        }
    }
}