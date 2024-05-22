using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        tcpS.Activate("update_system");

        tcpS.send_signal_newmess += MonitorEvent;

        rules.Add("HOST", setHost);
    }

    // Update is called once per frame
    
    public static bool IsHost()
    {
        return isHost;
    }

    public void MonitorEvent(string type)
    {

    }

    private void setHost()
    {
        isHost = true;
    }
}
