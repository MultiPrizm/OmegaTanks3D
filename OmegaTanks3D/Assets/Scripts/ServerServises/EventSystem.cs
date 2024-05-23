using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSystem : MonoBehaviour
{
    private tcpScript tcpS;
    [SerializeField] private string id;

    [SerializeField] private Templates.REQUES_GETLOBBYCODE mes = new Templates.REQUES_GETLOBBYCODE();

    private bool update = true;
    Templates.RESPONSE_GETLOBBYCODE res;

    private delegate void Rule();

    private Dictionary<string, Rule> rules = new Dictionary<string, Rule>();

    private static bool isHost = false;

    void Start()
    {
        tcpS = GetComponent<tcpScript>();

        tcpS.Activate("EventSystem");

        tcpS.send_signal_newmess += MonitorEvent;

        rules.Add("STARTGAME", StartGame);
    }

    // Update is called once per frame
    
    public static bool IsHost()
    {
        return isHost;
    }

    public void MonitorEvent(string type)
    {
        rules[type]();
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
