using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject plane;
    float zoomSpeed = 1.0f;      // How fast the camera zooms
    float minZoom = 2.0f;        // Minimum zoom level (camera size)
    float maxZoom = 8.0f;       // Maximum zoom level (camera size)
    void LateUpdate()
    {
        // camera follows airplane along to provide "infinite" canvas and deal with edges
        transform.position = plane.transform.position + new Vector3(0, 0, -10);
        zoomCamera();
    }

    // allows for some zoom in/out using a mouse's scroll wheel
    void zoomCamera()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        Camera.main.orthographicSize -= scrollInput * zoomSpeed;

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
    }
}
