using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    public GameObject inimigoNormal;
    public GameObject inimigoRapido;
    public GameObject inimigoTanque;
    public GameObject inimigoPrimeiro;

    public GameObject variableStorage;
    public GameObject VITORIA;  //placeholder para tela de vitoria

    void Start()
    {
        StartCoroutine(SpawnEnemies());     //inicia a subrotina de spawn
    }

    public IEnumerator SpawnEnemies()
    {
        while (true) //tava assim na internet, nao sei se precisa disso
        {
            if (variableStorage.GetComponent<VariableStorage>().round == 11 && variableStorage.GetComponent<VariableStorage>().inimigosRemanescentes == 0)    //verifica se acabou
            {
                variableStorage.GetComponent<VariableStorage>().round++;    //quebra o numero de rounds
                Instantiate(VITORIA, spawnPoints[1].position, transform.rotation);  //substituir para tela de vitoria
                //enable a tela de ganhar
                variableStorage.GetComponent<VariableStorage>().inimigosRemanescentes = -1; //quebra o numero de inimigos
            }

            if (variableStorage.GetComponent<VariableStorage>().inimigosRemanescentes == 0)     //se nao tiver mais inimigos na tela, inicia um round novo
            {
                variableStorage.GetComponent<VariableStorage>().round++;
                Debug.Log(" ");
                Debug.Log(variableStorage.GetComponent<VariableStorage>().round);
                int qualSpawn = Random.Range(0, 4); //seleciona um spawner pro round
                variableStorage.GetComponent<VariableStorage>().inimigosRemanescentes++;
                Instantiate(inimigoPrimeiro, spawnPoints[qualSpawn].position, transform.rotation);  //spawna o indicador de wave

                for (int i = 0; i <= variableStorage.GetComponent<VariableStorage>().round; i += 4)    //a cada quatro rounds, vai adicionar um grupo novo de inimigos
                {
                    Debug.Log("spawns");
                    int qualInimigo = Random.Range(0, 3);   //seleciona o tipo de inimigos
                    switch (qualInimigo)    //verificacao de qual inimigo spawnar
                    {
                        case 0: //spawn normal
                            for (int i2 = 0; i2 <= 3 + variableStorage.GetComponent<VariableStorage>().round; i2+=2)   //ver relacao de com quantos comeca (3+round) e aumenta um inimigo por dois rounds (+=2)
                            {
                                variableStorage.GetComponent<VariableStorage>().inimigosRemanescentes++;
                                Instantiate(inimigoNormal, spawnPoints[qualSpawn].position, transform.rotation);
                                yield return new WaitForSeconds(0.5f);  //espera um pouco antes de sawnar o inimigo para nao ter clipping
                                Debug.Log("spawn interno normal");
                            }
                            break;

                        case 1: //spawn rapido
                            for (int i2 = 0; i2 <= 4 + variableStorage.GetComponent<VariableStorage>().round + 4; i2++)
                            {
                                variableStorage.GetComponent<VariableStorage>().inimigosRemanescentes++;
                                Instantiate(inimigoRapido, spawnPoints[qualSpawn].position, transform.rotation);
                                yield return new WaitForSeconds(0.5f);
                                Debug.Log("spawn interno rapido");
                            }
                            break;

                        case 2: //spawn tanque
                            for (int i2 = 0; i2 <= 1 + variableStorage.GetComponent<VariableStorage>().round; i2+=3)
                            {
                                variableStorage.GetComponent<VariableStorage>().inimigosRemanescentes++;
                                Instantiate(inimigoTanque, spawnPoints[qualSpawn].position, transform.rotation);
                                yield return new WaitForSeconds(0.5f);
                                Debug.Log("spawn interno tanque");
                            }
                            break;
                        

                    }
                    ////hudscript.UpdateEnemiesRemaining();
                    
                }

                Debug.Log("acabou spawns");
            }
            else
            {
                Debug.Log("esperando novo ciclo");
                yield return new WaitForSeconds(2); //faz o programa esperar um pouco antes de verificar denovo
            }
        }
    }




}