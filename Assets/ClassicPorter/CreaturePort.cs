using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using UtilDefinitions;
using System.Reflection;
using System.Linq;

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

    public static XElement GetXMLElement(CreatureData creatureData)
    {
        XElement element = new XElement("Character");
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
    
    /// <summary>
    /// Parse XML attribute value to taking into account the type.
    /// </summary>
    /// <param name="value">Value itself</param>
    /// <param name="targetType">Value Type</param>
    /// <param name="parsedValue"></param>
    /// <param name="parsingSuccess"></param>
    public static void ParseValue(string value, Type targetType, out object parsedValue, out bool parsingSuccess)
    {
        parsingSuccess = false;
        parsedValue = null;

        if (targetType == typeof(int))
        {
            int intValue;
            if (int.TryParse(value, out intValue))
            {
                parsedValue = intValue;
                parsingSuccess = true;
            }
        }
        else if (targetType == typeof(float))
        {
            float floatValue;
            if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out floatValue))
            {
                parsedValue = floatValue;
                parsingSuccess = true;
            }
        }
        else if (targetType == typeof(bool))
        {
            bool boolValue;
            if (bool.TryParse(value, out boolValue))
            {
                parsedValue = boolValue;
                parsingSuccess = true;
            }
        }
        else if (targetType == typeof(string))
        {
            parsedValue = value;
            parsingSuccess = true;
        }
    }

    public static void SetFromXML<T>(XElement property, T targetObject) {
        if (property != null && property.HasAttributes){
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
            string[] excludedMemberNames = { "GetXElement" };
            PropertyInfo[] properties = targetObject.GetType().GetProperties(flags);
            FieldInfo[] fields = targetObject.GetType().GetFields(flags);

            foreach (XAttribute attribute in property.Attributes()){
                string attributeName = attribute.Name.LocalName;
                string attributeValue = attribute.Value;

                PropertyInfo propertyInfo = properties.FirstOrDefault(p => p.Name == attributeName);

                if (propertyInfo != null){
                    object parsedValue;
                    bool parsingSuccess;

                    CreatureData.ParseValue(attributeValue, propertyInfo.PropertyType, out parsedValue, out parsingSuccess);

                    if (parsingSuccess){
                        if (propertyInfo.CanWrite){
                            propertyInfo.SetValue(targetObject, parsedValue); 
                        }
                        else {
                            Debug.LogWarning("Property does not have a setter: " + attributeName);
                        }
                    }
                    else{
                        Debug.LogWarning("Failed to parse attribute: " + attributeName);
                    }
                }
                else{
                    FieldInfo fieldInfo = fields.FirstOrDefault(f => f.Name == attributeName);

                    if (fieldInfo != null){
                        object parsedValue;
                        bool parsingSuccess;

                        CreatureData.ParseValue(attributeValue, fieldInfo.FieldType, out parsedValue, out parsingSuccess);

                        if (parsingSuccess){
                            fieldInfo.SetValue(targetObject, parsedValue);
                        }
                        else{
                            Debug.LogWarning("Failed to parse attribute: " + attributeName);
                        }
                    }
                    else{
                        Debug.LogWarning("Unrecognized attribute or non-property member: " + attributeName);
                    }
                }
            }
        }
    }
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

    public float mysticalEfficiencyFactor = 100f;

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

    /// <summary>
    /// Read from XML Element
    /// </summary>
    /// <param name="property"></param>
    public CharacterAttributes(XElement property){
        CreatureData.SetFromXML(property, this);
    }

    /// <summary>
    /// Read from attributes data and create XML Element
    /// </summary>
    /// <returns></returns>    
    public XElement GetXElement()
    {
        Type type = typeof(CharacterAttributes);
        FieldInfo[] fields = type.GetFields();

        XElement element = new XElement("CharacterAttributes");
        List<XAttribute> attributes = new List<XAttribute>();

        foreach (FieldInfo field in fields)
        {
            string fieldName = field.Name;
            object fieldValue = field.GetValue(this);

            element.Add(new XAttribute(fieldName, fieldValue));
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

    /// <summary>
    /// Read from XML Element
    /// </summary>
    /// <param name="property"></param>
    public BehaviorProperties(XElement property){
        CreatureData.SetFromXML(property, this);
    }

    /// <summary>
    /// Read from attributes data and create XML Element
    /// </summary>
    /// <returns></returns>    
    public XElement GetXElement()
    {
        Type type = typeof(BehaviorProperties);
        FieldInfo[] fields = type.GetFields();

        XElement element = new XElement("BehaviorProperties");
        List<XAttribute> attributes = new List<XAttribute>();

        foreach (FieldInfo field in fields)
        {
            string fieldName = field.Name;
            object fieldValue = field.GetValue(this);

            element.Add(new XAttribute(fieldName, fieldValue));
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
    public float visualScaleRatio = 1;
    public string color = "255,255,255";

    public CreaturePhysics()
    {
    }

    public CreaturePhysics(XElement property){
        CreatureData.SetFromXML(property, this);
    }

    /// <summary>
    /// Read from attributes data and create XML Element
    /// </summary>
    /// <returns></returns>    
    public XElement GetXElement()
    {
        Type type = typeof(CreaturePhysics);
        FieldInfo[] fields = type.GetFields();

        XElement element = new XElement("CreaturePhysics");
        List<XAttribute> attributes = new List<XAttribute>();

        foreach (FieldInfo field in fields)
        {
            string fieldName = field.Name;
            object fieldValue = field.GetValue(this);

            element.Add(new XAttribute(fieldName, fieldValue));
        }

        return element;
    }
}

[System.Serializable]
public class Loot
{
    public float deepAmountRatio = 0.5f;
    public List<NameFloatPair> natureDrops = new List<NameFloatPair>();
    public List<LootObjectDropElement> objectDrops = new List<LootObjectDropElement>();


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
        objectDrops = new List<LootObjectDropElement>();

        if (objectDropsElement != null && objectDropsElement.HasAttributes)
        {
            string[] amountValueArray = new string[2];

            foreach (var drop2 in objectDropsElement.Attributes())
            {
                amountValueArray = drop2.Value.Split("|");
                if (amountValueArray.Length == 2)
                {
                    objectDrops.Add(new LootObjectDropElement(drop2.Name.ToString(), int.Parse(amountValueArray[0]), float.Parse(amountValueArray[1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture)));
                }
                else
                {
                    Debug.LogError("Failed to parse object drop "+drop2.Name+ " [ "+drop2.Value+" ] as a loot object drop. Please check format {name='amount|chance'}");
                }
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

        foreach (LootObjectDropElement pair2 in objectDrops)
        {
            objectDropsElement.Add(new XAttribute(pair2.name, pair2.amount.ToString(System.Globalization.CultureInfo.InvariantCulture)+"|"+pair2.chance.ToString(System.Globalization.CultureInfo.InvariantCulture)));
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
