using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraController : MonoBehaviour
{
    private float m_cameraSpeed = 12.0f;
    private float m_cameraBound = 40f;

    private float m_zoomSpeed = 20.0f;

    private float m_cameraLimitDown = 2.0f;
    private float m_cameraLimitUp = 2.0f * Globals.GridSizeY;
    private float m_cameraLimitLeft = 15.0f;
    private float m_cameraLimitRight = 3.0f * Globals.GridSizeX;

    void Update()
    {
        if (GameHelper.IsInLevelSelect())
        {
            return;
        }

        HandleMovement();
        //HandleScrolling();
    }

    private void HandleMovement()
    {

        Vector3 movementVec = new Vector3();

        if (ScrollLeft())
        {
            movementVec.x -= m_cameraSpeed * Time.deltaTime;
        }

        if (ScrollRight())
        {
            movementVec.x += m_cameraSpeed * Time.deltaTime;
        }

        if (ScrollDown())
        {
            movementVec.y -= m_cameraSpeed * Time.deltaTime;
        }

        if (ScrollUp())
        {
            movementVec.y += m_cameraSpeed * Time.deltaTime;
        }

        transform.localPosition += movementVec;
    }

    private bool ScrollLeft()
    {
        bool mouseAtEdge = Input.mousePosition.x <= m_cameraBound && Input.mousePosition.x >= 0;
        bool keyPressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool withinLimit = transform.position.x > m_cameraLimitLeft;

        bool edgeScrolling = mouseAtEdge && Globals.m_canScroll;

        return (edgeScrolling || keyPressed) && withinLimit;
    }

    private bool ScrollRight()
    {
        bool mouseAtEdge = Input.mousePosition.x >= Screen.width - m_cameraBound && Input.mousePosition.x <= Screen.width;
        bool keyPressed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool withinLimit = transform.position.x < m_cameraLimitRight;

        bool edgeScrolling = mouseAtEdge && Globals.m_canScroll;

        return (edgeScrolling || keyPressed) && withinLimit;
    }

    private bool ScrollDown()
    {
        bool mouseAtEdge = Input.mousePosition.y <= m_cameraBound && Input.mousePosition.y >= 0;
        bool keyPressed = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool withinLimit = transform.position.y > m_cameraLimitDown;

        bool edgeScrolling = mouseAtEdge && Globals.m_canScroll;

        return (edgeScrolling || keyPressed) && withinLimit;
    }

    private bool ScrollUp()
    {
        bool mouseAtEdge = Input.mousePosition.y >= Screen.height - m_cameraBound && Input.mousePosition.y <= Screen.height;
        bool keyPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool withinLimit = transform.position.y < m_cameraLimitUp;

        bool edgeScrolling = mouseAtEdge && Globals.m_canScroll;

        return (edgeScrolling || keyPressed) && withinLimit;
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
