using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.ItemScripts
{
    public class Try : MonoBehaviour,IInteractable
    {
        public void Interact()
        {
            Debug.Log("Interacted with :" + gameObject.name);
        }
    }
}
