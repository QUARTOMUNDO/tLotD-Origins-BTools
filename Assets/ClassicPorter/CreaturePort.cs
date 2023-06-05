using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using UtilDefinitions;

[System.Serializable]
public class CreatureData
{
    public enum CreaturePropertyType
    {
        NatureResistance,
        CharacterAttributes,
        BehaviorProperties,
        CreaturePhysics,
        Loot,
        CustomProperties
    }

    public string name = "Creature in game name";
    public string varName = "Creature file name";

    public NatureResistance natureResistance;
    public CharacterAttributes characterAttributes;
    public BehaviorProperties behaviorProperties;
    public CreaturePhysics creaturePhysics;
    public Loot loot;

    public List<NamePair> customProperties = new List<NamePair>();

    public CreatureData()
    {
        natureResistance = new NatureResistance();
        characterAttributes = new CharacterAttributes();
        behaviorProperties = new BehaviorProperties();
        creaturePhysics = new CreaturePhysics();
        loot = new Loot();
    }

    #region Xml to Creature

    public CreatureData(XmlNode creatureNode)
    {
        throw new NotImplementedException("XmlDocument implementation is not done. Please use XDocument and XElement functions instead");

        //for (int i = 0; i < creatureNode.ChildNodes.Count; i++)
        //{
        //    XmlNode propertyNode = creatureNode.ChildNodes[i];
        //    //str += "\n  Property node: [ " + propertyNode.Name + " ]";
        //    foreach (CreaturePropertyType propertyType in Enum.GetValues(typeof(CreaturePropertyType)))
        //    {

        //        Debug.Log(creatureNode.SelectSingleNode(propertyType.ToString()).InnerXml);
        //    }
        //    if (Enum.TryParse(propertyNode.Name, out CreaturePropertyType creaturePropertyType))
        //    {
        //        switch (creaturePropertyType)
        //        {
        //            case CreaturePropertyType.NatureResistance:
        //                break;
        //            case CreaturePropertyType.CharacterAttributes:
        //                break;
        //            case CreaturePropertyType.BehaviorProperties:
        //                break;
        //            case CreaturePropertyType.CreaturePhysics:
        //                break;
        //            case CreaturePropertyType.Loot:
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    for (int j = 0; j < propertyNode.Attributes.Count; j++)
        //    {
        //        //str += "\n  " + creatureNode.ChildNodes[i].Attributes[j].Name + " = " + creatureNode.ChildNodes[i].Attributes[j].Value;
        //    }
        //}
    }

    public CreatureData(XElement creatureElement)
    {
        if (creatureElement.Attribute("name") != null) name = creatureElement.Attribute("name").Value;
        if (creatureElement.Attribute("varName") != null) varName = creatureElement.Attribute("varName").Value;

        foreach (CreaturePropertyType propertyType in Enum.GetValues(typeof(CreaturePropertyType)))
        {
            XElement property = creatureElement.Element(propertyType.ToString());

            switch (propertyType)
            {
                case CreaturePropertyType.NatureResistance:

                    natureResistance = new NatureResistance(property);

                    break;
                case CreaturePropertyType.CharacterAttributes:

                    characterAttributes = new CharacterAttributes(property);

                    break;
                case CreaturePropertyType.BehaviorProperties:

                    behaviorProperties = new BehaviorProperties(property);

                    break;
                case CreaturePropertyType.CreaturePhysics:

                    creaturePhysics = new CreaturePhysics(property);

                    break;
                case CreaturePropertyType.Loot:

                    loot = new Loot(property);

                    break;
                case CreaturePropertyType.CustomProperties:

                    customProperties.Clear();

                    if (property != null)
                    {
                        foreach (XElement customProp in property.Elements())
                        {
                            if (customProp.Attribute("var") != null && customProp.Attribute("value") != null)
                            {
                                customProperties.Add(new NamePair(customProp.Attribute("var").Value, customProp.Attribute("value").Value));
                            }
                        }
                    }

                    break;
                default:
                    Debug.LogError("Attempt to process the node for [ " + creatureElement.Name + " ] but it seems to have a non implemented property. \n" +
                        "Add this property to the switch cases in order to import it");
                    break;
            }
        }
    }

