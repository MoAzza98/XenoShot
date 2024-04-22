using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingDamageNumber : MonoBehaviour
{
    //[SerializeField] private Transform pfDamagePopup;

    // Start is called before the first frame update
    void Start()
    {
        //DamagePopup.Create(Vector3.zero, 300);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            DamagePopup.Create(Vector3.zero, 300, true);
        }
    }
}
