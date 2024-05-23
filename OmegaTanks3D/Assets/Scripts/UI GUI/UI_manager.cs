using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_manager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TMP_InputField _OutputText;
    private string PlayerName = ""; // нужно поменять эту строку для отображения ника в меню
    [Header("Buttons that needs to be interactive after checking that user name are correct")]
    [SerializeField] private Button[] InteractiveButtons;
    //Host 
    private string RoomCode = "0o0o0"; // 5-6 symbols

    bool lobbyIsStarted = false;

    private tcpScript _tcpSCR;
    private string id;
    private void Start()
    {

        _tcpSCR = GetComponent<tcpScript>();
        _tcpSCR.Activate();
        id = _tcpSCR.Get_ID();
        _tcpSCR.send_signal_newmess += CheckReg;
        /*
        if (PlayerPrefs.GetString("PlayerName") != null)
        {
            PlayerName = PlayerPrefs.GetString("PlayerName");
            for (int i = 0; i < InteractiveButtons.Length; i++)
            {
                InteractiveButtons[i].interactable = true;
            }
        }*/
        _OutputText.text = PlayerName;

    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.U))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void ValiableInput()
    {
        string input = _inputField.text;

        if (input.Length < 4 && input.Length > 25)
        {
            _OutputText.text = null;
            for (int i = 0; i < InteractiveButtons.Length; i++)
            {
                InteractiveButtons[i].interactable = false;
            }
        }
        else
        {
            //PlayerPrefs.SetString("PlayerName", _inputField.text);
            for (int i = 0; i < InteractiveButtons.Length; i++)
            {
                InteractiveButtons[i].interactable = true;
            }
            Templates.REQUES_REG reg = new Templates.REQUES_REG();
            reg.id = id;
            reg.body = _inputField.text;
            SocketDispatcher.SendMessageToServer<Templates.REQUES_REG>(reg);
        }
    }
    public void CheckReg(string type)
    {
        Debug.Log("AAAA");
        if (type == "REG")
        {
            Templates.RESPONSE_REG reg = _tcpSCR.GetMes<Templates.RESPONSE_REG>();
            if (reg.code == 200)
            {

            }
            else
            {

            }
        }
    }

    public void showUI(GameObject UI)
    {
        UI.SetActive(true);
    }
    public void makeUIInvisuable(GameObject UI)
    {
        UI.SetActive(false);
    }

    public void ButtonChangeIntaractive(GameObject Button)
    {
        Button.GetComponent<Button>().interactable = !Button.GetComponent<Button>().interactable;
    }

    //Host
    public void HostRoom(TMP_Text CodeText)
    {
        CodeText.text = RoomCode;
        lobbyIsStarted = true;
    }
    private void PlayersUpdater()
    {

    }
    public void CloseLobby()
    {
        //нужно сделать тему которая позволит закрывать лоби при нажатие на кнопку закрытие окна
        lobbyIsStarted = false;
    }
}
