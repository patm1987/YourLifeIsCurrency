﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using Firebase;
using Firebase.Functions;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class CreateGameButton : MonoBehaviour, IPointerClickHandler
{
    [Inject] private FirebaseFunctions _functions;
    private Coroutine _createGameCoroutine;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_createGameCoroutine == null)
        {
            _createGameCoroutine = StartCoroutine(CreateGameCoroutine());
        }
    }

    private IEnumerator CreateGameCoroutine()
    {
        var createGame = new CallFunction(_functions, "createGame");
        yield return createGame;
        var sb = new StringBuilder();
        var resultDictionary = (Dictionary<object, object>) createGame.Result.Data;
        foreach (var keyValuePair in resultDictionary)
        {
            sb.Append(keyValuePair.Key).Append("::").Append(keyValuePair.Value).Append("; ");
        }
        Debug.Log($"Create Game Result: {sb}");
        _createGameCoroutine = null;
    }
}