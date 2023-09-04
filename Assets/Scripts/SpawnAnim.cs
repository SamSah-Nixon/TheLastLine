using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnim : MonoBehaviour
{
    private float size;
    // Start is called before the first frame update
    void Start()
    {
        size = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (size < 5)
        {
            size += 0.1f;
        }
        else
        {
            Destroy(gameObject);
        }
        transform.localScale = new Vector3(size, size, size);
    }
}
