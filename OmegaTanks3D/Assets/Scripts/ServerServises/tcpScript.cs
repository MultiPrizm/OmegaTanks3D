using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class tcpScript : MonoBehaviour
{
    [SerializeField] private string id_;
    private GameObject NetWork;
    [SerializeField] private Router router;

    private string mes_buffer;

    public delegate void new_mess();

    public event new_mess send_signal_newmess;

    private void Awake()
    {
        NetWork = GameObject.FindGameObjectWithTag("NetWork");

        router = NetWork.GetComponent<Router>();

        id_ = router.GetID(this);
    }

    void Update()
    {
        
    }

    public void PutMes(string mes_)
    {
        mes_buffer = mes_;

        send_signal_newmess?.Invoke();
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
