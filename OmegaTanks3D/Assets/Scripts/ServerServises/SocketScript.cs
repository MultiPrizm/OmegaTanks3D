using System;
using UnityEngine;
using System.Net.Sockets;

[CreateAssetMenu(fileName = "Socket", menuName = "NetWork/Socket")]
public class SocketScript : ScriptableObject
{

    private TcpClient client;
    private NetworkStream stream;

    private bool isConnected = false;

    public static Action onConnect;
    public static Action onDisConnect;

    public void ConnectToServer(string serverAddress, int serverPort)
    {
        try
        {

            client = new TcpClient(serverAddress, serverPort);
            stream = client.GetStream();

            isConnected = true;

            onConnect?.Invoke();

            Debug.Log("Socket[ OK ]: connected to server");
        }
        catch (Exception e)
        {
            Debug.Log("Socket[ ERROR ]:Fail connect => " + e.Message);
        }
    }

    public bool GetStatusConnect()
    {
        return isConnected;
    }

    public void DisconnectFromServer()
    {
        try
        {
            stream.Close();
            client.Close();

            isConnected = false;

            onDisConnect?.Invoke();

            Debug.Log("Socket[ INFO : disconnected from server");
        }
        catch (Exception e)
        {
            Debug.Log("Socket[ ERROR ]:Fail disconnect => " + e.Message);
        }
    }

    public NetworkStream GetSocket()
    {
        return stream;
    }

    void OnDestroy()
    {
        DisconnectFromServer();
    }
}
