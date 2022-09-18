using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Commands;
using DefaultNamespace;
using TwitchLib.Client.Models;
using UnityEngine;

public class IngredientCommand : Command
{
    [SerializeField] private int _votingDuration;

    private SortedList<Ingredient.Type, int> _votes = new SortedList<Ingredient.Type, int>();
    private float _processNextVote = 0;

    private void Update()
    {
        _processNextVote += Time.deltaTime;
        if (_processNextVote >= _votingDuration)
        {
            Ingredient.Type ingredient = Ingredient.Type.NONE;
            int max = 0;
            foreach (KeyValuePair<Ingredient.Type, int> vote in _votes)
            {
                if (vote.Value > max)
                {
                    ingredient = vote.Key;
                    max = vote.Value;
                }
            }

            if (ingredient != Ingredient.Type.NONE)
            {
                Debug.Log($"Spawning ingredient {ingredient.ToString()} with {max} votes");
                _votes.Clear();
                _processNextVote = 0;
                BotEvents.OnSpawnIngredient(ingredient);
            }
        }
    }

    protected override void Execute(ChatMessage message, string[] arguments)
    {
        commandUsed = commandUsed.ToUpper();
        Ingredient.Type ingredient = Ingredient.Type.NONE;
        Ingredient.Type.TryParse(commandUsed, out ingredient);
        Debug.Log($"Parsed to {ingredient}");
        if (_votes.ContainsKey(ingredient))
        {
            _votes[ingredient]++;
        }
        else
        {
            _votes[ingredient] = 1;
        }
        Debug.Log($"Vote registered for: {ingredient} ");
    }

    protected override bool Validate(ChatMessage message, string[] arguments)
    {
        return true;
    }
}