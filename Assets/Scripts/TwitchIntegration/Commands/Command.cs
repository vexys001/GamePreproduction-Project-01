using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TwitchLib.Client.Models;
using UnityEngine;

namespace Commands
{
    public abstract class Command : MonoBehaviour
    {
        [SerializeField] protected List<string> commandIds;
        [SerializeField] private float _cooldown;
        [SerializeField] private bool isOnCooldown;
        protected string commandUsed;
        protected abstract void Execute(ChatMessage message, string[] arguments);
        protected abstract bool Validate(ChatMessage message, string[] arguments);

        private void Evalutate(ChatMessage message)
        {
            string[] commandText = message.Message.Split();
            string[] arguments = commandText[1..];
            commandUsed = commandText[0].ToLower();
            
            if (!isOnCooldown && commandIds.Contains(commandUsed))
            {
                if (Validate(message, arguments))
                {
                    Execute(message, arguments);
                    isOnCooldown = true;
                    StartCoroutine(StartCooldown());
                }
            }
        }

        void OnEnable()
        {
            BotEvents.OnBotCommand += Evalutate;
        }
        
        private void OnDisable()
        {
            BotEvents.OnBotCommand -= Evalutate;
        }

        IEnumerator StartCooldown()
        {
            yield return new WaitForSecondsRealtime(_cooldown);
            isOnCooldown = false;
        }
    }
}