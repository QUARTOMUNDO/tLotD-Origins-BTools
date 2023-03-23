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

    public PropertyDisplay nameDisplay;
    public PropertyDisplay varNameDisplay;

    public void DisplayCreature(ref CreatureData creature)
    {
        nameDisplay.Setup("Creature name: ", creature.name, UtilDefinitions.PropertyDisplayTypes.String);
        varNameDisplay.Setup("Var name: ", creature.varName, UtilDefinitions.PropertyDisplayTypes.String);


        if (natureResistanceDisplay) { natureResistanceDisplay.Setup(ref creature); }
        if (characterAttributesDisplay) { characterAttributesDisplay.Setup(ref creature); }

    }

}
