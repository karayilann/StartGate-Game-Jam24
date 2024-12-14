using System.Collections;
using _Project.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Character
{
    public class ActionWithObjects : MonoBehaviour
    {
        public Image interactProgressBar;
        private bool _isInteracting;
        private Coroutine _interactionCoroutine;
        private GameObject _currentInteractableObject;
        private bool _isTaskDone;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Interactable"))
            {
                interactProgressBar.gameObject.SetActive(true);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Interactable"))
            {
                _currentInteractableObject = other.gameObject;

                if (Input.GetKey(KeyCode.E) && !_isInteracting)
                {
                    _interactionCoroutine = StartCoroutine(FillInteractProgress());
                }
                else if (Input.GetKeyUp(KeyCode.E))
                {
                    ResetInteraction();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Interactable"))
            {
                ResetInteraction();
                interactProgressBar.gameObject.SetActive(false);
                if (_isTaskDone)
                {
                    _currentInteractableObject.GetComponent<IInteractable>().Interact();
                }
                _currentInteractableObject = null;
            }
        }

        private IEnumerator FillInteractProgress()
        {
            _isInteracting = true;
            float elapsedTime = 0f;
            float duration = 1.5f;

            while (elapsedTime < duration)
            {
                if (!Input.GetKey(KeyCode.E)) break;

                elapsedTime += Time.deltaTime;
                interactProgressBar.fillAmount = elapsedTime / duration;
                yield return null;
            }

            if (elapsedTime >= duration)
            {
                interactProgressBar.gameObject.SetActive(false);
                _currentInteractableObject.GetComponent<IInteractable>().Interact();
                _isTaskDone = true;
            }
            ResetInteraction();
        }

        private void ResetInteraction()
        {
            if (_interactionCoroutine != null)
            {
                StopCoroutine(_interactionCoroutine);
                _interactionCoroutine = null;
            }
            _isInteracting = false;
            interactProgressBar.fillAmount = 0f;
        }
    }
}
