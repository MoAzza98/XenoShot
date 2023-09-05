using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPopulator : MonoBehaviour
{
    public GameObject propsParent;
    public Vector2 size;
    public int maxItems;

    [SerializeField] private GameObject[] roomObjects;

    // Start is called before the first frame update
    void Start()
    {
        //FillRoom();
    }

    private void Awake()
    {
        FillRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillRoom()
    {
        int itemsInRoom = Random.Range(0, maxItems);

        for(int i = 0; i <= itemsInRoom; i++)
        {
            Vector3 randomSpawnPos = new Vector3(Random.Range(transform.position.x + size.x, transform.position.x + size.y + 1), 0, Random.Range(transform.position.z + size.x, transform.position.z + size.y + 1));
            Instantiate(roomObjects[Random.Range(0, roomObjects.Length)], randomSpawnPos, Quaternion.identity, propsParent.transform);
        }
    }
}
