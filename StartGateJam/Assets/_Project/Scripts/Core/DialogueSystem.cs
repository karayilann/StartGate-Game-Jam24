using System.Collections;
using _Project.Scripts.Character;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class DialogueSystem : MonoBehaviour
    {
        public TextMeshProUGUI txtComponent;
        public string[] lines;
        public float textSpeed;

        private int _index;
        private bool _isTyping; // Yeni bayrak: Şu anda yazı yazılıyor mu?

        private void Start()
        {
            txtComponent.text = string.Empty;
            StartDialogue();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_isTyping)
                {
                    StopCoroutine("TypeLine");
                    txtComponent.text = lines[_index];
                    _isTyping = false;
                }
                else
                {
                    NextLine();
                }
            }
        }

        private IEnumerator AutomaticSwitchLine()
        {
            yield return new WaitForSeconds(0.5f);
            if (!_isTyping) // Sadece yazma işlemi bitmişse geç
            {
                NextLine();
            }
        }

        private void StartDialogue()
        {
            _index = 0;
            StartCoroutine(TypeLine());
        }

        IEnumerator TypeLine()
        {
            _isTyping = true; // Yazı başlıyor
            txtComponent.text = string.Empty;

            foreach (char c in lines[_index].ToCharArray())
            {
                txtComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }

            _isTyping = false; // Yazı tamamlandı
            StartCoroutine(AutomaticSwitchLine());
        }

        private void NextLine()
        {
            if (_index < lines.Length - 1)
            {
                _index++;
                StartCoroutine(TypeLine());
            }
            else
            {   FPSController.Instance.canLook = true;
                FPSController.Instance.canMove = true;
                gameObject.SetActive(false); // Diyalog sistemi devre dışı
            }
        }
    }
}
