using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hits
{
    public static int hitcount = 0;
}

public class Octree
{
    private OctreeNode node;
    private int depth;
    

    public Octree(Vector3 position, float size, int depth)
    {
        node = new OctreeNode(position, size);
        node.Subdivide(depth);
        node.CreateCollider();
    }

    public class OctreeNode
    {
        Vector3 position;
        float size;
        OctreeNode[] subNodes;   //non-leaf nodes hold children
        bool isOccupied;                //leaf nodes can be occupied or unoccupied
        BoxCollider collider;

        public OctreeNode(Vector3 position, float size){
            this.position = position;
            this.size = size;
        }

        public IEnumerable<OctreeNode> Nodes
        {
            get { return subNodes; }
        }

        public Vector3 Position
        {
            get { return position; }
        }

        public float Size
        {
            get { return size;  }
        }

        public bool IsOccupied
        {
            get { return isOccupied; }
        }

        public bool isLeaf
        {
            get { return subNodes == null; }
        }

        public void Subdivide(int depth = 1)
        {
            subNodes = new OctreeNode[8];
            for(int i = 0; i < subNodes.Length; ++i)
            {
                Vector3 newPos = position;
                Vector3 newCollider;
                float newsize = size * 0.25f;
                
                if((i&4) == 4)
                { 
                    newPos.y += newsize;
                    newCollider.y = newsize;
                }
                else 
                {
                    newPos.y -= newsize;
                    newCollider.y = -newsize;
                }

                if((i&2) == 2) 
                {
                    newPos.x += newsize;
                    newCollider.x = newsize;
                }
                else
                {
                    newPos.x -= newsize;
                    newCollider.x = -newsize;
                }

                if((i&1) == 1) 
                {
                    newPos.z += newsize;
                    newCollider.z = newsize;
                }
                else
                {
                    newPos.z -= newsize;
                    newCollider.z = -newsize;
                }

                subNodes[i] = new OctreeNode(newPos, size * 0.5f);
                //subNodes[i].collider = new BoxCollider();
                //subNodes[i].collider.center = newCollider;
                //subNodes[i].collider.size = new Vector3(newsize, newsize, newsize);
                if (depth > 0)
                {
                    subNodes[i].Subdivide(depth - 1);
                }
            }
        }

        public void CreateCollider()
        {
            foreach(var subnode in this.Nodes)
            {
                if(subnode.isLeaf)
                {
                    subnode.collider = new BoxCollider();
                }
                else
                {
                    subnode.CreateCollider();
                }
            }
        }
        
        public void objectHit()
        {
            hits.hitcount += 1;
        }
    }

    private int GetIndexOfPosition(Vector3 lookupPosition, Vector3 nodePosition)
    {
        int index = 0;

        index |= lookupPosition.y > nodePosition.y ? 0b100 : 0; //check if node is above
        index |= lookupPosition.x > nodePosition.x ? 0b010 : 0; //check if node is left
        index |= lookupPosition.z > nodePosition.z ? 0b001 : 0; //check if node is front

        return index;
    }

    public OctreeNode GetRoot()
    {
        return node;
    }
}
