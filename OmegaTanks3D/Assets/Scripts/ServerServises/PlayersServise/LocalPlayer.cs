using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class LocalPlayer : MonoBehaviour
{
    private tcpScript tcpS;
    [SerializeField] private string id;

    [SerializeField] private GameObject LocalPlPref;
    private Player_Movement localPlayer;

    private Templates.REQUES_UPDATEPLAYER updatePlayer;
    private Templates.REQUES_UPDATEPLAYER_level2 updatePlayerLevel2;

    private bool update = true;

    private void Start()
    {
        tcpS = GetComponent<tcpScript>();

        updatePlayer = new Templates.REQUES_UPDATEPLAYER();
        updatePlayerLevel2 = new Templates.REQUES_UPDATEPLAYER_level2();

        tcpS.Activate();
        id = tcpS.Get_ID();

        updatePlayerLevel2.id = id;
        SpawnPlayer();
    }

    private void Update()
    {
        SendStatistick();
    }

    private async void SendStatistick()
    {
        if (update && localPlayer != null)
        {
            update = false;

            updatePlayerLevel2.body = localPlayer.GetStatistick();

            updatePlayer.body = JsonUtility.ToJson(updatePlayerLevel2);

            await SocketDispatcher.SendMessageToServer<Templates.REQUES_UPDATEPLAYER>(updatePlayer);

            await Task.Delay(100);
            update = true;
        }
    }
    private void SpawnPlayer()
    {
        localPlayer = Instantiate(LocalPlPref, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Player_Movement>();
    }
}
