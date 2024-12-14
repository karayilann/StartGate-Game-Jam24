using System;
using _Project.Scripts.Character;
using _Project.Scripts.Core;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.InteractableScripts
{
    public class WomenChat : MonoBehaviour, IInteractable
    {
        public DialogueSystem dialogueSystem;
        public GameObject dialoguePanel;

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            dialoguePanel.gameObject.SetActive(false);
            dialogueSystem.enabled = false;
            FPSController.Instance.canLook = true;
            FPSController.Instance.canMove = true;
        }

        public void Interact()
        {
            DoTask();
        }
        
        public void DoTask()
        {
            FPSController.Instance.canLook = false;
            FPSController.Instance.canMove = false;
            dialoguePanel.gameObject.SetActive(true);
            dialogueSystem.enabled = true;
        }
        
    }
}
