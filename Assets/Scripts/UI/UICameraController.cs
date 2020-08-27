using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraController : MonoBehaviour
{
    private float m_cameraSpeed = 12.0f;
    private float m_cameraBound = 40f;

    private float m_zoomSpeed = 20.0f;

    private float m_cameraLimitDown = 2.0f;
    private float m_cameraLimitUp = 40.0f;
    private float m_cameraLimitLeft = 15.0f;
    private float m_cameraLimitRight = 40.0f;

    void Update()
    {
        HandleMovement();
        //HandleScrolling();
    }

    private void HandleMovement()
    {

        if (!Globals.m_canScroll)
        {
            return;
        }

        Vector3 movementVec = new Vector3();

        if (Input.mousePosition.x <= m_cameraBound && Input.mousePosition.x >= 0)
        {
            if (transform.position.x > m_cameraLimitLeft)
            {
                movementVec.x -= m_cameraSpeed * Time.deltaTime;
            }
        }

        if (Input.mousePosition.x >= Screen.width - m_cameraBound && Input.mousePosition.x <= Screen.width)
        {
            if (transform.position.x < m_cameraLimitRight)
            {
                movementVec.x += m_cameraSpeed * Time.deltaTime;
            }
        }

        if (Input.mousePosition.y <= m_cameraBound && Input.mousePosition.y >= 0)
        {
            if (transform.position.y > m_cameraLimitDown)
            {
                movementVec.y -= m_cameraSpeed * Time.deltaTime;
            }
        }

        if (Input.mousePosition.y >= Screen.height - m_cameraBound && Input.mousePosition.y <= Screen.height)
        {
            if (transform.position.y < m_cameraLimitUp)
            {
                movementVec.y += m_cameraSpeed * Time.deltaTime;
            }
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
