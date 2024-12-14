using System;
using _Project.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.ItemScripts
{
    public class Table : MonoBehaviour, IInteractable
    {
        public GameObject infoPanel;
        
        public void Interact()
        {
            DoTask();
        }
        
        public void DoTask()
        {
            bool isOn = infoPanel.activeSelf;
            if (!isOn)
            {
                infoPanel.SetActive(true);
            }else
            {
                infoPanel.SetActive(false);
            }
        }
        
    }
}
