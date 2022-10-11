using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public GameObject variableStorage;

    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    public IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(30f);
        //spawnervariables.inimigosRemanescentes--;
        //aumentar o numero de kills ou diminuir o numero de inimigos
        Destroy(gameObject);
        variableStorage.GetComponent<VariableStorage>().inimigosRemanescentes--;  //ao se autodestruir, diminui 1
        variableStorage.GetComponent<VariableStorage>().kills++;
    }
}
