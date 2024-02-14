using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public GameBehaviour gameManager;

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
        }
    }
}
