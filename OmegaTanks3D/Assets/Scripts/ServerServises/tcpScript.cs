using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tcpScript : MonoBehaviour
{
    [SerializeField] private string id_;
    private GameObject NetWork;
    [SerializeField] private Router router;

    private string mes_buffer;

    public delegate void new_mess(string type);

    public event new_mess send_signal_newmess;

    private void Awake()
    {
        NetWork = GameObject.FindGameObjectWithTag("NetWork");

        router = NetWork.GetComponent<Router>();
    }

    void Update()
    {
        
    }

    public void Activate(string id = null)
    {
        if (id == null)
        {
            id_ = router.GetID(this);
            return;
        }
        else
        {
            id_ = router.SetID(this, id);
        }
    }

    public void PutMes(string mes_)
    {
        Templates.BaseResponse res = JsonUtility.FromJson<Templates.BaseResponse>(mes_);

        mes_buffer = res.response;

        send_signal_newmess?.Invoke(res.name);
    }

    public T GetMes<T>()
    {
        T buffer = JsonUtility.FromJson<T>(mes_buffer);

        mes_buffer = null;

        return buffer;
    }

    public string Get_ID()
    {
        return id_;
    }

}
