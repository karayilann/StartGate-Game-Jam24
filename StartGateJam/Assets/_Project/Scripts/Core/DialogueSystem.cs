using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Core
{
    public class DialogueSystem : MonoBehaviour
    { 
        public TextMeshProUGUI txtComponent;
        public string[] lines;
        public float textSpeed;

        private int _index;

        private void Start()
        {
            txtComponent.text = string.Empty;
            StartDialogue();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(txtComponent.text == lines[_index])
                {
                    NextLine();
                } 
                else
                {
                    StopAllCoroutines();
                    txtComponent.text = lines[_index];
                }
            }
        }

        private void StartDialogue()
        {
            _index = 0;
            StartCoroutine(TypeLine());
        }

        IEnumerator TypeLine()
        {
            foreach (char c in lines[_index].ToCharArray())
            {
                txtComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }

        private void NextLine()
        {
            if (_index < lines.Length - 1)
            {
                _index++;
                txtComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
