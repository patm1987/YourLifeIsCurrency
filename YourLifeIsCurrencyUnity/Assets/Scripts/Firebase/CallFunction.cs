using Firebase.Functions;
using UnityEngine;

namespace Firebase
{
    public class CallFunction : CustomYieldInstruction
    {
        private bool _finished;

        public CallFunction(string functionName)
        {
            var functions = FirebaseFunctions.DefaultInstance;
            functions.GetHttpsCallable(functionName).CallAsync().ContinueWith(task =>
            {
                Result = task.Result;
                _finished = true;
            });
        }

        public override bool keepWaiting => !_finished;
        public HttpsCallableResult Result { get; private set; }
    }
}