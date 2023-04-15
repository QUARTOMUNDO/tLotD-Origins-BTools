using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PHD's Custom Assets/WorkLog")]
public class ProgressLog : ScriptableObject
{
    public bool paid = false;
    public string date = "day/month/year";
    public float hours = 0f;
    public float minutes = 0f;
    [Multiline(20)]public string details = "did this thing";
}
