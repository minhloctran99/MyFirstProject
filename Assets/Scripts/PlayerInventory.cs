using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private CoinCounterUI coinCounterUI;
    [SerializeField]
    private InventoryUI inventoryUI;

    [SerializeField]
    private int maxSlots = 5; // Bài 3-Chương 8: Giới hạn tối đa 5 loại item khác nhau

    private int coinCount;

    private List<Item> items = new();

    public IReadOnlyList<Item> Items
    {
        get { return items; }
    }

    public int CoinCount
    {
        get
        {
            return coinCount;
        }
    }

    private void Start()
    {
        LoadInventory();
        if (coinCounterUI != null)
        {
            coinCounterUI.SetCoinCount(coinCount);
        }
        RefreshUI();
    }

    public void AddCoin(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        coinCount += amount;
        SaveInventory();
        if (coinCounterUI != null)
        {
            coinCounterUI.SetCoinCount(coinCount);
        }
    }

    public Item FindItem(int id)
    {
        foreach (Item item in items)
        {
            if (item.Id == id)
            {
                return item;
            }
        }

        return null;
    }

    // Bài 4: Kiểm tra xem inventory có đủ số lượng item theo id yêu cầu hay không
    public bool HasItem(int id, int amount)
    {
        if (amount <= 0) return false;

        Item item = FindItem(id);
        if (item == null) return false;

        return item.Quantity >= amount;
    }

    public void AddItem(Item newItem)
    {
        if (newItem == null) return;
        Item existingItem = FindItem(newItem.Id);

        if (existingItem != null)
        {
            // Nếu item đã tồn tại -> cộng dồn số lượng (stack)
            existingItem.Quantity += newItem.Quantity;
        }
        else
        {
            // Bài 3: Nếu là item mới hoàn toàn, kiểm tra xem đã đạt giới hạn maxSlots chưa
            if (items.Count >= maxSlots)
            {
                Debug.LogWarning("Inventory is full! Cannot add new item type.");
                return; // Từ chối thêm item mới nếu hết slot
            }

            items.Add(newItem);
        }

        SaveInventory();
        RefreshUI();
    }

    public bool RemoveItem(int id, int amount)
    {
        if (amount <= 0) return false;

        Item item = FindItem(id);
        if (item == null) return false;
        if (item.Quantity < amount) return false;

        item.Quantity -= amount;
        if (item.Quantity == 0)
        {
            items.Remove(item);
        }
        SaveInventory();
        RefreshUI();
        return true;
    }

    public void UseHealthPotion(PlayerHealth playerHealth)
    {
        const int healthPotionId = 1;
        bool removed = RemoveItem(healthPotionId, 1);
        if (!removed) return;

        playerHealth.Heal(25);
    }

    public void UseLargeHealthPotion(PlayerHealth playerHealth)
    {
        const int largePotionId = 5;
        bool removed = RemoveItem(largePotionId, 1);
        if (!removed) return;

        playerHealth.Heal(50);
    }

    private void RefreshUI()
    {
        if (inventoryUI != null)
        {
            inventoryUI.Refresh(items);
        }
    }

    private void LoadInventory()
    {
        if (GameSession.Instance == null)
        {
            return;
        }

        coinCount = GameSession.Instance.CoinCount;
        items = GameSession.Instance.LoadItems();
    }

    private void SaveInventory()
    {
        if (GameSession.Instance == null)
        {
            return;
        }

        GameSession.Instance.CoinCount = coinCount;
        GameSession.Instance.SaveItems(items);
    }

    public ItemSaveData[] ExportItems()
    {
        ItemSaveData[] result =
        new ItemSaveData[items.Count];

        for (int i = 0; i < items.Count; i++)
        {
            ItemSaveData itemData =
            new()
            {
                id = items[i].Id,
                itemName = items[i].ItemName,
                quantity = items[i].Quantity
            };

            result[i] = itemData;
        }
        return result;
    }

    public void ImportItems(ItemSaveData[] savedItems)
    {
        items.Clear();

        if (savedItems == null)
        {
            RefreshUI();
            return;
        }

        foreach (ItemSaveData savedItem in savedItems)
        {
            if (savedItem.quantity <= 0) continue;

            Item item = new(
            savedItem.id,
            savedItem.itemName,
            savedItem.quantity
            );

            items.Add(item);
        }
        SaveInventory();
        RefreshUI();
    }

    public void SetCoinCount(int amount)
    {
        coinCount = Mathf.Max(amount, 0);

        SaveInventory();

        if (coinCounterUI != null)
        {
            coinCounterUI.SetCoinCount(coinCount);
        }
    }

}
