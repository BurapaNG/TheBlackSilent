using UnityEngine;

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
