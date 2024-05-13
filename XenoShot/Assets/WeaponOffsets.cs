using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOffsets : MonoBehaviour
{
    public enum EditableWeaponType
    {
        Smaw,
        PlasmaSniper,
        PlasmaCannon,
        Benelli,
        Mp5
    }

    // Assuming the existing enum 'WeaponType' is accessible here
    // public enum WeaponType { Smaw, PlasmaSniper, PlasmaCannon, Benelli, Mp5 }

    [Header("Configuration")]
    public GameObject offset;

    [Header("Runtime Editing")]
    public EditableWeaponType editableWeaponType;  // Set this in the inspector or at runtime for editing

    [Header("Offsets")]
    public Vector3[] smawOffset = new Vector3[3];
    public Vector3[] plasmaSniperOffset = new Vector3[3];
    public Vector3[] plasmaCannonOffset = new Vector3[3];
    public Vector3[] benelliOffset = new Vector3[3];
    public Vector3[] mp5Offset = new Vector3[3];

    void Start()
    {
        // Optionally call SetOffset here if you want to set an offset at start
    }

    public void SetOffset(WeaponType weapon)
    {
        Vector3[] offsetValues = null;

        switch (weapon)
        {
            case WeaponType.SMAW:
                offsetValues = smawOffset;
                break;
            case WeaponType.PlasmaRifle:
                offsetValues = plasmaSniperOffset;
                break;
            case WeaponType.PlasmaCannon:
                offsetValues = plasmaCannonOffset;
                break;
            case WeaponType.Benelli:
                offsetValues = benelliOffset;
                break;
            case WeaponType.MP5:
                offsetValues = mp5Offset;
                break;
            default:
                Debug.LogError("Unknown weapon type: " + weapon);
                offsetValues = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.one };
                break;
        }

        if (offsetValues != null)
        {
            offset.transform.localPosition = offsetValues[0];
            offset.transform.localEulerAngles = offsetValues[1];
            offset.transform.localScale = offsetValues[2];
        }
    }

    // Runtime editor functionality
    public void LoadOffsets(EditableWeaponType weapon)
    {
        // Load the current offset's transform into the selected weapon's offset array
        switch (weapon)
        {
            case EditableWeaponType.Smaw:
                LoadOffset(ref smawOffset);
                break;
            case EditableWeaponType.PlasmaSniper:
                LoadOffset(ref plasmaSniperOffset);
                break;
            case EditableWeaponType.PlasmaCannon:
                LoadOffset(ref plasmaCannonOffset);
                break;
            case EditableWeaponType.Benelli:
                LoadOffset(ref benelliOffset);
                break;
            case EditableWeaponType.Mp5:
                LoadOffset(ref mp5Offset);
                break;
            default:
                Debug.LogError("Unknown editable weapon type: " + weapon);
                break;
        }
    }

    private void LoadOffset(ref Vector3[] offsetArray)
    {
        offsetArray[0] = offset.transform.localPosition;
        offsetArray[1] = offset.transform.localEulerAngles;
        offsetArray[2] = offset.transform.localScale;
    }

    void Update()
    {
        // Optionally, continually update offsets in real time for editing purposes
        /*
        if (Input.GetKeyDown(KeyCode.U))  // Press 'U' to update offsets for the currently selected editable weapon
        {
            LoadOffsets(editableWeaponType);
            Debug.Log("Offsets updated for " + editableWeaponType);
        }*/
    }
}
