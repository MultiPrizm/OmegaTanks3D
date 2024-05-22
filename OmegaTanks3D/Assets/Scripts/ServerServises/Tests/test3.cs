using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class test3 : MonoBehaviour
{
    private tcpScript t;
    [SerializeField] private string id;

    [SerializeField] private Templates.REQUES_GETPLAYERS mes = new Templates.REQUES_GETPLAYERS();

    private bool update = true;
    Templates.RESPONSE_GETPLAYERS res;

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

            await SocketDispatcher.SendMessageToServer<Templates.REQUES_GETPLAYERS>(mes);

            await Task.Delay(100);


            update = true;
        }
    }

    public void GetMess(string type)
    {
        res = t.GetMes<Templates.RESPONSE_GETPLAYERS>();

        Debug.Log($@"test3:{res.code}");

        foreach (var i in res.body)
        {
            Debug.Log($@"test3:{i}");
        }

        
    }

    private void OnDestroy()
    {
        t.send_signal_newmess -= GetMess;
    }
}
