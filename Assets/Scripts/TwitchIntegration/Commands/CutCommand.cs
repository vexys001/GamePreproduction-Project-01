using System.Collections;
using System.Collections.Generic;
using Commands;
using TwitchLib.Client.Models;
using UnityEngine;

public class CutCommand : Command
{
    [SerializeField] private CuttingBoard _twitchCuttingBoard;
    protected override void Execute(ChatMessage message, string[] arguments)
    {
        _twitchCuttingBoard.Use();
    }

    protected override bool Validate(ChatMessage message, string[] arguments)
    {
        return true;
    }
}
