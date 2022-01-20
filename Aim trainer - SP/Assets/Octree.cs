using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hits
{
    public static int hitcount = 0;
}

public class Octree
{
    public OctreeNode node;
    private int depth = 1;
    private Vector3 previousPosition;
    

    public Octree(Vector3 position, float size, int depth)
    {
        node = new OctreeNode(position, size);
        previousPosition = position;
        node.Subdivide(depth);
        node.Occupy();
    }

    public void UpdatePosition(Vector3 newPosition)
    {  
        node.position = newPosition;
        foreach(var subnode in node.subNodes)
        {
            node.PropagatePosition();
            node.Occupy();
        }
    }

    public void DestroyColliders()
    {
        node.DestroyColliders();
    }

    public class OctreeNode
    {
        public Vector3 position;
        public float size;
        public OctreeNode[] subNodes;   //non-leaf nodes hold children
        public GameObject collider = null;  //leaf nodes can be occupied or unoccupied

        public OctreeNode(Vector3 position, float size){
            this.position = position;
            this.size = size;
        }

        public bool isLeaf
        {
            get { return subNodes == null; }
        }

        public bool hasCollider
        {
            get { return collider == null; }
        }

        public void Subdivide(int depth = 1)
        {
            subNodes = new OctreeNode[8];
            for(int i = 0; i < subNodes.Length; ++i)
            {
                Vector3 newPos = position;
                float newsize = size * 0.25f;
                

                //provjere je li subNode gornji/donji, lijevi/desni, prednji/zadnji preko bitflagova
                if((i&4) == 4)
                { 
                    newPos.y += newsize;
                }
                else 
                {
                    newPos.y -= newsize;
                }

                if((i&2) == 2) 
                {
                    newPos.x += newsize;
                }
                else
                {
                    newPos.x -= newsize;
                }

                if((i&1) == 1) 
                {
                    newPos.z += newsize;
                }
                else
                {
                    newPos.z -= newsize;
                }

                subNodes[i] = new OctreeNode(newPos, size * 0.5f);
                if (depth > 1)
                {
                    subNodes[i].Subdivide(depth - 1);
                }
            }
        }

        public void Occupy()
        {
            if(isLeaf)
            {
                //add collider to occupied nodes
                collider = new GameObject("New box collider");
                //collider = GameObject.CreatePrimitive(PrimitiveType.Cube);
                collider.transform.position = position;
                collider.transform.localScale = Vector3.one * size;
                collider.AddComponent<BoxCollider>();
            }
            else
            {
                foreach(var subnode in subNodes)
                {
                    subnode.Occupy();
                }
            }
        }

        public void DestroyColliders()
        {
            if(hasCollider)
            {
                UnityEngine.Object.Destroy(collider);
            }
            if(subNodes != null)
            {
                foreach(var subnode in subNodes)
                {
                    subnode.DestroyColliders();
                }
            }
        }

        public void PropagatePosition()
        {
            if(subNodes != null)
            {
                for(int i = 0; i < 8; i++)
                {       
                    Vector3 newPos = position;
                    float offset = size * 0.25f;
                    if((i&4) == 4)
                    { 
                        newPos.y += offset;
                    }
                    else 
                    {
                        newPos.y -= offset;
                    }

                    if((i&2) == 2) 
                    {
                        newPos.x += offset;
                    }
                    else
                    {
                        newPos.x -= offset;
                    }

                    if((i&1) == 1) 
                    {
                        newPos.z += offset;
                    }
                    else
                    {
                        newPos.z -= offset;
                    }

                    subNodes[i].position = newPos;
                    subNodes[i].PropagatePosition();
                }
            }
        }
    }
}
