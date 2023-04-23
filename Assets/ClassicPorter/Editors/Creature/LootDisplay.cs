using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDisplay : MonoBehaviour
{
    CreatureData targetCreature;

    public PropertyDisplay deepAmountRatio;

    private void OnDrawGizmosSelected()
    {
        if (deepAmountRatio) deepAmountRatio.gameObject.name = "deepAmountRatio"; deepAmountRatio.previewPropertyName = "deepAmountRatio";        

    }


    private void Awake()
    {
        if (deepAmountRatio) deepAmountRatio.OnValueChanged2 += PropertyChangeResponse; deepAmountRatio.gameObject.name = "deepAmountRatio";

    }


    private void PropertyChangeResponse(string arg, string arg1)
    {
        switch (arg)
        {
            case "deepAmountRatio": targetCreature.loot.deepAmountRatio = float.Parse(arg1); break;

            default:
                break;
        }
    }


    public void Setup(ref CreatureEntry creatureEntry)
    {
        targetCreature = creatureEntry;

        deepAmountRatio.Setup("deepAmountRatio", creatureEntry.currentData.loot.deepAmountRatio.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.loot.deepAmountRatio.ToString(), creatureEntry.sourceData.loot.deepAmountRatio.ToString());
    }


}
