using System.Collections;
using System.Collections.Generic;
using Tarodev;
using UnityEngine;

public class HomingSpecials : MonoBehaviour
{
    public GameObject[] missiles;
    public Transform firePoint;
    public float fireCooldown = 1.5f;
    public int missileBunch = 5;

    private float currentTimer;
    private Target target;
    private EnemyLockOn lockOn;
    private Missile _missile;


    // Start is called before the first frame update
    void Start()
    {
        lockOn = GetComponent<EnemyLockOn>();
        currentTimer = fireCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            FireMissile();
        }
    }

    void FireMissile()
    {

        if (lockOn.currentTarget && lockOn.enemyLocked)
        {
            target = lockOn.currentTarget.root.GetComponent<Target>();
            _missile = missiles[0].GetComponent<Missile>();
            _missile._target = target;

            if(missileBunch > 1) {
                StartCoroutine(DelayInstantiate(0.1f));
            } else
            {
                GameObject AttackMissile = Instantiate(missiles[0], firePoint.position, Quaternion.identity);
                AttackMissile.transform.LookAt(target.transform);
            }

        }

    }

    public IEnumerator DelayInstantiate(float seconds)
    {
        for (int i = 0; i <= 5; i++)
        {
            GameObject AttackMissile = Instantiate(missiles[0], firePoint.position, Quaternion.identity);
            AttackMissile.transform.LookAt(target.transform);

            yield return new WaitForSeconds(seconds);
        }
    }

}
