using System.Net.Sockets;
using System;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

public class SocketDispatcher : MonoBehaviour
{
    [SerializeField] private SocketScript socket;
    [SerializeField] private Router router;

    public int serverPort = 8888;

    private TcpClient client;
    private static NetworkStream stream;

    private static bool isConnect = false;

    

    private void Start()
    {
        Debug.Log("NetWork[ OK ]:SocketDispatcher loaded");

        isConnect = socket.GetStatusConnect();
    }

    public bool ConnectToServer(string ip)
    {
        socket.ConnectToServer(ip, serverPort);
        isConnect = socket.GetStatusConnect();

        if (isConnect)
        {
            stream = socket.GetSocket();

            loop();
            return true;
        }

        return false;
    }

    public void DisConnectToServer()
    {
        socket.DisconnectFromServer();
        isConnect = false;
    }

    private async void loop()
    {
        while (isConnect)
        {
            router.RouteMes(await ReceiveMessagesAsync());
        }
    }

    public static async Task SendMessageToServer<T>(T json)
    {
        if (isConnect) { 
            string mes = JsonUtility.ToJson(json);

            mes += "\n";

            //Debug.Log(mes);

            byte[] data = System.Text.Encoding.ASCII.GetBytes(mes);
            stream.Write(data, 0, data.Length);
        }

        await Task.Delay(1);
    }

    private async Task<string> ReceiveMessagesAsync()
    {

        byte[] buffer = new byte[1024]; // Buffer for receiving messages

        byte a = 0x0A;
        string message ="";

        byte[] bytesRead = await ReadUntilDelimiter(stream, a);

        if (bytesRead.Length > 0)
        {
            message = System.Text.Encoding.UTF8.GetString(bytesRead);

            //Debug.Log(message);
            
        }
        return (message);
    }

    private async Task<byte[]> ReadUntilDelimiter(NetworkStream stream, byte delimiter)
    {
        List<byte> buffer = new List<byte>();
        byte[] readBuffer = new byte[1024];
        int bytesRead;

        while (true)
        {
            bytesRead = await stream.ReadAsync(readBuffer, 0, readBuffer.Length);

            if (bytesRead == 0)
            {
                // Кінець потоку
                break;
            }

            for (int i = 0; i < bytesRead; i++)
            {
                if (readBuffer[i] == delimiter)
                {
                    // Знайдено роздільник, повертаємо зчитані дані
                    return buffer.ToArray();
                }
                else
                {
                    buffer.Add(readBuffer[i]);
                }
            }
        }

        // Кінець потоку без знайденого роздільника
        return buffer.ToArray();
    }

    private void OnDestroy()
    {
        isConnect = false;
    }

    void OnApplicationQuit()
    {
        isConnect = false;
        Debug.Log("Application ending after " + Time.time + " seconds");
    }
}
