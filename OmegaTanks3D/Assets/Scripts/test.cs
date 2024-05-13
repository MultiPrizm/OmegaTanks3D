using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class test : MonoBehaviour
{
    [SerializeField] private SocketDispatcher t;
    [SerializeField] private string mes = "aaaaaa";

    private void Start()
    {
        t.Clear();
        t.ConnectToServer();       
        //StartCoroutine(foo());
    }
    void Update()
    {
        

    }

    public async void send()
    {
        t.SendMessageToServer(mes);
        Debug.Log(await t.ReceiveMessagesAsync());
    }

    IEnumerator foo()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("FOOOOO");
        StartCoroutine(foo());
    }
}
