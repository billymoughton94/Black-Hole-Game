using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Recipe_Controller : MonoBehaviour, IPointerEnterHandler ,IPointerExitHandler {
    
    private string name;
    private List<Item> requirements;

    [SerializeField]
    private TextMeshProUGUI UIName;
    [SerializeField]
    private TextMeshProUGUI UIRequirements;
    [SerializeField]
    private GameObject desciptionPrefab;

    private GameObject descriptionPanel;

    public void setValues(string name, List<Item> requirements) {
        this.name = name;
        this.requirements = requirements;
        updateRecipe();
    }

    private void updateRecipe() {
        UIName.text = name;
        foreach (Item item in requirements) {
            UIRequirements.text += item.getName() + " x" +item.getAmount() + "\n";
        }
    }
    
    public void OnPointerEnter(PointerEventData data) {
        descriptionPanel = Instantiate(desciptionPrefab, Input.mousePosition + new Vector3(125f, 50f, 0), Quaternion.identity, transform);
        descriptionPanel.GetComponent<Description_Controller>().setText(name);
    }

    public void OnPointerExit(PointerEventData data) {
        Destroy(descriptionPanel);	
    }
    
    public void craftItem() {
        GameObject player = GameObject.Find("Player");
        Crafting_Controller crafting = player.GetComponent<Crafting_Controller>();
        crafting.craftItem(name);
    }
    
    

}
