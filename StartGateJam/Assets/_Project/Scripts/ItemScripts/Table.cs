using System;
using _Project.Scripts.Character;
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
                FPSController.Instance.canMove = false;
                FPSController.Instance.canLook = false;
            }
            // else
            // {
            //     infoPanel.SetActive(false);
            // }
        }

        private void Update()
        {
            if (infoPanel.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    infoPanel.SetActive(false);
                    Debug.Log("Portal is opening");
                }
            }
        }
    }
}
