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

    public void Setup(ref CreatureData creatureData)
    {
        targetCreature = creatureData;
        foreach (NameFloatPair resistance in creatureData.natureResistance.resistances)
        {
            SetResistance(resistance.name, resistance.number.ToString());
            print("Set resistance for " + creatureData.name + "'s display [ " + resistance.name + " ] = [ " + resistance.number.ToString() + " ]");

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

    public void SetResistance(string resistanceName, string resistanceValue)
    {

        if (controlledDisplays.TryGetValue(resistanceName, out PropertyDisplay display))
        {
            display.Setup(resistanceName, resistanceValue.ToString());
        }
        else
        {
            GameObject prefab = resistancePropertyPrefabOverride;
            if (!prefab) prefab = PortController.single.propertyDisplayPrefabSmall;
            PropertyDisplay newPropertyDisplay = PropertyDisplay.Spawn(resistanceName, resistanceValue, resistancePropertiesParent, prefab);
            newPropertyDisplay.gameObject.name = resistanceName;
            newPropertyDisplay.displayType = PropertyDisplayTypes.Float;
            controlledDisplays.Add(resistanceName, newPropertyDisplay);
        }
    }

}

