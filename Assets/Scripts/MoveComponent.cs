using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    public AnimationCurve curve;
    public float duration;
    public float delay;
    public LeanTweenType easeType;
    public bool isRotating;

    
    void OnEnable() 
    { 
        if(easeType == LeanTweenType.animationCurve)
        {
            LeanTween.moveY(gameObject, 13.5f, duration).setDelay(delay).setLoopPingPong().setEase(curve);
            if(isRotating == true)
                LeanTween.rotateAround(gameObject, Vector3.up, 360, 2.5f).setLoopClamp();
        }
        else
        {
            LeanTween.moveY(gameObject, 13.5f, duration).setDelay(delay).setLoopPingPong().setEase(easeType);
            if(isRotating == true)            
                LeanTween.rotateAround(gameObject, Vector3.up, 360, 2.5f).setLoopClamp();
        }
    }
}
