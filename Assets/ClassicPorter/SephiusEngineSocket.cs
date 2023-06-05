using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;

public class SephiusEngineSocket
{
    public void SendCommand(string command)
    {
        Console.WriteLine("Trying To Send Socket Command");
        try
        {
            TcpClient client = new TcpClient("localhost", 12345);

            Byte[] data = Encoding.UTF8.GetBytes(command);

            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);

            Console.WriteLine("Sent command: {0}", command);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: {0}", e);
        }
    }
}