    public static CreatureData CreatureFromXml(XmlNode node)
    {
        CreatureData creature = new CreatureData(node);

        return creature;
    }

    public static CreatureData CreatureFromXml(XElement element)
    {
        CreatureData creature = new CreatureData(element);

        return creature;
    }

    #endregion

    public static implicit operator bool(CreatureData a) { return a != null; }

    #region Creature to Xml

    public static XElement XmlFromCreature(CreatureData creatureData)
    {
        XElement element = new XElement("Characters");
        XAttribute name = new XAttribute("name", creatureData.name);
        XAttribute varName = new XAttribute("varName", creatureData.varName);
        element.Add(varName);
        element.Add(name);

        element.Add(creatureData.natureResistance.GetXElement());
        element.Add(creatureData.characterAttributes.GetXElement());
        element.Add(creatureData.behaviorProperties.GetXElement());
        element.Add(creatureData.creaturePhysics.GetXElement());
        element.Add(creatureData.loot.GetXElement());

        return element;
    }

    #endregion


}

[System.Serializable]
public class NatureResistance
{
    public static List<NameFloatPair> DefaultResistances
    {
        get
        {
            return new List<NameFloatPair>() {
                new NameFloatPair("Physical",0f),
                new NameFloatPair("Air",0f),
                new NameFloatPair("Fire",0f),
                new NameFloatPair("Corruption",0f),
                new NameFloatPair("Darkness",0f),
                new NameFloatPair("Bio",0f),
                new NameFloatPair("Earth",0f),
                new NameFloatPair("Ice",0f),
                new NameFloatPair("Light",0f),
                new NameFloatPair("Psionica",0f),
                new NameFloatPair("Water",0f)
            };
        }
    }

    public List<NameFloatPair> resistances = new List<NameFloatPair>() {
        new NameFloatPair("Physical",0f),
        new NameFloatPair("Air",0f),
        new NameFloatPair("Fire",0f),
        new NameFloatPair("Corruption",0f),
        new NameFloatPair("Darkness",0f),
        new NameFloatPair("Bio",0f),
        new NameFloatPair("Earth",0f),
        new NameFloatPair("Ice",0f),
        new NameFloatPair("Light",0f),
        new NameFloatPair("Psionica",0f),
        new NameFloatPair("Water",0f)
    };

    public NatureResistance(XElement property)
    {
        if (property != null && property.HasAttributes)
        {
            resistances.Clear();
            foreach (XAttribute resistance in property.Attributes())
            {
                resistances.Add(new NameFloatPair(resistance.Name.ToString(), float.Parse(resistance.Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture)));
            }
        }
        else
        {
            resistances = DefaultResistances;
        }
    }

    public NatureResistance()
    {
    }

    public XElement GetXElement()
    {
        XElement element = new XElement("NatureResistance");
        List<XAttribute> attributes = new List<XAttribute>();
        foreach (NameFloatPair resistance in resistances)
        {
            attributes.Add(new XAttribute(resistance.name, resistance.number));
        }

        foreach (XAttribute attribute in attributes)
        {
            element.Add(attribute);
        }

        return element;
    }

    public NameFloatPair GetResistance(string resistanceName)
    {
        NameFloatPair result = new NameFloatPair(resistanceName, 0f);
        for (int i = 0; i < resistances.Count; i++)
        {
            if (resistances[i].name == resistanceName) return resistances[i];
        }
        Debug.LogWarning("WARNING: Attempt to get resistance [ " + resistanceName + " ] from a creature failed. Returning default resistance");
        return result;
    }

}

