using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public Common.ItemType itemType;

    [HideInInspector] // HideInInspector makes sure the default inspector won't show these fields.
    public Common.PotionType potionType;

    [HideInInspector] // HideInInspector makes sure the default inspector won't show these fields.
    public int healAmount;

    [HideInInspector]
    public int damageAmount;

    public int itemCount;
    public bool isStackable;

    public Sprite itemIcon;

    public int maxItemCount;

    // always make use itemCount is never over our limit
    private void OnEnable()
    {

    }

    // You can also use OnAfterDeserialize for the other way around
    public void OnAfterDeserialize()
    {
    }

    public bool isItemMax()
    {
        if (itemCount >= maxItemCount)
        {
            return true;
        }
        return false;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Item))] // allow the player to add a projectile if this is a projectile weapon
public class RandomScript_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // for other non-HideInInspector fields

        Item script = (Item)target;

        if (script.itemType == Common.ItemType.Potion) // if bool is true, show other fields
        {
            script.potionType = (Common.PotionType)EditorGUILayout.EnumPopup("Potion Type", script.potionType);
            if (script.potionType == Common.PotionType.HealthPotion)
            {
                script.healAmount = EditorGUILayout.IntField("Heal Amount", script.healAmount);
            }

        }

        if (script.itemType == Common.ItemType.Bomb)
        {
            script.damageAmount = EditorGUILayout.IntField("Damage Amount", script.damageAmount);
        }
    }
}
#endif