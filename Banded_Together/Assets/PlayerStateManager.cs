using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public int maxHealth;
    public int currHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getDamaged(int damage){
        currHealth = currHealth - damage;
        if(currHealth < 0) {
            Debug.Log("Player just died.");
        }
    }

}
