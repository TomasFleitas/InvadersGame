using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                Debug.Log("Le pegue al player");
                other.gameObject.GetComponent<PlayerController>().TakeDamage();
                Destroy(this.gameObject);
                break;
            case "Enemy":
                other.gameObject.GetComponent<EnemyController>().Die();
                Destroy(this.gameObject);
                break;
            case "Bullet":
                Destroy(other.gameObject);
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
}
