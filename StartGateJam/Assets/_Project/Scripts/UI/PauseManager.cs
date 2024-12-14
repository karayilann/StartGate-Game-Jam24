using _Project.Scripts.Character;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.UI
{
    public class PauseManager : MonoBehaviour
    {
        public FPSController fpsController;
        public void OnClickResume()
        {
            fpsController.canLook = true;
            fpsController.canMove = true;
            fpsController.canPlayable = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gameObject.SetActive(false);
        }
        
        public void OnClickQuit()
        {
            Application.Quit();
        }
        
    }
}
