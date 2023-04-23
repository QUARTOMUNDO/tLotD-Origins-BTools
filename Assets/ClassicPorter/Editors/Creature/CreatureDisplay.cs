using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureDisplay : MonoBehaviour
{
    [SerializeField] private CreatureEntry creatureEntry = null;

    public CreatureEntry CreatureEntry_P
    {
        get => creatureEntry; set
        {
            creatureEntry = value;
            if (creatureEntry != null)
            {
                DisplayCreature(ref creatureEntry);
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

    public void DisplayCreature(ref CreatureEntry creature)
    {
        //if (!PortController.single.creatures.TryGetValue(creatureEntry.VarName, out CreatureEntry entry))
        //{
        //    entry = new CreatureEntry(new CreatureData(), new CreatureData(), new CreatureData());
        //}
        if (!creature.defaultData) Debug.LogWarning("WARNING: Failed to load DEFAULT data for creature [ " + creatureEntry.VarName + " ], using ordinary defaults");
        if (!creature.sourceData) Debug.LogWarning("WARNING: Failed to load SOURCE data for creature [ " + creatureEntry.VarName + " ], using ordinary defaults");

        nameDisplay.Setup("Creature name: ", creature.currentData.name, UtilDefinitions.PropertyDisplayTypes.String, creature.defaultData.name, creature.sourceData.name);
        varNameDisplay.Setup("Var name: ", creature.VarName, UtilDefinitions.PropertyDisplayTypes.String, creature.VarName);

        if (natureResistanceDisplay) { natureResistanceDisplay.Setup(ref creature); }
        if (characterAttributesDisplay) { characterAttributesDisplay.Setup(ref creature); }
        if (behaviorPropertiesDisplay) { behaviorPropertiesDisplay.Setup(ref creature); }
        if (creaturePhysicsDisplay) { creaturePhysicsDisplay.Setup(ref creature); }
        if (lootDisplay) { lootDisplay.Setup(ref creature); }

    }

}
