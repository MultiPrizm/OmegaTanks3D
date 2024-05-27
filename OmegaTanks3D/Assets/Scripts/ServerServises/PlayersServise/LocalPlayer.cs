using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class LocalPlayer : MonoBehaviour
{
    private tcpScript tcpS;
    [SerializeField] private string _id;

    [SerializeField] private GameObject LocalPlPref;
    private Player_Movement localPlayer;

    private Templates.REQUES_UPDATEPLAYER updatePlayer;
    private Templates.REQUES_UPDATEPLAYER_level2 updatePlayerLevel2;

    private bool active = false;

    private void Awake()
    {
        tcpS = GetComponent<tcpScript>();

        updatePlayer = new Templates.REQUES_UPDATEPLAYER();
        updatePlayerLevel2 = new Templates.REQUES_UPDATEPLAYER_level2();

        tcpS.Activate();
        _id = tcpS.Get_ID();

        updatePlayerLevel2.id = _id;
    }

    private void Update()
    {
        if (active) Activate();
    }

    public void Load()
    {
        SpawnPlayer();
        Templates.REQUES_LOADPLAYER load_req = new Templates.REQUES_LOADPLAYER {id = _id};

        Templates.REQUES_LOADPLAYER_level2 level2 = new Templates.REQUES_LOADPLAYER_level2();

        level2.id = _id;
        level2.body = localPlayer.GetStatistick();

        load_req.body = JsonUtility.ToJson(level2);

        SocketDispatcher.SendMessageToServer<Templates.REQUES_LOADPLAYER>(load_req);
    }

    public void Activate()
    {
        active = true;

        updatePlayerLevel2.body = localPlayer.GetStatistick();

        updatePlayer.body = JsonUtility.ToJson(updatePlayerLevel2);

        SocketDispatcher.SendMessageToServer<Templates.REQUES_UPDATEPLAYER>(updatePlayer);
    }
    private void SpawnPlayer()
    {
        localPlayer = Instantiate(LocalPlPref, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Player_Movement>();
    }
}
