using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthbar : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 offset;
    public Image backgroundImage;
    public Image foregroundImage;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 direction = (targetTransform.position - Camera.main.transform.position).normalized;
        bool isBehind = Vector3.Dot(direction, Camera.main.transform.forward) <= 0.0f;
        foregroundImage.enabled = !isBehind;
        backgroundImage.enabled = !isBehind;

        transform.position = Camera.main.WorldToScreenPoint(targetTransform.position + offset);

    }

    public void SetHealthBarPercentage(float percentage)
    {
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float width = parentWidth * percentage;
        foregroundImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    public void ShowDamage()
    {

    }

}
