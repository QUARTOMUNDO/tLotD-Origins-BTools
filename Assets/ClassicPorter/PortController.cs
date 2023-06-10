using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System;
using UtilDefinitions;

public class PortController : MonoBehaviour
{
    [Header("DEBUG")]
    public bool debugMode = false;
    [Header("CONFIG")]
    public GameObject propertyDisplayPrefab;
    public GameObject propertyDisplayPrefabSmall;
    public GameObject propertyPairDisplayPrefab;
    public GameObject propertyIntFloatDisplayPrefab;
    public string defaultXML_Path = "F:/UnityProjects/QUARTOMUNDO/tLotD_Classic/Assets/Data/Test.xml";
    public string sourceXML_Path = "F:/UnityProjects/QUARTOMUNDO/tLotD_Classic/Assets/Data/Test.xml";
    public string exportXML_Path = "F:/UnityProjects/QUARTOMUNDO/tLotD_Classic/Assets/Data/";

    //[Multiline(30)]
    public List<string> creatureNodes = new List<string>();
    public List<CreatureData> creaturesLoaded = new List<CreatureData>();
    public Dictionary<string, CreatureData> defaultCreatures = new Dictionary<string, CreatureData>();
    public Dictionary<string, CreatureEntry> creatures = new Dictionary<string, CreatureEntry>();

    public event DelPortController OnCreatureReload;

    public static PortController single;

    private void Awake()
    {
        if (single && single != this)
        {
            Destroy(single.gameObject);
        }
        single = this;

        //LoadCreaturesFromXml();
        LoadCreatures();

    }


    [ContextMenu("TestFunction")]
    public void TestFunction()
    {
        XmlDocument xmlDocument = new XmlDocument();
        XmlElement root = xmlDocument.CreateElement("test");
        XmlElement dog = xmlDocument.CreateElement("dog");
        root.AppendChild(dog);
        XmlElement leg1 = xmlDocument.CreateElement("leg");
        leg1.InnerText = "Left Hind Leg";
        leg1.SetAttribute("state", "stronger");
        dog.AppendChild(leg1);
        XmlElement leg2 = xmlDocument.CreateElement("leg");
        leg2.InnerText = "Right Hind Leg";
        leg2.SetAttribute("state", "hurt");
        dog.AppendChild(leg2);
        XmlElement leg3 = xmlDocument.CreateElement("leg");
        leg3.InnerText = "Left Front Leg";
        dog.AppendChild(leg3);
        XmlElement leg4 = xmlDocument.CreateElement("leg");
        leg4.InnerText = "Right Front Leg";
        dog.AppendChild(leg4);
        XmlElement snout = xmlDocument.CreateElement("snout");
        snout.InnerText = "A beautiful wet snout";
        dog.AppendChild(snout);
        XmlElement tail = xmlDocument.CreateElement("snout");
        tail.InnerText = "A joyful long tail";
        dog.AppendChild(tail);
        print("print 1: [" + leg1.Attributes.GetNamedItem("state") == null + "]");
        print("print 2: [" + leg2.Attributes.GetNamedItem("state").NodeType + "]");
        print("print 3: [" + leg3.Attributes.GetNamedItem("state") + "]");
        print("print 4: [" + leg4.Attributes.GetNamedItem("state") == null + "]");
        xmlDocument.AppendChild(root);
        xmlDocument.Save(exportXML_Path);
        if (File.Exists(exportXML_Path))
        {
            print("Success");
        }
    }

