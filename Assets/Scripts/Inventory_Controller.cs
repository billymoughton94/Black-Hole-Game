using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Controller : MonoBehaviour
{

    private Dictionary<String, int> items;

    void Start()
    {
        items = new Dictionary<string, int>();
        // For testing purposes adding a bunch of items
        items.Add("Iron", 20);
        items.Add("Magnet", 2);
    }

    public void addItem(String item)
    {
        if (items.ContainsKey(item))
        {
            items[item] += 1;
        }
        else
        {
            items.Add(item, 1);
        }
    }

    public void removeItem(String item, int amount)
    {
        items[item] -= amount;
    }

    public int getItem(String name)
    {
        if (items.ContainsKey(name))
        {
            return items[name];
        }
        return 0;
    }

    public Dictionary<String, int> getItems()
    {
        return items;
    }



}