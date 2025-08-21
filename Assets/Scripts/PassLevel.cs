using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassLevel : MonoBehaviour
{
    public string level;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            GameController.instance.PassLevel(level);
        }
    }
}
