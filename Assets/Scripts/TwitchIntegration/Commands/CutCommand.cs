using System.Collections;
using System.Collections.Generic;
using Commands;
using TwitchLib.Client.Models;
using UnityEngine;

public class CutCommand : Command
{
    [SerializeField]
    protected override void Execute(ChatMessage message, string[] arguments)
    {
        throw new System.NotImplementedException();
    }

    protected override bool Validate(ChatMessage message, string[] arguments)
    {
        return true;
    }
}