    [ContextMenu("TestFunction2")]
    public void LoadCreaturesFromXml()
    {

        //XmlDocument xmlDocument = new XmlDocument();
        //xmlDocument.Load(sourceXML_Path);
        XDocument xDocument = XDocument.Load(sourceXML_Path);
        XElement root = xDocument.Element("Characters");
        creatureNodes.Clear();
        creaturesLoaded.Clear();
        defaultCreatures.Clear();

        int n = 0;
        foreach (XElement creatureElement in root.Elements())
        {
            string str = "[ " + creatureElement.Attribute("varName").Value + " ] " + creatureElement.Attribute("name").Value + "\n";
            //string str = "[ " + creatureElement.Attributes.GetNamedItem("varName").Value + " ] " + creatureElement.Attributes.GetNamedItem("name").Value + "\n";
            if (creatureElement.HasElements)
            {
                creaturesLoaded.Add(new CreatureData(creatureElement));

                if (!defaultCreatures.TryAdd(creatureElement.Attribute("varName").Value, new CreatureData(creatureElement)))
                {
                    Debug.LogWarning("WARNING: attempt to load creature [ " + creatureElement.Attribute("varName").Value + " ] from Xml document [ " + sourceXML_Path + " ], " +
                        "but a creature with the same varName was already declared before");
                }

                foreach (XElement propertyElement in creatureElement.Elements())
                {
                    str += "\n  Property node: [ " + propertyElement.Name + " ]";
                    foreach (XAttribute propertyAttribute in propertyElement.Attributes())
                    {
                        str += "\n  " + propertyAttribute.Name + " = " + propertyAttribute.Value;
                    }
                }
            }
            creatureNodes.Add(str);
            print("Creature [ " + n + " ] \n" + str);
            n++;
        }

        #region XmlDocument version

        /*
        XmlNode root = xmlDocument.FirstChild;
        creatureNodes.Clear();
        creatures.Clear();
        int n = 0;
        foreach (XmlNode creatureNode in root.ChildNodes)
        {
            string str = "[ "+creatureNode.Attributes.GetNamedItem("varName").Value+" ] "+ creatureNode.Attributes.GetNamedItem("name").Value + "\n";
            if (creatureNode.ChildNodes.Count>0)
            {
                creatures.Add(new CreatureData(creatureNode));

                for (int i = 0; i < creatureNode.ChildNodes.Count; i++)
                {
                    XmlNode propertyNode = creatureNode.ChildNodes[i];
                    str += "\n  Property node: [ " +propertyNode.Name+" ]";
                    for (int j = 0; j < propertyNode.Attributes.Count; j++)
                    {
                        str += "\n  " + creatureNode.ChildNodes[i].Attributes[j].Name + " = " + creatureNode.ChildNodes[i].Attributes[j].Value; 
                    }
                }
            }
            creatureNodes.Add(str);
            print("Creature [ "+n+" ] \n"+ str);
            n++;
        }    
        */

        #endregion

    }


    public void LoadCreatures()
    {
        CreatureEditorSaveData data = SaveData.LoadFile<CreatureEditorSaveData>("CreatureEditorData");
        if (data)
        {
            defaultXML_Path = data.defaultXML_Path;
            sourceXML_Path = data.sourceXML_Path;
            exportXML_Path = data.exportXML_Path;
            Debug.Log("loaded the following creature xml paths from persistent data: \n Default: " + defaultXML_Path + "\n Source: " + sourceXML_Path + "\n Export: " + exportXML_Path);
        }
        else Debug.LogWarning("Failed to find CreatureEditorData on persistent data, using engine defaults");

        XDocument xDocSource = XDocument.Load(sourceXML_Path);
        creaturesLoaded.Clear();
        defaultCreatures.Clear();

        creatures.Clear();
        //Loading defaults
        LoadCreatureDefaults(defaultXML_Path);
        //Loading source and current
        LoadCreatureFile(sourceXML_Path, true);
        OnCreatureReload?.Invoke(this);
    }

    private void LoadCreatureDefaults(string path)
    {
        XDocument xDoc = XDocument.Load(path);
        CreatureEntry creatureEntry = null;
        CreatureData creatureData = null;
        int n = 0;
        foreach (XElement creatureElement in xDoc.Element("Characters").Elements())
        {
            string str = "[ " + creatureElement.Attribute("varName").Value + " ] " + creatureElement.Attribute("name").Value + "\n";

            if (creatureElement.HasElements)
            {
                creatureData = new CreatureData(creatureElement);
                if (creatures.TryGetValue(creatureElement.Attribute("varName").Value, out creatureEntry))
                {
                    if (creatureEntry.defaultData)
                    {
                        Debug.LogWarning("WARNING: attempt to load DEFAULTS for creature [ " + creatureElement.Attribute("varName").Value
                            + " ] from Xml document [ " + path + " ], "
                            + "but a creature with the same varName was already declared before");
                    }
                    creatureEntry.defaultData = creatureData;
                }
                else
                {
                    creatures.Add(creatureElement.Attribute("varName").Value, new CreatureEntry(defaultData: creatureData));
                }

                //DEBUG
                if (debugMode)
                {
                    foreach (XElement propertyElement in creatureElement.Elements())
                    {
                        str += "\n  Property node: [ " + propertyElement.Name + " ]";
                        foreach (XAttribute propertyAttribute in propertyElement.Attributes())
                        {
                            str += "\n  " + propertyAttribute.Name + " = " + propertyAttribute.Value;
                        }
                    }
                }

            }
            if (debugMode) print("Creature [ " + n + " ] \n" + str);
            n++;
        }
    }

