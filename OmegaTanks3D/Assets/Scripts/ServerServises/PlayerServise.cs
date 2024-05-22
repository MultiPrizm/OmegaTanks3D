using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerServise : MonoBehaviour
{
    private tcpScript tcpS;
    [SerializeField] private string id;

    private delegate void Rule();
    private Dictionary<string, Rule> rules;

    private static bool isHost = false;

    [SerializeField] private GameObject playerPrefs;

    void Start()
    {
        tcpS = GetComponent<tcpScript>();

        tcpS.Activate("player_servise");

        tcpS.send_signal_newmess += MonitorEvent;

        rules.Add("NewPlayer", NewPlayer);
    }

    public static bool IsHost()
    {
        return isHost;
    }

    public void MonitorEvent(string type)
    {

    }

    public void NewPlayer()
    {
        GameObject pl = Instantiate(playerPrefs, new Vector3(3231.74f, 2.61f, -2613.95f), Quaternion.identity);

        if (isHost)
        {

        }
    }
}
