using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureDisplay : MonoBehaviour
{
    [SerializeField] private CreatureData creatureData = null;

    public CreatureData CreatureData
    {
        get => creatureData; set
        {
            creatureData = value;
            if (creatureData != null)
            {
                DisplayCreature(ref creatureData);
            }
        }
    }

    public NatureResistanceDisplay natureResistanceDisplay;
    public CharacterAttributesDisplay characterAttributesDisplay;

    public BehaviourPropertiesDisplay behaviourPropertiesDisplay;
    public CreaturePhysicsDisplay creaturePhysicsDisplay;

    public LootDisplay lootDisplay;

    public void DisplayCreature(ref CreatureData creature)
    {
        if (natureResistanceDisplay)
        {
            natureResistanceDisplay.Setup(ref creature);
        }
    }

}
