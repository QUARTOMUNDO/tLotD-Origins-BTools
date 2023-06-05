using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturePhysicsDisplay : MonoBehaviour
{
    CreatureData targetCreature;

    public PropertyDisplay height;
    public PropertyDisplay width;
    public PropertyDisplay offsetX;
    public PropertyDisplay offsetY;
    public PropertyDisplay density;
    public PropertyDisplay friction;
    public PropertyDisplay fixedRotation;

    /* Formatted var copy
     * 
     * height       
     * width        
     * offsetX      
     * offsetY      
     * density      
     * friction     
     * fixedRotation
     * 
     * 
     * "height"
     * "width"
     * "offsetX"
     * "offsetY"
     * "density"
     * "friction"
     * "fixedRotation"
     * 
     */

    private void OnDrawGizmosSelected()
    {
        if (height) height.gameObject.name = "height"; height.previewPropertyName = "height";
        if (width) width.gameObject.name = "width"; width.previewPropertyName = "width";
        if (offsetX) offsetX.gameObject.name = "offsetX"; offsetX.previewPropertyName = "offsetX";
        if (offsetY) offsetY.gameObject.name = "offsetY"; offsetY.previewPropertyName = "offsetY";
        if (density) density.gameObject.name = "density"; density.previewPropertyName = "density";
        if (friction) friction.gameObject.name = "friction"; friction.previewPropertyName = "friction";
        if (fixedRotation) fixedRotation.gameObject.name = "fixedRotation"; fixedRotation.previewPropertyName = "fixedRotation";

    }


    private void Awake()
    {
        if (height) height.OnValueChanged2 += PropertyChangeResponse; height.gameObject.name = "height";
        if (width) width.OnValueChanged2 += PropertyChangeResponse; width.gameObject.name = "width";
        if (offsetX) offsetX.OnValueChanged2 += PropertyChangeResponse; offsetX.gameObject.name = "offsetX";
        if (offsetY) offsetY.OnValueChanged2 += PropertyChangeResponse; offsetY.gameObject.name = "offsetY";
        if (density) density.OnValueChanged2 += PropertyChangeResponse; density.gameObject.name = "density";
        if (friction) friction.OnValueChanged2 += PropertyChangeResponse; friction.gameObject.name = "friction";
        if (fixedRotation) fixedRotation.OnValueChanged2 += PropertyChangeResponse; fixedRotation.gameObject.name = "fixedRotation";

    }


    private void PropertyChangeResponse(string arg, string arg1)
    {
        switch (arg)
        {
            case "height": targetCreature.creaturePhysics.height = float.Parse(arg1, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture); break;
            case "width": targetCreature.creaturePhysics.width = float.Parse(arg1, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture); break;
            case "offsetX": targetCreature.creaturePhysics.offsetX = float.Parse(arg1, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture); break;
            case "offsetY": targetCreature.creaturePhysics.offsetY = float.Parse(arg1, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture); break;
            case "density": targetCreature.creaturePhysics.density = float.Parse(arg1, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture); break;
            case "friction": targetCreature.creaturePhysics.friction = float.Parse(arg1, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture); break;
            case "fixedRotation": targetCreature.creaturePhysics.fixedRotation = bool.Parse(arg1); break;

            default:
                break;
        }
    }


    public void Setup(ref CreatureEntry creatureEntry)
    {
        targetCreature = creatureEntry;

        height       .Setup("height"        , creatureEntry.currentData.creaturePhysics.height       .ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.creaturePhysics.height       .ToString(), creatureEntry.sourceData.creaturePhysics.height       .ToString());
        width        .Setup("width"         , creatureEntry.currentData.creaturePhysics.width        .ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.creaturePhysics.width        .ToString(), creatureEntry.sourceData.creaturePhysics.width        .ToString());
        offsetX      .Setup("offsetX"       , creatureEntry.currentData.creaturePhysics.offsetX      .ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.creaturePhysics.offsetX      .ToString(), creatureEntry.sourceData.creaturePhysics.offsetX      .ToString());
        offsetY      .Setup("offsetY"       , creatureEntry.currentData.creaturePhysics.offsetY      .ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.creaturePhysics.offsetY      .ToString(), creatureEntry.sourceData.creaturePhysics.offsetY      .ToString());
        density      .Setup("density"       , creatureEntry.currentData.creaturePhysics.density      .ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.creaturePhysics.density      .ToString(), creatureEntry.sourceData.creaturePhysics.density      .ToString());
        friction     .Setup("friction"      , creatureEntry.currentData.creaturePhysics.friction     .ToString(), UtilDefinitions.PropertyDisplayTypes.Float, creatureEntry.defaultData.creaturePhysics.friction     .ToString(), creatureEntry.sourceData.creaturePhysics.friction     .ToString());
        fixedRotation.Setup("fixedRotation" , creatureEntry.currentData.creaturePhysics.fixedRotation.ToString(), UtilDefinitions.PropertyDisplayTypes.Bool , creatureEntry.defaultData.creaturePhysics.fixedRotation.ToString(), creatureEntry.sourceData.creaturePhysics.fixedRotation.ToString());
        
    }


}
