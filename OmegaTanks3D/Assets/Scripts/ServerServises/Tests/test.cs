using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class test : MonoBehaviour
{
    private tcpScript t;
    [SerializeField] private string id;

    [SerializeField] private Templates.REQUES_CREATELOBBY mes = new Templates.REQUES_CREATELOBBY();

    private bool update = true;
    Templates.RESPONSE_CREATELOBBY res;

    private void Start()
    {
        t = GetComponent<tcpScript>();

        t.Activate();

        id = t.Get_ID();
        t.send_signal_newmess += GetMess;

        //send();
        StartCoroutine(foo());
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

            await SocketDispatcher.SendMessageToServer<Templates.REQUES_CREATELOBBY>(mes);

            await Task.Delay(100);


            update = true;
        }
    }

    public void GetMess()
    {
        res = t.GetMes<Templates.RESPONSE_CREATELOBBY>();

        Debug.Log($@"{res.code}:{res.body}");
    }

    private void OnDestroy()
    {
        t.send_signal_newmess -= GetMess;
    }

    private IEnumerator foo()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("1");
        send();
    }
}
