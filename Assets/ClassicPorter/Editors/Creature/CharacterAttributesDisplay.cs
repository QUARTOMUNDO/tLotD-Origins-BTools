using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System;

public class CharacterAttributesDisplay : MonoBehaviour
{

    CreatureData targetCreature;

    public PropertyDisplay normalSplash;
    public PropertyDisplay weakSplash;
    public PropertyDisplay defenceSplash;
    public PropertyDisplay abnormalSplash;
    public PropertyDisplay sufferCriticable;
    public PropertyDisplay damagerCriticable;
    public PropertyDisplay regenerable;
    public PropertyDisplay degenerable;
    public PropertyDisplay inflictsBodyDamage;
    public PropertyDisplay inflictsAttackDamage;
    public PropertyDisplay baseLevel;
    public PropertyDisplay pullTypeID;
    public PropertyDisplay mysticalFactor;
    public PropertyDisplay attackPower;
    public PropertyDisplay bodyPower;
    public PropertyDisplay strengthFactor;
    public PropertyDisplay resistanceFactor;
    public PropertyDisplay efficiencyFactor;

    public PropertyDisplay mysticalEfficiencyFactor;

    public PropertyDisplay peripheralFactor;
    public PropertyDisplay bodyDamageRepeatTime;
    public PropertyDisplay bodyAttackWeight;
    public PropertyDisplay bodySufferWeight;
    public PropertyDisplay attackDamageRepeatTime;
    public PropertyDisplay attackWeight;
    public PropertyDisplay pullDirection;


    private void Awake(){
        BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        FieldInfo[] fields = this.GetType().GetFields(flags).Where(f => typeof(PropertyDisplay).IsAssignableFrom(f.FieldType)).ToArray();

        foreach (FieldInfo fieldInfo in fields) {
            PropertyDisplay fieldDisplay = null;

            string fieldName = fieldInfo.Name;
            object fieldValueObj = fieldInfo.GetValue(this);

            if (fieldValueObj != null){
                fieldDisplay = fieldValueObj as PropertyDisplay;
            }

            if (fieldDisplay != null) {
                fieldDisplay.OnValueChanged2 += PropertyChangeResponse;
                fieldDisplay.gameObject.name = fieldName;
            }
        }
    }

    private void PropertyChangeResponse(string arg, string arg1){
        FieldInfo Field = targetCreature.characterAttributes.GetType().GetField(arg, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        object currentValue = Field.GetValue(targetCreature.characterAttributes);

        if (Field != null){
            if (currentValue is bool){
                Field.SetValue(currentValue, bool.Parse(arg1));
            }
            else if (currentValue is float) {
                Field.SetValue(currentValue, float.Parse(arg1, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture));
            }
            else {
                Field.SetValue(currentValue, arg1);
            }
        }
    }

    public void Setup(ref CreatureEntry creatureEntry) {
        targetCreature = creatureEntry;

        BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        FieldInfo[] fields = creatureEntry.currentData.characterAttributes.GetType().GetFields(flags);

        foreach (FieldInfo field in fields)
        {
            object currentValue = field.GetValue(creatureEntry.currentData.characterAttributes);
            object defaultValue = field.GetValue(creatureEntry.defaultData.characterAttributes);
            object sourceValue = field.GetValue(creatureEntry.sourceData.characterAttributes);

            string fieldName = field.Name;
            UtilDefinitions.PropertyDisplayTypes displayType = UtilDefinitions.PropertyDisplayTypes.String;

            FieldInfo DisplayFieldInfo = this.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            PropertyDisplay fieldDisplay = DisplayFieldInfo.GetValue(this) as PropertyDisplay;

            if (currentValue is bool)
            {
                displayType = UtilDefinitions.PropertyDisplayTypes.Bool;
            }
            else if (currentValue is float)
            {
                displayType = UtilDefinitions.PropertyDisplayTypes.Float;
            }

            MethodInfo setupMethod = typeof(PropertyDisplay).GetMethod("Setup", new Type[] { typeof(string), typeof(string), typeof(UtilDefinitions.PropertyDisplayTypes), typeof(string), typeof(string) });

            string currentStr = (displayType == UtilDefinitions.PropertyDisplayTypes.Float) ? ((float)currentValue).ToString("G", CultureInfo.InvariantCulture) : currentValue.ToString();
            string defaultStr = (displayType == UtilDefinitions.PropertyDisplayTypes.Float) ? ((float)defaultValue).ToString("G", CultureInfo.InvariantCulture) : defaultValue.ToString();
            string sourceStr = (displayType == UtilDefinitions.PropertyDisplayTypes.Float) ? ((float)sourceValue).ToString("G", CultureInfo.InvariantCulture) : sourceValue.ToString();

            setupMethod.Invoke(fieldDisplay, new object[] { fieldName, currentStr, displayType, defaultStr, sourceStr });
        }
    }

    private void OnDrawGizmosSelected(){
        BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        FieldInfo[] fields = this.GetType().GetFields(flags).Where(f => typeof(PropertyDisplay).IsAssignableFrom(f.FieldType)).ToArray();

        foreach (FieldInfo fieldInfo in fields){
            PropertyDisplay fieldDisplay = null;

            string fieldName = fieldInfo.Name;
            object fieldValueObj = fieldInfo.GetValue(this);

            if (fieldValueObj != null) {
                fieldDisplay = fieldValueObj as PropertyDisplay;
            }

            if (fieldDisplay != null){
                fieldDisplay.gameObject.name = fieldName;
                fieldDisplay.previewPropertyName = fieldName;
            }
        }
    }
}
