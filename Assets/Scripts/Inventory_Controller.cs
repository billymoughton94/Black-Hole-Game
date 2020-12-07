using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Controller : MonoBehaviour {

    private Dictionary<String, Item> items;

    // Initialise the GameObjectt
    void Start() {
        items = new Dictionary<string, Item>();
        items.Add("Health Pack", new Item("Health Pack", 3, true));
    }

    
    public void addItem(Item item) {
        // Check to see if the item already exists in player inventory
        if (items.ContainsKey(item.getName())) {
            // If it does, add to the amount value held in the item object
            items[item.getName()].addAmount(item.getAmount());
        } else {
            // If it does not, add a new Item object to the inventory
            items.Add(item.getName(), item);
        }

        // checking the main components required to win the game
        switch (item.getName())
        {
            case "Antenna":
                Game_Manager.tickOffItem("Antenna");
                break;
           case "Ship Body":
                Game_Manager.tickOffItem("Ship Body");
                break;
            case "Fuel Containers":
                Game_Manager.tickOffItem("Fuel Containers");
                break;
        }
    }

    public void removeItem(Item item) {
        // Subtract from the amount value in the item object
        items[item.getName()].addAmount(-item.getAmount());
        // If amount reaches 0 or below, remove it from the inventory
        if (items[item.getName()].getAmount() <= 0) {
            items.Remove(item.getName());
        }
        // Update the UI
        UI_Controller UI = GameObject.Find("UI").GetComponent<UI_Controller>();
        UI.updateUI();
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