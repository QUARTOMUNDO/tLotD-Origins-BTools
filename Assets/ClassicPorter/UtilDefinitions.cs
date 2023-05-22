using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilDefinitions
{
    public enum PropertyDisplayTypes
    {
        String,
        Float,
        Int,
        Bool
    }

    public class NameFloatPair
    {
        public string name = "";
        public float number = 0f;

        public NameFloatPair() { name = ""; number = 0f; }

        public NameFloatPair(string name, float number)
        {
            this.name = name;
            this.number = number;
        }

        public static implicit operator string(NameFloatPair a)
        {
            return a.name;
        }

        public static implicit operator float(NameFloatPair a)
        {
            return a.number;
        }
    }

    public class NamePair
    {
        public string name = "";
        public string value = "";

        public NamePair() { name = ""; value = ""; }

        public NamePair(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public NamePair(System.Xml.Linq.XAttribute a)
        {
            name = a.Name.ToString();
            value = a.Value;
        }

        public static implicit operator string(NamePair a)
        {
            return a.name;
        }

    }

    public delegate void DelString(string arg);
    public delegate void DelStringString(string arg, string arg1);
    public delegate void DelBool(bool arg);
    public delegate void DelInt(int arg);
    public delegate void DelFloat(float arg);
    public delegate void DelPortController(PortController arg);
    public delegate void DelPropertyPairDisplay(PropertyPairDisplay arg);


    /// <summary>
        /// Custom Unity event that takes a no parameter
        /// </summary>
    [System.Serializable]
    public class UEvent : UnityEngine.Events.UnityEvent { }
    

}
