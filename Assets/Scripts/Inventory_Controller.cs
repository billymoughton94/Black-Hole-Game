using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Controller : MonoBehaviour {

    private Dictionary<String, Item> items;

    void Start() {
        items = new Dictionary<string, Item>();
        // For testing purposes adding a bunch of items
        items.Add("Iron", new Item("Iron", 20, false));
        items.Add("Magnet", new Item("Magnet", 2, false));
        items.Add("Food", new Item("Food", 2, true));
    }

    public void addItem(Item item) {
        if (items.ContainsKey(item.getName())) {
            items[item.getName()].addAmount(item.getAmount());
        } else {
            items.Add(item.getName(), item);
        }
    }

    public void removeItem(Item item) {
        items[item.getName()].addAmount(-item.getAmount());
        if (items[item.getName()].getAmount() <= 0) {
            items.Remove(item.getName());
        }
    }

    public Item getItem(String name) {
        if (items.ContainsKey(name)) {
            return items[name];
        }
        return null;
    }

    public Dictionary<String, Item> getItems() {
        return items;
    }
}

public class Item {
    private string name;
    private int amount;
    private bool useable;
    
    public Item(String name) {
        this.name = name;
        this.amount = 1;
        this.useable = false;
    }

    public Item(String name, int amount) {
        this.name = name;
        this.amount = amount;
        this.useable = false;
    }

    public Item(string name, int amount, bool useable) {
        this.name = name;
        this.amount = amount;
        this.useable = useable;
    }

    public void addAmount(int amount) {
        this.amount += amount;
    }
    
    public String getName() {
        return name;
    }

    public int getAmount() {
        return amount;
    }

    public bool getUseable() {
        return useable;
    }
    
    public override bool Equals(System.Object obj) {
        //Check for null and compare run-time types.
        if ((obj == null) || ! this.GetType().Equals(obj.GetType())) {
            return false;
        } else {
            Item i = (Item) obj;
            return (name == i.name);
        }
    }
    
    public override int GetHashCode() {
        return (name.GetHashCode() << 2) ^ 7;
    }
}