using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooter : MonoBehaviour
{
    [SerializeField] Camera cam;

    void Update(){
        if(Input.GetMouseButtonDown(0)){    //na lijevi klik misa
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));  //stvara se novi ray iz sredine ekrana (0.5, 0.5)
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                OctreeComponent.Instance.ObjectHit();
            }
        }
    }
}