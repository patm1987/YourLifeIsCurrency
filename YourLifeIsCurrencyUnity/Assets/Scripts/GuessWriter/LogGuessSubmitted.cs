using UnityEngine;

namespace GuessWriter
{
    public class LogGuessSubmitted : MonoBehaviour
    {
        [SerializeField] private Writer _writer;

        private void Reset()
        {
            _writer = FindObjectOfType<Writer>();
        }

        private void Start()
        {
            _writer.OnGuessSubmitted.AddListener(HandleGuessSubmitted);
        }

        private void HandleGuessSubmitted(Writer.GuessType guessType, string guess)
        {
            Debug.Log($"Guess:{guessType}::{guess}");
        }
    }
}
