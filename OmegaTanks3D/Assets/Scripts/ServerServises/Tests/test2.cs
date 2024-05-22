using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class test2 : MonoBehaviour
{
    private tcpScript t;
    [SerializeField] private string id;

    [SerializeField] private Templates.REQUES_GETAPI mes = new Templates.REQUES_GETAPI();

    private bool update = true;
    Templates.RESPONSE_GETAPI res;

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

    public async void send()
    {
        if (update)
        {
            update = false;

            mes.id = id;

            await SocketDispatcher.SendMessageToServer<Templates.REQUES_GETAPI>(mes);

            await Task.Delay(100);


            update = true;
        }
    }

    public void GetMess(string type)
    {
        res = t.GetMes<Templates.RESPONSE_GETAPI>();

        Debug.Log($@"test2:{res.code}:{res.body}");
    }

    private void OnDestroy()
    {
        t.send_signal_newmess -= GetMess;
    }
}
