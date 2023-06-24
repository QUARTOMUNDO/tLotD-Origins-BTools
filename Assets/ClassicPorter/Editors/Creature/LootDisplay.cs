using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UtilDefinitions;
using System;
using System.Globalization;
using System.ComponentModel;

public class LootDisplay : MonoBehaviour
{
    CreatureData targetCreature;
    private CreatureEntry targetCreatureEntry;
    public Button addNatureDropButton;
    public Button addObjectDropButton;
    public Transform natureDropsContainer;
    public Transform objectDropsContainer;

    List<PropertyPairDisplay> natureDropsDisplays = new List<PropertyPairDisplay>();
    List<PropertyIntFloatDisplay> objectDropsDisplays = new List<PropertyIntFloatDisplay>();

    public PropertyDisplay deepAmountRatio;
    HashSet<string> uniqueNames = new HashSet<string>();

    private void OnDrawGizmosSelected()
    {
        if (deepAmountRatio) deepAmountRatio.gameObject.name = "deepAmountRatio"; deepAmountRatio.previewPropertyName = "deepAmountRatio";

    }


    private void Awake()
    {
        if (deepAmountRatio) deepAmountRatio.OnValueChanged2 += PropertyChangeResponse; deepAmountRatio.gameObject.name = "deepAmountRatio";

    }

    private void OnEnable()
    {
        addNatureDropButton.onClick.AddListener(AddNatureDropDisplay);
        addObjectDropButton.onClick.AddListener(AddObjectDropDisplay);
    }

    private void OnDisable()
    {
        addNatureDropButton.onClick.RemoveListener(AddNatureDropDisplay);
        addObjectDropButton.onClick.RemoveListener(AddObjectDropDisplay);
    }


    private void PropertyChangeResponse(string arg, string arg1)
    {
        switch (arg)
        {
            case "deepAmountRatio": targetCreature.loot.deepAmountRatio = float.Parse(arg1, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture); break;

            default:
                break;
        }
    }


    public void Setup(ref CreatureEntry creatureEntry)
    {
        targetCreature = creatureEntry;
        targetCreatureEntry = creatureEntry;

        deepAmountRatio.Setup("deepAmountRatio", creatureEntry.currentData.loot.deepAmountRatio.ToString(CultureInfo.InvariantCulture), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.loot.deepAmountRatio.ToString(CultureInfo.InvariantCulture), creatureEntry.sourceData.loot.deepAmountRatio.ToString(CultureInfo.InvariantCulture));


        ClearDisplayContainer(natureDropsContainer, natureDropsDisplays);
        foreach (NameFloatPair item in targetCreature.loot.natureDrops)
        {
            PropertyPairDisplay spawnedDisplay = PropertyPairDisplay.Spawn(item.name, item.number.ToString(), natureDropsContainer);
            spawnedDisplay.Setup(item.name, item.number.ToString(), PropertyDisplayTypes.Float);
            natureDropsDisplays.Add(spawnedDisplay);
            spawnedDisplay.OnDeleteRequested += NatureDropsOnDeleteRequestedResponse;
            spawnedDisplay.OnValueChanged2 += NatureDropsOnValueChanged2Response;
        }


        ClearDisplayContainer(objectDropsContainer, objectDropsDisplays);
        foreach (LootObjectDropElement item in targetCreature.loot.objectDrops)
        {
            AddObjectDropDisplay(item.name, item.amount.ToString(CultureInfo.InvariantCulture), item.chance.ToString(CultureInfo.InvariantCulture));
            //PropertyIntFloatDisplay spawnedDisplay = PropertyIntFloatDisplay.Spawn(item.name, item.amount.ToString(CultureInfo.InvariantCulture), item.chance.ToString(CultureInfo.InvariantCulture), objectDropsContainer);
            //spawnedDisplay.Setup(item.name, item.amount.ToString(CultureInfo.InvariantCulture), item.chance.ToString(CultureInfo.InvariantCulture), PropertyDisplayTypes.Float);
            //objectDropsDisplays.Add(spawnedDisplay);
            //spawnedDisplay.OnDeleteRequested += ObjectDropsOnDeleteRequestedResponse;
            //spawnedDisplay.OnValueChanged2 += ObjectDropsOnValueChanged2Response;
        }

    }

    private void NatureDropsOnValueChanged2Response(string arg, string arg1)
    {
        UpdateNatureDrops();
    }

    private void ObjectDropsOnValueChanged2Response(string arg, string arg1)
    {
        UpdateObjectDrops();
    }

    private void NatureDropsOnDeleteRequestedResponse(PropertyPairDisplay arg)
    {
        natureDropsDisplays.Remove(arg);
        arg.gameObject.SetActive(false);
        Destroy(arg.gameObject, 0.1f);
        UpdateNatureDrops();
    }

