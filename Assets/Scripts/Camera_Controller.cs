using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour {
    public float moveSpeed;

    // Update is called once per frame
    void Update() {
        float xMovement = Input.GetAxis("Horizontal") * (moveSpeed * Time.deltaTime);
        float zMovement = Input.GetAxis("Vertical") * (moveSpeed * Time.deltaTime);
        Vector3 movement = new Vector3(xMovement, 0.0f, zMovement);

        gameObject.transform.Translate(movement);

        float xRotation = Input.GetAxis("Mouse X");
        float yRotation = Input.GetAxis("Mouse Y");
        gameObject.transform.Rotate(yRotation, xRotation, 0.0f);
    }
}
