using UnityEngine;

public class DestroyerAI : MonoBehaviour
{
    public float moveSpeed;
    public Transform baseLocation;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = 5f;
    }

    void Update()
    {
        SpeedControl();
        transform.LookAt(new Vector3(baseLocation.position.x, transform.position.y, baseLocation.position.z));
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.forward * moveSpeed * 500f * Time.deltaTime, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}