using UnityEngine;

namespace Firebase
{
    public class CheckAndFixDependencies : CustomYieldInstruction
    {
        public State CurrentState { get; private set; } = State.Waiting;

        public CheckAndFixDependencies()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                if (task.Result == DependencyStatus.Available)
                {
                    CurrentState = State.Success;
                }
                else
                {
                    CurrentState = State.Failure;
                }
            });
        }

        public override bool keepWaiting => CurrentState == State.Waiting;

        public enum State
        {
            Waiting,
            Success,
            Failure
        }
    }
}