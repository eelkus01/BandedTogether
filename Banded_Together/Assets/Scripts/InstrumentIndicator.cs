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
    public GameObject borderOn;
    public int instrumentID;
    public GameHandler gameHandler;
    public bool hasParts;

    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        childImage = GetComponentInChildren<Image>();
        originalColor = childImage.color;
        //make first instrument selected from start
        if (instrumentID == 1) {
            borderOn.SetActive(true);
        } else {
            borderOn.SetActive(false);
        }
        hasParts = gameHandler.hasAllParts;
    }

    void Update()
    {
        //keep checkig if player has all the parts
        hasParts = gameHandler.hasAllParts;
    }

    public void SetSelectedState(bool selected) {
        isSelected = selected;
        UpdateAppearanceOnSelectAction();
    }

    private void UpdateAppearanceOnSelectAction(){
        if(isSelected) {
            borderOn.SetActive(true);
        }
        else{
            borderOn.SetActive(false);
        }
    }
}
