using System;
using System.Collections;
using _Project.Scripts.Character;
using _Project.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

namespace _Project.Scripts.ItemScripts
{
    public class Table : MonoBehaviour, IInteractable
    {
        public GameObject infoPanel;
        public VisualEffect visualEffect;
        public int minRate = 100;
        public int maxRate = 10000;

        private int _currentSpawnRate;
        private bool _portalCanOpen = false;
        private void Start()
        {
            if (visualEffect != null)
            {
                _currentSpawnRate = minRate;
                visualEffect.SetInt("Spawn rate", _currentSpawnRate);
                
            }
            else
            {
                Debug.LogError("Visual Effect bileşeni atanmadı!");
            }
        }

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
        }

        private void Update()
        {
            if (infoPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                CloseInfoPanel();
            }
            
            UpdateSpawnRate();
        }

        private void CloseInfoPanel()
        {
            if (visualEffect == null)
            {
                Debug.LogError("Visual Effect is missing! Cannot update spawn rate.");
                return;
            }

            infoPanel.SetActive(false);
            _portalCanOpen = true;
        }

        private void UpdateSpawnRate()
        {
            if (!_portalCanOpen) return;
    
            visualEffect.gameObject.SetActive(true);
    
            StartCoroutine(GradualEffectIncrease());
        }

        private IEnumerator GradualEffectIncrease()
        {
            float elapsedTime = 0f;
            float increaseDuration = 5f;
            float increaseDurationSize = 5f;
    
            int startSpawnRate = _currentSpawnRate;
            float startParticleSize = visualEffect.GetFloat("Particle Size");

            while (elapsedTime < increaseDuration)
            {
                _currentSpawnRate = Mathf.RoundToInt(
                    Mathf.Lerp(startSpawnRate, maxRate, elapsedTime / increaseDuration)
                );

                float currentParticleSize = Mathf.Lerp(startParticleSize, 20f, elapsedTime / increaseDurationSize);

                visualEffect.SetInt("Spawn rate", _currentSpawnRate);
                visualEffect.SetFloat("Particle Size", currentParticleSize);
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _currentSpawnRate = maxRate;
            visualEffect.SetInt("Spawn rate", _currentSpawnRate);
            visualEffect.SetFloat("Particle Size", 900f);
        }
    }
}
