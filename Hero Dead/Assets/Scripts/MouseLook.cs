using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSens = 100f;
    public Transform playerBody;

    public GameObject bullet;
    public float bulletSpeed = 100f;
    public Transform muzzle;

    float xRot = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if (Input.GetMouseButtonDown(0))
        {
            //GameObject newBullet = Instantiate(bullet, muzzle.transform.position + new Vector3(1, 0, 0), muzzle.transform.rotation) as GameObject;
            GameObject newBullet = Instantiate(bullet, muzzle.transform.position, transform.rotation) as GameObject;

            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();

            bulletRB.velocity = muzzle.transform.forward * bulletSpeed;

        }
    }
}
