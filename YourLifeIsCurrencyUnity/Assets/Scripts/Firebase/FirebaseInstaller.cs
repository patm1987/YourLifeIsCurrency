using Firebase.Auth;
using Firebase.Functions;
using UnityEngine;
using Zenject;

namespace Firebase
{
    [CreateAssetMenu(menuName = "FirebaseInstaller")]
    public class FirebaseInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<FirebaseApp>().FromInstance(FirebaseApp.DefaultInstance).AsSingle();
            Container.Bind<FirebaseAuth>().FromInstance(FirebaseAuth.DefaultInstance).AsSingle();
            Container.Bind<FirebaseFunctions>().FromInstance(FirebaseFunctions.DefaultInstance).AsSingle();
        }
    }
}