using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class test3 : MonoBehaviour
{
    private tcpScript t;
    [SerializeField] private string id;

    [SerializeField] private Templates.REQUES_PING mes = new Templates.REQUES_PING();

    private bool update = true;
    Templates.RESPONSE_PING res;

    private void Start()
    {
        t = GetComponent<tcpScript>();

        id = t.Get_ID();
        t.send_signal_newmess += GetMess;
    }

    void Update()
    {
        

        send();
    }

    public async void send()
    {
        if (update)
        {
            update = false;

            mes.id = id;

            await SocketDispatcher.SendMessageToServer<Templates.REQUES_PING>(mes);

            await Task.Delay(100);


            update = true;
        }
    }

    public void GetMess()
    {
        res = t.GetMes<Templates.RESPONSE_PING>();

        Debug.Log($@"{res.code}:{res.body}");
    }

    private void OnDestroy()
    {
        t.send_signal_newmess -= GetMess;
    }
}
