using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AiSensor : MonoBehaviour
{
    public bool debug;
    public bool debugTargets;
    public float distance = 10;
    public float angle = 30;
    public float height = 1.0f;
    public Color meshColor = Color.red;
    public int scanFrequency = 30;
    public LayerMask layers;
    public LayerMask occlusionLayers;
    public List<GameObject> Objects {
        get {
            objects.RemoveAll(obj => !obj);
            return objects;
        }
    }
    private List<GameObject> objects = new List<GameObject>();

    Collider[] colliders = new Collider[50];
    Mesh mesh;
    int count;
    float scanInterval;
    float scanTimer;
    float distanceSq;

    // Start is called before the first frame update
    void Start()
    {
        scanInterval = 1.0f / scanFrequency;
        distanceSq = distance * distance;
    }

    // Update is called once per frame
    void Update()
    {
        scanTimer -= Time.deltaTime;
        if (scanTimer < 0) {
            scanTimer += scanInterval;
            Scan();
        }
    }

    private void Scan() {
        count = Physics.OverlapSphereNonAlloc(transform.position, distance, colliders, layers, QueryTriggerInteraction.Collide);

        objects.Clear();
        for (int i = 0; i <count; ++i) {
            GameObject obj = colliders[i].gameObject;
            if (obj == gameObject) {
                continue;
            }

            if (IsInSight(obj)) {
                objects.Add(obj);
            }
        }
    }

    public bool IsInSight(GameObject obj) {

        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;
        if (direction.y > height || direction.y < -0.01f) {
            return false;
        }

        direction.y = 0;
        if (direction.sqrMagnitude > distanceSq) {
            return false;
        }

        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if (deltaAngle > angle) {
            return false;
        }

        origin.y += height / 2;
        dest.y = origin.y;
        if (Physics.Linecast(origin, dest, occlusionLayers)) {
            return false;
        }

        return true;
    }

    Mesh CreateWedgeMesh() {
        Mesh mesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;

        int vert = 0;

        // left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        // right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;
        for(int i = 0; i < segments; ++i) {
            
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;

            topRight = bottomRight + Vector3.up * height;
            topLeft = bottomLeft + Vector3.up * height;

            // far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            // top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            // bottom
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;
        }

        for(int i = 0; i < numVertices; ++i) {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    private void OnValidate() {
        mesh = CreateWedgeMesh();
        scanInterval = 1.0f / scanFrequency;
        distanceSq = distance * distance;
    }

    private void OnDrawGizmos() {
        if (!debug) {
            return;
        }

        if (mesh) {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * transform.rotation * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * transform.rotation * Vector3.forward * distance;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;

        Vector3 p = transform.position + Vector3.up * 0.001f;


        Color lineColor = meshColor * 0.3f;
        lineColor.a = 0.8f;

        Gizmos.color = lineColor;
        Gizmos.DrawLine(p + bottomCenter, p + bottomLeft);
        Gizmos.DrawLine(p + bottomLeft, p + topLeft);
        Gizmos.DrawLine(p + topLeft, p + topCenter);
        Gizmos.DrawLine(p + topCenter, p + bottomCenter);
        
        Gizmos.DrawLine(p + bottomCenter, p + bottomRight);
        Gizmos.DrawLine(p + bottomRight, p + topRight);
        Gizmos.DrawLine(p + topRight, p + topCenter);

        int segments = 10;
        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;
        for(int i = 0; i < segments; ++i) {

            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * transform.rotation * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * transform.rotation * Vector3.forward * distance;

            topRight = bottomRight + Vector3.up * height;
            topLeft = bottomLeft + Vector3.up * height;

            Gizmos.DrawLine(p + topLeft, p + topRight);
            Gizmos.DrawLine(p + bottomLeft, p + bottomRight);

            currentAngle += deltaAngle;
        }

        Gizmos.color = Color.green;
        if (debugTargets) {
            foreach (var obj in Objects) {
                Gizmos.DrawSphere(obj.transform.position, 0.2f);
            }
        }
        

        bottomCenter = Vector3.zero;
        bottomLeft = Quaternion.Euler(0, -angle, 0) * transform.rotation * Vector3.forward * distance;
        bottomRight = Quaternion.Euler(0, angle, 0) * transform.rotation * Vector3.forward * distance;

        topCenter = bottomCenter + Vector3.up * height;
        topRight = bottomRight + Vector3.up * height;
        topLeft = bottomLeft + Vector3.up * height;
    }

    public int Filter(GameObject[] buffer, string layerName, string tagName = null) {
        int layer = LayerMask.NameToLayer(layerName);
        int count = 0;
        foreach (var obj in Objects) {
            if (tagName != null && !obj.CompareTag(tagName)) {
                continue;
            }

            if (obj.layer == layer) {
                buffer[count++] = obj;
            }

            if (buffer.Length == count) {
                break; // buffer is full
            }
        }

        return count;
    }
}
