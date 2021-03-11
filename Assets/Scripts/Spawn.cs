using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public List<GameObject> platforms = new List<GameObject>();
    public List<Transform> currentPlatforms = new List<Transform>();
    public int offset;
    private Transform player;
    private Transform currentPlatformPoint;
    private int platformIndex;
    
    private float firstPathPosition_X = (float)-5.4;
    private float secondPathPosition_X = (float)0;
    private float thirdPathPosition_X = (float)5.2;

    public GameObject roadBlockObject;
    public GameObject carObject;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i =0; i< platforms.Count; i++) {
            Transform p = Instantiate(platforms[i], new Vector3(0, 0, i * 86), transform.rotation).transform;
            currentPlatforms.Add(p);
            offset += 86;
        }
        currentPlatformPoint = currentPlatforms[platformIndex].GetComponent<Platform>().point;
        
    }


    // Update is called once per frame
    void Update()
    {
        float distance = player.position.z - currentPlatformPoint.position.z;
        Debug.Log("distance: " + distance);
        if(distance >= 5) { // Acho que esse valor '5' precisará ficar dinâmico, proporcional à velocidade do player. 
                            // Caso contrário, ao aumentar a velocidade as plataformas não são recicladas a tempo do player atingir o fim da última plataforma.
            Recycle(currentPlatforms[platformIndex].gameObject);
            platformIndex++;
            currentPlatformPoint = currentPlatforms[platformIndex].GetComponent<Platform>().point;
        }
    }

    public void Recycle(GameObject platform) {
        platform.transform.position = new Vector3(0, 0, offset);

        GenerateRandomObjects();
        
        Debug.Log("reciclou");
        offset += 86;
    }

    /*Preciso melhorar, mas está funcionando... Porém, acho que vou mudar pra que as posições dos objetos fiquem de acordo com a posição da nova plataforma, e não do player.
     Faz mais sentido, e acho que vai facilitar tbm*/

    private void GenerateRandomObjects()
    {
        float numberOfNewObjects = 8; //Não sei se precisa ou não ser de acordo com o quão avançado o player está... com mais jogabilidade vamos descobrir.

        float distanceFromPlayer = 50; //talvez também precisará ficar dinâmico, conforme a velocidade do player. 
        List<GameObject> newObjects = new List<GameObject>();
        int idx;

        for(int i = 0; i < numberOfNewObjects; i++)
        {
            idx = Random.Range(0, 2);
            distanceFromPlayer += 25;
            switch (idx)
            {
                case 0:
                    GameObject newCar = carObject;
                    Vector3 newCarPosition = GenerateRandomPosition(distanceFromPlayer);                

                    newObjects.Add(newCar);

                    Instantiate(carObject, newCarPosition, this.transform.rotation);
                    break;    
                case 1:
                    GameObject newRoadBlock = roadBlockObject;
                    Vector3 newRoadBlockPosition = GenerateRandomPosition(distanceFromPlayer);


                    newObjects.Add(newRoadBlock);

                    Instantiate(carObject, newRoadBlockPosition, this.transform.rotation);
                    break;
            }
        }

    }

    private Vector3 GenerateRandomPosition(float distanceFromPlayer)
    {
        Vector3 playerPosition = player.transform.position;
        float position_X = -3;
        int pathIdx = Random.Range(0, 3);

        if (pathIdx == 0) position_X = firstPathPosition_X;
        else if (pathIdx == 1) position_X = secondPathPosition_X;
        else if (pathIdx == 2) position_X = thirdPathPosition_X;
        else Debug.Log("pathIdx fora do limite");

        return new Vector3(position_X, playerPosition.y , playerPosition.z + distanceFromPlayer);
    }
}