[System.Serializable]
public class CharacterAttributes
{
    public string normalSplash = "CrautaryusLight";
    public string weakSplash = "CrautaryusLight";
    public string defenceSplash = "CrautaryusLight";
    public string abnormalSplash = "CrautaryusLight";
    public bool sufferCriticable = false;
    public bool damagerCriticable = false;
    public bool regenerable = false;
    public bool degenerable = false;
    public bool inflictsBodyDamage = true;
    public bool inflictsAttackDamage = false;
    public int baseLevel = 0;
    public int pullTypeID = 0;
    public float mysticalFactor = 100;
    public float attackPower = 0f;
    public float bodyPower = 2.8f;
    public float strengthFactor = 100f;
    public float resistanceFactor = 100f;
    public float efficiencyFactor = 100f;
    public float peripheralFactor = 25f;
    public float bodyDamageRepeatTime = 0.5f;
    public float bodyAttackWeight = 0.8f;
    public float bodySufferWeight = 0.4f;
    public float attackDamageRepeatTime = 0.5f;
    public float attackWeight = 0f;
    public float pullDirection = -55;

    public CharacterAttributes()
    {
    }

    public CharacterAttributes(XElement property)
    {
        if (property != null && property.HasAttributes)
        {
            if (property.Attribute("baseLevel") != null) int.TryParse(property.Attribute("baseLevel").Value, out baseLevel);
            if (property.Attribute("attackPower") != null) float.TryParse(property.Attribute("attackPower").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out attackPower);
            if (property.Attribute("bodyPower") != null) float.TryParse(property.Attribute("bodyPower").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out bodyPower);
            if (property.Attribute("inflictsBodyDamage") != null) bool.TryParse(property.Attribute("inflictsBodyDamage").Value, out inflictsBodyDamage);
            if (property.Attribute("inflictsAttackDamage") != null) bool.TryParse(property.Attribute("inflictsAttackDamage").Value, out inflictsAttackDamage);
            //String values \/
            if (property.Attribute("inflictsBodyDamage") != null) normalSplash = property.Attribute("normalSplash").Value;
            if (property.Attribute("inflictsBodyDamage") != null) weakSplash = property.Attribute("weakSplash").Value;
            if (property.Attribute("inflictsBodyDamage") != null) defenceSplash = property.Attribute("defenceSplash").Value;
            if (property.Attribute("inflictsBodyDamage") != null) abnormalSplash = property.Attribute("abnormalSplash").Value;
            //String values /\
            if (property.Attribute("strengthFactor") != null) float.TryParse(property.Attribute("strengthFactor").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out strengthFactor);
            if (property.Attribute("resistanceFactor") != null) float.TryParse(property.Attribute("resistanceFactor").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out resistanceFactor);
            if (property.Attribute("efficiencyFactor") != null) float.TryParse(property.Attribute("efficiencyFactor").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out efficiencyFactor);
            if (property.Attribute("peripheralFactor") != null) float.TryParse(property.Attribute("peripheralFactor").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out peripheralFactor);
            if (property.Attribute("mysticalFactor") != null) float.TryParse(property.Attribute("mysticalFactor").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out mysticalFactor);
            if (property.Attribute("sufferCriticable") != null) bool.TryParse(property.Attribute("sufferCriticable").Value, out sufferCriticable);
            if (property.Attribute("damagerCriticable") != null) bool.TryParse(property.Attribute("damagerCriticable").Value, out damagerCriticable);
            if (property.Attribute("bodyDamageRepeatTime") != null) float.TryParse(property.Attribute("bodyDamageRepeatTime").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out bodyDamageRepeatTime);
            if (property.Attribute("bodyAttackWeight") != null) float.TryParse(property.Attribute("bodyAttackWeight").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out bodyAttackWeight);
            if (property.Attribute("bodySufferWeight") != null) float.TryParse(property.Attribute("bodySufferWeight").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out bodySufferWeight);
            if (property.Attribute("attackDamageRepeatTime") != null) float.TryParse(property.Attribute("attackDamageRepeatTime").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out attackDamageRepeatTime);
            if (property.Attribute("attackWeight") != null) float.TryParse(property.Attribute("attackWeight").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out attackWeight);
            if (property.Attribute("pullDirection") != null) float.TryParse(property.Attribute("pullDirection").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out pullDirection);
            if (property.Attribute("pullTypeID") != null) int.TryParse(property.Attribute("pullTypeID").Value, out pullTypeID);
            if (property.Attribute("regenerable") != null) bool.TryParse(property.Attribute("regenerable").Value, out regenerable);
            if (property.Attribute("degenerable") != null) bool.TryParse(property.Attribute("degenerable").Value, out degenerable);
        }
    }

