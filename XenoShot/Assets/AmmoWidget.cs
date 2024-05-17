using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoWidget : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    
    public void RefreshAmmo(int ammoCount)
    {
        ammoText.text = ammoCount.ToString();
    }
}
