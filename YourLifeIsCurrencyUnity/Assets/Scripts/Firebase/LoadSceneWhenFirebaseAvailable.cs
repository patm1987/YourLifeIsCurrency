using System.Collections;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Firebase
{
    public class LoadSceneWhenFirebaseAvailable : MonoBehaviour
    {
        [Inject] private FirebaseApp _firebase;
        [SerializeField] private string _scene;

        private void Start()
        {
            StartCoroutine(WaitForFirebaseAndLoadScene());
        }

        private IEnumerator WaitForFirebaseAndLoadScene()
        {
            var awaitFirebaseDependencies = new CheckAndFixDependencies();
            yield return awaitFirebaseDependencies;
            if (awaitFirebaseDependencies.CurrentState == CheckAndFixDependencies.State.Success)
            {
                SceneManager.LoadScene(_scene);
            }
            else
            {
                Debug.Log("Failed to initialize firebase");
            }
        }
    }
}