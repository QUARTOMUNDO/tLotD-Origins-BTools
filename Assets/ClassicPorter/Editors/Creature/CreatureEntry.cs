using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreatureEntry
{
    public CreatureData defaultData = null;
    public CreatureData sourceData = null;
    public CreatureData currentData = null;

    public CreatureEntry(CreatureData defaultData = null, CreatureData sourceData = null, CreatureData currentData = null)
    {
        this.defaultData = defaultData;
        this.sourceData = sourceData;
        this.currentData = currentData;
    }

    public static implicit operator bool(CreatureEntry a) { return a != null; }
    public static implicit operator CreatureData(CreatureEntry a) { return a.currentData; }

    public string VarName
    {
        get
        {
            if (defaultData) return defaultData.varName;
            if (sourceData) return sourceData.varName;
            if (currentData) return currentData.varName;
            return "VarNameNotFound";
        }
    }

    public static bool TryGet(string creatureVarName, out CreatureEntry entry)
    {
        if (!PortController.single.creatures.TryGetValue(creatureVarName, out CreatureEntry foundEntry))
        {
            entry = null;
            return false;
        }
        else
        {
            entry = foundEntry;
            return true;
        }
    }

    public static CreatureEntry Get(string creatureVarName)
    {
        TryGet(creatureVarName, out CreatureEntry entry);
        return entry;
    }

}
