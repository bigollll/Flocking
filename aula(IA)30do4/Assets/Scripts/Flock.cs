using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager myManager;     //pega o gerenciador do cardume
    public float speed;                //velocidade do cardume
    bool turning = false;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);    //igualando a velocidado com as variaveis criadas no manager     
    }

    // Update is called once per frame
    void Update()
    {
        Bounds b = new Bounds(myManager.transform.position, myManager.swinLimits * 2); //faz voltar pro ponto central
        if(!b.Contains(transform.position))
        {
            turning = true;
        }
        else
            turning = false;

        if(turning)       //acha a rotação
        {
            Vector3 direction = myManager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (Random.Range(0, 100) < 10)      //faz ter uma velocidade de rotação pra ele voltar pro grupo
                speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
            if(Random.Range(0, 100)< 20) 
                ApplyRules();
            

            
        }
       
        transform.Translate(0, 0, Time.deltaTime * speed);        //movimentação
    }

    void ApplyRules()
    {
        GameObject[] gos;
        gos = myManager.allFish;           //gos recebe o as caracteristicas do allFish

        Vector3 vcentro = Vector3.zero;    //ve o ponto central do cardume
        Vector3 vavoid = Vector3.zero;     //evita colizao
        float gSpeed = 0.01f;              //serve pra acionar a movimentação
        float nDistance;                   //ve a distancia entre eles
        int groupSize = 0;                 //monta outros grupos de peixes se o distanciamento deles for grande
   
        foreach(GameObject go in gos)
        {
            if(go != this.gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if(nDistance <= myManager.neighbourDistance)
                {
                    vcentro += go.transform.position;
                    groupSize++;

                    if(nDistance < 1.0)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;

                }
            }
        }
        if(groupSize>0)                     //verifica se o grupo é maior que zero e aplica as rotaçoes
        {
            vcentro = vcentro / groupSize + (myManager.goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vcentro + vavoid) - transform.position;
            if(direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
            }


        }
    
    }
}

