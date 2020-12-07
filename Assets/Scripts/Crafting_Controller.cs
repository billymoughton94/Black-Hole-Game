using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crafting_Controller : MonoBehaviour {

    private Inventory_Controller inventory;
    private Dictionary<String, Dictionary<Item, List<Item>>> recipes;

    // Initialise the GameObject
    void Start() {
        inventory = GetComponent<Inventory_Controller>();
        setRecipes();
    }

    // Set up the recipes dictionary and add the recipes to be used in the game
    private void setRecipes()
    {
        recipes = new Dictionary<string, Dictionary<Item, List<Item>>>();
        Dictionary<Item, List<Item>> antennaRecipe = new Dictionary<Item, List<Item>>() {
            {new Item(("Antenna")), new List<Item>() {
                {new Item("Iron", 15)},
                {new Item("Magnet", 1)}
            }}
        };

        Dictionary<Item, List<Item>> magnetRecipe = new Dictionary<Item, List<Item>>() {
            {new Item(("Magnet")), new List<Item>() {
                {new Item("Iron", 5)}
            }}
            };

        Dictionary<Item, List<Item>> fuelContainersRecipe = new Dictionary<Item, List<Item>>() {
            {new Item(("Fuel Containers")), new List<Item>() {
                {new Item("Obsidian", 8)},
                {new Item("Iron", 5)}
            }}
        };

        Dictionary<Item, List<Item>> shipBodyRecipe = new Dictionary<Item, List<Item>>() {
            {new Item(("Ship Body")), new List<Item>() {
                {new Item("Plate Sheet", 2)},
                {new Item("Obsidian", 2)}
            }}
        };

        Dictionary<Item, List<Item>> plateSheetRecipe = new Dictionary<Item, List<Item>>() {
            {new Item(("Plate Sheet")), new List<Item>() {
                {new Item("Iron", 5)}
            }}
        };

        recipes.Add("Antenna", antennaRecipe);
        recipes.Add("Magnet", magnetRecipe);
        recipes.Add("Fuel Containers", fuelContainersRecipe);
        recipes.Add("Ship Body", shipBodyRecipe);
        recipes.Add("Plate Sheet", plateSheetRecipe);
    }

        // Return the recipe dictionary
        public Dictionary<String, Dictionary<Item, List<Item>>> getRecipes() {
        return recipes;
    }
    

    public void craftItem(String name) {
        // Check if the player can craft the item
        Dictionary<Item, List<Item>> requirements = recipes[name];
        foreach (var item in requirements[requirements.Keys.First()]) {
            if (inventory.getItem(item.getName()).getAmount() < item.getAmount()) {
                Debug.Log("Cannot craft " + name);
                return;
            }
        }
        // Player is able to craft item, remove all required items from player inventory
        foreach (var item in requirements[requirements.Keys.First()]) {
            inventory.removeItem(item);
        }
        // Add the item to the player inventory
        inventory.addItem(requirements.Keys.First());
        // Special items that can only be crafted once are to be removed once crafted
        if (name.Equals("Antenna")) {
            recipes.Remove(name);
        }
        // Update the UI
        UI_Controller UI = GameObject.Find("UI").GetComponent<UI_Controller>();
        UI.updateUI();
    }
}