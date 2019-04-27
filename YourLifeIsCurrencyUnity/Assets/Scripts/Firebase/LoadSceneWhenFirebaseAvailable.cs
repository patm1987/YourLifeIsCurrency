using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Firebase
{
    public class LoadSceneWhenFirebaseAvailable : MonoBehaviour
    {
        [SerializeField] private FirebaseHelper _firebaseHelper;
        [SerializeField] private string _scene;

        private void Start()
        {
            StartCoroutine(WaitForFirebaseAndLoadScene());
        }

        private IEnumerator WaitForFirebaseAndLoadScene()
        {
            var awaitFirebase = _firebaseHelper.Firebase;
            yield return awaitFirebase;
            if (awaitFirebase.Result != null)
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