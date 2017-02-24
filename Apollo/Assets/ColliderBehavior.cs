using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class ColliderBehavior : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            ((EnemyController)collision.gameObject.GetComponent(typeof(EnemyController))).kill();
            //Destroy(collision.gameObject);
        }
    }
}
