using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public List<GameObject> platforms = new List<GameObject>();
    public List<Transform> currentPlatforms = new List<Transform>();
    public List<GameObject> commonObstacles = new List<GameObject>();
    public List<GameObject> positiveObjectsToColide = new List<GameObject>();
    public GameObject goldCoin;
    public GameObject alcoholBottle;
    public GameObject pills;
    public GameObject crowd;

    private Transform player;
    private Transform currentPlatformPoint;
    private int platformIndex;
    
    private int offset = 0;
    private float firstPathPosition_X = (float)-5.4;
    private float secondPathPosition_X = (float)0;
    private float thirdPathPosition_X = (float)5.2;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i =0; i< platforms.Count; i++) {
            Debug.Log(platforms[i]);
            Transform p = Instantiate(platforms[i], new Vector3(0, 0, i * 86), transform.rotation).transform;
            currentPlatforms.Add(p);
            offset += 86;
        }
        currentPlatformPoint = currentPlatforms[platformIndex].GetComponent<Platform>().point;
        
    }


    // Update is called once per frame
    void Update()
    {
        float distance = player.transform.position.z - currentPlatforms[platformIndex].transform.position.z;
        if (distance >= 80)
        { 
            Recycle(currentPlatforms[platformIndex].gameObject);

            if (platformIndex == platforms.Count - 1) platformIndex = 0;
            else platformIndex++;

            currentPlatformPoint = currentPlatforms[platformIndex].GetComponent<Platform>().point;
        }
    }

    public void Recycle(GameObject platform) {
        platform.transform.position = new Vector3(0, 0, offset);

        GenerateRandomObjectsAtPlatform(platform);
        
        Debug.Log("reciclou");
        offset += 86;
    }

    private void GenerateObstacles(GameObject platform)
    {
        float numberOfNewObjects = 4; 
        float newObjectPositionZ;
        float newObjectPositionX;
        float distanceAtPlatform = platform.transform.position.z - 86;
        List<GameObject> newObjects = new List<GameObject>();

        int idx;


        //Limpando os obstáculos antigos.
        GameObject obstacles = platform.transform.Find("Obstacles").gameObject;
        foreach (Transform child in obstacles.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < numberOfNewObjects; i++)
        {
            idx = Random.Range(0, this.commonObstacles.Count);
            Debug.Log("index: " + idx);
            distanceAtPlatform += 20;

            InstantiateObstacle(commonObstacles[idx], platform.transform, distanceAtPlatform);
        }
    }

    private void InstantiateObstacle(GameObject obstacle, Transform platformTransform, float distanceAtPlatform)
    {
        GameObject obstacles = platformTransform.Find("Obstacles").gameObject;
        Vector3 newObstaclePosition = GenerateRandomPosition(platformTransform.position, distanceAtPlatform);
        GameObject newInstance;

        //TODO: Isso aqui tá um pouco idiota kkkk fiz errado.. dá pra configurar isso aí no prefab. Aqui só preciso alterar a posição.
        switch (obstacle.name)
        {
            case "RoadBlock_D":
                newObstaclePosition.y = (float)0.5;
                obstacle.transform.localScale = new Vector3(2, 2, 2);
                obstacle.transform.position = newObstaclePosition;
                obstacle.transform.eulerAngles = new Vector3(-90, 0, 90);
                break;
            case "RoadBlock_A":
                newObstaclePosition.y = (float)0.4;
                obstacle.transform.localScale = new Vector3(2, 2, 2);
                obstacle.transform.eulerAngles = new Vector3(-90, 0, 90);
                obstacle.transform.position = newObstaclePosition;
                break;
            case "RoadBlock_B":
                newObstaclePosition.y = (float)0.4;
                obstacle.transform.eulerAngles = new Vector3(-90, 0, 90);
                obstacle.transform.position = newObstaclePosition;
                break;
        }

        newInstance = Instantiate(obstacle);
        newInstance.transform.parent = obstacles.transform;
    }

    private void GenerateGoldCoins(GameObject platform)
    {
        //Limpando moedas antigas
        GameObject goldCoins = platform.transform.Find("GoldCoins").gameObject;
        foreach (Transform child in goldCoins.transform)
        {
            Destroy(child.gameObject);
        }

        GameObject newGoldCoinInstance;
        Vector3 newGoldCoinPosition = GenerateRandomPosition(platform.transform.position, platform.transform.position.z);
        newGoldCoinPosition.y = 3;
        for(int i = 0; i < 10; i++)
        {
            newGoldCoinPosition.z = platform.transform.position.z + i * 3;
            this.goldCoin.transform.position = newGoldCoinPosition;
            newGoldCoinInstance = Instantiate(goldCoin);
            newGoldCoinInstance.transform.parent = goldCoins.transform;

        }
    }

    private void GenerateBadStuff(GameObject platform)
    {

        //por enquanto gera apenas a caixa de pílulas

        Vector3 newBadStuffPosition = GenerateRandomPosition(platform.transform.position, platform.transform.position.z);
        GameObject newBadStuffInstance;

        GameObject badStuff = platform.transform.Find("BadStuff").gameObject;
        foreach (Transform child in badStuff.transform)
        {
            Destroy(child.gameObject);
        }
        newBadStuffPosition.y = (float)3.2;
        newBadStuffPosition.z = platform.transform.position.z + 50;
        this.pills.transform.position = newBadStuffPosition;
        newBadStuffInstance = Instantiate(this.pills);
        newBadStuffInstance.transform.parent = badStuff.transform;


        // criando aglomeração
        Vector3 newCrowdPosition = GenerateRandomPosition(platform.transform.position, platform.transform.position.z);
        GameObject newCrowdInstance;

/*        GameObject crowd = platform.transform.Find("Crowd").gameObject;
*/        foreach (Transform child in platform.transform)
        {
            Debug.Log(child.name);
            if(child.name == "Crowd(Clone)")
                Destroy(child.gameObject);
        }
        newCrowdPosition.y = (float)0.4;
        newCrowdPosition.z = platform.transform.position.z + 65;
        this.crowd.transform.position = newCrowdPosition;
        newCrowdInstance = Instantiate(this.crowd);
        newCrowdInstance.transform.parent = platform.transform;
    }

    private void GenerateGoodStuff(GameObject platform)
    {

        //por enquanto gera apenas a garrafa de álcool em gel como exemplo

        Vector3 newGoodStuffPosition = GenerateRandomPosition(platform.transform.position, platform.transform.position.z);
        GameObject newGoodStuffInstance;

        GameObject goodStuff = platform.transform.Find("GoodStuff").gameObject;
        foreach (Transform child in goodStuff.transform)
        {
            Destroy(child.gameObject);
        }
        newGoodStuffPosition.y = (float)3.2;
        newGoodStuffPosition.z = platform.transform.position.z + 40;
        this.alcoholBottle.transform.position = newGoodStuffPosition;
        newGoodStuffInstance = Instantiate(alcoholBottle);
        newGoodStuffInstance.transform.parent = goodStuff.transform;
    }
    private void GenerateRandomObjectsAtPlatform(GameObject platform)
    {
        GenerateGoldCoins(platform);
        GenerateObstacles(platform);
        GenerateGoodStuff(platform);
        GenerateBadStuff(platform);

    }

    //Gera qual o caminho em que o objeto estará.
    private Vector3 GenerateRandomPosition(Vector3 plataformPosition, float distanceAtPlatform)
    {
/*        Vector3 playerPosition = player.transform.position;
*/      float position_X = 0;
        int pathIdx = Random.Range(0, 3);

        if (pathIdx == 0) position_X = firstPathPosition_X;
        else if (pathIdx == 1) position_X = secondPathPosition_X;
        else if (pathIdx == 2) position_X = thirdPathPosition_X;
        else Debug.Log("pathIdx fora do limite");

        return new Vector3(position_X, plataformPosition.y , distanceAtPlatform);
    }
}
