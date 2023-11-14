using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class PulseToBeat : MonoBehaviour {
 
    public float BPM = 20f;
    public Color StartColor;
    public Color EndColor;
    //private Color rendColor;
 
    // Use this for initialization
    void Start () {
        //rendColor = GetComponent<SpriteRenderer>().color;
    }
    
    // Update is called once per frame
    void Update () {
        var baseValue = Mathf.Cos(((Time.time * Mathf.PI) * (BPM / 60f)) % Mathf.PI);
        var target = Color.Lerp(EndColor, StartColor, baseValue);
        GetComponent<Image>().color = target;
    }
}
