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
        BehaviourProperties,
        CreaturePhysics,
        Loot,
        CustomProperties
    }

    public string name = "Creature in game name";
    public string varName = "Creature file name";

    public NatureResistance natureResistance;
    public CharacterAttributes characterAttributes;
    public BehaviourProperties behaviourProperties;
    public CreaturePhysics creaturePhysics;
    public Loot loot;

    public List<NamePair> customProperties = new List<NamePair>();

    public CreatureData()
    {
        natureResistance = new NatureResistance();
        characterAttributes = new CharacterAttributes();
        behaviourProperties = new BehaviourProperties();
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
        //            case CreaturePropertyType.BehaviourProperties:
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
                case CreaturePropertyType.BehaviourProperties:

                    behaviourProperties = new BehaviourProperties(property);

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

    #region Creature to Xml



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
                resistances.Add(new NameFloatPair(resistance.Name.ToString(), float.Parse(resistance.Value)));
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
}

[System.Serializable]
public class CharacterAttributes
{
    public int baseLevel = 0;
    public float attackPower = 0f;
    public float bodyPower = 2.8f;
    public bool inflictsBodyDamage = true;
    public bool inflictsAttackDamage = false;
    public string normalSplash = "CrautaryusLight";
    public string weakSplash = "CrautaryusLight";
    public string defenceSplash = "CrautaryusLight";
    public string abnormalSplash = "CrautaryusLight";
    public float strengthFactor = 100f;
    public float resistanceFactor = 100f;
    public float efficiencyFactor = 100f;
    public float peripheralFactor = 25f;
    public float mysticalFactor = 100;
    public bool sufferCriticable = false;
    public bool damagerCriticable = false;
    public float bodyDamageRepeatTime = 0.5f;
    public float bodyAttackWeight = 0.8f;
    public float bodySufferWeight = 0.4f;
    public float attackDamageRepeatTime = 0.5f;
    public float attackWeight = 0f;
    public float pullDirection = -55;
    public int pullTypeID = 0;
    public bool regenerable = false;
    public bool degenerable = false;

    public CharacterAttributes()
    {
    }

