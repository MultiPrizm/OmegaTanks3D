using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ServerMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TMP_InputField _OutputText;
    private SocketDispatcher _sd;
    private void Start()
    {
        _sd = GameObject.FindGameObjectWithTag("NetWork").GetComponent<SocketDispatcher>();
    }
    private void LateUpdate()
    {
        
    }
    public void SetIP()
    {
        string IP = _inputField.text;
        if(_sd.ConnectToServer(_inputField.text, 8888))
        {

        }
    }
}