    private void LoadCreatureFile(string path, bool overrideCurrent = true)
    {
        XDocument xDoc = XDocument.Load(path);
        CreatureEntry creatureEntry = null;
        CreatureData creatureData = null;
        int n = 0;
        foreach (XElement creatureElement in xDoc.Element("Characters").Elements())
        {
            string str = "[ " + creatureElement.Attribute("varName").Value + " ] " + creatureElement.Attribute("name").Value + "\n";

            if (creatureElement.HasElements)
            {
                creatureData = new CreatureData(creatureElement);
                if (creatures.TryGetValue(creatureElement.Attribute("varName").Value, out creatureEntry))
                {
                    if (creatureEntry.sourceData)
                    {
                        Debug.LogWarning("WARNING: attempt to load SOURCE for creature [ " + creatureElement.Attribute("varName").Value
                            + " ] from Xml document [ " + path + " ], "
                            + "but a creature with the same varName was already declared before");
                    }
                    creatureEntry.sourceData = creatureData;
                }
                else
                {
                    creatureEntry = new CreatureEntry(sourceData: creatureData);
                    creatures.Add(creatureElement.Attribute("varName").Value, creatureEntry);
                }
                if (overrideCurrent) creatureEntry.currentData = new CreatureData(creatureElement);

                //DEBUG
                if (debugMode)
                {
                    foreach (XElement propertyElement in creatureElement.Elements())
                    {
                        str += "\n  Property node: [ " + propertyElement.Name + " ]";
                        foreach (XAttribute propertyAttribute in propertyElement.Attributes())
                        {
                            str += "\n  " + propertyAttribute.Name + " = " + propertyAttribute.Value;
                        }
                    }
                }

            }
            if (debugMode) print("Creature [ " + n + " ] \n" + str);
            n++;
        }
    }

    public void SaveCreaturesToSource()
    {
        ExportCreaturesToXmlAtPath(sourceXML_Path);
        Debug.Log("Saved [ " + creatures.Keys.Count + " ] creatures to source Xml [" + sourceXML_Path + "]");
    }

    public void ExportCreaturesToXml()
    {
        ExportCreaturesToXmlAtPath(exportXML_Path);
        Debug.Log("Exported [ " + creatures.Keys.Count + " ] creatures to output Xml [" + exportXML_Path + "]");

        SephiusEngineSocket client = new SephiusEngineSocket();
        client.SendCommand("Update Character Properties");
        print("Update Character Properties");
    }

    private void ExportCreaturesToXmlAtPath(string path)
    {
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        XDocument doc = new XDocument();
        XElement root = new XElement("Characters");

        foreach (CreatureEntry entry in creatures.Values)
        {
            root.Add(CreatureData.XmlFromCreature(entry.currentData));
        }

        doc.Add(root);
        doc.Save(stream);
        stream.Close();
        LoadCreatures();
    }

    public void RequestCreatureReload()
    {
        LoadCreatures();
        //OnCreatureReload?.Invoke(this);
    }

    public void Quit()
    {
        Application.Quit(0);
    }


