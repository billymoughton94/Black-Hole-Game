using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.Build.Content;

public class UI_Controller : MonoBehaviour {
    
    // Dictionary containing the parts and the quantity obtained
    private Dictionary<string, int> parts;

    private GameObject activePanel;
    [SerializeField]
    private GameObject itemPrefab;
    
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

    public void togglePanel(String name) {
        if (activePanel is null) {
            showPanel(name);
            toggleMouse();
        } else if (!String.Equals(activePanel.name, name + "Panel")) {
            hidePanel();
            showPanel(name);
        } else {
            hidePanel();
            toggleMouse();
        }
    }
    
    private void showPanel(String name) {
        activePanel = transform.Find(name + "Panel").gameObject;
        if (String.Equals(name, "Inventory")) {
            updateItemList();
        }
        activePanel.SetActive(true);
    }

    private void hidePanel() {
        activePanel.SetActive(false);
        activePanel = null;
    }

    private void toggleMouse() {
        if (Cursor.lockState == CursorLockMode.None) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void updateItemList() {
        Inventory_Controller inventory = GameObject.Find("Player").GetComponent<Inventory_Controller>();
        GameObject itemList = transform.Find("InventoryPanel/ItemList").gameObject;
        const float startY = 150f;
        const float spacing = -50f;
        foreach (Transform child in itemList.transform) {
            Destroy(child.gameObject);
        }

        int i = 0;
        foreach (KeyValuePair<String, Item> item in inventory.getItems()) {
            GameObject go = Instantiate(itemPrefab, transform.position + new Vector3(0, startY + (spacing * i), 0), Quaternion.identity, itemList.transform);
            go.GetComponent<Item_Controller>().setValues(item.Key, item.Value.getAmount(), item.Value.getUseable());
            i++;
        }
    }

    public void retryPressed() {
        // Restart the game.
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
