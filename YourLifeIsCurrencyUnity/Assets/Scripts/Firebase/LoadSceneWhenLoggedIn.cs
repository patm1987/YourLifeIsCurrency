using System;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Firebase
{
    public class LoadSceneWhenLoggedIn : MonoBehaviour
    {
        [SerializeField] private string _scene;
        [Inject] private FirebaseAuth _auth;

        private void Start()
        {
            _auth.StateChanged += HandleStateChanged;
            LoadSceneIfUserExists();
        }

        private void OnDestroy()
        {
            _auth.StateChanged -= HandleStateChanged;
        }

        private void LoadSceneIfUserExists()
        {
            if (_auth.CurrentUser != null)
            {
                SceneManager.LoadScene(_scene);
            }
        }

        private void HandleStateChanged(object sender, EventArgs e)
        {
            LoadSceneIfUserExists();
        }
    }
}