using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

    private PlatformerController playerController;

    public GameController gameController;

    public float maxHealth = 1f;
    private float currentHealth;

    public float maxHeight = 1.5f;
    public float minHeight = 1f;

    void Start () {

        playerController = GetComponent<PlatformerController>();

        currentHealth = maxHealth;

	}

    void Update() {

        SetSize();

    }

    void SetSize() {

        Vector3 newSize = new Vector3(1f, minHeight + (maxHeight - minHeight) / (currentHealth/maxHealth), 1f);

        transform.localScale = newSize;

    }

    public void Die() {

        PlayDeathAnimation();

        Destroy(gameObject);

        gameController.PlayerHasDied();

    }

    void PlayDeathAnimation() {



    }

    void OnTriggerStay(Collider other) {

        if(other.transform.tag == "DeathTrigger") {
            
            if(playerController.isOnGround()) {

                currentHealth -= Time.deltaTime * 10;
                
                if (currentHealth <= 0)
                    Die();

            }

        }

    }

    void OnTriggerExit(Collider other) {

        if(other.transform.tag == "DeathTrigger") {

            currentHealth = maxHealth;

        }

    }

}
