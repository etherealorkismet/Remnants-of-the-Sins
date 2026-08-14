using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatEffect
{
    public string stat;
    public string type;
    public float value;
}

[Serializable]
public class ItemData
{
    public int id;
    public string name;
    public List<StatEffect> effects;
}

[Serializable]
public class ItemDatabase
{
    public List<ItemData> items;
}