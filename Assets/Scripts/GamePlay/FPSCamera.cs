using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    [SerializeField] Transform fpsCamera;
    [SerializeField] float sensitivity = 200f;

    float xRotation = 0f;
    float yRotation = 0f;
    [SerializeField] Vector2 xLimit;
    [SerializeField] Vector2 yLimit;

    [SerializeField] float lastXRotation;
    [SerializeField] float lastYRotation;

    Answer lastAnswerSelected;

    void Awake()
    {
        fpsCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.currentState == GameManager.GameState.Question)
        {
            CameraMovement();
            CameraRayCast();
        }

        if(GameManager.Instance.currentState == GameManager.GameState.Talking)
        {
            xRotation = 0f;
            yRotation = 0f;
            CenterCamera();
        }         
    }

    void CenterCamera()
    {
        lastXRotation = transform.localRotation.eulerAngles.x;
        if(lastXRotation > 35.5f)
        {
            lastXRotation = lastXRotation - 360;
        } 
        lastYRotation = transform.localRotation.eulerAngles.y;
        if(lastYRotation > 60.5f)
        {
            lastYRotation = lastYRotation - 360;
        }

        float lerpedlastXRotation = Mathf.Lerp(lastXRotation, 0, 0.1f);
        float lerpedlastYRotation = Mathf.Lerp(lastYRotation, 0, 0.1f);

        transform.rotation = Quaternion.Euler(lerpedlastXRotation, lerpedlastYRotation, 0);
    }

    void CameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation += mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, xLimit.x, xLimit.y);
        yRotation = Mathf.Clamp(yRotation, yLimit.x, yLimit.y);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    void CameraRayCast()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100.0f)) 
        {
            Answer answerObject = hit.transform.gameObject.GetComponent<Answer>();

            if(answerObject != null)
            {
                lastAnswerSelected = answerObject;

                answerObject.ShowAnswer();

                if(Input.GetMouseButtonDown(0) && answerObject.selectable)
                {
                    answerObject.AnswerQuestion();
                }
            }
            else if(answerObject == null && lastAnswerSelected != null)
            {
                lastAnswerSelected.HideAnswer();
            }
        }
    }
}
