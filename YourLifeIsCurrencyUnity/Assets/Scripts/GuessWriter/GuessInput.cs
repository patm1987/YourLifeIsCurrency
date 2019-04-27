using TMPro;
using UnityEngine;

namespace GuessWriter
{
    public class GuessInput : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _input;
        [SerializeField] private Writer _writer;
        public string Guess => _input.text;

        private void Reset()
        {
            _input = GetComponent<TMP_InputField>();
            _writer = FindObjectOfType<Writer>();
        }

        private void Start()
        {
            _input.onSubmit.AddListener(HandleSubmit);
        }

        private void HandleSubmit(string value)
        {
            _writer.SubmitGuess(value);
        }
    }
}
