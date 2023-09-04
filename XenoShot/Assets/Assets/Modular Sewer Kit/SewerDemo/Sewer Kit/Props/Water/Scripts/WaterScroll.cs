using UnityEngine;

public class WaterScroll : MonoBehaviour
{
    [SerializeField] private Vector2 scrollVector = new Vector2(0.05f, 0.05f);
    Vector2 offset = new Vector2(0f, 0f);

    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        offset += scrollVector * Time.deltaTime;
        rend.material.SetTextureOffset("_MainTex", offset);
    }
}
