using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCreatureButton : MonoBehaviour
{

    public Button button;
    public TMPro.TMP_Text buttonText;
    [HideInInspector] public CreatureData creature;
    [HideInInspector] public CreatureDisplay creatureDisplay;
    private void Awake()
    {
        if (!button)
        {
            TryGetComponent(out button);
        }
        if (button) button.onClick.AddListener(LoadCreature);
    }

    public void Setup(CreatureData creatureData, CreatureDisplay display)
    {
        creature = creatureData;
        creatureDisplay = display;
        buttonText.text = creature.name;
    }


    public void LoadCreature()
    {
        if (creatureDisplay)
        {
            creatureDisplay.CreatureData = creature;
        }
    }
}
