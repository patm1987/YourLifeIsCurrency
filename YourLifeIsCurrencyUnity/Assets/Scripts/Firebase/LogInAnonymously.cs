using Firebase.Auth;
using UnityEngine;

namespace Firebase
{
    public class LogInAnonymously : CustomYieldInstruction
    {
        public State CurrentState { get; private set; }

        public FirebaseUser Result { get; private set; }
        
        public LogInAnonymously(FirebaseAuth auth)
        {
            auth.SignInAnonymouslyAsync().ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    CurrentState = State.Cancelled;
                    return;
                }

                if (task.IsFaulted)
                {
                    CurrentState = State.Faulted;
                    return;
                }

                Result = task.Result;
                CurrentState = State.Success;
            });
        }

        public override bool keepWaiting => CurrentState == State.Waiting;

        public enum State
        {
            Waiting,
            Cancelled,
            Faulted,
            Success
        }
    }
}
