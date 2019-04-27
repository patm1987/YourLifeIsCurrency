using UnityEngine;

namespace Firebase
{
    [CreateAssetMenu(menuName = "Firebase Helper")]
    public class FirebaseHelper : ScriptableObject
    {
        private WaitForFirebase _waitForFirebase;

        public WaitForFirebase Firebase => _waitForFirebase ?? (_waitForFirebase = new WaitForFirebase());
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