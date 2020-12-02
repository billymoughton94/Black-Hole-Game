using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.Build.Content;

public class UI_Controller : MonoBehaviour {
    
    // Dictionary containing the parts and the quantity obtained
    private Dictionary<string, int> parts;
    private bool panelOpen;
    
    
    void Start() {
        // Initialise the parts dictionary and add the initial values
        parts = new Dictionary<string, int>();
        parts.Add("Antenna", 0);
        parts.Add("Ship Body", 0);
        parts.Add("Fuel Containers", 0);
        panelOpen = false;
    }

    public void updatePartsList(GameObject item) {
        // Add to the quantity
        parts[item.name] += 1;
        // Get the text object
        TextMeshProUGUI partsList = transform.Find("PartList").GetComponent<TextMeshProUGUI>();
        // Create the string to be displayed
        string partsListText = "Parts Collected:\n";
        foreach (KeyValuePair<String, int> part in parts) {
            partsListText += part.Key + ": " + part.Value + "/1\n";
        }
        // Set the text
        partsList.text = partsListText;
    }

    private void updateInventory() {
        TextMeshProUGUI itemList = transform.Find("InventoryPanel").Find("ItemList").GetComponent<TextMeshProUGUI>();
        Inventory_Controller inventory = GameObject.Find("Player").GetComponent<Inventory_Controller>();
        Dictionary<String, int> items = inventory.getItems();
        String itemListText = "";
        foreach (KeyValuePair<String, int> item in items) {
            itemListText += item.Key + ": " + item.Value + "\n";
        }
        itemList.text = itemListText;
    }

    public void showVictoryPanel() {
        // Get the victory panel game object and set it to active
        GameObject VictoryPanel = transform.Find("VictoryPanel").gameObject;
        VictoryPanel.SetActive(true);
    }

    public void showDefeatPanel() {
        // Get the defeat panel game object and set it to active
        GameObject DefeatPanel = transform.Find("DefeatPanel").gameObject;
        DefeatPanel.SetActive(true);
    }
    

<<<<<<< HEAD
    public void retryPressed() {
        // Restart the game.
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
=======
    public void hideInventoryPanel() {
        GameObject InventoryPanel = transform.Find("InventoryPanel").gameObject;
        InventoryPanel.SetActive(false);
    }
    
    public void hideCraftingPanel() {
        GameObject CraftingPanel = transform.Find("CraftingPanel").gameObject;
        CraftingPanel.SetActive(false);
    }

    public void togglePanel(String name) {
        if (panelOpen) {
            hideCraftingPanel();
            hideInventoryPanel();
            panelOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else {
            GameObject panel = transform.Find(name + "Panel").gameObject;
            updateInventory();
            panel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            panelOpen = true;
        }
        
    }
    public void retryPressed() {
        // Restart the game.      
        SceneManager.LoadScene("NewScene", LoadSceneMode.Single);
>>>>>>> parent of be725d1... .

    }
}
