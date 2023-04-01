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

    public BehaviorPropertiesDisplay behaviorPropertiesDisplay;
    public CreaturePhysicsDisplay creaturePhysicsDisplay;

    public LootDisplay lootDisplay;

    public PropertyDisplay nameDisplay;
    public PropertyDisplay varNameDisplay;

    public void DisplayCreature(ref CreatureData creature)
    {
        if (!PortController.single.defaultCreatures.TryGetValue(creatureData.varName, out CreatureData defaultData))
        {
            Debug.LogWarning("WARNING: Failed to load defaults for creature [ " + creatureData.varName + " ], using ordinary defaults");
            defaultData = new CreatureData();
        }

        nameDisplay.Setup("Creature name: ", creature.name, UtilDefinitions.PropertyDisplayTypes.String, defaultData.name);
        varNameDisplay.Setup("Var name: ", creature.varName, UtilDefinitions.PropertyDisplayTypes.String, defaultData.varName);

        if (natureResistanceDisplay) { natureResistanceDisplay.Setup(ref creature); }
        if (characterAttributesDisplay) { characterAttributesDisplay.Setup(ref creature); }
        if (behaviorPropertiesDisplay) { behaviorPropertiesDisplay.Setup(ref creature); }
        //if (creaturePhysicsDisplay) { creaturePhysicsDisplay.Setup(ref creature); }
        //if (lootDisplay) { lootDisplay.Setup(ref creature); }

    }

}
