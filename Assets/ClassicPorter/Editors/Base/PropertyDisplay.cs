using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UtilDefinitions;

public class PropertyDisplay : MonoBehaviour
{    

    public TMP_Text propertyName;
    public TMP_InputField propertyValue;

    public void Setup(string newName, string newValue)
    {
        propertyName.text = newName;
        propertyValue.text = newValue;
    }

    public void Setup(NamePair namePair)
    {
        Setup(namePair.name, namePair.value);
    }

    public static PropertyDisplay Spawn(NamePair namePair, Transform parent = null, GameObject basePrefabType = null)
    {
        return Spawn(namePair.name, namePair.value, parent, basePrefabType);
    }

    public static PropertyDisplay Spawn(string pName = "", string pValue = "", Transform parent = null, GameObject basePrefabType=null)
    {
        GameObject prefab = basePrefabType;
        GameObject spawnedObject = null;
        PropertyDisplay spawnedDisplay = null;
        if (!prefab) prefab = PortController.single.propertyDisplayPrefab;

        if (parent)
        {
            spawnedObject = Instantiate(prefab, parent);
        }
        else
        {
            spawnedObject = Instantiate(prefab);
        }        

        if (spawnedObject.TryGetComponent(out spawnedDisplay))
        {
            spawnedDisplay.Setup(pName, pValue);
        }

        return spawnedDisplay;
    }

}
