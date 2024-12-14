using UnityEngine;

namespace _Project.Scripts.UI
{
    public class PauseManager : MonoBehaviour
    {
        public void OnClickResume()
        {
            Time.timeScale = 1;
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
