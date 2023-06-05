using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UtilDefinitions;
using System;
using System.Globalization;

public class LootDisplay : MonoBehaviour
{
    CreatureData targetCreature;
    private CreatureEntry targetCreatureEntry;
    public Button addNatureDropButton;
    public Button addObjectDropButton;
    public Transform natureDropsContainer;
    public Transform objectDropsContainer;

    List<PropertyPairDisplay> natureDropsDisplays = new List<PropertyPairDisplay>();
    List<PropertyPairDisplay> objectDropsDisplays = new List<PropertyPairDisplay>();

    public PropertyDisplay deepAmountRatio;

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
            case "deepAmountRatio": targetCreature.loot.deepAmountRatio = float.Parse(arg1); break;

            default:
                break;
        }
    }


    public void Setup(ref CreatureEntry creatureEntry)
    {
        targetCreature = creatureEntry;
        targetCreatureEntry = creatureEntry;

        deepAmountRatio.Setup("deepAmountRatio", creatureEntry.currentData.loot.deepAmountRatio.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.loot.deepAmountRatio.ToString(), creatureEntry.sourceData.loot.deepAmountRatio.ToString());


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
        foreach (NameFloatPair item in targetCreature.loot.objectDrops)
        {
            PropertyPairDisplay spawnedDisplay = PropertyPairDisplay.Spawn(item.name, item.number.ToString(), objectDropsContainer);
            spawnedDisplay.Setup(item.name, item.number.ToString(), PropertyDisplayTypes.Float);
            objectDropsDisplays.Add(spawnedDisplay);
            spawnedDisplay.OnDeleteRequested += ObjectDropsOnDeleteRequestedResponse;
            spawnedDisplay.OnValueChanged2 += ObjectDropsOnValueChanged2Response;
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
    }

    private void ObjectDropsOnDeleteRequestedResponse(PropertyPairDisplay arg)
    {
        objectDropsDisplays.Remove(arg);
        arg.gameObject.SetActive(false);
        Destroy(arg.gameObject, 0.1f);
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

    void UpdateNatureDrops()
    {
        targetCreature.loot.natureDrops.Clear();
        float parsedValue = 0f;
        foreach (PropertyPairDisplay display in natureDropsDisplays)
        {
            if (float.TryParse(display.propertyValue.text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out parsedValue))
            {
                targetCreature.loot.natureDrops.Add(new NameFloatPair(display.propertyName.text, parsedValue));
            }
        }
    }

    void UpdateObjectDrops()
    {
        targetCreature.loot.objectDrops.Clear();
        float parsedValue = 0f;
        foreach (PropertyPairDisplay display in objectDropsDisplays)
        {
            if (float.TryParse(display.propertyValue.text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out parsedValue))
            {
                targetCreature.loot.objectDrops.Add(new NameFloatPair(display.propertyName.text, parsedValue));
            }
        }
    }

    private void AddObjectDropDisplay(string name, string number)
    {
        PropertyPairDisplay spawnedDisplay = PropertyPairDisplay.Spawn(name, number, objectDropsContainer);
        spawnedDisplay.Setup(name, number, PropertyDisplayTypes.Float);
        objectDropsDisplays.Add(spawnedDisplay);
        spawnedDisplay.OnDeleteRequested += ObjectDropsOnDeleteRequestedResponse;
        spawnedDisplay.OnValueChanged2 += ObjectDropsOnValueChanged2Response;
    }

    private void AddObjectDropDisplay()
    {
        AddObjectDropDisplay("NewObjectDrop","0");
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
        AddNatureDropDisplay("NewNatureDrop","0");
    }

}
