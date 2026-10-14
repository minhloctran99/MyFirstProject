using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName = "Health Potion";
    public int healAmount = 50;

    void Start()
    {
        Debug.Log("Item Name: " + itemName);
        Debug.Log("Heal Amount: " + healAmount);
    }
}
