using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public static int hits = 0;

    public void hit()
    {
        ++hits;
        transform.position = TargetBounds.Instance.GetRandomPosition();
    }
}
