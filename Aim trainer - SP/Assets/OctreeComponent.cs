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

    public float size = 8;
    public int depth = 2;
    public Octree octree = null;
    public static int hitcount = 0;
    public int timesInitiated = 0;

    void Start()
    {
        transform.position = TargetBounds.Instance.GetRandomPosition();
        octree = new Octree(transform.position, size, depth);
    }

    void OnDrawGizmos()
    {
        if(octree == null)
        {
            octree = new Octree(this.transform.position, size, depth);
            timesInitiated++;
        }
        DrawNode(octree.node);
    }

    private void DrawNode(Octree.OctreeNode node)
    {
        Gizmos.color = Color.green;
        if (node.isLeaf)
        {
            Gizmos.DrawWireCube(node.position, Vector3.one * node.size);
        }
        else
        {
            foreach (var subnode in node.subNodes)
            {
                DrawNode(subnode);
            }
        }
    }

    public void ObjectHit()
    {
        ++hitcount;
        octree.DestroyColliders();
        transform.position = TargetBounds.Instance.GetRandomPosition();
        octree.UpdatePosition(transform.position);
    }
}