    public CharacterAttributes(XElement property)
    {
        if (property != null && property.HasAttributes)
        {
            if (property.Attribute("baseLevel") != null) int.TryParse(property.Attribute("baseLevel").Value, out baseLevel);
            if (property.Attribute("attackPower") != null) float.TryParse(property.Attribute("attackPower").Value, out attackPower);
            if (property.Attribute("bodyPower") != null) float.TryParse(property.Attribute("bodyPower").Value, out bodyPower);
            if (property.Attribute("inflictsBodyDamage") != null) bool.TryParse(property.Attribute("inflictsBodyDamage").Value, out inflictsBodyDamage);
            if (property.Attribute("inflictsAttackDamage") != null) bool.TryParse(property.Attribute("inflictsAttackDamage").Value, out inflictsAttackDamage);
            //String values \/
            if (property.Attribute("inflictsBodyDamage") != null) normalSplash = property.Attribute("normalSplash").Value;
            if (property.Attribute("inflictsBodyDamage") != null) weakSplash = property.Attribute("weakSplash").Value;
            if (property.Attribute("inflictsBodyDamage") != null) defenceSplash = property.Attribute("defenceSplash").Value;
            if (property.Attribute("inflictsBodyDamage") != null) abnormalSplash = property.Attribute("abnormalSplash").Value;
            //String values /\
            if (property.Attribute("strengthFactor") != null) float.TryParse(property.Attribute("strengthFactor").Value, out strengthFactor);
            if (property.Attribute("resistanceFactor") != null) float.TryParse(property.Attribute("resistanceFactor").Value, out resistanceFactor);
            if (property.Attribute("efficiencyFactor") != null) float.TryParse(property.Attribute("efficiencyFactor").Value, out efficiencyFactor);
            if (property.Attribute("peripheralFactor") != null) float.TryParse(property.Attribute("peripheralFactor").Value, out peripheralFactor);
            if (property.Attribute("mysticalFactor") != null) float.TryParse(property.Attribute("mysticalFactor").Value, out mysticalFactor);
            if (property.Attribute("sufferCriticable") != null) bool.TryParse(property.Attribute("sufferCriticable").Value, out sufferCriticable);
            if (property.Attribute("damagerCriticable") != null) bool.TryParse(property.Attribute("damagerCriticable").Value, out damagerCriticable);
            if (property.Attribute("bodyDamageRepeatTime") != null) float.TryParse(property.Attribute("bodyDamageRepeatTime").Value, out bodyDamageRepeatTime);
            if (property.Attribute("bodyAttackWeight") != null) float.TryParse(property.Attribute("bodyAttackWeight").Value, out bodyAttackWeight);
            if (property.Attribute("bodySufferWeight") != null) float.TryParse(property.Attribute("bodySufferWeight").Value, out bodySufferWeight);
            if (property.Attribute("attackDamageRepeatTime") != null) float.TryParse(property.Attribute("attackDamageRepeatTime").Value, out attackDamageRepeatTime);
            if (property.Attribute("attackWeight") != null) float.TryParse(property.Attribute("attackWeight").Value, out attackWeight);
            if (property.Attribute("pullDirection") != null) float.TryParse(property.Attribute("pullDirection").Value, out pullDirection);
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

}

[System.Serializable]
public class BehaviourProperties
{
    public float timeAgonizing = 0;
    public bool retainDeath = false;
    public bool craven = false;
    public bool startAggressive = true;
    public bool naturallyAggressive = true;
    public bool racialAggressive = true;
    public bool naturallyProtector = false;
    public float actionsMaxRange = 2000f;
    public float patrolRange = 500f;
    public float followRange = 900f;
    public float combatRange = 850f;
    public float meleeRange = 100f;
    public float idealRange = 50f;
    public float speed = 4f;
    public bool groundAdapt = false;
    public bool invertableCharacter = true;
    public bool delayedInverted = false;
    public float jumpFrequency = 0f;
    public int jumpFrame = 0;
    public float patrolRangeRatio = 2f;
    public float actionDelay = 60f;

    public BehaviourProperties()
    {
    }

    public BehaviourProperties(XElement property)
    {
        if (property != null && property.HasAttributes)
        {
            if (property.Attribute("timeAgonizing") != null) float.TryParse(property.Attribute("timeAgonizing").Value, out timeAgonizing);
            if (property.Attribute("retainDeath") != null) bool.TryParse(property.Attribute("retainDeath").Value, out retainDeath);
            if (property.Attribute("craven") != null) bool.TryParse(property.Attribute("craven").Value, out craven);
            if (property.Attribute("startAggressive") != null) bool.TryParse(property.Attribute("startAggressive").Value, out startAggressive);
            if (property.Attribute("naturallyAggressive") != null) bool.TryParse(property.Attribute("naturallyAggressive").Value, out naturallyAggressive);
            if (property.Attribute("racialAggressive") != null) bool.TryParse(property.Attribute("racialAggressive").Value, out racialAggressive);
            if (property.Attribute("naturallyProtector") != null) bool.TryParse(property.Attribute("naturallyProtector").Value, out naturallyProtector);
            if (property.Attribute("actionsMaxRange") != null) float.TryParse(property.Attribute("actionsMaxRange").Value, out actionsMaxRange);
            if (property.Attribute("patrolRange") != null) float.TryParse(property.Attribute("patrolRange").Value, out patrolRange);
            if (property.Attribute("followRange") != null) float.TryParse(property.Attribute("followRange").Value, out followRange);
            if (property.Attribute("combatRange") != null) float.TryParse(property.Attribute("combatRange").Value, out combatRange);
            if (property.Attribute("meleeRange") != null) float.TryParse(property.Attribute("meleeRange").Value, out meleeRange);
            if (property.Attribute("idealRange") != null) float.TryParse(property.Attribute("idealRange").Value, out idealRange);
            if (property.Attribute("speed") != null) float.TryParse(property.Attribute("speed").Value, out speed);
            if (property.Attribute("groundAdapt") != null) bool.TryParse(property.Attribute("groundAdapt").Value, out groundAdapt);
            if (property.Attribute("invertableCharacter") != null) bool.TryParse(property.Attribute("invertableCharacter").Value, out invertableCharacter);
            if (property.Attribute("delayedInverted") != null) bool.TryParse(property.Attribute("delayedInverted").Value, out delayedInverted);
            if (property.Attribute("jumpFrequency") != null) float.TryParse(property.Attribute("jumpFrequency").Value, out jumpFrequency);
            if (property.Attribute("jumpFrame") != null) int.TryParse(property.Attribute("jumpFrame").Value, out jumpFrame);
            if (property.Attribute("patrolRangeRatio") != null) float.TryParse(property.Attribute("patrolRangeRatio").Value, out patrolRangeRatio);
            if (property.Attribute("actionDelay") != null) float.TryParse(property.Attribute("actionDelay").Value, out actionDelay);

        }
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
            if (property.Attribute("height") != null) float.TryParse(property.Attribute("height").Value, out height);
            if (property.Attribute("width") != null) float.TryParse(property.Attribute("width").Value, out width);
            if (property.Attribute("offsetX") != null) float.TryParse(property.Attribute("offsetX").Value, out offsetX);
            if (property.Attribute("offsetY") != null) float.TryParse(property.Attribute("offsetY").Value, out offsetY);
            if (property.Attribute("density") != null) float.TryParse(property.Attribute("density").Value, out density);
            if (property.Attribute("friction") != null) float.TryParse(property.Attribute("friction").Value, out friction);
            if (property.Attribute("fixedRotation") != null) bool.TryParse(property.Attribute("fixedRotation").Value, out fixedRotation);
        }
    }
}

[System.Serializable]
public class Loot
{
    public float deepAmountRatio = 0.5f;

    public Loot()
    {
    }

    public Loot(XElement property)
    {
        if (property != null && property.HasAttributes)
        {
            if (property.Attribute("deepAmountRatio") != null) float.TryParse(property.Attribute("deepAmountRatio").Value, out deepAmountRatio);
        }
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
