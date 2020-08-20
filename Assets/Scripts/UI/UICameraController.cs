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
        //HandleScrolling();
    }

    private void HandleMovement()
    {
        Vector3 movementVec = new Vector3();

        if (Input.mousePosition.x <= m_cameraBound && Input.mousePosition.x >= 0)
        {
            movementVec.x -= m_cameraSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x >= Screen.width - m_cameraBound && Input.mousePosition.x <= Screen.width)
        {
            movementVec.x += m_cameraSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y <= m_cameraBound && Input.mousePosition.y >= 0)
        {
            movementVec.y -= m_cameraSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y >= Screen.height - m_cameraBound && Input.mousePosition.y <= Screen.height)
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
