using System;
using System.Collections.Generic;
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

    public void craftItem(String name) {
        Debug.Log(name);
        if (!canCraft(name)) {
            return;
        }
        List<Item> requirements = recipes.getRecipe(name);
        // foreach (KeyValuePair<String, int> requirement in requirements) {
        //     inventory.removeItem(requirement.Key, requirement.Value);
        // }
        // inventory.addItem(name);
        // Debug.Log("Crafted " + name);
    }

    private bool canCraft(String name) {
        List<Item> requirements = recipes.getRecipe(name);
        foreach (var item in requirements) {
            if (inventory.getItem(item.getName()).getAmount() < item.getAmount()) {
                Debug.Log("Cannot craft " + name);
                return false;
            }
        }
        return true;
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

    public List<Item> getRecipe(String name) {
        //return recipeList[new Item(name)];
        return null;
    }
}