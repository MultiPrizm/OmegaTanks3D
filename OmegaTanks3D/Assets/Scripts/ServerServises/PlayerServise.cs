using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerServise : MonoBehaviour
{
    private tcpScript tcpS;
    [SerializeField] private string id;

    private delegate void Rule();
    private Dictionary<string, Rule> rules = new Dictionary<string, Rule>();

    [SerializeField] private GameObject playerPrefs;
    private GameObject localPlayer;

    void Start()
    {
        tcpS = GetComponent<tcpScript>();

        tcpS.Activate("PlayerServise");
        id = tcpS.Get_ID();

        tcpS.send_signal_newmess += MonitorEvent;

        rules.Add("LOADCOMPLETE", LoadComplete);
        rules.Add("GETSTATUSGAME", SetLocalPlayer);

        Templates.REQUES_GETGAMESTATUS req = new Templates.REQUES_GETGAMESTATUS { id = id };
        SocketDispatcher.SendMessageToServer<Templates.REQUES_GETGAMESTATUS>(req);

        Debug.Log("NetWork[ OK ]:PlayerServise loaded");
    }


    public void MonitorEvent(string type)
    {
        rules[type]();
    }

    private void SetLocalPlayer()
    {
        Debug.Log("PlayerServise[ INFO ]:check status game");
        Templates.RESPONSE_GETGAMESTATUS res = tcpS.GetMes<Templates.RESPONSE_GETGAMESTATUS>();

        Debug.Log($@"{res.code}:{res.body}");

        if (res.body == "yes")
        {
            Debug.Log("PlayerServise[ INFO ]:start loading");
            localPlayer = Instantiate(playerPrefs, new Vector3(0, 0, 0), Quaternion.identity);

            //localPlayer.transform.SetParent(transform);

            LocalPlayer scr = localPlayer.GetComponent<LocalPlayer>();

            scr.Load();
        }
    }

    public void LoadComplete()
    {
        Debug.Log("PlayerServise[ OK ]:load complete");
        LocalPlayer scr = localPlayer.GetComponent<LocalPlayer>();

        scr.Activate();
    }

    private void OnDestroy()
    {
        //tcpS.send_signal_newmess -= MonitorEvent;
        //Debug.Log("aa");
    }
}
