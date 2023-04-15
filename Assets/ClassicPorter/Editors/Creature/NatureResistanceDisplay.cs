using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilDefinitions;

public class NatureResistanceDisplay : MonoBehaviour
{
    public GameObject resistancePropertyPrefabOverride;
    public List<string> baseResistances = new List<string>()
    {
        "Physical"   ,
        "Air"        ,
        "Fire"       ,
        "Corruption" ,
        "Darkness"   ,
        "Bio"        ,
        "Earth"      ,
        "Ice"        ,
        "Light"      ,
        "Psionica"   ,
        "Water"
    };

    public Transform resistancePropertiesParent;
    public Dictionary<string, PropertyDisplay> controlledDisplays = new Dictionary<string, PropertyDisplay>();

    CreatureData targetCreature;

    public void Setup(ref CreatureEntry creatureEntry)
    {
        targetCreature = creatureEntry;

        foreach (NameFloatPair resistance in creatureEntry.currentData.natureResistance.resistances)
        {
            SetResistance(resistance.name, resistance.number.ToString(),
                creatureEntry.defaultData.natureResistance.GetResistance(resistance.name).number.ToString(),
                creatureEntry.sourceData.natureResistance.GetResistance(resistance.name).number.ToString());
            //print("Set resistance for " + creatureData.name + "'s display [ " + resistance.name + " ] = [ " + resistance.number.ToString() + " ]");

            //if (controlledDisplays.TryGetValue(resistance.name, out PropertyDisplay display))
            //{
            //    display.Setup(resistance.name, resistance.number.ToString());
            //}
            //else
            //{
            //    GameObject spawnedProperty = Instantiate(PortController.single.propertyDisplayPrefab, resistancePropertiesParent);
            //    if (spawnedProperty.TryGetComponent(out PropertyDisplay newPropertyDisplay))
            //    {
            //        controlledDisplays.Add(resistance.name, newPropertyDisplay);
            //        newPropertyDisplay.Setup(resistance.name, resistance.number.ToString());
            //    }
            //}
        }

    }

    public void SetResistance(string resistanceName, string resistanceValue, string defaultResistanceValue, string sourceResistanceValue)
    {

        if (controlledDisplays.TryGetValue(resistanceName, out PropertyDisplay display))
        {
            display.Setup(resistanceName, resistanceValue.ToString(), PropertyDisplayTypes.Float, newDefaultValue: defaultResistanceValue, newSourceValue: sourceResistanceValue);
        }
        else
        {
            GameObject prefab = resistancePropertyPrefabOverride;
            if (!prefab) prefab = PortController.single.propertyDisplayPrefabSmall;
            PropertyDisplay newPropertyDisplay = PropertyDisplay.Spawn(resistanceName, resistanceValue, resistancePropertiesParent, prefab);
            newPropertyDisplay.Setup(resistanceName, resistanceValue, PropertyDisplayTypes.Float, newDefaultValue: defaultResistanceValue, newSourceValue: sourceResistanceValue);
            newPropertyDisplay.gameObject.name = resistanceName;
            newPropertyDisplay.OnValueChanged2 += ResistanceValueChangeResponse;
            controlledDisplays.Add(resistanceName, newPropertyDisplay);
        }
    }

    private void ResistanceValueChangeResponse(string arg, string arg1)
    {
        if (!controlledDisplays.TryGetValue(arg, out PropertyDisplay targetDisplay))
        {
            Debug.LogError("ERROR: Attempt to verify the value of a resistance [ " + arg + " ] that is not listed in the controled displays for creature [ " + targetCreature.varName + " ]");
            return;
        }

        if (float.TryParse(arg1, out float result))
        {
            targetCreature.natureResistance.GetResistance(arg).number = result;
        }
    }
}