    public CharacterAttributes(int baseLevel, float attackPower, float bodyPower, bool inflictsBodyDamage, bool inflictsAttackDamage, string normalSplash, string weakSplash, string defenceSplash, string abnormalSplash, float strengthFactor, float resistanceFactor, float efficiencyFactor, float peripheralFactor, float mysticalFactor, bool sufferCriticable, bool damagerCriticable, float bodyDamageRepeatTime, float bodyAttackWeight, float bodySufferWeight, float attackDamageRepeatTime, float attackWeight, float pullDirection, int pullTypeID, bool regenerable, bool degenerable)
    {
        this.baseLevel = baseLevel;
        this.attackPower = attackPower;
        this.bodyPower = bodyPower;
        this.inflictsBodyDamage = inflictsBodyDamage;
        this.inflictsAttackDamage = inflictsAttackDamage;
        this.normalSplash = normalSplash;
        this.weakSplash = weakSplash;
        this.defenceSplash = defenceSplash;
        this.abnormalSplash = abnormalSplash;
        this.strengthFactor = strengthFactor;
        this.resistanceFactor = resistanceFactor;
        this.efficiencyFactor = efficiencyFactor;
        this.peripheralFactor = peripheralFactor;
        this.mysticalFactor = mysticalFactor;
        this.sufferCriticable = sufferCriticable;
        this.damagerCriticable = damagerCriticable;
        this.bodyDamageRepeatTime = bodyDamageRepeatTime;
        this.bodyAttackWeight = bodyAttackWeight;
        this.bodySufferWeight = bodySufferWeight;
        this.attackDamageRepeatTime = attackDamageRepeatTime;
        this.attackWeight = attackWeight;
        this.pullDirection = pullDirection;
        this.pullTypeID = pullTypeID;
        this.regenerable = regenerable;
        this.degenerable = degenerable;
    }

    public XElement GetXElement()
    {
        XElement element = new XElement("CharacterAttributes");
        List<XAttribute> attributes = new List<XAttribute>();

        attributes.Add(new XAttribute("normalSplash", normalSplash));
        attributes.Add(new XAttribute("weakSplash", weakSplash));
        attributes.Add(new XAttribute("defenceSplash", defenceSplash));
        attributes.Add(new XAttribute("abnormalSplash", abnormalSplash));
        attributes.Add(new XAttribute("sufferCriticable", sufferCriticable));
        attributes.Add(new XAttribute("damagerCriticable", damagerCriticable));
        attributes.Add(new XAttribute("regenerable", regenerable));
        attributes.Add(new XAttribute("degenerable", degenerable));
        attributes.Add(new XAttribute("inflictsBodyDamage", inflictsBodyDamage));
        attributes.Add(new XAttribute("inflictsAttackDamage", inflictsAttackDamage));
        attributes.Add(new XAttribute("baseLevel", baseLevel));
        attributes.Add(new XAttribute("pullTypeID", pullTypeID));
        attributes.Add(new XAttribute("mysticalFactor", mysticalFactor));
        attributes.Add(new XAttribute("attackPower", attackPower));
        attributes.Add(new XAttribute("bodyPower", bodyPower));
        attributes.Add(new XAttribute("strengthFactor", strengthFactor));
        attributes.Add(new XAttribute("resistanceFactor", resistanceFactor));
        attributes.Add(new XAttribute("efficiencyFactor", efficiencyFactor));
        attributes.Add(new XAttribute("peripheralFactor", peripheralFactor));
        attributes.Add(new XAttribute("bodyDamageRepeatTime", bodyDamageRepeatTime));
        attributes.Add(new XAttribute("bodyAttackWeight", bodyAttackWeight));
        attributes.Add(new XAttribute("bodySufferWeight", bodySufferWeight));
        attributes.Add(new XAttribute("attackDamageRepeatTime", attackDamageRepeatTime));
        attributes.Add(new XAttribute("attackWeight", attackWeight));
        attributes.Add(new XAttribute("pullDirection", pullDirection));

        foreach (XAttribute attribute in attributes)
        {
            element.Add(attribute);
        }

        return element;
    }

}

