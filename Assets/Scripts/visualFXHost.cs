using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visualFXHost : MonoBehaviour
{
    [SerializeField] Transform ice;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(ice.position.x,ice.position.y,ice.position.z);
    }
}
