using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctreeComponent : MonoBehaviour
{
    public static OctreeComponent Instance;

    void Awake()
    {
        Instance = this;
    }

    public float size = 5;
    public int depth = 2;
    public Octree octree = null;
    public int hitcount = 0;

    void Start()
    {
        transform.position = TargetBounds.Instance.GetRandomPosition();
    }

    void OnDrawGizmos()
    {
        Debug.Log("Drawing Gizmos!");
        octree = new Octree(this.transform.position, size, depth);
        DrawNode(octree.GetRoot());
    }

    private void DrawNode(Octree.OctreeNode node)
    {
        Gizmos.color = Color.green;
        if (node.isLeaf)
        {
            Gizmos.DrawCube(node.Position, Vector3.one * node.Size);
        }
        else
        {
            foreach (var subnode in node.Nodes)
            {
                DrawNode(subnode);
            }
        }
    }

    public void ObjectHit()
    {
        ++hitcount;
        transform.position = TargetBounds.Instance.GetRandomPosition();
    }
}