using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackScript : MonoBehaviour
{
    public GameBehaviour gameManager;
    public int amount = 25;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameBehaviour>();
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        if (other.gameObject.name == "PlayerCol")
        {
            Destroy(this.transform.parent.gameObject);
            Debug.Log("Item Collected");

            gameManager.Items += 1;

            gameManager._playerHP += amount;
            if (gameManager._playerHP > gameManager.maxHealth){
                gameManager._playerHP = gameManager.maxHealth;
            }
        }
    }
}
