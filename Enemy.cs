using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public float enemyHP;
    private float moveSpeed;
    private int i = 0;

    public List<Transform> movePoints;
    public GameObject variableStorage;


    private void Start()
    {
        //setup de move speed e HP
        if (gameObject.tag == "Rapido")
        {
            moveSpeed = 10f;
            enemyHP = 50 + 5 * variableStorage.GetComponent<VariableStorage>().round;
        }
        else if (gameObject.tag == "Normal")
        {
            moveSpeed = 5f;
            enemyHP = 100 + 15*variableStorage.GetComponent<VariableStorage>().round;
        }
        else if (gameObject.tag == "Tanque")
        {
            moveSpeed = 3f;
            enemyHP = 250 + 25 * variableStorage.GetComponent<VariableStorage>().round;
        }

        switch (variableStorage.GetComponent<VariableStorage>().qualSpawn)
        {
            case 0: //top
                movePoints.Add(GameObject.Find("T0").transform);
                movePoints.Add(GameObject.Find("T1").transform);
                movePoints.Add(GameObject.Find("T2").transform);
                break;
            case 1: //east
                movePoints.Add(GameObject.Find("E0").transform);
                movePoints.Add(GameObject.Find("E1").transform);
                movePoints.Add(GameObject.Find("E2").transform);
                break;
            case 2: //down
                movePoints.Add(GameObject.Find("D0").transform);
                movePoints.Add(GameObject.Find("D1").transform);
                movePoints.Add(GameObject.Find("D2").transform);
                break;
            case 3: //west
                movePoints.Add(GameObject.Find("W0").transform);
                movePoints.Add(GameObject.Find("W1").transform);
                movePoints.Add(GameObject.Find("W2").transform);
                break;

        }

    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        if (enemyHP <= 0)
        {
            Destroy(gameObject);
            //adicionar animacao de morte ou coisa assim
        }

        if (i < movePoints.Count)
        {
            //start position, end position, speed
            transform.position = Vector3.MoveTowards(transform.position, movePoints[i].transform.position, moveSpeed * Time.deltaTime);

            //verifica se as posicoes do ponto de referecia e do Enemy sao proximas
            if (Vector3.Distance(transform.position, movePoints[i].position) < 1f)
            {
                //troca o ponto de movimentacao
                i++;
            }
        }
    }
}