[System.Serializable]
public class BehaviorProperties
{
    public bool retainDeath = false;
    public bool craven = false;
    public bool startAggressive = true;
    public bool naturallyAggressive = true;
    public bool racialAggressive = true;
    public bool naturallyProtector = false;
    public bool groundAdapt = false;
    public bool invertableCharacter = true;
    public bool delayedInverted = false;
    public int jumpFrame = 0;
    public float timeAgonizing = 0f;
    public float actionsMaxRange = 2000f;
    public float patrolRange = 500f;
    public float followRange = 900f;
    public float combatRange = 850f;
    public float meleeRange = 100f;
    public float idealRange = 50f;
    public float speed = 4f;
    public float jumpFrequency = 0f;
    public float patrolRangeRatio = 2f;
    public float actionDelay = 60f;

    public BehaviorProperties()
    {
    }

    public BehaviorProperties(XElement property)
    {
        if (property != null && property.HasAttributes)
        {
            if (property.Attribute("timeAgonizing") != null) float.TryParse(property.Attribute("timeAgonizing").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out timeAgonizing);
            if (property.Attribute("retainDeath") != null) bool.TryParse(property.Attribute("retainDeath").Value, out retainDeath);
            if (property.Attribute("craven") != null) bool.TryParse(property.Attribute("craven").Value, out craven);
            if (property.Attribute("startAggressive") != null) bool.TryParse(property.Attribute("startAggressive").Value, out startAggressive);
            if (property.Attribute("naturallyAggressive") != null) bool.TryParse(property.Attribute("naturallyAggressive").Value, out naturallyAggressive);
            if (property.Attribute("racialAggressive") != null) bool.TryParse(property.Attribute("racialAggressive").Value, out racialAggressive);
            if (property.Attribute("naturallyProtector") != null) bool.TryParse(property.Attribute("naturallyProtector").Value, out naturallyProtector);
            if (property.Attribute("actionsMaxRange") != null) float.TryParse(property.Attribute("actionsMaxRange").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out actionsMaxRange);
            if (property.Attribute("patrolRange") != null) float.TryParse(property.Attribute("patrolRange").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out patrolRange);
            if (property.Attribute("followRange") != null) float.TryParse(property.Attribute("followRange").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out followRange);
            if (property.Attribute("combatRange") != null) float.TryParse(property.Attribute("combatRange").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out combatRange);
            if (property.Attribute("meleeRange") != null) float.TryParse(property.Attribute("meleeRange").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out meleeRange);
            if (property.Attribute("idealRange") != null) float.TryParse(property.Attribute("idealRange").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out idealRange);
            if (property.Attribute("speed") != null) float.TryParse(property.Attribute("speed").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out speed);
            if (property.Attribute("groundAdapt") != null) bool.TryParse(property.Attribute("groundAdapt").Value, out groundAdapt);
            if (property.Attribute("invertableCharacter") != null) bool.TryParse(property.Attribute("invertableCharacter").Value, out invertableCharacter);
            if (property.Attribute("delayedInverted") != null) bool.TryParse(property.Attribute("delayedInverted").Value, out delayedInverted);
            if (property.Attribute("jumpFrequency") != null) float.TryParse(property.Attribute("jumpFrequency").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out jumpFrequency);
            if (property.Attribute("jumpFrame") != null) int.TryParse(property.Attribute("jumpFrame").Value, out jumpFrame);
            if (property.Attribute("patrolRangeRatio") != null) float.TryParse(property.Attribute("patrolRangeRatio").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out patrolRangeRatio);
            if (property.Attribute("actionDelay") != null) float.TryParse(property.Attribute("actionDelay").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out actionDelay);

        }
    }

