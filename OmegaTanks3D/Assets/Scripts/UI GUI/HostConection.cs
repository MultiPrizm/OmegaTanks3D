using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HostConnection : MonoBehaviour
{
    private tcpScript _tcpScript;
    private string _id;
    [SerializeField] private GameObject showMenu;//HOST
    [SerializeField] private GameObject hideMenu;//HOST
    [SerializeField] private List<string> playersNames = new List<string>();
    [SerializeField] private TMP_Text[] namePanelTexts;
    [SerializeField] private TMP_Text roomCodeText;

    [Header("Join Menu")]
    [SerializeField] private GameObject hideJoinMenu;
    [SerializeField] private TMP_InputField roomCodeInput;
    [SerializeField] private GameObject HidenButtonStart;

    private void Start()
    {
        _tcpScript = GetComponent<tcpScript>();
        _tcpScript.Activate();
        _id = _tcpScript.Get_ID();
        _tcpScript.send_signal_newmess += CheckMessage;
    }

    private void LateUpdate()
    {
        // If there's no code needed here, consider removing this method
    }

    public void CheckMessage(string type)
    {
        Debug.Log(type);
        switch (type)
        {
            case "GETLOBBYCODE":
                HandleGetLobbyCode();
                break;
            case "GETPLAYERS":
                HandleGetPlayers();
                break;
            case "CREATELOBBY":
                HandleCreateLobby();
                break;
            case "JOINTOLOBBY":
                HandleGetJoin();
                break;
            default:
                Debug.LogWarning("Unknown message type received: " + type);
                break;

        }
    }
    private void HandleGetJoin()
    {
        Templates.RESPONSE_JOINLOBBY response = _tcpScript.GetMes<Templates.RESPONSE_JOINLOBBY>();
        if (response.code == 200)
        {
            showMenu.SetActive(true);
            hideJoinMenu.SetActive(false);
            HidenButtonStart.SetActive(false);
            ShowRoom();
            StartCoroutine(PlayersListState());
            Debug.Log("ITS WORK!11");
        }
    }
    private void HandleGetLobbyCode()
    {
        //Debug.Log("SSSS");
        Templates.RESPONSE_GETLOBBYCODE response = _tcpScript.GetMes<Templates.RESPONSE_GETLOBBYCODE>();
        if (response.code == 200)
        {
            roomCodeText.text = response.body;

        }
    }
    private void HandleGetPlayers()
    {
        Templates.RESPONSE_GETPLAYERS response = _tcpScript.GetMes<Templates.RESPONSE_GETPLAYERS>();
        if (response.code == 200)
        {
            playersNames = response.body;
            UpdatePlayerList();
        }
    }

    private void HandleCreateLobby()
    {
        Templates.RESPONSE_CREATELOBBY response = _tcpScript.GetMes<Templates.RESPONSE_CREATELOBBY>();
        if (response.code == 200)
        {
            showMenu.SetActive(true);
            hideMenu.SetActive(false);
            ShowRoom();
            StartCoroutine(PlayersListState());
        }
        else
        {
            //Debug.LogError("Failed to create lobby. Error code: " + response.code);
        }
    }
    public void JoinRoom()
    {
        string roomCode = roomCodeInput.text;
        Templates.REQUES_JOINLOBBY request = new Templates.REQUES_JOINLOBBY { id = _id, body = roomCodeInput.text };
        SocketDispatcher.SendMessageToServer(request);
    }
    public void HostRoom()
    {
        Templates.REQUES_CREATELOBBY request = new Templates.REQUES_CREATELOBBY { id = _id };
        SocketDispatcher.SendMessageToServer(request);
    }

    private void ShowRoom()
    {
        Debug.Log("puk puk kak kak");
        Templates.REQUES_GETLOBBYCODE request = new Templates.REQUES_GETLOBBYCODE { id = _id };
        SocketDispatcher.SendMessageToServer(request);
        //Debug.Log(request.name);
    }

    private void UpdatePlayerList()
    {
        for (int i = 0; i < namePanelTexts.Length; i++)
        {
            if (playersNames.Count > i)
            {
                namePanelTexts[i].text = playersNames[i];
                namePanelTexts[i].color = Color.white;
            }
            else
            {
                namePanelTexts[i].text = "Empty";
                namePanelTexts[i].color = new Color(0.007843138f, 0.3686275f, 0.6156863f);
            }
        }
    }

    private IEnumerator PlayersListState()
    {
        while (true)
        {
            Templates.REQUES_GETPLAYERS request = new Templates.REQUES_GETPLAYERS { id = _id };
            SocketDispatcher.SendMessageToServer(request);
            yield return new WaitForSeconds(1f);
            UpdatePlayerList();
        }
    }
}