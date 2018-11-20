using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemyRowWidth; //Cantidad de enemigos a lo ancho (DEBE SER IMPAR)
    [SerializeField] private int enemyRowHeight; //Cantidad de enemigos a lo alto
    

    //[SerializeField] private int currentHorizontalOffset;
    //[SerializeField] private int currentVerticalOffset;

    private EnemyController[][] enemies;
    /*
     * Armo un array de array de EnemyController y no de GameObjects porque
     * me interesa tener los scripts de cada objeto ya que ellos son los que 
     * me permiten controlar a cada nave alienigena.
     */

    [SerializeField] private float ticksInSeconds;
    private float timeForNextTick;
    [SerializeField] private int minZValue;
    [SerializeField] private int maxHorizontalOffset;
    [Header("Probabilities")]
    public float doingSomethingProb; //Si va a aprovechar el tick para hacer algo
    public float movingProb; //Si no se mueve entonces dispara
    public float movingHorizontalProb; //Si no se mueve horizontal se mueve hacia abajo
    public float columnShootProb;

    // Use this for initialization
    void Start () {
        enemies = new EnemyController[enemyRowWidth][];

        for (int i = 0; i < enemyRowWidth; i++)
        {
            enemies[i] = new EnemyController[enemyRowHeight];
            float rowOffset = (1 - enemyRowWidth) / 2f + i;
            /*
             * Desplazamiento para que la columna central de enmigos quede centrada con la del spawner
             * ejemplo:
             * Si tenemos que enemyRowWidth = 5 el offset de la tercera columna (indice 2) en el eje X será:
             *  rowOffset = (1 - 5) / 2  + 2 = 0
             */


            for (int j = 0; j< enemyRowHeight; j++)
            {
                GameObject alien = Instantiate(enemyPrefab, this.transform);
                alien.transform.position = new Vector3(this.transform.position.x + rowOffset, this.transform.position.y, this.transform.position.z + j);
                enemies[i][j] = alien.GetComponent<EnemyController>();
            }
        }
        /*
         * El código de arriba inicializa el array de arrays de manera que los aliens
         * de una misma columna sean más faciles de recorrer.
         */

    }

    // Update is called once per frame
    void Update () {
        if (timeForNextTick < 0)
        {
            timeForNextTick = ticksInSeconds;
            if(UnityEngine.Random.Range(0.0f, 1.0f) < doingSomethingProb)
            {
                if (UnityEngine.Random.Range(0.0f, 1.0f) < movingProb)
                {
                    //Va a tratar de moverse
                    if (UnityEngine.Random.Range(0.0f, 1.0f) < movingHorizontalProb)
                    {
                        //Va a tratar de moverse horizontalmente
                        if (UnityEngine.Random.Range(0.0f, 1.0f) < 0.5f)
                        {
                            //Va a tratar de moverse horizontalmente a la izquierda
                            TryMovingLeft();
                        }
                        else
                        {
                            //Va a tratar de moverse horizontalmente a la derecha
                            TryMovingRight();
                        }
                    }
                    else
                    {
                        //Va a tratar de moverse hacia abajo
                        TryMovingDown();
                    }
                }
                else
                {
                    //Va a tratar de disparar
                    TryShooting();
                }
            }
        }
        else
        {
            timeForNextTick -= Time.deltaTime;
        }

    }

    private void TryShooting()
    {
        //Debug.Log("Todavía no estoy implementado pero imaginate que estoy DISPARANDO");
        for(int i = 0; i < enemyRowWidth; i++)
        {
            var columna = enemies[i];

            if (UnityEngine.Random.Range(0.0f, 1.0f) < columnShootProb)
            {
                for(int j = 0; j<enemyRowHeight; j++)
                {
                    if (columna[j].IsAlive())
                    {
                        columna[j].Shoot();
                        break;
                    }
                }
            }
        }

        
    }

    private void TryMovingDown()
    {
        //Debug.Log("Todavía no estoy implementado pero imaginate que estoy MOVIENDOME HACIA ABAJO"); 
        if(this.transform.position.z > minZValue)
        {
            this.transform.position += Vector3.back;
        }
    }

    private void TryMovingRight()
    {
        Debug.Log("Todavía no estoy implementado pero imaginate que estoy MOVIENDOME HACIA LA DERECHA");
        if(this.transform.position.x + (enemyRowWidth-1) /2f < maxHorizontalOffset)
        {
            this.transform.position += Vector3.right;
        }
    }

    private void TryMovingLeft()
    {
        Debug.Log("Todavía no estoy implementado pero imaginate que estoy MOVIENDOME HACIA LA IZQUIERDA");
        if (this.transform.position.x - (enemyRowWidth - 1) / 2f > maxHorizontalOffset * (-1))
        {
            this.transform.position += Vector3.left;
        }
    }
}
