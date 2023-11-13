using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler:MonoBehaviour
{
    public List<InstrumentIndicator> instrumentIndicators;
    private int activeInstrumentID;
    void Start()
    {
        GameObject[] instrumentIndicatorObjects = GameObject.FindGameObjectsWithTag("InstrumentIndicator");
        instrumentIndicators = new List<InstrumentIndicator>();

        foreach (var obj in instrumentIndicatorObjects)
        {
            InstrumentIndicator indicator = obj.GetComponent<InstrumentIndicator>();
            if (indicator != null)
            {
                instrumentIndicators.Add(indicator);
            }
        }
    }
    
    void Update() {
        CheckForKeyPress();
    }

    private void CheckForKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateSelectedInstrument(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpdateSelectedInstrument(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpdateSelectedInstrument(3);
        }
    }

    private void UpdateSelectedInstrument(int selectedInstrumentID)
    {
        activeInstrumentID = selectedInstrumentID;
        foreach (var indicator in instrumentIndicators)
        {
            indicator.SetSelectedState(indicator.instrumentID == selectedInstrumentID);
        }
    }

}