    private void ObjectDropsOnDeleteRequestedResponse(PropertyIntFloatDisplay arg)
    {
        objectDropsDisplays.Remove(arg);
        arg.gameObject.SetActive(false);
        Destroy(arg.gameObject, 0.1f);
        UpdateObjectDrops();
    }

    private static void ClearDisplayContainer(Transform container, List<PropertyPairDisplay> list)
    {
        for (int i = 0; i < container.childCount; i++)
        {
            container.GetChild(i).gameObject.SetActive(false);
            Destroy(container.GetChild(i).gameObject, 0.1f);
        }
        list.Clear();
    }
    private static void ClearDisplayContainer(Transform container, List<PropertyIntFloatDisplay> list)
    {
        for (int i = 0; i < container.childCount; i++)
        {
            container.GetChild(i).gameObject.SetActive(false);
            Destroy(container.GetChild(i).gameObject, 0.1f);
        }
        list.Clear();
    }

    void UpdateNatureDrops()
    {
        targetCreature.loot.natureDrops.Clear();
        float parsedValue = 0f;
        uniqueNames.Clear();
        foreach (PropertyPairDisplay display in natureDropsDisplays)
        {
            if (float.TryParse(display.propertyValue.text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out parsedValue))
            {
                if (!uniqueNames.Contains(display.propertyName.text))
                {
                    uniqueNames.Add(display.propertyName.text);
                    targetCreature.loot.natureDrops.Add(new NameFloatPair(display.propertyName.text, parsedValue));
                }
                else
                {
                    Debug.LogError("ERROR: Duplicate nature drop name detected at [" + targetCreature.varName + "]. \n" +
                        " Fix this before saving to avoid errors in xml");
                }
            }
        }
    }

    void UpdateObjectDrops()
    {
        targetCreature.loot.objectDrops.Clear();
        int parsedValue1 = 0;
        float parsedValue2 = 0f;
        uniqueNames.Clear();
        foreach (PropertyIntFloatDisplay display in objectDropsDisplays)
        {
            if (float.TryParse(display.propertyValue.text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out parsedValue2)
                && int.TryParse(display.propertyAmount.text, out parsedValue1))
            {
                if (!uniqueNames.Contains(display.propertyName.text))
                {
                    uniqueNames.Add(display.propertyName.text);
                    targetCreature.loot.objectDrops.Add(new LootObjectDropElement(display.propertyName.text, parsedValue1, parsedValue2));
                }
                else
                {
                    Debug.LogError("ERROR: Duplicate object drop name detected at [" + targetCreature.varName + "]. \n" +
                        " Fix this before saving to avoid errors in xml");
                }
            }
        }
    }

    private void AddObjectDropDisplay(string name, string newAmount, string newValue)
    {
        PropertyIntFloatDisplay spawnedDisplay = PropertyIntFloatDisplay.Spawn(name, newAmount, newValue, objectDropsContainer);
        spawnedDisplay.Setup(name, newAmount, newValue, PropertyDisplayTypes.Float);
        objectDropsDisplays.Add(spawnedDisplay);
        spawnedDisplay.OnDeleteRequested += ObjectDropsOnDeleteRequestedResponse;
        spawnedDisplay.OnValueChanged2 += ObjectDropsOnValueChanged2Response;
    }

    private void AddObjectDropDisplay()
    {
        string uniqueDropName = "NewObjectDrop";
        int loopCount = 0;
        bool unique = false;
        while (!unique && loopCount < 100)
        {
            unique = true;
            foreach (var item in objectDropsDisplays)
            {
                if (item.propertyName.text.Equals(uniqueDropName))
                {
                    unique = false; break;
                }
            }
            loopCount++;
            if (!unique) uniqueDropName = "NewObjectDrop" + loopCount;
        }
        AddObjectDropDisplay(uniqueDropName, "1", "0.1");
    }

    private void AddNatureDropDisplay(string name, string number)
    {
        PropertyPairDisplay spawnedDisplay = PropertyPairDisplay.Spawn(name, number, natureDropsContainer);
        spawnedDisplay.Setup(name, number, PropertyDisplayTypes.Float);
        natureDropsDisplays.Add(spawnedDisplay);
        spawnedDisplay.OnDeleteRequested += NatureDropsOnDeleteRequestedResponse;
        spawnedDisplay.OnValueChanged2 += NatureDropsOnValueChanged2Response;
    }

    private void AddNatureDropDisplay()
    {
        string uniqueDropName = "NewNatureDrop";
        int loopCount = 0;
        bool unique = false;
        while (!unique && loopCount < 100)
        {
            unique = true;
            foreach (var item in natureDropsDisplays)
            {
                if (item.propertyName.text.Equals(uniqueDropName))
                {
                    unique = false; break;
                }
            }
            loopCount++;
            if (!unique) uniqueDropName = "NewNatureDrop" + loopCount;
        }

        AddNatureDropDisplay(uniqueDropName, "0");
    }

}
