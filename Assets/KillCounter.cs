using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    public GameObject killerObject;
    TextMeshProUGUI text;
    float number;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        number = killerObject.GetComponent<Weapon>().killCount;
        text.text = "Score: " + number;
    }
}
