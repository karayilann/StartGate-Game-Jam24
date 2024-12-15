using System;
using _Project.Scripts.Character;
using _Project.Scripts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class MenuManager : MonoSingleton<MenuManager>
    {
        [Header("Panels")]
        public GameObject settingsPanel;
        public GameObject mainMenuPanel;
        public GameObject creditsPanel;
        
        [Header("Sliders")]
        public Slider masterVolumeSlider;
        public Slider mouseSensitivitySlider;

        [Header("Values")]
        private static float _masterVolume = 1f ;
        private static float _mouseSensitivity = 5f;
        public static float MasterVolumeValue => _masterVolume;
        public static float MouseSensitivityValue => _mouseSensitivity;
        
        [Header("Texts")]
        public TextMeshProUGUI masterVolumeText;
        public TextMeshProUGUI mouseSensitivityText;
        
        public float MasterVolume
        {
            get => _masterVolume;
            set
            {
                _masterVolume = value;
                masterVolumeText.text = _masterVolume.ToString();
            }
        }

        public float MouseSensitivity
        {
            get => _mouseSensitivity;
            set
            {
                _mouseSensitivity = value;
                mouseSensitivityText.text = _mouseSensitivity.ToString();
            }
        }
        
        private void Start()
        {
            masterVolumeSlider.onValueChanged.AddListener(delegate { MasterVolume = masterVolumeSlider.value; });
            mouseSensitivitySlider.onValueChanged.AddListener(delegate { MouseSensitivity = mouseSensitivitySlider.value; });
            Time.timeScale = 1f;
        }

        private void OnDestroy()
        {
            masterVolumeSlider.onValueChanged.RemoveAllListeners();
            mouseSensitivitySlider.onValueChanged.RemoveAllListeners();
        }

        public void OnClickQuit()
        {
            Application.Quit();
        }
        
        public void OnClickStart()
        {
            SceneManager.LoadScene(1);
        }
        
        public void OnClickSettings()
        {
            mainMenuPanel.SetActive(false);
            settingsPanel.SetActive(true);
            creditsPanel.SetActive(false);
        }
        
        public void OnClickBack()
        {
            settingsPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
            creditsPanel.SetActive(false);
        }
        
        public void OnClickCredits()
        {
            creditsPanel.SetActive(true);
            mainMenuPanel.SetActive(false);
            settingsPanel.SetActive(false);
        }
    }
}
