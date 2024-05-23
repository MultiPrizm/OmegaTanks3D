using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ServerMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TMP_InputField _OutputText;
    [SerializeField] private GameObject thisMenu;
    private SocketDispatcher _sd;
    private void Start()
    {
        _sd = GameObject.FindGameObjectWithTag("NetWork").GetComponent<SocketDispatcher>();
    }
    private void LateUpdate()
    {
        
    }
    public void SetIP(GameObject NextMenu)
    {
        string IP = _inputField.text;
        bool thisIP = _sd.ConnectToServer(_inputField.text);
        if (thisIP == true)
        {
            NextMenu.SetActive(true);
            thisMenu.SetActive(false);
            //Debug.Log("gg");
            Debug.Log(thisIP);
        }
        else
        {
            _OutputText.text = null;
            //Debug.Log("gay");
        }
    }
}
