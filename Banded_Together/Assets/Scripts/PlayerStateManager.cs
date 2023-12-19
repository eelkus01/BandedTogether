using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public int maxHealth;
    public int currHealth;

    public GameObject healthBar;
    public GameObject gameHandler;
    public AudioSource hurtSFX;

    // Start is called before the first frame update
    void Start()
    {
       Debug.Log("starting");
       currHealth = maxHealth;
       healthBar =  GameObject.FindGameObjectWithTag("HealthBar");
       gameHandler = GameObject.Find("GameHandler");
       hurtSFX = GetComponent<AudioSource>();
    }

    public void getDamaged(int damage){
        currHealth = currHealth - damage;
        healthBar.GetComponent<HealthBar>().UpdateHealthBar(currHealth, maxHealth);
        if(currHealth <= 0) {
            gameHandler.GetComponent<GameHandler>().handleDeath();
        }
        hurtSFX.Play();
        GetComponent<PlayerAnimation>().triggerHurtAnimation();
    }

    public void getHealed(int healAmount) {
        currHealth = Mathf.Min(healAmount+currHealth, maxHealth);
        if (currHealth < maxHealth) {
            healthBar.GetComponent<HealthBar>().UpdateHealthBar(currHealth, maxHealth);
        }
    }

    public void RespawnPlayer(Transform respawnPoint){
        currHealth = maxHealth;
        healthBar.GetComponent<HealthBar>().UpdateHealthBar(currHealth, maxHealth);

        transform.position = respawnPoint.position;
    }

}
