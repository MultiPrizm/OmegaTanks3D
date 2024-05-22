using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class test4 : MonoBehaviour
{
    private tcpScript t;
    [SerializeField] private string id;
    [SerializeField] private string lobbyCode;

    private bool update = true;

    private void Start()
    {
        t = GetComponent<tcpScript>();

        t.Activate();

        id = t.Get_ID();
        t.send_signal_newmess += GetMess;
    }

    void Update()
    {

    }

    public async void createLobby()
    {
        if (update)
        {
            update = false;

            Templates.REQUES_CREATELOBBY mes = new Templates.REQUES_CREATELOBBY();

            mes.id = id;

            await SocketDispatcher.SendMessageToServer<Templates.REQUES_CREATELOBBY>(mes);

            await Task.Delay(100);


            update = true;
        }
    }

    public async void joinLobby()
    {
        if (update)
        {
            update = false;

            Templates.REQUES_JOINLOBBY mes = new Templates.REQUES_JOINLOBBY();

            mes.id = id;
            mes.body = lobbyCode;

            await SocketDispatcher.SendMessageToServer<Templates.REQUES_JOINLOBBY>(mes);

            await Task.Delay(100);


            update = true;
        }
    }

    public void GetMess(string type)
    {
        if(type == "CREATELOBBY")
        {
            Templates.RESPONSE_CREATELOBBY res = t.GetMes<Templates.RESPONSE_CREATELOBBY>();
            Debug.Log($@"test4:{res.code}:{res.body}");
        }
        else if(type == "JOINTOLOBBY")
        {
            Templates.RESPONSE_JOINLOBBY res = t.GetMes<Templates.RESPONSE_JOINLOBBY>();
            Debug.Log($@"test4:{res.code}:{res.body}");
        }  
    }

    private void OnDestroy()
    {
        t.send_signal_newmess -= GetMess;
    }

}
