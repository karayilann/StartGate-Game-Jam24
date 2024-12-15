using System;
using _Project.Scripts.Core;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.InteractableScripts
{
    public class PuzzleObjects : MonoBehaviour,IInteractable
    {
        public int id;

        public void Interact()
        {
            TakeObject();
        }

        private void TakeObject()
        {
            PuzzleManager.Instance.AddedObject(id);
            Destroy(gameObject);
        }
    }
}
