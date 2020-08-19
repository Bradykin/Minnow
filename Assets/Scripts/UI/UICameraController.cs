using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraController : MonoBehaviour
{
    private float m_cameraSpeed = 6.0f;
    private float m_cameraBound = 40f;

    private float m_zoomSpeed = 20.0f;

    void Update()
    {
        HandleMovement();
        HandleScrolling();
    }

    private void HandleMovement()
    {
        Vector3 movementVec = new Vector3();

        if (Input.mousePosition.x <= m_cameraBound)
        {
            movementVec.x -= m_cameraSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x >= Screen.width - m_cameraBound)
        {
            movementVec.x += m_cameraSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y <= m_cameraBound)
        {
            movementVec.y -= m_cameraSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y >= Screen.height - m_cameraBound)
        {
            movementVec.y += m_cameraSpeed * Time.deltaTime;
        }

        transform.localPosition += movementVec;
    }

    private void HandleScrolling()
    {
        float size = GetComponent<Camera>().orthographicSize;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            size -= m_zoomSpeed * Time.deltaTime;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            size += m_zoomSpeed * Time.deltaTime;
        }

        GetComponent<Camera>().orthographicSize = size;
    }
}
