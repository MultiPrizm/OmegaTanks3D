using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class test : MonoBehaviour
{
    private tcpScript t;
    [SerializeField] private string id;

    private bool update = true;

    private void Start()
    {
        t = GetComponent<tcpScript>();

        t.Activate();

        id = t.Get_ID();
        t.send_signal_newmess += GetMess;
        send();
    }

    void Update()
    {
        //send();
    }

    public async void send()
    {
        if (update)
        {
            update = false;

            Templates.REQUES_GETLOBBYCODE mes = new Templates.REQUES_GETLOBBYCODE();

            mes.id = id;

            await SocketDispatcher.SendMessageToServer<Templates.REQUES_GETLOBBYCODE>(mes);

            await Task.Delay(100);


            update = true;
        }
    }

    public void GetMess(string type)
    {
        Templates.RESPONSE_GETLOBBYCODE res = t.GetMes<Templates.RESPONSE_GETLOBBYCODE>();

        Debug.Log($@"test1:{res.code}:{res.body}");
    }

    private void OnDestroy()
    {
        t.send_signal_newmess -= GetMess;
    }
}
