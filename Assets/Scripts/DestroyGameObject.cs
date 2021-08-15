using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    public float timeToDestroy = 1;

    private void Start()
    {
        Invoke("DestroyThisObject", timeToDestroy);
    }

    private void DestroyThisObject()
    {
        Destroy(this.gameObject);
    }
}
