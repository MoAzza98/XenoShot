using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entryspawn : MonoBehaviour
{
    public GameObject enemys;



    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enemys.SetActive(true);
        }
    }
}
