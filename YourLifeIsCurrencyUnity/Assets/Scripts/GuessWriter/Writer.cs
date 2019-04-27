using UnityEngine;
using UnityEngine.Events;

namespace GuessWriter
{
    public class Writer : MonoBehaviour
    {
        [SerializeField] private GuessType _guessType;
        [SerializeField] private string _guess;
        public GuessSubmittedEvent OnGuessSubmitted = new GuessSubmittedEvent();

        public void SubmitGuess(string guess)
        {
            _guess = guess;
            OnGuessSubmitted.Invoke(_guessType, _guess);
        }

        public enum GuessType
        {
            Truth,
            Lie
        }

        [System.Serializable]
        public class GuessSubmittedEvent : UnityEvent<GuessType, string>
        {
        }
    }
}