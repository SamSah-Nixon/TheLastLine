using UnityEngine;

public class Weapon : MonoBehaviour
{

    public KeyCode fireKey = KeyCode.Mouse0;
    public float fireRate = 0.5f;
    public Transform lazer;
    public Transform barrel;
    public Transform firePoint;
    public int killCount = 0;

    float fireTimer;
    public MeshRenderer fireRenderer;
    MeshRenderer lazerRenderer;
    bool lazerEnabled = false;
    
    // Start is called before the first frame update
    void Start()
    {
        lazerRenderer = lazer.GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (lazerEnabled)
        {
            lazerEnabled = false;
            Invoke("disableLazer", 0.05f);
        }
        if (Time.time >= fireTimer)
        {
            if (Input.GetKey(fireKey))
            {
                fireTimer = Time.time + fireRate;
                fireLazer();
            }
        }
        else if (Input.GetKeyDown(fireKey))
        {
            fireLazer();
        }
    }

    Vector3 FindMidpoint(Vector3 start , Vector3 end)
    {
        return new Vector3((start.x + end.x) / 2, (start.y + end.y) / 2, (start.z + end.z) / 2);
    }

    void fireLazer()
    {
        if (Physics.Raycast(firePoint.position, firePoint.forward, out var hit))
        {
            if (Vector3.Distance(firePoint.position, hit.point) > 1)
            {


                //Position
                lazer.position = FindMidpoint(barrel.position, hit.point);
                //test.position = transform.position;

                //Scale
                lazer.localScale = new Vector3(0.05f, Vector3.Distance(barrel.position, hit.point) / 2, 0.05f);

                //Rotation
                lazer.up = hit.point - barrel.position;
                lazerRenderer.enabled = true;
            }

            fireRenderer.enabled = true;
            lazerEnabled = true;


            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                killCount++;
                if (hit.collider.gameObject.transform.parent != null)
                {
                    Destroy(hit.collider.gameObject.transform.parent.gameObject);
                }
                else
                {
                    
                    Destroy(hit.collider.gameObject);
                }


            }
        }
        else
        {

            lazer.position = FindMidpoint(barrel.position, transform.TransformPoint(Vector3.forward * 10f));
            lazer.localScale = new Vector3(0.05f, Vector3.Distance(barrel.position, transform.TransformPoint(Vector3.forward * 10f)) / 2, 0.05f);
            lazer.up = transform.TransformPoint(Vector3.forward * 10f) - barrel.position;
            fireRenderer.enabled = true;
            lazerRenderer.enabled = true;
            lazerEnabled = true;
            Invoke("disableLazer", 0.1f);

        }
    }


    void disableLazer()
    {
        lazerRenderer.enabled = false;
        fireRenderer.enabled = false;
    }
}
