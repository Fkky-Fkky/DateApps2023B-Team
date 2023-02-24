using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverDirection : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionEffect = null;

    [SerializeField]
    private Transform explosionPointA = null;
    
    [SerializeField]
    private Transform explosionPointB = null;

    private int generateCount = 0;
    private float generateTime = 0.0f;
    private bool isFirstGenerate = false;
    private GameManager gameManager = null;

    const int MAX_GENERATE_COUNT = 15;
    const float MAX_GENERATE_TIME = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        generateCount = 0;
        generateTime = 0.0f;
        isFirstGenerate = false;
        gameManager = GetComponentInParent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.IsGameOver)
        {
            return;
        }

        if(generateCount > MAX_GENERATE_COUNT)
        {
            return;
        }

        generateTime -= Time.deltaTime;

        if(generateTime <= 0.0f)
        {
            GenerateExplosion();
            generateTime = MAX_GENERATE_TIME;
        }
    }

    void GenerateExplosion()
    {
        Vector3 position = Vector3.one;
        position.x = Random.Range(explosionPointA.position.x, explosionPointB.position.x);
        position.z = Random.Range(explosionPointB.position.z, explosionPointA.position.z);
        if (!isFirstGenerate) {
            position.x = 0.0f;
            position.z = 0.0f;
            isFirstGenerate = true;
        }
        Instantiate(explosionEffect, position, Quaternion.identity);
        generateCount++;
    }
}
