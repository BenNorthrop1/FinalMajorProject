using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  
  public class EmissionControl :MonoBehaviour
  {
    public Material thisMat;
    Color c;
    public float value; 

    void Start () {
        c = GetComponent<Renderer>().material.color;
        thisMat.EnableKeyword("_EMISSION");
        value = c.r;
    }
    
    
    void Update () {
        thisMat.SetColor("_EmissionColor", new Color(c.r,c.g,c.b));
    }
  }