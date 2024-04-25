using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class DamagePopup : MonoBehaviour
{
    private Transform parentTransform;
    private TextMeshProUGUI textMesh;

    private const float DISAPPEAR_TIMER_MAX = 1f;
    public float destroyTime;
    private Color textColor;
    private Vector3 moveVector;

    public static DamagePopup Create(Vector3 position, int damageAmount, bool isCriticalHit, bool isHeadShot)
    {
        GameObject damagePopTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity).gameObject;
        damagePopTransform.transform.SetParent(GameAssets.i.pfDamageParent, false);

        DamagePopup damagePop = damagePopTransform.GetComponent<DamagePopup>();
        damagePop.Setup(damageAmount, isCriticalHit, isHeadShot);
        return damagePop;
    }

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshProUGUI>();
    }

    public void Setup(int damageAmount, bool isCriticalHit, bool isHeadShot)
    {
        textMesh.SetText(damageAmount.ToString());
        if (isHeadShot)
        {
            textMesh.fontSize = 25;
            textColor = UtilsClass.GetColorFromString("FFFE00");
        }
        else if (!isCriticalHit)
        {
            textColor = UtilsClass.GetColorFromString("FFFCFC");
        }
        else
        {
            textMesh.fontSize = 36;
            textColor = UtilsClass.GetColorFromString("F63300");
        }
        textMesh.color = textColor;
        destroyTime = DISAPPEAR_TIMER_MAX;

        int x = Random.Range(10, 15);
        int y = Random.Range(-2, 3);

        int n = Random.Range(0, 2);

        if(n == 0)
        {
            x = -Mathf.Abs(x);
        }

        moveVector = new Vector3(x, y) * 30f;
    }

    void Start()
    {

    }

    void Update()
    {
        float moveYSpeed = 20f;
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if(destroyTime > DISAPPEAR_TIMER_MAX * 0.5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        destroyTime -= Time.deltaTime;
        if(destroyTime < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}