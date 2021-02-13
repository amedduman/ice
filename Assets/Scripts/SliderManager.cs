using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [SerializeField] Image bar;
    [SerializeField] Transform ice;
    float icePos;
    float endPos;
    float currentDistance;
    float initialDistance;
    float barScale;
    
    void Start()
    {
        icePos = ice.position.x;
        endPos = gameObject.transform.position.x;
        initialDistance = endPos - icePos;
        
    }

    void Update()
    {
        icePos = ice.position.x;
        currentDistance = endPos - icePos;
        barScale = 1-(currentDistance/initialDistance);
        bar.rectTransform.localScale = new Vector3(barScale,1,1);
        
    }
}
