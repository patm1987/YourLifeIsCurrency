using System.Collections;
using System.Collections.Generic;
using System.Text;
using Firebase;
using Firebase.Functions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CreateOrJoinGame : MonoBehaviour
{
    [SerializeField] private TMP_InputField _gameId;
    [SerializeField] private Button _joinGameButton;
    [SerializeField] private Button _createGameButton;

    [Inject] private FirebaseFunctions _functions;

    private Coroutine _coroutine;

    void Awake()
    {
        _joinGameButton.onClick.AddListener(HandleJoinGameButtonClicked);
        _createGameButton.onClick.AddListener(HandleCreateGameButtonClicked);
    }

    private void HandleJoinGameButtonClicked()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(JoinGame(_gameId.text));
        }
    }

    private IEnumerator JoinGame(string gameId)
    {
        var gameDictionary = new Dictionary<string, string> {["text"] = gameId};
        var joinGame = new CallFunction(_functions, "joinGame", gameDictionary);
        yield return joinGame;
        var sb = new StringBuilder();
        var resultDictionary = (Dictionary<object, object>) joinGame.Result.Data;
        foreach (var keyValuePair in resultDictionary)
        {
            sb.Append(keyValuePair.Key).Append("::").Append(keyValuePair.Value).Append("; ");
        }
        Debug.Log($"Create Game Result: {sb}");
        _coroutine = null;
        
    }

    private void HandleCreateGameButtonClicked()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(CreateGame());
        }
    }

    private IEnumerator CreateGame()
    {
        var joinGame = new CallFunction(_functions, "createGame");
        yield return joinGame;
        var sb = new StringBuilder();
        var resultDictionary = (Dictionary<object, object>) joinGame.Result.Data;
        foreach (var keyValuePair in resultDictionary)
        {
            sb.Append(keyValuePair.Key).Append("::").Append(keyValuePair.Value).Append("; ");
        }
        Debug.Log($"Create Game Result: {sb}");
        _coroutine = null;
    }
}