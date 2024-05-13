using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public RaycastWeapon weaponFab;
    private DisplayWeaponPickup pickup;

    private string weaponName;
    private int rarityIndex;
    private GameObject displayModel;

    public Material[] rarityMaterials;
    public RaycastWeapon[] commonWeaponList;
    public RaycastWeapon[] rareWeaponList;
    public RaycastWeapon[] epicWeaponList;
    public RaycastWeapon[] legendaryWeaponList;
    public GameObject[] weaponModels;

    [SerializeField] private MeshRenderer mRenderer;


    public void Start()
    {
        //we determine the rarity of the drop, and set rarityindex to the corresponding number
        SetRarity();
        pickup = GameObject.FindAnyObjectByType<DisplayWeaponPickup>();

        //based on the rarityindex, we select the correct meterial from the array we've filled out in the inspector.
        //the materials in the inspector should be placed in order of rarity
        try
        {
            mRenderer.material = rarityMaterials[rarityIndex-1];
        }
        catch(System.Exception e)
        {
            Debug.Log(e);
        }

        int weaponSelectIndex = Random.Range(0, commonWeaponList.Length);

        switch (rarityIndex)
        {
            case 1:
                weaponFab = commonWeaponList[weaponSelectIndex];
                break;
            case 2:
                weaponFab = rareWeaponList[weaponSelectIndex];
                break;
            case 3:
                weaponFab = epicWeaponList[weaponSelectIndex];
                break;
            case 4:
                weaponFab = legendaryWeaponList[weaponSelectIndex];
                break;
        }

        displayModel = Instantiate(weaponModels[weaponSelectIndex], gameObject.transform);
    }

    private void OnTriggerEnter(Collider other)
    {

        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if(activeWeapon != null)
        {
            pickup.DisplayPickupText(rarityIndex, displayModel.name);
        }

        if (activeWeapon)
        {
            RaycastWeapon weapon = Instantiate(weaponFab, activeWeapon.weaponParent);
            activeWeapon.EquipWeapon(weapon);
        }

        AiWeapon aiWeapon = other.gameObject.GetComponent<AiWeapon>();
        if (aiWeapon)
        {
            WeaponOffsets offsetVal = other.gameObject.GetComponentInChildren<WeaponOffsets>();
            RaycastWeapon newWeapon = Instantiate(weaponFab);
            offsetVal.SetOffset(newWeapon.weaponType);
            aiWeapon.Equip(newWeapon);
        }

    }

    private void SetRarity()
    {
        int raritySelector = Random.Range(0, 100);

        if(raritySelector <= 50)
        {
            rarityIndex = 1;
        } 
        else if(raritySelector > 50 && raritySelector < 70)
        {
            rarityIndex = 2;
        } 
        else if(raritySelector >= 70 && raritySelector < 90)
        {
            rarityIndex = 3;
        }
        else if(raritySelector >= 90)
        {
            rarityIndex = 4;
        }

    }
}
