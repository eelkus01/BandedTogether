using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class PulseToBeat : MonoBehaviour {
 
    public float BPM = 100f;
    public Color StartColor;
    public Color EndColor;
    private Material mat;
 
    // Use this for initialization
    void Start () {
        mat = GetComponent<Renderer>().material;
    }
    
    // Update is called once per frame
    void Update () {
        var baseValue = Mathf.Cos(((Time.time * Mathf.PI) * (BPM / 60f)) % Mathf.PI);
        var target = Color.Lerp(EndColor, StartColor, baseValue);
        mat.SetColor("_EmissionColor", target);
    }
}
