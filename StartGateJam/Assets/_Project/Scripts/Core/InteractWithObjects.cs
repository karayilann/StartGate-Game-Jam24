using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class InteractWithObjects : MonoBehaviour
    {
        [SerializeField] private float maxInteractionDistance = 5f;
        [SerializeField] private Camera mainCamera;
        private Outline _currentOutline;

        private void Update()
        {
            HandleObjectInteraction();
        }

        private void HandleObjectInteraction()
        {
            Ray screenPointRay = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(screenPointRay, out RaycastHit hit, maxInteractionDistance))
            {
                GameObject hitObject = hit.transform.gameObject;
                
                // if (hitObject.TryGetComponent(out IInteractable interactableObject))
                // {
                //     interactableObject.Interact();
                // }

                UpdateObjectOutline(hitObject);
            }
            else
            {
                ClearCurrentOutline();
            }
        }

        private void UpdateObjectOutline(GameObject hitObject)
        {
            if (hitObject.TryGetComponent(out Outline outline))
            {
                if (_currentOutline != outline)
                {
                    ClearCurrentOutline();
                    
                    _currentOutline = outline;
                    _currentOutline.OutlineMode = Outline.Mode.OutlineVisible;
                }
            }
            else
            {
                ClearCurrentOutline();
            }
        }

        private void ClearCurrentOutline()
        {
            if (_currentOutline != null)
            {
                _currentOutline.OutlineMode = Outline.Mode.OutlineHidden;
                _currentOutline = null;
            }
        }
    }
}