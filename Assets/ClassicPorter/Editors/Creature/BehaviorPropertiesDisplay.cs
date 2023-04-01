using System;
using System.Collections;
using System.Collections.Generic;
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
            case "timeAgonizing": targetCreature.behaviorProperties.timeAgonizing = float.Parse(arg1); break;
            case "actionsMaxRange": targetCreature.behaviorProperties.actionsMaxRange = float.Parse(arg1); break;
            case "patrolRange": targetCreature.behaviorProperties.patrolRange = float.Parse(arg1); break;
            case "followRange": targetCreature.behaviorProperties.followRange = float.Parse(arg1); break;
            case "combatRange": targetCreature.behaviorProperties.combatRange = float.Parse(arg1); break;
            case "meleeRange": targetCreature.behaviorProperties.meleeRange = float.Parse(arg1); break;
            case "idealRange": targetCreature.behaviorProperties.idealRange = float.Parse(arg1); break;
            case "speed": targetCreature.behaviorProperties.speed = float.Parse(arg1); break;
            case "jumpFrequency": targetCreature.behaviorProperties.jumpFrequency = float.Parse(arg1); break;
            case "patrolRangeRatio": targetCreature.behaviorProperties.patrolRangeRatio = float.Parse(arg1); break;
            case "actionDelay": targetCreature.behaviorProperties.actionDelay = float.Parse(arg1); break;
            default:
                break;
        }
    }

    public void Setup(ref CreatureData creatureData)
    {
        targetCreature = creatureData;
        if (!PortController.single.defaultCreatures.TryGetValue(creatureData.varName, out CreatureData defaultData))
        {
            Debug.LogWarning("WARNING: Failed to load defaults for creature [ " + creatureData.varName + " ], using ordinary defaults");
            defaultData = new CreatureData();
        }

        retainDeath.Setup("retainDeath", creatureData.behaviorProperties.retainDeath.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, defaultData.behaviorProperties.retainDeath.ToString());
        craven.Setup("craven", creatureData.behaviorProperties.craven.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, defaultData.behaviorProperties.craven.ToString());
        startAggressive.Setup("startAggressive", creatureData.behaviorProperties.startAggressive.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, defaultData.behaviorProperties.startAggressive.ToString());
        naturallyAggressive.Setup("naturallyAggressive", creatureData.behaviorProperties.naturallyAggressive.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, defaultData.behaviorProperties.naturallyAggressive.ToString());
        racialAggressive.Setup("racialAggressive", creatureData.behaviorProperties.racialAggressive.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, defaultData.behaviorProperties.racialAggressive.ToString());
        naturallyProtector.Setup("naturallyProtector", creatureData.behaviorProperties.naturallyProtector.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, defaultData.behaviorProperties.naturallyProtector.ToString());
        groundAdapt.Setup("groundAdapt", creatureData.behaviorProperties.groundAdapt.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, defaultData.behaviorProperties.groundAdapt.ToString());
        invertableCharacter.Setup("invertableCharacter", creatureData.behaviorProperties.invertableCharacter.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, defaultData.behaviorProperties.invertableCharacter.ToString());
        delayedInverted.Setup("delayedInverted", creatureData.behaviorProperties.delayedInverted.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool, defaultData.behaviorProperties.delayedInverted.ToString());
        jumpFrame.Setup("jumpFrame", creatureData.behaviorProperties.jumpFrame.ToString(), UtilDefinitions.PropertyDisplayTypes.Int, defaultData.behaviorProperties.jumpFrame.ToString());
        timeAgonizing.Setup("timeAgonizing", creatureData.behaviorProperties.timeAgonizing.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, defaultData.behaviorProperties.timeAgonizing.ToString());
        actionsMaxRange.Setup("actionsMaxRange", creatureData.behaviorProperties.actionsMaxRange.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, defaultData.behaviorProperties.actionsMaxRange.ToString());
        patrolRange.Setup("patrolRange", creatureData.behaviorProperties.patrolRange.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, defaultData.behaviorProperties.patrolRange.ToString());
        followRange.Setup("followRange", creatureData.behaviorProperties.followRange.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, defaultData.behaviorProperties.followRange.ToString());
        combatRange.Setup("combatRange", creatureData.behaviorProperties.combatRange.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, defaultData.behaviorProperties.combatRange.ToString());
        meleeRange.Setup("meleeRange", creatureData.behaviorProperties.meleeRange.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, defaultData.behaviorProperties.meleeRange.ToString());
        idealRange.Setup("idealRange", creatureData.behaviorProperties.idealRange.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, defaultData.behaviorProperties.idealRange.ToString());
        speed.Setup("speed", creatureData.behaviorProperties.speed.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, defaultData.behaviorProperties.speed.ToString());
        jumpFrequency.Setup("jumpFrequency", creatureData.behaviorProperties.jumpFrequency.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, defaultData.behaviorProperties.jumpFrequency.ToString());
        patrolRangeRatio.Setup("patrolRangeRatio", creatureData.behaviorProperties.patrolRangeRatio.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, defaultData.behaviorProperties.patrolRangeRatio.ToString());
        actionDelay.Setup("actionDelay", creatureData.behaviorProperties.actionDelay.ToString(), UtilDefinitions.PropertyDisplayTypes.Float, defaultData.behaviorProperties.actionDelay.ToString());
    }

}
