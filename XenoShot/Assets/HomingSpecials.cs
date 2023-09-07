using System.Collections;
using System.Collections.Generic;
using Tarodev;
using UnityEngine;

public class HomingSpecials : MonoBehaviour
{
    public GameObject[] missiles;
    private Target target;
    private EnemyLockOn lockOn;
    private Missile _missile;

    // Start is called before the first frame update
    void Start()
    {
        lockOn = GetComponent<EnemyLockOn>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            FireMissile();
        }
    }

    void FireMissile()
    {
        if(lockOn.currentTarget && lockOn.enemyLocked)
        {
            target = lockOn.currentTarget.root.GetComponent<Target>();
            _missile = missiles[0].GetComponent<Missile>();
            _missile._target = target;
            GameObject AttackMissile = Instantiate(missiles[0], new Vector3(transform.position.x,transform.position.y + 5, transform.position.z), Quaternion.identity);
            AttackMissile.transform.LookAt(target.transform);
        }
    }

}
