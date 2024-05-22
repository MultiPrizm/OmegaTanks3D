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

    [SerializeField] private string[] PlayersNames;//need
    [SerializeField] private TMP_Text[] NamePanelTexsts;//need
    bool lobbyIsStarted = false;
    private void Start()
    {

        if (PlayerPrefs.GetString("PlayerName") != null)
        {
            PlayerName = PlayerPrefs.GetString("PlayerName");
            for (int i = 0; i < InteractiveButtons.Length; i++)
            {
                InteractiveButtons[i].interactable = true;
            }
        }
        _OutputText.text = PlayerName;

    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.U))
        {
            PlayerPrefs.DeleteAll();
        }
    }
    private void LateUpdate()
    {
        if(lobbyIsStarted == true) { 
            for (int i = 0; i < NamePanelTexsts.Length; i++)
            {
                if (PlayersNames.Length > i) { 
                    NamePanelTexsts[i].text = PlayersNames[i];
                    NamePanelTexsts[i].color = Color.white;
                }
                if (PlayersNames.Length <= i)
                {
                    NamePanelTexsts[i].text = "Empty";
                    NamePanelTexsts[i].color = new Color(0.007843138f, 0.3686275f, 0.6156863f);
                }
            }
        }
    }
    public void ValiableInput()
    {
        string input = _inputField.text;
        
        if(input.Length < 4)
        {
            _OutputText.text = null;
            for (int i = 0; i < InteractiveButtons.Length; i++)
            {
                InteractiveButtons[i].interactable = false;
            }
        }
        else
        {
            PlayerPrefs.SetString("PlayerName", _inputField.text);
            for (int i = 0; i < InteractiveButtons.Length; i++)
            {
                InteractiveButtons[i].interactable = true;
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
