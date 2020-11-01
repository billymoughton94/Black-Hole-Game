using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_Controller : MonoBehaviour {
    
    // Dictionary containing the parts and the quantity obtained
    private Dictionary<string, int> parts;
    
    void Start() {
        // Initialise the parts dictionary and add the initial values
        parts = new Dictionary<string, int>();
        parts.Add("Antenna", 0);
        parts.Add("Ship Body", 0);
        parts.Add("Fuel Containers", 0);
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

    public void retryPressed() { 
        // Restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    
    
    
}
