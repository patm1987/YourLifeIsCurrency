using UnityEngine;
using UnityEngine.UI;

namespace GuessWriter
{
    public class GuessButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GuessInput _guessInput;
        [SerializeField] private Writer _writer;

        private void Reset()
        {
            _button = GetComponent<Button>();
            _guessInput = FindObjectOfType<GuessInput>();
            _writer = FindObjectOfType<Writer>();
        }

        private void Start()
        {
            _button.onClick.AddListener(HandleClick);
        }

        private void HandleClick()
        {
            _writer.SubmitGuess(_guessInput.Guess);
        }
    }
}