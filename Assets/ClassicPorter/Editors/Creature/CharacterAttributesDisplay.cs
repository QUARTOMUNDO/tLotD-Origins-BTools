using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public PropertyDisplay peripheralFactor;
    public PropertyDisplay bodyDamageRepeatTime;
    public PropertyDisplay bodyAttackWeight;
    public PropertyDisplay bodySufferWeight;
    public PropertyDisplay attackDamageRepeatTime;
    public PropertyDisplay attackWeight;
    public PropertyDisplay pullDirection;



    private void Awake()
    {
        if (normalSplash) normalSplash.OnValueChanged2 += PropertyChangeResponse; normalSplash.gameObject.name = "normalSplash";
        if (weakSplash) weakSplash.OnValueChanged2 += PropertyChangeResponse; weakSplash.gameObject.name = "weakSplash";
        if (defenceSplash) defenceSplash.OnValueChanged2 += PropertyChangeResponse; defenceSplash.gameObject.name = "defenceSplash";
        if (abnormalSplash) abnormalSplash.OnValueChanged2 += PropertyChangeResponse; abnormalSplash.gameObject.name = "abnormalSplash";
        if (sufferCriticable) sufferCriticable.OnValueChanged2 += PropertyChangeResponse; sufferCriticable.gameObject.name = "sufferCriticable";
        if (damagerCriticable) damagerCriticable.OnValueChanged2 += PropertyChangeResponse; damagerCriticable.gameObject.name = "damagerCriticable";
        if (regenerable) regenerable.OnValueChanged2 += PropertyChangeResponse; regenerable.gameObject.name = "regenerable";
        if (degenerable) degenerable.OnValueChanged2 += PropertyChangeResponse; degenerable.gameObject.name = "degenerable";
        if (inflictsBodyDamage) inflictsBodyDamage.OnValueChanged2 += PropertyChangeResponse; inflictsBodyDamage.gameObject.name = "inflictsBodyDamage";
        if (inflictsAttackDamage) inflictsAttackDamage.OnValueChanged2 += PropertyChangeResponse; inflictsAttackDamage.gameObject.name = "inflictsAttackDamage";
        if (baseLevel) baseLevel.OnValueChanged2 += PropertyChangeResponse; baseLevel.gameObject.name = "baseLevel";
        if (pullTypeID) pullTypeID.OnValueChanged2 += PropertyChangeResponse; pullTypeID.gameObject.name = "pullTypeID";
        if (mysticalFactor) mysticalFactor.OnValueChanged2 += PropertyChangeResponse; mysticalFactor.gameObject.name = "mysticalFactor";
        if (attackPower) attackPower.OnValueChanged2 += PropertyChangeResponse; attackPower.gameObject.name = "attackPower";
        if (bodyPower) bodyPower.OnValueChanged2 += PropertyChangeResponse; bodyPower.gameObject.name = "bodyPower";
        if (strengthFactor) strengthFactor.OnValueChanged2 += PropertyChangeResponse; strengthFactor.gameObject.name = "strengthFactor";
        if (resistanceFactor) resistanceFactor.OnValueChanged2 += PropertyChangeResponse; resistanceFactor.gameObject.name = "resistanceFactor";
        if (efficiencyFactor) efficiencyFactor.OnValueChanged2 += PropertyChangeResponse; efficiencyFactor.gameObject.name = "efficiencyFactor";
        if (peripheralFactor) peripheralFactor.OnValueChanged2 += PropertyChangeResponse; peripheralFactor.gameObject.name = "peripheralFactor";
        if (bodyDamageRepeatTime) bodyDamageRepeatTime.OnValueChanged2 += PropertyChangeResponse; bodyDamageRepeatTime.gameObject.name = "bodyDamageRepeatTime";
        if (bodyAttackWeight) bodyAttackWeight.OnValueChanged2 += PropertyChangeResponse; bodyAttackWeight.gameObject.name = "bodyAttackWeight";
        if (bodySufferWeight) bodySufferWeight.OnValueChanged2 += PropertyChangeResponse; bodySufferWeight.gameObject.name = "bodySufferWeight";
        if (attackDamageRepeatTime) attackDamageRepeatTime.OnValueChanged2 += PropertyChangeResponse; attackDamageRepeatTime.gameObject.name = "attackDamageRepeatTime";
        if (attackWeight) attackWeight.OnValueChanged2 += PropertyChangeResponse; attackWeight.gameObject.name = "attackWeight";
        if (pullDirection) pullDirection.OnValueChanged2 += PropertyChangeResponse; pullDirection.gameObject.name = "pullDirection";

    }

    private void PropertyChangeResponse(string arg, string arg1)
    {
        switch (arg)
        {
            case "normalSplash": targetCreature.characterAttributes.normalSplash = arg1; break;
            case "weakSplash": targetCreature.characterAttributes.weakSplash = arg1; break;
            case "defenceSplash": targetCreature.characterAttributes.defenceSplash = arg1; break;
            case "abnormalSplash": targetCreature.characterAttributes.abnormalSplash = arg1; break;
            case "sufferCriticable": targetCreature.characterAttributes.sufferCriticable = bool.Parse(arg1); break;
            case "damagerCriticable": targetCreature.characterAttributes.damagerCriticable = bool.Parse(arg1); break;
            case "regenerable": targetCreature.characterAttributes.regenerable = bool.Parse(arg1); break;
            case "degenerable": targetCreature.characterAttributes.degenerable = bool.Parse(arg1); break;
            case "inflictsBodyDamage": targetCreature.characterAttributes.inflictsBodyDamage = bool.Parse(arg1); break;
            case "inflictsAttackDamage": targetCreature.characterAttributes.inflictsAttackDamage = bool.Parse(arg1); break;
            case "baseLevel": targetCreature.characterAttributes.baseLevel = int.Parse(arg1); break;
            case "pullTypeID": targetCreature.characterAttributes.pullTypeID = int.Parse(arg1); break;
            case "mysticalFactor": targetCreature.characterAttributes.mysticalFactor = float.Parse(arg1); break;
            case "attackPower": targetCreature.characterAttributes.attackPower = float.Parse(arg1); break;
            case "bodyPower": targetCreature.characterAttributes.bodyPower = float.Parse(arg1); break;
            case "strengthFactor": targetCreature.characterAttributes.strengthFactor = float.Parse(arg1); break;
            case "resistanceFactor": targetCreature.characterAttributes.resistanceFactor = float.Parse(arg1); break;
            case "efficiencyFactor": targetCreature.characterAttributes.efficiencyFactor = float.Parse(arg1); break;
            case "peripheralFactor": targetCreature.characterAttributes.peripheralFactor = float.Parse(arg1); break;
            case "bodyDamageRepeatTime": targetCreature.characterAttributes.bodyDamageRepeatTime = float.Parse(arg1); break;
            case "bodyAttackWeight": targetCreature.characterAttributes.bodyAttackWeight = float.Parse(arg1); break;
            case "bodySufferWeight": targetCreature.characterAttributes.bodySufferWeight = float.Parse(arg1); break;
            case "attackDamageRepeatTime": targetCreature.characterAttributes.attackDamageRepeatTime = float.Parse(arg1); break;
            case "attackWeight": targetCreature.characterAttributes.attackWeight = float.Parse(arg1); break;
            case "pullDirection": targetCreature.characterAttributes.pullDirection = float.Parse(arg1); break;


            default:
                break;
        }
    }

    public void Setup(ref CreatureEntry creatureEntry)
    {
        targetCreature = creatureEntry;


        normalSplash.Setup("normalSplash", creatureEntry.currentData.characterAttributes.normalSplash, UtilDefinitions.PropertyDisplayTypes.String, creatureEntry.defaultData.characterAttributes.normalSplash.ToString(), creatureEntry.sourceData.characterAttributes.normalSplash.ToString());
        weakSplash.Setup("weakSplash", creatureEntry.currentData.characterAttributes.weakSplash, UtilDefinitions.PropertyDisplayTypes.String, creatureEntry.defaultData.characterAttributes.weakSplash.ToString(), creatureEntry.sourceData.characterAttributes.weakSplash.ToString());
        defenceSplash.Setup("defenceSplash", creatureEntry.currentData.characterAttributes.defenceSplash, UtilDefinitions.PropertyDisplayTypes.String, creatureEntry.defaultData.characterAttributes.defenceSplash.ToString(), creatureEntry.sourceData.characterAttributes.defenceSplash.ToString());
        abnormalSplash.Setup("abnormalSplash", creatureEntry.currentData.characterAttributes.abnormalSplash, UtilDefinitions.PropertyDisplayTypes.String, creatureEntry.defaultData.characterAttributes.abnormalSplash.ToString(), creatureEntry.sourceData.characterAttributes.abnormalSplash.ToString());
        sufferCriticable.Setup("sufferCriticable", creatureEntry.currentData.characterAttributes.sufferCriticable.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, creatureEntry.defaultData.characterAttributes.sufferCriticable.ToString(), creatureEntry.sourceData.characterAttributes.sufferCriticable.ToString());
        damagerCriticable.Setup("damagerCriticable", creatureEntry.currentData.characterAttributes.damagerCriticable.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, creatureEntry.defaultData.characterAttributes.damagerCriticable.ToString(), creatureEntry.sourceData.characterAttributes.damagerCriticable.ToString());
        regenerable.Setup("regenerable", creatureEntry.currentData.characterAttributes.regenerable.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, creatureEntry.defaultData.characterAttributes.regenerable.ToString(), creatureEntry.sourceData.characterAttributes.regenerable.ToString());
        degenerable.Setup("degenerable", creatureEntry.currentData.characterAttributes.degenerable.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, creatureEntry.defaultData.characterAttributes.degenerable.ToString(), creatureEntry.sourceData.characterAttributes.degenerable.ToString());
        inflictsBodyDamage.Setup("inflictsBodyDamage", creatureEntry.currentData.characterAttributes.inflictsBodyDamage.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, creatureEntry.defaultData.characterAttributes.inflictsBodyDamage.ToString(), creatureEntry.sourceData.characterAttributes.inflictsBodyDamage.ToString());
        inflictsAttackDamage.Setup("inflictsAttackDamage", creatureEntry.currentData.characterAttributes.inflictsAttackDamage.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, creatureEntry.defaultData.characterAttributes.inflictsAttackDamage.ToString(), creatureEntry.sourceData.characterAttributes.inflictsAttackDamage.ToString());
        baseLevel.Setup("baseLevel", creatureEntry.currentData.characterAttributes.baseLevel.ToString(), UtilDefinitions.PropertyDisplayTypes.Int, creatureEntry.defaultData.characterAttributes.baseLevel.ToString(), creatureEntry.sourceData.characterAttributes.baseLevel.ToString());
        pullTypeID.Setup("pullTypeID", creatureEntry.currentData.characterAttributes.pullTypeID.ToString(), UtilDefinitions.PropertyDisplayTypes.Int, creatureEntry.defaultData.characterAttributes.pullTypeID.ToString(), creatureEntry.sourceData.characterAttributes.pullTypeID.ToString());
        mysticalFactor.Setup("mysticalFactor", creatureEntry.currentData.characterAttributes.mysticalFactor.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.characterAttributes.mysticalFactor.ToString(), creatureEntry.sourceData.characterAttributes.mysticalFactor.ToString());
        attackPower.Setup("attackPower", creatureEntry.currentData.characterAttributes.attackPower.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.characterAttributes.attackPower.ToString(), creatureEntry.sourceData.characterAttributes.attackPower.ToString());
        bodyPower.Setup("bodyPower", creatureEntry.currentData.characterAttributes.bodyPower.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.characterAttributes.bodyPower.ToString(), creatureEntry.sourceData.characterAttributes.bodyPower.ToString());
        strengthFactor.Setup("strengthFactor", creatureEntry.currentData.characterAttributes.strengthFactor.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.characterAttributes.strengthFactor.ToString(), creatureEntry.sourceData.characterAttributes.strengthFactor.ToString());
        resistanceFactor.Setup("resistanceFactor", creatureEntry.currentData.characterAttributes.resistanceFactor.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.characterAttributes.resistanceFactor.ToString(), creatureEntry.sourceData.characterAttributes.resistanceFactor.ToString());
        efficiencyFactor.Setup("efficiencyFactor", creatureEntry.currentData.characterAttributes.efficiencyFactor.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.characterAttributes.efficiencyFactor.ToString(), creatureEntry.sourceData.characterAttributes.efficiencyFactor.ToString());
        peripheralFactor.Setup("peripheralFactor", creatureEntry.currentData.characterAttributes.peripheralFactor.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.characterAttributes.peripheralFactor.ToString(), creatureEntry.sourceData.characterAttributes.peripheralFactor.ToString());
        bodyDamageRepeatTime.Setup("bodyDamageRepeatTime", creatureEntry.currentData.characterAttributes.bodyDamageRepeatTime.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.characterAttributes.bodyDamageRepeatTime.ToString(), creatureEntry.sourceData.characterAttributes.bodyDamageRepeatTime.ToString());
        bodyAttackWeight.Setup("bodyAttackWeight", creatureEntry.currentData.characterAttributes.bodyAttackWeight.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.characterAttributes.bodyAttackWeight.ToString(), creatureEntry.sourceData.characterAttributes.bodyAttackWeight.ToString());
        bodySufferWeight.Setup("bodySufferWeight", creatureEntry.currentData.characterAttributes.bodySufferWeight.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.characterAttributes.bodySufferWeight.ToString(), creatureEntry.sourceData.characterAttributes.bodySufferWeight.ToString());
        attackDamageRepeatTime.Setup("attackDamageRepeatTime", creatureEntry.currentData.characterAttributes.attackDamageRepeatTime.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.characterAttributes.attackDamageRepeatTime.ToString(), creatureEntry.sourceData.characterAttributes.attackDamageRepeatTime.ToString());
        attackWeight.Setup("attackWeight", creatureEntry.currentData.characterAttributes.attackWeight.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.characterAttributes.attackWeight.ToString(), creatureEntry.sourceData.characterAttributes.attackWeight.ToString());
        pullDirection.Setup("pullDirection", creatureEntry.currentData.characterAttributes.pullDirection.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.characterAttributes.pullDirection.ToString(), creatureEntry.sourceData.characterAttributes.pullDirection.ToString());
    }

    private void OnDrawGizmosSelected()
    {
        if (normalSplash) normalSplash.gameObject.name = "normalSplash";
        if (weakSplash) weakSplash.gameObject.name = "weakSplash";
        if (defenceSplash) defenceSplash.gameObject.name = "defenceSplash";
        if (abnormalSplash) abnormalSplash.gameObject.name = "abnormalSplash";
        if (sufferCriticable) sufferCriticable.gameObject.name = "sufferCriticable";
        if (damagerCriticable) damagerCriticable.gameObject.name = "damagerCriticable";
        if (regenerable) regenerable.gameObject.name = "regenerable";
        if (degenerable) degenerable.gameObject.name = "degenerable";
        if (inflictsBodyDamage) inflictsBodyDamage.gameObject.name = "inflictsBodyDamage";
        if (inflictsAttackDamage) inflictsAttackDamage.gameObject.name = "inflictsAttackDamage";
        if (baseLevel) baseLevel.gameObject.name = "baseLevel";
        if (pullTypeID) pullTypeID.gameObject.name = "pullTypeID";
        if (mysticalFactor) mysticalFactor.gameObject.name = "mysticalFactor";
        if (attackPower) attackPower.gameObject.name = "attackPower";
        if (bodyPower) bodyPower.gameObject.name = "bodyPower";
        if (strengthFactor) strengthFactor.gameObject.name = "strengthFactor";
        if (resistanceFactor) resistanceFactor.gameObject.name = "resistanceFactor";
        if (efficiencyFactor) efficiencyFactor.gameObject.name = "efficiencyFactor";
        if (peripheralFactor) peripheralFactor.gameObject.name = "peripheralFactor";
        if (bodyDamageRepeatTime) bodyDamageRepeatTime.gameObject.name = "bodyDamageRepeatTime";
        if (bodyAttackWeight) bodyAttackWeight.gameObject.name = "bodyAttackWeight";
        if (bodySufferWeight) bodySufferWeight.gameObject.name = "bodySufferWeight";
        if (attackDamageRepeatTime) attackDamageRepeatTime.gameObject.name = "attackDamageRepeatTime";
        if (attackWeight) attackWeight.gameObject.name = "attackWeight";
        if (pullDirection) pullDirection.gameObject.name = "pullDirection";
    }

}
