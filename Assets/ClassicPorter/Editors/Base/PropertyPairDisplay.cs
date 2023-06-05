using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UtilDefinitions;

public class PropertyPairDisplay : MonoBehaviour
{

    public TMP_InputField propertyName;
    public TMP_InputField propertyValue;
    public TMP_Text propertyDefaultValue;
    public TMP_Text propertySourceValue;
    public Button deleteButton;
    public PropertyDisplayTypes displayType = PropertyDisplayTypes.String;
    string defaultValue = "";
    string sourceValue = "";

    [Header("Preview")]
    public bool enablePreview = true;
    public string previewPropertyName = "Prop name";
    public string previewPropertyValue = "Prop value";

    public event DelString OnValueChanged;
    public event DelStringString OnValueChanged2;
    public event DelPropertyPairDisplay OnDeleteRequested;

    public void Setup(string newName, string newValue, PropertyDisplayTypes type = PropertyDisplayTypes.String, string newDefaultValue = "DefaultValue", string newSourceValue = "SourceValue")
    {
        propertyName.text = newName;
        gameObject.name = newName;
        propertyValue.text = newValue;
        displayType = type;
        defaultValue = newDefaultValue;
        sourceValue = newSourceValue;
        propertyDefaultValue.text = defaultValue;
        propertySourceValue.text = sourceValue;
        
        UpdateTextColor();
    }

    private void OnEnable()
    {
        propertyValue.onEndEdit.AddListener(ValueChangedResponse);
        propertyName.onEndEdit.AddListener(NameChangedResponse);
        deleteButton.onClick.AddListener(DeleteButtonClickedResponse);
    }
    private void OnDisable()
    {
        propertyValue.onEndEdit.RemoveListener(ValueChangedResponse);
        propertyName.onEndEdit.RemoveListener(NameChangedResponse);
        deleteButton.onClick.RemoveListener(DeleteButtonClickedResponse);
    }


    private void DeleteButtonClickedResponse()
    {
        OnDeleteRequested?.Invoke(this);
    }

    public void Setup(NamePair namePair, PropertyDisplayTypes type = PropertyDisplayTypes.String)
    {
        Setup(namePair.name, namePair.value, type);
    }

    public static PropertyPairDisplay Spawn(NamePair namePair, Transform parent = null, GameObject basePrefabType = null)
    {
        return Spawn(namePair.name, namePair.value, parent, basePrefabType);
    }

    public static PropertyPairDisplay Spawn(string pName = "", string pValue = "", Transform parent = null, GameObject basePrefabType = null)
    {
        GameObject prefab = basePrefabType;
        GameObject spawnedObject = null;
        PropertyPairDisplay spawnedDisplay = null;
        if (!prefab) prefab = PortController.single.propertyPairDisplayPrefab;

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

    private void ValueChangedResponse(string newValue)
    {
        switch (displayType)
        {
            case PropertyDisplayTypes.String:

                break;
            case PropertyDisplayTypes.Float:
                if (!float.TryParse(newValue, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float a)) { propertyValue.text = defaultValue; return; }
                break;
            case PropertyDisplayTypes.Int:
                if (!int.TryParse(newValue, out int b)) { propertyValue.text = defaultValue; return; }
                break;
            case PropertyDisplayTypes.Bool:
                if (!bool.TryParse(newValue, out bool c)) { propertyValue.text = defaultValue; return; }
                break;
            default:
                break;
        }
        UpdateTextColor();
        OnValueChanged?.Invoke(propertyValue.text);
        OnValueChanged2?.Invoke(gameObject.name, propertyValue.text);

    }

    private void NameChangedResponse(string newName)
    {
        if (newName == "")
        {
            previewPropertyName = name;
            return;
        }

        name = newName;
        //OnValueChanged?.Invoke(propertyValue.text);
        OnValueChanged2?.Invoke(gameObject.name, propertyValue.text);
    }

    private void UpdateTextColor()
    {
        //if (propertyValue.text.Equals(defaultValue)) { propertyValue.textComponent.color = Color.white; return; }
        //if (propertyValue.text.Equals(sourceValue)) { propertyValue.textComponent.color = Color.blue; return; }
        //propertyValue.textComponent.color = Color.green;
    }

    private void OnDrawGizmosSelected()
    {
        if (enablePreview)
        {
            gameObject.name = previewPropertyName;
            if (propertyName) propertyName.text = previewPropertyName;
            if (propertyValue) propertyValue.text = previewPropertyValue;
        }
    }

}
