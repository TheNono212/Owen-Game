using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingNewItem : MonoBehaviour
{
    public float DestroyTime = 3f;

    public Vector3 offset = new Vector3(250f,-150f,0f);

    void Start()
    {
        Destroy(gameObject, DestroyTime);
        
        transform.localPosition = offset;
    }
}
