using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Models;
using UnityEngine;

namespace Commands
{
    public class HelloCommand : Command
    {
        [SerializeField] private TextMeshProUGUI _helloText;
        
        protected override void Execute(ChatMessage message, string[] arguments)
        {
            _helloText.text = $"Well hello {arguments[0]}";
            _helloText.gameObject.SetActive(true);
        }

        protected override bool Validate(ChatMessage message, string[] arguments)
        {
            return arguments.Length == 1 && message.UserType > UserType.Moderator;
        }
    }
}
