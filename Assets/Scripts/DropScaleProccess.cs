using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScaleProccess : MonoBehaviour
{
    public bool isOnObstacle=false;
    float radius;
    float growthRate;
    [SerializeField] float maxDropSize=1.5f;
    [SerializeField] float minDropSize = 0.5f;
    [SerializeField] float speedDiveder = 2f;
    void Start()
    {
        radius = transform.localScale.x;
        growthRate = Time.deltaTime/speedDiveder;
    }

    // Update is called once per frame
    void Update()
    {
        radius = transform.localScale.x;
        if (isOnObstacle)
        {
            if (radius<maxDropSize)
            {
                transform.localScale = new Vector3(transform.localScale.x+growthRate,transform.localScale.y,transform.localScale.z+growthRate);
            }
        }
        else
        {
            if (radius > minDropSize)
            {
                transform.localScale = new Vector3(transform.localScale.x - growthRate, transform.localScale.y, transform.localScale.z - growthRate);
            }
        }
    }
}
