using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class AmmoWidget : MonoBehaviour {
    public TMPro.TMP_Text ammoText;
    public TMPro.TMP_Text clipText;

    public void Refresh(int ammoCount, int clipCount) {
        ammoText.text = ammoCount.ToString();
        clipText.text = clipCount.ToString();
    }
}