    public XElement GetXElement()
    {
        XElement element = new XElement("BehaviorProperties");
        List<XAttribute> attributes = new List<XAttribute>();

        attributes.Add(new XAttribute("retainDeath", retainDeath));
        attributes.Add(new XAttribute("craven", craven));
        attributes.Add(new XAttribute("startAggressive", startAggressive));
        attributes.Add(new XAttribute("naturallyAggressive", naturallyAggressive));
        attributes.Add(new XAttribute("racialAggressive", racialAggressive));
        attributes.Add(new XAttribute("naturallyProtector", naturallyProtector));
        attributes.Add(new XAttribute("groundAdapt", groundAdapt));
        attributes.Add(new XAttribute("invertableCharacter", invertableCharacter));
        attributes.Add(new XAttribute("delayedInverted", delayedInverted));
        attributes.Add(new XAttribute("jumpFrame", jumpFrame));
        attributes.Add(new XAttribute("timeAgonizing", timeAgonizing));
        attributes.Add(new XAttribute("actionsMaxRange", actionsMaxRange));
        attributes.Add(new XAttribute("patrolRange", patrolRange));
        attributes.Add(new XAttribute("followRange", followRange));
        attributes.Add(new XAttribute("combatRange", combatRange));
        attributes.Add(new XAttribute("meleeRange", meleeRange));
        attributes.Add(new XAttribute("idealRange", idealRange));
        attributes.Add(new XAttribute("speed", speed));
        attributes.Add(new XAttribute("jumpFrequency", jumpFrequency));
        attributes.Add(new XAttribute("patrolRangeRatio", patrolRangeRatio));
        attributes.Add(new XAttribute("actionDelay", actionDelay));

        foreach (XAttribute attribute in attributes)
        {
            element.Add(attribute);
        }

        return element;
    }

}

[System.Serializable]
public class CreaturePhysics
{
    public float height = 60f;
    public float width = 60;
    public float offsetX = 0f;
    public float offsetY = -10f;
    public float density = 15f;
    public float friction = 0.5f;
    public bool fixedRotation = true;

    public CreaturePhysics()
    {
    }

    public CreaturePhysics(XElement property)
    {
        if (property != null && property.HasAttributes)
        {
            if (property.Attribute("height") != null) float.TryParse(property.Attribute("height").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out height);
            if (property.Attribute("width") != null) float.TryParse(property.Attribute("width").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out width);
            if (property.Attribute("offsetX") != null) float.TryParse(property.Attribute("offsetX").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out offsetX);
            if (property.Attribute("offsetY") != null) float.TryParse(property.Attribute("offsetY").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out offsetY);
            if (property.Attribute("density") != null) float.TryParse(property.Attribute("density").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out density);
            if (property.Attribute("friction") != null) float.TryParse(property.Attribute("friction").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out friction);
            if (property.Attribute("fixedRotation") != null) bool.TryParse(property.Attribute("fixedRotation").Value, out fixedRotation);
        }
    }

    public XElement GetXElement()
    {
        XElement element = new XElement("CreaturePhysics");
        List<XAttribute> attributes = new List<XAttribute>();

        attributes.Add(new XAttribute("height", height));
        attributes.Add(new XAttribute("width", width));
        attributes.Add(new XAttribute("offsetX", offsetX));
        attributes.Add(new XAttribute("offsetY", offsetY));
        attributes.Add(new XAttribute("density", density));
        attributes.Add(new XAttribute("friction", friction));
        attributes.Add(new XAttribute("fixedRotation", fixedRotation));

        foreach (XAttribute attribute in attributes)
        {
            element.Add(attribute);
        }

        return element;
    }

}

