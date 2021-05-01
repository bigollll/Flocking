using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject fishPrefab;                          // pega o prefab do peixe
    public int numFish = 20;                               //numero de peixes
    public GameObject[] allFish;                           //array do peixe para colocar mais peixes
    public Vector3 swinLimits = new Vector3(5, 5, 5);      //limitador de espaço

    [Header("Configurações do Cardume")]
    [Range(0.0f, 5.0f)]                                    //configura a velocidade do cardume que foi pego no flock
    public float minSpeed;                                 
    [Range(0.0f, 5.0f)]                                    //configura a velocidade do cardume que foi pego no flock
    public float maxSpeed;


    private void Start()
    {
        allFish = new GameObject[numFish];                 //igual o array ao numero de peixes criados
        for(int i = 0; i < numFish; i++)                   //cria os peixes em diferentes locais vizualizando a proximidade dos outros
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x), Random.Range(-swinLimits.y, swinLimits.y), Random.Range(-swinLimits.z, swinLimits.z));
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            allFish[i].GetComponent<Flock>().myManager = this;
        }
    }
}
