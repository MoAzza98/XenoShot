using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPickup : MonoBehaviour
{
    public RaycastWeapon weaponFab;
    public DisplayWeaponPickup pickup;

    private string weaponName;
    private int rarityIndex;
    private GameObject displayModel;

    public Material[] rarityMaterials;
    public RaycastWeapon[] weaponList;
    public GameObject[] weaponModels;

    [SerializeField] private MeshRenderer mRenderer;


    public void Start()
    {
        //we determine the rarity of the drop, and set rarityindex to the corresponding number
        SetRarity();


        //based on the rarityindex, we select the correct meterial from the array we've filled out in the inspector.
        //the materials in the inspector should be placed in order of rarity
        mRenderer.material = rarityMaterials[rarityIndex-1];

        int weaponSelectIndex = Random.Range(0, weaponList.Length);

        weaponFab = weaponList[weaponSelectIndex];

        displayModel = Instantiate(weaponModels[weaponSelectIndex], gameObject.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        pickup.DisplayPickupText(rarityIndex, displayModel.name);

        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();

        if (activeWeapon)
        {
            RaycastWeapon weapon = Instantiate(weaponFab, activeWeapon.weaponParent);
            activeWeapon.EquipWeapon(weapon);
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
