using UnityEngine;

public class DRotation : MonoBehaviour
{
    public Transform CompassPointer;
    public Transform cm;
    void Start()
    {
        CompassPointer.rotation = Quaternion.identity;
    }

    void Update()
    {
        CompassPointer.rotation = Quaternion.Lerp(CompassPointer.rotation, transform.rotation, Time.deltaTime * 5f);
        cm.rotation = CompassPointer.rotation;

        if (Input.GetKey(KeyCode.R))
        {
            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 100f);
        }

        Vector3 input = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) input += Vector3.up;
        if (Input.GetKey(KeyCode.S)) input += Vector3.down;
        if (Input.GetKey(KeyCode.A)) input += Vector3.left;
        if (Input.GetKey(KeyCode.D)) input += Vector3.right;

        if (input != Vector3.zero)
        {
            Vector3 moveDir = transform.rotation * input.normalized;
            float moveSpeed = 5f;
            Vector3 targetPos = transform.position + moveDir * moveSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.5f); 
        }
    }
}