using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicUIInterface : MonoBehaviour
{
    [SerializeField] private GameObject[] Menus;
    public void ShowUI(int ShowIndex)
    {
        for(int i = 0; i < Menus.Length; i++)
        {
            Menus[i].SetActive(false);
        }
        Menus[ShowIndex].SetActive(true);
    }
}
