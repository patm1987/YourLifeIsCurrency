using System.Collections;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Firebase
{
    public class LogInAnonymouslyButton : MonoBehaviour
    {
        [Inject] private FirebaseAuth _auth;
        [SerializeField] private Button _button;

        private void Reset()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(HandleButtonClicked);
        }

        private void HandleButtonClicked()
        {
            StartCoroutine(LogInAnonymouslyButtonClicked());
        }

        private IEnumerator LogInAnonymouslyButtonClicked()
        {
            _button.interactable = false;
            var loginAnonymously = new LogInAnonymously(_auth);
            yield return loginAnonymously;
            _button.interactable = true;
        }
    }
}
