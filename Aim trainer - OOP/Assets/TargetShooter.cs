using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooter : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Canvas EndScreen;

    void Update(){
        if(Timer.Instance.currentTime > 0)
        {
            if(Input.GetMouseButtonDown(0)){    //na lijevi klik misa
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));  //stvara se novi ray iz sredine ekrana (0.5, 0.5)
                if(Physics.Raycast(ray, out RaycastHit hit))
                {
                    Target target = hit.collider.gameObject.GetComponent<Target>();
                    if(target != null)
                    {
                        target.hit();
                    }
                }
            }
        }
        else
        {
            EndScreen.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}