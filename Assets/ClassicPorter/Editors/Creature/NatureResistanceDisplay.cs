using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UtilDefinitions;

public class NatureResistanceDisplay : MonoBehaviour
{
    public GameObject resistancePropertyPrefabOverride;
    public Button addResistancebutton;
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
    public Dictionary<string, PropertyDisplay> controlledDisplaysMap = new Dictionary<string, PropertyDisplay>();

    public List<PropertyPairDisplay> controlledDisplays = new List<PropertyPairDisplay>();

    CreatureData targetCreature;
    CreatureEntry targetCreatureEntry;

    private void OnEnable()
    {
        addResistancebutton.onClick.AddListener(AddResistanceButtonClickedResponse);
    }

    private void OnDisable()
    {
        addResistancebutton.onClick.RemoveListener(AddResistanceButtonClickedResponse);        
    }

    public void Setup(ref CreatureEntry creatureEntry)
    {
        targetCreature = creatureEntry;
        targetCreatureEntry = creatureEntry;
        ClearControlledDisplays();

        foreach (NameFloatPair resistance in creatureEntry.currentData.natureResistance.resistances)
        { 
            AddResistanceDisplay(creatureEntry, resistance.name, resistance.number.ToString());
        }


        //foreach (NameFloatPair resistance in creatureEntry.currentData.natureResistance.resistances)
        //{
        //    SetResistance(resistance.name, resistance.number.ToString(),
        //        creatureEntry.defaultData.natureResistance.GetResistance(resistance.name).number.ToString(),
        //        creatureEntry.sourceData.natureResistance.GetResistance(resistance.name).number.ToString());
        //    //print("Set resistance for " + creatureData.name + "'s display [ " + resistance.name + " ] = [ " + resistance.number.ToString() + " ]");

        //    //if (controlledDisplays.TryGetValue(resistance.name, out PropertyDisplay display))
        //    //{
        //    //    display.Setup(resistance.name, resistance.number.ToString());
        //    //}
        //    //else
        //    //{
        //    //    GameObject spawnedProperty = Instantiate(PortController.single.propertyDisplayPrefab, resistancePropertiesParent);
        //    //    if (spawnedProperty.TryGetComponent(out PropertyDisplay newPropertyDisplay))
        //    //    {
        //    //        controlledDisplays.Add(resistance.name, newPropertyDisplay);
        //    //        newPropertyDisplay.Setup(resistance.name, resistance.number.ToString());
        //    //    }
        //    //}
        //}

    }

    private void AddResistanceDisplay(CreatureEntry creatureEntry, string resistanceName, string resistanceValue)
    {
        GameObject prefab = resistancePropertyPrefabOverride;
        if (!prefab) prefab = PortController.single.propertyPairDisplayPrefab;
        PropertyPairDisplay newPropertyPairDisplay = PropertyPairDisplay.Spawn(resistanceName, resistanceValue, resistancePropertiesParent, prefab);
        newPropertyPairDisplay.Setup(resistanceName, resistanceValue, PropertyDisplayTypes.Float,
            newDefaultValue: creatureEntry.defaultData.natureResistance.GetResistance(resistanceName).number.ToString(),
            newSourceValue: creatureEntry.sourceData.natureResistance.GetResistance(resistanceName).number.ToString());
        newPropertyPairDisplay.gameObject.name = resistanceName;
        newPropertyPairDisplay.OnValueChanged2 += ResistanceValueChangeResponse;
        newPropertyPairDisplay.OnDeleteRequested += OnDeleteElementbuttonPressedResponse;
        controlledDisplays.Add(newPropertyPairDisplay);
    }

    private void ClearControlledDisplays()
    {
        foreach (PropertyPairDisplay display in controlledDisplays)
        {
            display.gameObject.SetActive(false);
            Destroy(display.gameObject, 0.1f);
        }
        controlledDisplays.Clear();
    }

    private void UpdateCreatureResistances()
    {
        targetCreature.natureResistance.resistances.Clear();
        float parsedValue = 0f;
        foreach (PropertyPairDisplay display in controlledDisplays)
        {
            if (float.TryParse(display.propertyValue.text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out parsedValue))
            {
                targetCreature.natureResistance.resistances.Add(new NameFloatPair(display.propertyName.text, parsedValue));
            }
        }
    }

    public void SetResistance(string resistanceName, string resistanceValue, string defaultResistanceValue, string sourceResistanceValue)
    {

        if (controlledDisplaysMap.TryGetValue(resistanceName, out PropertyDisplay display))
        {
            display.Setup(resistanceName, resistanceValue.ToString(CultureInfo.InvariantCulture), PropertyDisplayTypes.Float, newDefaultValue: defaultResistanceValue, newSourceValue: sourceResistanceValue);
        }
        else
        {
            GameObject prefab = resistancePropertyPrefabOverride;
            if (!prefab) prefab = PortController.single.propertyDisplayPrefabSmall;
            PropertyDisplay newPropertyDisplay = PropertyDisplay.Spawn(resistanceName, resistanceValue.ToString(CultureInfo.InvariantCulture), resistancePropertiesParent, prefab);
            newPropertyDisplay.Setup(resistanceName, resistanceValue, PropertyDisplayTypes.Float, newDefaultValue: defaultResistanceValue, newSourceValue: sourceResistanceValue);
            newPropertyDisplay.gameObject.name = resistanceName;
            newPropertyDisplay.OnValueChanged2 += ResistanceValueChangeResponse;
            controlledDisplaysMap.Add(resistanceName, newPropertyDisplay);
        }
    }

    private void ResistanceValueChangeResponse(string arg, string arg1)
    {
        //if (!controlledDisplaysMap.TryGetValue(arg, out PropertyDisplay targetDisplay))
        //{
        //    Debug.LogError("ERROR: Attempt to verify the value of a resistance [ " + arg + " ] that is not listed in the controled displays for creature [ " + targetCreature.varName + " ]");
        //    return;
        //}

        //if (float.TryParse(arg1, out float result))
        //{
        //    targetCreature.natureResistance.GetResistance(arg).number = result;
        //}

        UpdateCreatureResistances();

    }

    private void OnDeleteElementbuttonPressedResponse(PropertyPairDisplay arg)
    {

        controlledDisplays.Remove(arg);
        Destroy(arg.gameObject, 0.1f);
        UpdateCreatureResistances();

    }

    private void AddResistanceButtonClickedResponse()
    {
        AddResistanceDisplay(targetCreatureEntry, "NewResistance", "0");
    }

}

