using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();






    public enum StatToChange
    {
        Health,
        Mana,
        Strength,
        Defense,
        Agility,
        Intelligence
    };
}
