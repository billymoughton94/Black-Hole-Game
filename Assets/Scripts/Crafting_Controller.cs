using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crafting_Controller : MonoBehaviour
{

    private Inventory_Controller inventory;
    private RecipeController recipes;

    void Start()
    {
        inventory = GetComponent<Inventory_Controller>();
        recipes = new RecipeController();
    }
    

    private void craftItem(String name) {
        Dictionary<Item, List<Item>> requirements = recipes.getRecipe(name);
        foreach (var item in requirements[requirements.Keys.First()]) {
            if (inventory.getItem(item.getName()).getAmount() < item.getAmount()) {
                Debug.Log("Cannot craft " + name);
                return;
            }
        }
        foreach (var item in requirements[requirements.Keys.First()]) {
            inventory.removeItem(item);
        }
        inventory.addItem(requirements.Keys.First());
    }
}


class RecipeController {
    private Dictionary<String, Dictionary<Item, List<Item>>> recipeList;

    public RecipeController() {
        recipeList = new Dictionary<String, Dictionary<Item, List<Item>>>();

        // The recipes to be used in the game
        Dictionary<Item, List<Item>> antennaRecipe = new Dictionary<Item, List<Item>>() {
            {new Item(("Antenna")), new List<Item>() {
                {new Item("Iron", 10)},
                {new Item("Magnet", 1)}
            }}, 
        };

        recipeList.Add("Antenna", antennaRecipe);
    }

    public Dictionary<Item, List<Item>> getRecipe(String name) {
        return recipeList[name];
    }
}