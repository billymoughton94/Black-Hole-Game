using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_Controller : MonoBehaviour, IPointerEnterHandler ,IPointerExitHandler {

    private string name;
    private int amount;
    private bool useable;

    [SerializeField]
    private TextMeshProUGUI UIText;
    [SerializeField]
    private GameObject UIButton;
    [SerializeField]
    private GameObject desciptionPrefab;

    private GameObject descriptionPanel;

    public void setValues(string name, int amount, bool useable) {
        this.name = name;
        this.amount = amount;
        this.useable = useable;
        updateItem();
    }

    private void updateItem() {
        UIText.text = name + ": " + amount;
        UIButton.SetActive(useable);
    }
    
    public void OnPointerEnter(PointerEventData data) {
        descriptionPanel = Instantiate(desciptionPrefab, Input.mousePosition + new Vector3(125f, 50f, 0), Quaternion.identity, transform);
        descriptionPanel.GetComponent<Description_Controller>().setText(name);
    }

    public void OnPointerExit(PointerEventData data) {
        Destroy(descriptionPanel);	
    }


    public void useItem() {
        GameObject player = GameObject.Find("Player");
        Survival_Controller survival =player.GetComponent<Survival_Controller>();
        Inventory_Controller inventory = player.GetComponent<Inventory_Controller>();
        switch (name) {
            case "Monster Meat":
                int hungerValue = 20;
                survival.restoreHunger(hungerValue);
                inventory.removeItem(new Item("Monster Meat"));
                break;
            case "Health Pack":
                int healthValue = 10;
                survival.changeHealth(healthValue);
                inventory.removeItem(new Item("Health Pack"));
                break;
            default:
                Debug.Log("Item not recognised");
                break;
        }
    }





}
