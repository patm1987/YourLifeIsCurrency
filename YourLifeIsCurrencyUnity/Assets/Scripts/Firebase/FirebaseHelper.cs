using System;
using UnityEngine;

namespace Firebase
{
    [CreateAssetMenu(menuName = "Firebase Helper")]
    public class FirebaseHelper : ScriptableObject
    {
        private WaitForFirebase _waitForFirebase;
        private FirebaseHelperProxy _proxy;

        public WaitForFirebase Firebase => GetProxy().Firebase;

        private FirebaseHelperProxy GetProxy()
        {
            if (_proxy == null)
            {
                var proxyGo = new GameObject("FirebaseHelperProxy");
                _proxy = proxyGo.AddComponent<FirebaseHelperProxy>();
            }
            return _proxy;
        }
    }

    public class FirebaseHelperProxy : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            Firebase = new WaitForFirebase();
        }
        
        public WaitForFirebase Firebase { get; private set; }
    }

    public class WaitForFirebase : CustomYieldInstruction
    {
        public FirebaseApp Result { get; private set; }
        private State _state = State.Waiting;

        public WaitForFirebase()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                if (task.Result == DependencyStatus.Available)
                {
                    Result = FirebaseApp.DefaultInstance;
                    _state = State.Success;
                }
                else
                {
                    Result = null;
                    _state = State.Failure;
                }
            });
        }

        public override bool keepWaiting => _state == State.Waiting;

        private enum State
        {
            Waiting,
            Success,
            Failure
        }
    }
}