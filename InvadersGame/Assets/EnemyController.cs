using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField] private float shootDelayInSeconds;
    private float timeForNextShotAvaiable;
    [SerializeField] private bool shootOrderReceived;
    /*
     * En el space invaders solo puede disparar la primer nave de cada columna, 
     * como desde este script no sabemos si esta nave enemiga puede disparar vamos a 
     * use shootOrderReceived para que alguien nos avise si deberíamos disparar.
     * Además, de esta manera podemos probar nuestro código tildando shootOrderReceived
     * en TRUE desde el editor durante play.
     */
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject bulletPrefab;
    private MeshFilter myMeshFilter;
    private bool dead;


    // Use this for initialization
    void Start () {
        timeForNextShotAvaiable = 0f;
        shootOrderReceived = false;
        myMeshFilter = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update () {
		if(timeForNextShotAvaiable < 0 && shootOrderReceived)
        {
            shootOrderReceived = false;
            /*
            * Al disparar tenemos que "consumir" la orden de disparo.
            */
            timeForNextShotAvaiable = shootDelayInSeconds;

            Debug.Log("ALIEN PEW!");

            var bullet = Instantiate(bulletPrefab, this.transform.position + Vector3.back * 1, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = Vector3.back * bulletSpeed;
            /*
            * Estas ultimas dos lineas son iguales a como hicimos para disparar en PlayerController 
            * solo que usamos Vector3.back en lugar de Vector3.forward para que la bala se dirija 
            * hacia el player.
            */


        }
        else
        {
            timeForNextShotAvaiable -= Time.deltaTime;
        }
	}

    
    public void Shoot()
    {
        shootOrderReceived = true;
        
    }

    public void ChangeMesh(Mesh newMesh)
    {
        myMeshFilter.mesh = newMesh;
    }

    public bool IsAlive()
    {
        return !dead;
    }

    public void Die()
    {
        this.gameObject.SetActive(false);
        dead = true;
    }
}
