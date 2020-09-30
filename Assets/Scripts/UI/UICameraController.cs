using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraController : Singleton<UICameraController>, IReset
{
    private float m_cameraSpeed = 15.0f;
    private float m_cameraBound = 40f;

    private float m_zoomSpeed = 20.0f;

    private float m_cameraLimitDown = 1.0f;
    private float m_cameraLimitUp = 2.0f * (Globals.GridSizeY + 4);
    private float m_cameraLimitLeft = 10.0f;
    private float m_cameraLimitRight = 3.0f * (Globals.GridSizeX - 3);

    private Vector3 m_startingTransform;

    private Vector3 m_smoothTarget;
    private float m_smoothSpeed;

    void Start()
    {
        m_startingTransform = transform.position;

        m_smoothTarget = new Vector3(0,0,0);
        m_smoothSpeed = m_cameraSpeed;
    }

    void Update()
    {
        if (GameHelper.IsInLevelSelect())
        {
            return;
        }

        if (transform.position == m_smoothTarget)
        {
            m_smoothTarget = new Vector3(0,0,0);
        }
        else if (m_smoothTarget != new Vector3(0,0,0))
        {
            transform.position = Vector3.MoveTowards(transform.position, m_smoothTarget, m_smoothSpeed * Time.deltaTime);
        }

        if (!GameHelper.IsOpponentsTurn())
        {
            HandleMovement();
        }
        //HandleScrolling();
    }

    public void SmoothCameraTransitionToGameObject(GameObject obj)
    {
        Vector3 vec = new Vector3(obj.transform.position.x, obj.transform.position.y, -10.0f);
        m_smoothTarget = vec;

        float dist = Vector3.Distance(transform.position, m_smoothTarget);

        m_smoothSpeed = dist / 3.0f;

        if (m_smoothSpeed <= m_cameraSpeed * 2.0f)
        {
            m_smoothSpeed = m_cameraSpeed * 2.0f;
        }
    }

    public void SnapToGameObject(GameObject obj)
    {
        if (obj == null)
        {
            return;
        }

        Vector3 objPos = obj.transform.position;

        transform.position = new Vector3(objPos.x, objPos.y, transform.position.z);
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

        return (keyPressed) && withinLimit;
    }

    private bool ScrollRight()
    {
        bool mouseAtEdge = Input.mousePosition.x >= Screen.width - m_cameraBound && Input.mousePosition.x <= Screen.width;
        bool keyPressed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool withinLimit = transform.position.x < m_cameraLimitRight;

        bool edgeScrolling = mouseAtEdge && Globals.m_canScroll;

        return (keyPressed) && withinLimit;
    }

    private bool ScrollDown()
    {
        bool mouseAtEdge = Input.mousePosition.y <= m_cameraBound && Input.mousePosition.y >= 0;
        bool keyPressed = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool withinLimit = transform.position.y > m_cameraLimitDown;

        bool edgeScrolling = mouseAtEdge && Globals.m_canScroll;

        return (keyPressed) && withinLimit;
    }

    private bool ScrollUp()
    {
        bool mouseAtEdge = Input.mousePosition.y >= Screen.height - m_cameraBound && Input.mousePosition.y <= Screen.height;
        bool keyPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool withinLimit = transform.position.y < m_cameraLimitUp;

        bool edgeScrolling = mouseAtEdge && Globals.m_canScroll;

        return (keyPressed) && withinLimit;
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

    public  void Activate()
    {

    }

    public void Reset()
    {
        transform.position = m_startingTransform;
    }
}