[System.Serializable]
public class Loot
{
    public float deepAmountRatio = 0.5f;
    public List<NameFloatPair> natureDrops = new List<NameFloatPair>();
    public List<NameFloatPair> objectDrops = new List<NameFloatPair>();


    public Loot()
    {
    }

    public Loot(XElement property)
    {
        if (property == null) return;

        if (property.HasAttributes)
        {
            if (property.Attribute("deepAmountRatio") != null) float.TryParse(property.Attribute("deepAmountRatio").Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out deepAmountRatio);
        }

        XElement natureDropsElement = property.Element("NatureDrops");
        natureDrops = new List<NameFloatPair>();
        if (natureDropsElement != null && natureDropsElement.HasAttributes)
        {
            foreach (var drop in natureDropsElement.Attributes())
            {
                natureDrops.Add(new NameFloatPair(drop.Name.ToString(), float.Parse(drop.Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture)));
            }
        }

        XElement objectDropsElement = property.Element("ObjectDrops");
        objectDrops = new List<NameFloatPair>();

        if (objectDropsElement != null && objectDropsElement.HasAttributes)
        {
            foreach (var drop2 in objectDropsElement.Attributes())
            {
                objectDrops.Add(new NameFloatPair(drop2.Name.ToString(), float.Parse(drop2.Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture)));
            }
        }

    }

    public XElement GetXElement()
    {
        XElement element = new XElement("Loot");
        XElement natureDropsElement = new XElement("NatureDrops");
        XElement objectDropsElement = new XElement("ObjectDrops");
        //List<XAttribute> attributes = new List<XAttribute>();

        element.Add(new XAttribute("deepAmountRatio", deepAmountRatio));
        element.Add(natureDropsElement);
        element.Add(objectDropsElement);



        foreach (NameFloatPair pair in natureDrops)
        {
            natureDropsElement.Add(new XAttribute(pair.name, pair.number));
        }

        foreach (NameFloatPair pair2 in objectDrops)
        {
            objectDropsElement.Add(new XAttribute(pair2.name, pair2.number));
        }


        //foreach (XAttribute attribute in attributes)
        //{
        //    element.Add(attribute);
        //}

        return element;
    }

}

/*
 * EXAMPLE
 * 
 <Characters varName="DISERTHEUS_BIG" name="Big Disertheus">
    <NatureResistance Physical="0" Air="100" Fire="100" Corruption="100" Darkness="0" Bio="100" Earth="100" Ice="100" Light="100" Psionica="100" Water="100"/>
    <CharacterAttributes baseLevel="2" attackPower="0" bodyPower="2.8" inflictsBodyDamage="true" inflictsAttackDamage="false" normalSplash="CrautaryusLight" weakSplash="CrautaryusLight" defenceSplash="CrautaryusLight" abnormalSplash="CrautaryusLight" strengthFactor="100" resistanceFactor="100" efficiencyFactor="100" peripheralFactor="25" mysticalFactor="100" sufferCriticable="false" damagerCriticable="false" bodyDamageRepeatTime="0.5" bodyAttackWeight=".8" bodySufferWeight="0.4" attackDamageRepeatTime="0.5" attackWeight="0" pullDirection="-55" pullTypeID="0" regenerable="false" degenerable="false"/>
    <BehaviorProperties timeAgonizing="0" retainDeath="false" craven="false" startAggressive="true" naturallyAggressive="true" racialAggressive="true" naturallyProtector="false" actionsMaxRange="2000" patrolRange="500" followRange="900" combatRange="850" meleeRange="100" idealRange="50" speed="4" groundAdapt="false" invertableCharacter="true" delayedInverted="false" jumpFrequency="0" jumpFrame="0" patrolRangeRatio="2" actionDelay="60"/>
    <CreaturePhysics height="60" width="60" offsetX="0" offsetY="-10" density="15" friction="0.5" fixedRotation="true"/>
    <Loot deepAmountRatio="0.5"/>
  </Characters>
     */
