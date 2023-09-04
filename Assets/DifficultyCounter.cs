using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficultyCounter : MonoBehaviour
{

    public GameObject enemySpawn;
    private float number;
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        number = enemySpawn.GetComponent<SpawnEnemy>().spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        number = enemySpawn.GetComponent<SpawnEnemy>().spawnRate;
        text.text = "Spawn Rate: " + string.Format("{0:0.00}", number) + " seconds";
    }
}
