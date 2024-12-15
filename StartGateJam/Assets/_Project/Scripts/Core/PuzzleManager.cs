using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class PuzzleManager : MonoSingleton<PuzzleManager>
    {
        [System.Serializable]
        public class PuzzleSound
        {
            public int id;
            public AudioClip soundClip;
        }
        
        public GameObject popupPanel;
        public bool isCalled;
        public TextMeshProUGUI popupText;
        [SerializeField] private List<PuzzleSound> puzzleSounds = new List<PuzzleSound>();
        [SerializeField] private AudioSource audioSource;
        
        [SerializeField] private List<int> correctSequence = new List<int>();

        private readonly List<int> _collectedObjectIds = new List<int>();
        private readonly List<int> _pressedSequence = new List<int>();

        private void Start()
        {
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
                
                if (audioSource == null)
                {
                    audioSource = gameObject.AddComponent<AudioSource>();
                }
            }

            if (correctSequence.Count == 0)
            {
                Debug.LogWarning("No correct sequence defined!");
            }
        }

        public void AddedObject(int id)
        {
            if (!_collectedObjectIds.Contains(id))
            {
                _collectedObjectIds.Add(id);
                Debug.Log("Object added to the list: " + id);
            }
        }

        private void Update()
        {
            if (!AreAllObjectsCollected()) return;
            if (!isCalled)
            {
                MoveToScene("All notes collected. Skip this step by pressing the 1, 2, 3 keys in the correct combination.");
                isCalled = true;
            }
            for (int i = 1; i <= 4; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                {
                    ProcessKeyPress(i - 1);
                }
            }
        }

        private void MoveToScene(string text)
        {
            popupPanel.SetActive(true);
            popupText.text = text;
            
            Vector2 currentAnchoredPosition = popupPanel.GetComponent<RectTransform>().anchoredPosition;

            float newY = Mathf.Lerp(currentAnchoredPosition.y, -50.95f, Time.deltaTime * 1.5f);

            popupPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentAnchoredPosition.x, newY);
            Debug.Log("Popup panel is active");
            StartCoroutine(PopupPanelDelay());
        }


        private IEnumerator PopupPanelDelay()
        {
            yield return new WaitForSeconds(2f);
            popupPanel.SetActive(false);
        }
        
        
        private void ProcessKeyPress(int index)
        {
            if (index < 0 || index >= _collectedObjectIds.Count)
            {
                Debug.LogWarning($"No object collected at index {index}");
                return;
            }

            _pressedSequence.Add(_collectedObjectIds[index]);
            Debug.Log("Pressed key: " + _collectedObjectIds[index]);

            if (IsSequenceCorrect())
            {
                Debug.Log("Correct sequence! Well done!");
    
                // Sekansı sırayla çal
                //PlaySoundForCorrectSequence();
                MoveToScene("Correct sequence! Well done!");
                StartCoroutine(LoadMainMenu());
                _pressedSequence.Clear();
            }

            else if (_pressedSequence.Count >= correctSequence.Count)
            {
                MoveToScene("Wrong sequence! Try again!");
                _pressedSequence.Clear();
            }
        }

        private IEnumerator LoadMainMenu()
        {
            yield return new WaitForSeconds(2f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        private bool IsSequenceCorrect()
        {
            if (_pressedSequence.Count > correctSequence.Count)
                return false;

            List<int> remainingCorrectSequence = new List<int>(correctSequence);

            foreach (int pressedValue in _pressedSequence)
            {
                int index = remainingCorrectSequence.IndexOf(pressedValue);
                if (index == -1)
                    return false;

                remainingCorrectSequence.RemoveAt(index);
            }

            return _pressedSequence.Count == correctSequence.Count;
        }

        private void PlaySoundForKeyPress(int index)
        {
            int objectId = _collectedObjectIds[index];

            PuzzleSound soundToPlay = puzzleSounds.Find(ps => ps.id == objectId);

            if (soundToPlay != null && soundToPlay.soundClip != null)
            {
                audioSource.PlayOneShot(soundToPlay.soundClip);
                Debug.Log($"Playing sound for object ID: {objectId}");
            }
            else
            {
                Debug.LogWarning($"No sound found for object ID: {objectId}");
            }
        }

        private void PlaySoundForCorrectSequence()
        {
            StartCoroutine(PlaySequenceCoroutine());
        }

        private IEnumerator PlaySequenceCoroutine()
        {
            foreach (int objectId in correctSequence)
            {
                PuzzleSound soundToPlay = puzzleSounds.Find(ps => ps.id == objectId);

                if (soundToPlay != null && soundToPlay.soundClip != null)
                {
                    audioSource.PlayOneShot(soundToPlay.soundClip);
                    Debug.Log($"Playing sound for object ID: {objectId}");
                    yield return new WaitForSeconds(soundToPlay.soundClip.length);
                }
                else
                {
                    Debug.LogWarning($"No sound found for object ID: {objectId}");
                }
            }
        }
        
        public bool AreAllObjectsCollected()
        {
            return _collectedObjectIds.Count == 3;
        }
    }
}