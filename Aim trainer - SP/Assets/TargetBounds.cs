using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBounds : MonoBehaviour
{
    public static TargetBounds Instance;
    [SerializeField] BoxCollider col;

    void Awake()
    {
        Instance = this;
    }
    
    // find the bounds of TargetBounds object and generates a random point within
    public Vector3 GetRandomPosition()  
    {
        Vector3 center = col.center + transform.position;

        float minX = center.x - col.size.x * 0.5f;
        float maxX = center.x + col.size.x * 0.5f;

        float minY = center.y - col.size.y * 0.5f;
        float maxY = center.y + col.size.y * 0.5f;

        float minZ = center.z - col.size.z * 0.5f;
        float maxZ = center.z + col.size.z * 0.5f;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float randomZ = Random.Range(minZ, maxZ);

        return new Vector3(randomX, randomY, randomZ);
    }
}