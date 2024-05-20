using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class test4 : MonoBehaviour
{
    private tcpScript t;
    [SerializeField] private string id;

    [SerializeField] private Templates.REQUES_GETLOBBYCODE mes = new Templates.REQUES_GETLOBBYCODE();

    private bool update = true;
    Templates.RESPONSE_GETLOBBYCODE res;

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

            await SocketDispatcher.SendMessageToServer<Templates.REQUES_GETLOBBYCODE>(mes);

            await Task.Delay(100);


            update = true;
        }
    }

    public void GetMess()
    {
        res = t.GetMes<Templates.RESPONSE_GETLOBBYCODE>();

        Debug.Log($@"{res.code}:{res.body}");
    }

    private void OnDestroy()
    {
        t.send_signal_newmess -= GetMess;
    }
}
