using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] Transform ice;
    void Start()
    {
        
    }

    void Update()
    {
        gameObject.transform.position = new Vector3(ice.position.x, 0.5f, transform.position.z);
    }
}
