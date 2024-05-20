using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Router : MonoBehaviour
{
    private List<string> using_id = new List<string>();
    private Dictionary<string, tcpScript> tcpObjects = new Dictionary<string, tcpScript>();

    //[SerializeField] private SocketDispatcher S_D;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public string GetID(tcpScript script)
    {
        string id;
        string rand_pool = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        while (true)
        {
            id = "";

            for (int i = 0; i < 8; i++)
            {
                id += rand_pool[Random.Range(0, rand_pool.Length - 1)];
            }

            if (!using_id.Contains(id))
            {
                break;
            }
        }

        tcpObjects.Add(id, script);
        using_id.Add(id);

        return id;
    }

    public void RemoveID(string id)
    {
        using_id.Remove(id);
    }

    public void RouteMes(string mes)
    {
        try
        {
            Templates.BaseResponse res = JsonUtility.FromJson<Templates.BaseResponse>(mes);

            //Debug.Log(res.id);

            tcpObjects[res.id].PutMes(res.response);
        }
        catch (KeyNotFoundException)
        {

        }
    }
}
