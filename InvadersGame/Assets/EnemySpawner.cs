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
		
        
	}

    
    
}
