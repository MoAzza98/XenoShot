using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hitmarker : MonoBehaviour
{

    public float hitmarkerTimer;
    public float deathmarkerTimer;
    private Image hitmarkerImage;

    // Start is called before the first frame update
    void Start()
    {
        hitmarkerTimer = 0;
        hitmarkerImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deathmarkerTimer > 0)
        {
            hitmarkerImage.color = Color.red;
            hitmarkerImage.enabled = true;
            deathmarkerTimer = deathmarkerTimer - Time.deltaTime;
        }
        else
        {
            hitmarkerImage.enabled = false;
            hitmarkerImage.color = Color.white;
            deathmarkerTimer = 0;
        }

        if (hitmarkerTimer > 0)
        {
            hitmarkerImage.enabled = true;
            hitmarkerTimer = hitmarkerTimer - Time.deltaTime;
        }
        else
        {
            hitmarkerImage.enabled = false;
            hitmarkerTimer = 0;
        }
    }
}
