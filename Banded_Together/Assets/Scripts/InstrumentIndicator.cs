using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstrumentIndicator : MonoBehaviour
{
    public bool isSelected;
    public Color originalColor;
    private Renderer rend;
    private Image childImage;
    public int instrumentID;
    // Start is called before the first frame update
    void Start()
    {
        childImage = GetComponentInChildren<Image>();
        originalColor = childImage.color;
    }

    public void SetSelectedState(bool selected) {
        Debug.Log("Instrument with ID "+instrumentID+" instrument ");
        isSelected = selected;
        UpdateAppearanceOnSelectAction();
    }

    private void UpdateAppearanceOnSelectAction(){
        if(isSelected) {
            childImage.color = Color.red;
        }
        else{
            childImage.color = originalColor;
        }
    }
}
