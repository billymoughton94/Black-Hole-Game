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

    public void craftItem(String name)
    {
        Debug.Log(name);
        if (!canCraft(name))
        {
            return;
        }
        Dictionary<String, int> requirements = recipes.getRecipe(name);
        foreach (KeyValuePair<String, int> requirement in requirements)
        {
            inventory.removeItem(requirement.Key, requirement.Value);
        }
        inventory.addItem(name);
        Debug.Log("Crafted " + name);
    }

    private bool canCraft(String name)
    {
        Dictionary<String, int> requirements = recipes.getRecipe(name);
        foreach (KeyValuePair<String, int> requirement in requirements)
        {
            if (inventory.getItem(requirement.Key) < requirement.Value)
            {
                Debug.Log("Cannot craft " + name);
                return false;
            }
        }
        return true;
    }
}


class RecipeController
{
    private Dictionary<String, Dictionary<String, int>> recipeList;

    public RecipeController()
    {
        recipeList = new Dictionary<string, Dictionary<String, int>>();

        // The recipes to be used in the game
        recipeList.Add("Antenna", new Dictionary<string, int>() {
            {"Iron", 10},
            {"Magnet", 1}
        });
    }

    public Dictionary<String, int> getRecipe(String name)
    {
        Debug.Log("recipeList[name]");
        return recipeList[name];
    }
}