    /*
     ------------------------------------------------------------------------
    ------------------------------- CHARACTER ------------------------------ 
    ------------------------------------------------------------------------ 

    public static function createCharacterXML():XML{
            var cCharacterProperties:CharacterProperties;
            var cCharacterAttributes:CharactersAttributes;
            var cActions:Actions;
            var CPXML:XML = new XML(<Characters/>);
    var cpNode:XML;
            var cpCharStaticAttrNode:XML;
            var attackIntensityNode:XML;
            var damageNatureNode:XML;
            var behaviorPropertiesNode:XML;
            var actionsNode:XML;
            var creaturePhysicsNode:XML;
            var lootNode:XML;
            var natureNode:XML;
            var natureName:String;
            var sephiusNode:XML;
            var player:Sephius;

            for each(cCharacterProperties in CharacterProperties.PROPERTY_LIST2)
    {
        cpNode = new XML(<{ "Characters" } />);
        cpNode.@varName = cCharacterProperties.varName;
        cpNode.@name = cCharacterProperties.name;

        natureNode = new XML(<{ "NatureResistance" } />);

        natureNode.@Physical = cCharacterProperties.staticAttribues.natureResistances.Physical;
        natureNode.@Air = cCharacterProperties.staticAttribues.natureResistances.Air;
        natureNode.@Fire = cCharacterProperties.staticAttribues.natureResistances.Fire;
        natureNode.@Corruption = cCharacterProperties.staticAttribues.natureResistances.Corruption;
        natureNode.@Darkness = cCharacterProperties.staticAttribues.natureResistances.Darkness;
        natureNode.@Bio = cCharacterProperties.staticAttribues.natureResistances.Bio;
        natureNode.@Earth = cCharacterProperties.staticAttribues.natureResistances.Earth;
        natureNode.@Ice = cCharacterProperties.staticAttribues.natureResistances.Ice;
        natureNode.@Light = cCharacterProperties.staticAttribues.natureResistances.Light;
        natureNode.@Psionica = cCharacterProperties.staticAttribues.natureResistances.Psionica;
        natureNode.@Water = cCharacterProperties.staticAttribues.natureResistances.Water;

        cpCharStaticAttrNode = new XML(<{ "CharacterAttributes" } />);

        cpCharStaticAttrNode.@baseLevel = cCharacterProperties.staticAttribues.baseLevel;
        cpCharStaticAttrNode.@attackPower = cCharacterProperties.staticAttribues.attackPower;
        cpCharStaticAttrNode.@bodyPower = cCharacterProperties.staticAttribues.bodyPower;
        cpCharStaticAttrNode.@inflictsBodyDamage = cCharacterProperties.staticAttribues.inflictsBodyDamage;
        cpCharStaticAttrNode.@inflictsAttackDamage = cCharacterProperties.staticAttribues.inflictsAttackDamage;

        if (cCharacterProperties.staticAttribues.harmfullImpact != -1 && cCharacterProperties.staticAttribues.harmfullImpact != CharacterStaticAttributes.defaultHarmfullImpact)
            cpCharStaticAttrNode.@harmfullImpact = cCharacterProperties.staticAttribues.harmfullImpact;

        cpCharStaticAttrNode.@normalSplash = cCharacterProperties.staticAttribues.normalSplash;
        cpCharStaticAttrNode.@weakSplash = cCharacterProperties.staticAttribues.weakSplash;
        cpCharStaticAttrNode.@defenceSplash = cCharacterProperties.staticAttribues.defenceSplash;
        cpCharStaticAttrNode.@abnormalSplash = cCharacterProperties.staticAttribues.abnormalSplash;
        cpCharStaticAttrNode.@strengthFactor = cCharacterProperties.staticAttribues.strengthFactor;
        cpCharStaticAttrNode.@resistanceFactor = cCharacterProperties.staticAttribues.resistanceFactor;
        cpCharStaticAttrNode.@efficiencyFactor = cCharacterProperties.staticAttribues.efficiencyFactor;
        cpCharStaticAttrNode.@peripheralFactor = cCharacterProperties.staticAttribues.peripheralFactor;
        cpCharStaticAttrNode.@mysticalFactor = cCharacterProperties.staticAttribues.mysticalFactor;
        cpCharStaticAttrNode.@sufferCriticable = cCharacterProperties.staticAttribues.sufferCriticable;
        cpCharStaticAttrNode.@damagerCriticable = cCharacterProperties.staticAttribues.damagerCriticable;
        cpCharStaticAttrNode.@bodyDamageRepeatTime = cCharacterProperties.staticAttribues.bodyDamageRepeatTime;
        cpCharStaticAttrNode.@bodyAttackWeight = cCharacterProperties.staticAttribues.bodyAttackWeight;
        cpCharStaticAttrNode.@bodySufferWeight = cCharacterProperties.staticAttribues.bodySufferWeight;
        cpCharStaticAttrNode.@attackDamageRepeatTime = cCharacterProperties.staticAttribues.attackDamageRepeatTime;
        cpCharStaticAttrNode.@attackWeight = cCharacterProperties.staticAttribues.attackWeight;
        cpCharStaticAttrNode.@pullDirection = cCharacterProperties.staticAttribues.pullDirection;
        cpCharStaticAttrNode.@pullTypeID = cCharacterProperties.staticAttribues.pullTypeID;
        cpCharStaticAttrNode.@regenerable = cCharacterProperties.staticAttribues.regenerable;
        cpCharStaticAttrNode.@degenerable = cCharacterProperties.staticAttribues.degenerable;

        behaviorPropertiesNode = new XML(<{ "BehaviorProperties" } />);

        behaviorPropertiesNode.@timeAgonizing = cCharacterProperties.behaviorProperties.timeAgonizing;
        behaviorPropertiesNode.@retainDeath = cCharacterProperties.behaviorProperties.retainDeath;
        behaviorPropertiesNode.@craven = cCharacterProperties.behaviorProperties.craven;
        behaviorPropertiesNode.@startAggressive = cCharacterProperties.behaviorProperties.startAggressive;
        behaviorPropertiesNode.@naturallyAggressive = cCharacterProperties.behaviorProperties.naturallyAggressive;
        behaviorPropertiesNode.@racialAggressive = cCharacterProperties.behaviorProperties.racialAggressive;
        behaviorPropertiesNode.@naturallyProtector = cCharacterProperties.behaviorProperties.naturallyProtector;
        behaviorPropertiesNode.@actionsMaxRange = cCharacterProperties.behaviorProperties.actionsMaxRange;
        behaviorPropertiesNode.@patrolRange = cCharacterProperties.behaviorProperties.patrolRange;
        behaviorPropertiesNode.@followRange = cCharacterProperties.behaviorProperties.followRange;
        behaviorPropertiesNode.@combatRange = cCharacterProperties.behaviorProperties.combatRange;
        behaviorPropertiesNode.@meleeRange = cCharacterProperties.behaviorProperties.meleeRange;
        behaviorPropertiesNode.@idealRange = cCharacterProperties.behaviorProperties.idealRange;
        behaviorPropertiesNode.@speed = cCharacterProperties.behaviorProperties.speed;
        behaviorPropertiesNode.@groundAdapt = cCharacterProperties.behaviorProperties.groundAdapt;
        behaviorPropertiesNode.@invertableCharacter = cCharacterProperties.behaviorProperties.invertableCharacter;
        behaviorPropertiesNode.@delayedInverted = cCharacterProperties.behaviorProperties.delayedInverted;
        behaviorPropertiesNode.@jumpFrequency = cCharacterProperties.behaviorProperties.jumpFrequency;
        behaviorPropertiesNode.@jumpFrame = cCharacterProperties.behaviorProperties.jumpFrame;
        behaviorPropertiesNode.@patrolRangeRatio = cCharacterProperties.behaviorProperties.patrolRangeRatio;
        behaviorPropertiesNode.@actionDelay = cCharacterProperties.behaviorProperties.actionDelay;

        creaturePhysicsNode = new XML(<{ "CreaturePhysics" } />);

        creaturePhysicsNode.@height = cCharacterProperties.physics.height;
        creaturePhysicsNode.@width = cCharacterProperties.physics.width;
        creaturePhysicsNode.@offsetX = cCharacterProperties.physics.offsetX;
        creaturePhysicsNode.@offsetY = cCharacterProperties.physics.offsetY;
        creaturePhysicsNode.@density = cCharacterProperties.physics.density;
        creaturePhysicsNode.@friction = cCharacterProperties.physics.friction;
        creaturePhysicsNode.@fixedRotation = cCharacterProperties.physics.fixedRotation;

        lootNode = new XML(<{ "Loot" } />);
        lootNode.@deepAmountRatio = cCharacterProperties.loot.deepAmountRatio;

        CPXML.appendChild(cpNode);
        cpNode.appendChild(natureNode);
        cpNode.appendChild(cpCharStaticAttrNode);
        cpNode.appendChild(behaviorPropertiesNode);
        cpNode.appendChild(creaturePhysicsNode);
        cpNode.appendChild(lootNode);
    }

            return CPXML;
            saveXML(CPXML, "CharacterProperties");

}
     */

}
