using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UtilDefinitions;

public class PropertyIntFloatDisplay : MonoBehaviour
{

    public TMP_InputField propertyName;
    public TMP_InputField propertyAmount;
    public TMP_InputField propertyValue;
    public TMP_Text propertyDefaultValue;
    public TMP_Text propertySourceValue;
    public Button deleteButton;
    public PropertyDisplayTypes displayAmountType = PropertyDisplayTypes.Int;
    public PropertyDisplayTypes displayValueType = PropertyDisplayTypes.Float;
    string defaultValue = "";
    string sourceValue = "";

    [Header("Preview")]
    public bool enablePreview = true;
    public string previewPropertyName = "Prop name";
    public string previewPropertyAmount = "Prop value";
    public string previewPropertyValue = "Prop value";

    public event DelString OnValueChanged;
    public event DelStringString OnValueChanged2;
    public event DelStringStringString OnValueChanged3;
    public event DelPropertyIntFloatDisplay OnDeleteRequested;

    public void Setup(string newName, string newAmount, string newValue, PropertyDisplayTypes amountType = PropertyDisplayTypes.Int, PropertyDisplayTypes valueType = PropertyDisplayTypes.Float, string newDefaultValue = "DefaultValue", string newSourceValue = "SourceValue")
    {
        propertyName.text = newName;
        gameObject.name = newName;
        propertyValue.text = newValue;
        propertyAmount.text = newAmount;
        displayValueType = valueType;
        displayAmountType = amountType;
        defaultValue = newDefaultValue;
        sourceValue = newSourceValue;
        propertyDefaultValue.text = defaultValue;
        propertySourceValue.text = sourceValue;

        UpdateTextColor();
    }

    private void OnEnable()
    {
        propertyValue.onEndEdit.AddListener(ValueChangedResponse);
        propertyAmount.onEndEdit.AddListener(AmountChangedResponse);
        propertyName.onEndEdit.AddListener(NameChangedResponse);
        deleteButton.onClick.AddListener(DeleteButtonClickedResponse);
    }
    private void OnDisable()
    {
        propertyValue.onEndEdit.RemoveListener(ValueChangedResponse);
        propertyAmount.onEndEdit.RemoveListener(AmountChangedResponse);
        propertyName.onEndEdit.RemoveListener(NameChangedResponse);
        deleteButton.onClick.RemoveListener(DeleteButtonClickedResponse);
    }


    private void DeleteButtonClickedResponse()
    {
        OnDeleteRequested?.Invoke(this);
    }

    public void Setup(LootObjectDropElement dropElement, PropertyDisplayTypes newAmountType = PropertyDisplayTypes.Int, PropertyDisplayTypes newValueType = PropertyDisplayTypes.Float)
    {
        Setup(dropElement.name,dropElement.amount.ToString(System.Globalization.CultureInfo.InvariantCulture), dropElement.chance.ToString(System.Globalization.CultureInfo.InvariantCulture),newAmountType, newValueType);
    }

    public static PropertyIntFloatDisplay Spawn(LootObjectDropElement dropElement, Transform parent = null, GameObject basePrefabType = null)
    {
        return Spawn(dropElement.name, dropElement.amount.ToString(System.Globalization.CultureInfo.InvariantCulture), dropElement.chance.ToString(System.Globalization.CultureInfo.InvariantCulture), parent, basePrefabType);
    }

    public static PropertyIntFloatDisplay Spawn(string pName = "", string pAmount = "", string pValue = "", Transform parent = null, GameObject basePrefabType = null)
    {
        GameObject prefab = basePrefabType;
        GameObject spawnedObject = null;
        PropertyIntFloatDisplay spawnedDisplay = null;
        if (!prefab) prefab = PortController.single.propertyIntFloatDisplayPrefab;

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
            spawnedDisplay.Setup(pName,pAmount, pValue);
        }

        return spawnedDisplay;
    }

    private void ValueChangedResponse(string newValue)
    {
        switch (displayValueType)
        {
            case PropertyDisplayTypes.String:

                break;
            case PropertyDisplayTypes.Float:
                if (!float.TryParse(newValue, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float a)) { propertyValue.text = "0"; return; }
                break;
            case PropertyDisplayTypes.Int:
                if (!int.TryParse(newValue, out int b)) { propertyValue.text = "0"; return; }
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
        OnValueChanged3?.Invoke(gameObject.name, propertyAmount.text, propertyValue.text);

    }

    private void AmountChangedResponse(string newValue)
    {
        switch (displayAmountType)
        {
            case PropertyDisplayTypes.String:

                break;
            case PropertyDisplayTypes.Float:
                if (!float.TryParse(newValue, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float a)) { propertyAmount.text = "0"; return; }
                break;
            case PropertyDisplayTypes.Int:
                if (!int.TryParse(newValue, out int b)) { propertyAmount.text = "0"; return; }
                break;
            case PropertyDisplayTypes.Bool:
                if (!bool.TryParse(newValue, out bool c)) { propertyAmount.text = defaultValue; return; }
                break;
            default:
                break;
        }
        UpdateTextColor();
        OnValueChanged?.Invoke(propertyAmount.text);
        OnValueChanged2?.Invoke(gameObject.name, propertyAmount.text);
        OnValueChanged3?.Invoke(gameObject.name, propertyAmount.text, propertyValue.text);

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
        OnValueChanged3?.Invoke(gameObject.name, propertyAmount.text, propertyValue.text);
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
            if (propertyAmount) propertyAmount.text = previewPropertyAmount;
        }
    }

}
