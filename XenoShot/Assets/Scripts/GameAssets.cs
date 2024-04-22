using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }

    public Transform pfDamagePopup;
    public Transform pfDamageParent;
    public Camera cam;

    private void Awake()
    {
        pfDamageParent = GameObject.Find("Canvas").transform;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

}