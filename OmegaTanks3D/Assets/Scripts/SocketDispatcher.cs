using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System;
using UnityEngine;
using System.Threading.Tasks;


[CreateAssetMenu(fileName = "SocketDispatcher", menuName = "TCP/SocketDispatcher")]
public class SocketDispatcher : ScriptableObject
{
    public string serverAddress = "127.0.0.1"; // Адреса сервера
    public int serverPort = 8888; // Порт сервера

    private TcpClient client;
    private NetworkStream stream;
    private bool isConnected = false;

    public void Clear()
    {
        isConnected = false;
    }
    public void ConnectToServer()
    {
        try
        {
            if (!isConnected)
            {
                client = new TcpClient(serverAddress, serverPort);
                stream = client.GetStream();
                Debug.Log("Connected to server.");
                isConnected = true;
            }
            else
            {
                Debug.Log("No");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error: " + e.Message);
        }
    }

    public void SendMessageToServer(string message)
    {
        if (!isConnected)
        {
            Debug.LogError("Not connected to server.");
            return;
        }

        try
        {
            string mes = message + "\n";
            byte[] data = System.Text.Encoding.ASCII.GetBytes(mes);
            stream.Write(data, 0, data.Length);
            Debug.Log("Message sent: " + message);
        }
        catch (Exception e)
        {
            Debug.LogError("Error sending message: " + e.Message);
        }
    }

    public async Task<string> ReceiveMessagesAsync()
    {
        string message = "";

        byte[] buffer = new byte[1024]; // Buffer for receiving messages
        try
        {
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            if (bytesRead > 0)
            {
                message = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);

                return message;
                // Here you can handle the received message as needed
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error receiving message: " + e.Message);
            isConnected = false;
        }

        return (message);

    }
    void OnDestroy()
    {
        DisconnectFromServer();
    }

    void DisconnectFromServer()
    {
        if (isConnected)
        {
            stream.Close();
            client.Close();
            isConnected = false;
            Debug.Log("Disconnected from server.");
        }
    }
}
