using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class BehaviorPropertiesDisplay : MonoBehaviour
{

    CreatureData targetCreature;

    public PropertyDisplay retainDeath;
    public PropertyDisplay craven;
    public PropertyDisplay startAggressive;
    public PropertyDisplay naturallyAggressive;
    public PropertyDisplay racialAggressive;
    public PropertyDisplay naturallyProtector;
    public PropertyDisplay groundAdapt;
    public PropertyDisplay invertableCharacter;
    public PropertyDisplay delayedInverted;
    public PropertyDisplay jumpFrame;
    public PropertyDisplay timeAgonizing;
    public PropertyDisplay actionsMaxRange;
    public PropertyDisplay patrolRange;
    public PropertyDisplay followRange;
    public PropertyDisplay combatRange;
    public PropertyDisplay meleeRange;
    public PropertyDisplay idealRange;
    public PropertyDisplay speed;
    public PropertyDisplay jumpFrequency;
    public PropertyDisplay patrolRangeRatio;
    public PropertyDisplay actionDelay;

    /*    Formated Var Copy
     *    
     *    retainDeath        
     *    craven             
     *    startAggressive    
     *    naturallyAggressive
     *    racialAggressive   
     *    naturallyProtector 
     *    groundAdapt        
     *    invertableCharacter
     *    delayedInverted    
     *    jumpFrame          
     *    timeAgonizing      
     *    actionsMaxRange    
     *    patrolRange        
     *    followRange        
     *    combatRange        
     *    meleeRange         
     *    idealRange         
     *    speed              
     *    jumpFrequency      
     *    patrolRangeRatio   
     *    actionDelay             
     */

    private void OnDrawGizmosSelected()
    {
        if (retainDeath) retainDeath.gameObject.name = "retainDeath"; retainDeath.previewPropertyName = "retainDeath";
        if (craven) craven.gameObject.name = "craven"; craven.previewPropertyName = "craven";
        if (startAggressive) startAggressive.gameObject.name = "startAggressive"; startAggressive.previewPropertyName = "startAggressive";
        if (naturallyAggressive) naturallyAggressive.gameObject.name = "naturallyAggressive"; naturallyAggressive.previewPropertyName = "naturallyAggressive";
        if (racialAggressive) racialAggressive.gameObject.name = "racialAggressive"; racialAggressive.previewPropertyName = "racialAggressive";
        if (naturallyProtector) naturallyProtector.gameObject.name = "naturallyProtector"; naturallyProtector.previewPropertyName = "naturallyProtector";
        if (groundAdapt) groundAdapt.gameObject.name = "groundAdapt"; groundAdapt.previewPropertyName = "groundAdapt";
        if (invertableCharacter) invertableCharacter.gameObject.name = "invertableCharacter"; invertableCharacter.previewPropertyName = "invertableCharacter";
        if (delayedInverted) delayedInverted.gameObject.name = "delayedInverted"; delayedInverted.previewPropertyName = "delayedInverted";
        if (jumpFrame) jumpFrame.gameObject.name = "jumpFrame"; jumpFrame.previewPropertyName = "jumpFrame";
        if (timeAgonizing) timeAgonizing.gameObject.name = "timeAgonizing"; timeAgonizing.previewPropertyName = "timeAgonizing";
        if (actionsMaxRange) actionsMaxRange.gameObject.name = "actionsMaxRange"; actionsMaxRange.previewPropertyName = "actionsMaxRange";
        if (patrolRange) patrolRange.gameObject.name = "patrolRange"; patrolRange.previewPropertyName = "patrolRange";
        if (followRange) followRange.gameObject.name = "followRange"; followRange.previewPropertyName = "followRange";
        if (combatRange) combatRange.gameObject.name = "combatRange"; combatRange.previewPropertyName = "combatRange";
        if (meleeRange) meleeRange.gameObject.name = "meleeRange"; meleeRange.previewPropertyName = "meleeRange";
        if (idealRange) idealRange.gameObject.name = "idealRange"; idealRange.previewPropertyName = "idealRange";
        if (speed) speed.gameObject.name = "speed"; speed.previewPropertyName = "speed";
        if (jumpFrequency) jumpFrequency.gameObject.name = "jumpFrequency"; jumpFrequency.previewPropertyName = "jumpFrequency";
        if (patrolRangeRatio) patrolRangeRatio.gameObject.name = "patrolRangeRatio"; patrolRangeRatio.previewPropertyName = "patrolRangeRatio";
        if (actionDelay) actionDelay.gameObject.name = "actionDelay"; actionDelay.previewPropertyName = "actionDelay";
    }

    private void Awake()
    {
        if (retainDeath) retainDeath.OnValueChanged2 += PropertyChangeResponse; retainDeath.gameObject.name = "retainDeath";
        if (craven) craven.OnValueChanged2 += PropertyChangeResponse; craven.gameObject.name = "craven";
        if (startAggressive) startAggressive.OnValueChanged2 += PropertyChangeResponse; startAggressive.gameObject.name = "startAggressive";
        if (naturallyAggressive) naturallyAggressive.OnValueChanged2 += PropertyChangeResponse; naturallyAggressive.gameObject.name = "naturallyAggressive";
        if (racialAggressive) racialAggressive.OnValueChanged2 += PropertyChangeResponse; racialAggressive.gameObject.name = "racialAggressive";
        if (naturallyProtector) naturallyProtector.OnValueChanged2 += PropertyChangeResponse; naturallyProtector.gameObject.name = "naturallyProtector";
        if (groundAdapt) groundAdapt.OnValueChanged2 += PropertyChangeResponse; groundAdapt.gameObject.name = "groundAdapt";
        if (invertableCharacter) invertableCharacter.OnValueChanged2 += PropertyChangeResponse; invertableCharacter.gameObject.name = "invertableCharacter";
        if (delayedInverted) delayedInverted.OnValueChanged2 += PropertyChangeResponse; delayedInverted.gameObject.name = "delayedInverted";
        if (jumpFrame) jumpFrame.OnValueChanged2 += PropertyChangeResponse; jumpFrame.gameObject.name = "jumpFrame";
        if (timeAgonizing) timeAgonizing.OnValueChanged2 += PropertyChangeResponse; timeAgonizing.gameObject.name = "timeAgonizing";
        if (actionsMaxRange) actionsMaxRange.OnValueChanged2 += PropertyChangeResponse; actionsMaxRange.gameObject.name = "actionsMaxRange";
        if (patrolRange) patrolRange.OnValueChanged2 += PropertyChangeResponse; patrolRange.gameObject.name = "patrolRange";
        if (followRange) followRange.OnValueChanged2 += PropertyChangeResponse; followRange.gameObject.name = "followRange";
        if (combatRange) combatRange.OnValueChanged2 += PropertyChangeResponse; combatRange.gameObject.name = "combatRange";
        if (meleeRange) meleeRange.OnValueChanged2 += PropertyChangeResponse; meleeRange.gameObject.name = "meleeRange";
        if (idealRange) idealRange.OnValueChanged2 += PropertyChangeResponse; idealRange.gameObject.name = "idealRange";
        if (speed) speed.OnValueChanged2 += PropertyChangeResponse; speed.gameObject.name = "speed";
        if (jumpFrequency) jumpFrequency.OnValueChanged2 += PropertyChangeResponse; jumpFrequency.gameObject.name = "jumpFrequency";
        if (patrolRangeRatio) patrolRangeRatio.OnValueChanged2 += PropertyChangeResponse; patrolRangeRatio.gameObject.name = "patrolRangeRatio";
        if (actionDelay) actionDelay.OnValueChanged2 += PropertyChangeResponse; actionDelay.gameObject.name = "actionDelay";
    }

    private void PropertyChangeResponse(string arg, string arg1)
    {
        switch (arg)
        {
            case "retainDeath": targetCreature.behaviorProperties.retainDeath = bool.Parse(arg1); break;
            case "craven": targetCreature.behaviorProperties.craven = bool.Parse(arg1); break;
            case "startAggressive": targetCreature.behaviorProperties.startAggressive = bool.Parse(arg1); break;
            case "naturallyAggressive": targetCreature.behaviorProperties.naturallyAggressive = bool.Parse(arg1); break;
            case "racialAggressive": targetCreature.behaviorProperties.racialAggressive = bool.Parse(arg1); break;
            case "naturallyProtector": targetCreature.behaviorProperties.naturallyProtector = bool.Parse(arg1); break;
            case "groundAdapt": targetCreature.behaviorProperties.groundAdapt = bool.Parse(arg1); break;
            case "invertableCharacter": targetCreature.behaviorProperties.invertableCharacter = bool.Parse(arg1); break;
            case "delayedInverted": targetCreature.behaviorProperties.delayedInverted = bool.Parse(arg1); break;
            case "jumpFrame": targetCreature.behaviorProperties.jumpFrame = int.Parse(arg1); break;
            case "timeAgonizing": targetCreature.behaviorProperties.timeAgonizing = float.Parse(arg1, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture); break;
            case "actionsMaxRange": targetCreature.behaviorProperties.actionsMaxRange = float.Parse(arg1, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture); break;
            case "patrolRange": targetCreature.behaviorProperties.patrolRange = float.Parse(arg1, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture); break;
            case "followRange": targetCreature.behaviorProperties.followRange = float.Parse(arg1, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture); break;
            case "combatRange": targetCreature.behaviorProperties.combatRange = float.Parse(arg1, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture); break;
            case "meleeRange": targetCreature.behaviorProperties.meleeRange = float.Parse(arg1, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture); break;
            case "idealRange": targetCreature.behaviorProperties.idealRange = float.Parse(arg1, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture); break;
            case "speed": targetCreature.behaviorProperties.speed = float.Parse(arg1, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture); break;
            case "jumpFrequency": targetCreature.behaviorProperties.jumpFrequency = float.Parse(arg1, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture); break;
            case "patrolRangeRatio": targetCreature.behaviorProperties.patrolRangeRatio = float.Parse(arg1, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture); break;
            case "actionDelay": targetCreature.behaviorProperties.actionDelay = float.Parse(arg1, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture); break;
            default:
                break;
        }
    }

    public void Setup(ref CreatureEntry creatureEntry)
    {
        targetCreature = creatureEntry;

        retainDeath.Setup("retainDeath", creatureEntry.currentData.behaviorProperties.retainDeath.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, creatureEntry.defaultData.behaviorProperties.retainDeath.ToString(), creatureEntry.sourceData.behaviorProperties.retainDeath.ToString());
        craven.Setup("craven", creatureEntry.currentData.behaviorProperties.craven.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, creatureEntry.defaultData.behaviorProperties.craven.ToString(), creatureEntry.sourceData.behaviorProperties.craven.ToString());
        startAggressive.Setup("startAggressive", creatureEntry.currentData.behaviorProperties.startAggressive.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, creatureEntry.defaultData.behaviorProperties.startAggressive.ToString(), creatureEntry.sourceData.behaviorProperties.startAggressive.ToString());
        naturallyAggressive.Setup("naturallyAggressive", creatureEntry.currentData.behaviorProperties.naturallyAggressive.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, creatureEntry.defaultData.behaviorProperties.naturallyAggressive.ToString(), creatureEntry.sourceData.behaviorProperties.naturallyAggressive.ToString());
        racialAggressive.Setup("racialAggressive", creatureEntry.currentData.behaviorProperties.racialAggressive.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, creatureEntry.defaultData.behaviorProperties.racialAggressive.ToString(), creatureEntry.sourceData.behaviorProperties.racialAggressive.ToString());
        naturallyProtector.Setup("naturallyProtector", creatureEntry.currentData.behaviorProperties.naturallyProtector.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, creatureEntry.defaultData.behaviorProperties.naturallyProtector.ToString(), creatureEntry.sourceData.behaviorProperties.naturallyProtector.ToString());
        groundAdapt.Setup("groundAdapt", creatureEntry.currentData.behaviorProperties.groundAdapt.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, creatureEntry.defaultData.behaviorProperties.groundAdapt.ToString(), creatureEntry.sourceData.behaviorProperties.groundAdapt.ToString());
        invertableCharacter.Setup("invertableCharacter", creatureEntry.currentData.behaviorProperties.invertableCharacter.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, creatureEntry.defaultData.behaviorProperties.invertableCharacter.ToString(), creatureEntry.sourceData.behaviorProperties.invertableCharacter.ToString());
        delayedInverted.Setup("delayedInverted", creatureEntry.currentData.behaviorProperties.delayedInverted.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, creatureEntry.defaultData.behaviorProperties.delayedInverted.ToString(), creatureEntry.sourceData.behaviorProperties.delayedInverted.ToString());
        jumpFrame.Setup("jumpFrame", creatureEntry.currentData.behaviorProperties.jumpFrame.ToString(), UtilDefinitions.PropertyDisplayTypes.Int, creatureEntry.defaultData.behaviorProperties.jumpFrame.ToString(), creatureEntry.sourceData.behaviorProperties.jumpFrame.ToString());
        timeAgonizing.Setup("timeAgonizing", creatureEntry.currentData.behaviorProperties.timeAgonizing.ToString(CultureInfo.InvariantCulture), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.behaviorProperties.timeAgonizing.ToString(CultureInfo.InvariantCulture), creatureEntry.sourceData.behaviorProperties.timeAgonizing.ToString(CultureInfo.InvariantCulture));
        actionsMaxRange.Setup("actionsMaxRange", creatureEntry.currentData.behaviorProperties.actionsMaxRange.ToString(CultureInfo.InvariantCulture), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.behaviorProperties.actionsMaxRange.ToString(CultureInfo.InvariantCulture), creatureEntry.sourceData.behaviorProperties.actionsMaxRange.ToString(CultureInfo.InvariantCulture));
        patrolRange.Setup("patrolRange", creatureEntry.currentData.behaviorProperties.patrolRange.ToString(CultureInfo.InvariantCulture), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.behaviorProperties.patrolRange.ToString(CultureInfo.InvariantCulture), creatureEntry.sourceData.behaviorProperties.patrolRange.ToString(CultureInfo.InvariantCulture));
        followRange.Setup("followRange", creatureEntry.currentData.behaviorProperties.followRange.ToString(CultureInfo.InvariantCulture), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.behaviorProperties.followRange.ToString(CultureInfo.InvariantCulture), creatureEntry.sourceData.behaviorProperties.followRange.ToString(CultureInfo.InvariantCulture));
        combatRange.Setup("combatRange", creatureEntry.currentData.behaviorProperties.combatRange.ToString(CultureInfo.InvariantCulture), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.behaviorProperties.combatRange.ToString(CultureInfo.InvariantCulture), creatureEntry.sourceData.behaviorProperties.combatRange.ToString(CultureInfo.InvariantCulture));
        meleeRange.Setup("meleeRange", creatureEntry.currentData.behaviorProperties.meleeRange.ToString(CultureInfo.InvariantCulture), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.behaviorProperties.meleeRange.ToString(CultureInfo.InvariantCulture), creatureEntry.sourceData.behaviorProperties.meleeRange.ToString(CultureInfo.InvariantCulture));
        idealRange.Setup("idealRange", creatureEntry.currentData.behaviorProperties.idealRange.ToString(CultureInfo.InvariantCulture), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.behaviorProperties.idealRange.ToString(CultureInfo.InvariantCulture), creatureEntry.sourceData.behaviorProperties.idealRange.ToString(CultureInfo.InvariantCulture));
        speed.Setup("speed", creatureEntry.currentData.behaviorProperties.speed.ToString(CultureInfo.InvariantCulture), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.behaviorProperties.speed.ToString(CultureInfo.InvariantCulture), creatureEntry.sourceData.behaviorProperties.speed.ToString(CultureInfo.InvariantCulture));
        jumpFrequency.Setup("jumpFrequency", creatureEntry.currentData.behaviorProperties.jumpFrequency.ToString(CultureInfo.InvariantCulture), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.behaviorProperties.jumpFrequency.ToString(CultureInfo.InvariantCulture), creatureEntry.sourceData.behaviorProperties.jumpFrequency.ToString(CultureInfo.InvariantCulture));
        patrolRangeRatio.Setup("patrolRangeRatio", creatureEntry.currentData.behaviorProperties.patrolRangeRatio.ToString(CultureInfo.InvariantCulture), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.behaviorProperties.patrolRangeRatio.ToString(CultureInfo.InvariantCulture), creatureEntry.sourceData.behaviorProperties.patrolRangeRatio.ToString(CultureInfo.InvariantCulture));
        actionDelay.Setup("actionDelay", creatureEntry.currentData.behaviorProperties.actionDelay.ToString(CultureInfo.InvariantCulture), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.behaviorProperties.actionDelay.ToString(CultureInfo.InvariantCulture), creatureEntry.sourceData.behaviorProperties.actionDelay.ToString(CultureInfo.InvariantCulture));
    }

}
