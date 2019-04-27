﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Firebase
{
    public class LogInAnonymouslyButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Reset()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(HandleButtonClicked);
        }

        private void HandleButtonClicked()
        {
            StartCoroutine(LogInAnonymouslyButtonClicked());
        }

        private IEnumerator LogInAnonymouslyButtonClicked()
        {
            _button.interactable = false;
            var loginAnonymously = new LogInAnonymously();
            yield return loginAnonymously;
            _button.interactable = true;
        }
    }
}