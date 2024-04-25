using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public class DisplayWeaponPickup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rarityText;
    [SerializeField] private TextMeshProUGUI weaponText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //using a rarity index of 1-4, determine the rarity and set the text onscreen to that rarity, 
    public void DisplayPickupText(int rarityIndex, string weaponName)
    {
        switch (rarityIndex)
        {
            case 1:
                Debug.Log("Common");
                rarityText.SetText("Common");
                rarityText.color = Color.white;
                break;
            case 2:
                Debug.Log("Rare");
                rarityText.SetText("Rare");
                rarityText.color = UtilsClass.GetColorFromString("4EFFFA");
                break;
            case 3:
                rarityText.SetText("Epic");
                rarityText.color = UtilsClass.GetColorFromString("C800FF");
                break;
            case 4:
                rarityText.SetText("Legendary");
                rarityText.color = UtilsClass.GetColorFromString("FFC200");
                break;

        }

        weaponText.text = weaponName;
    }
}
