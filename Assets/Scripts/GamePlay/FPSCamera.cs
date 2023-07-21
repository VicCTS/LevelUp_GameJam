using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    [SerializeField] Transform fpsCamera;
    [SerializeField] float sensitivity = 200f;

    float xRotation = 0f;
    float yRotation = 90f;
    [SerializeField] Vector2 xLimit;
    [SerializeField] Vector2 yLimit;

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
            yRotation = 90f;
            CenterCamera();
        }         
    }

    void CenterCamera()
    {
        float xPosition = fpsCamera.rotation.eulerAngles.x;
        float yPosition = fpsCamera.rotation.eulerAngles.y;

        float lerpedXPosition = Mathf.Lerp(xPosition, 0, 0.1f);
        float lerpedYPosition = Mathf.Lerp(yPosition, 180, 0.1f);

        fpsCamera.rotation = Quaternion.Euler(lerpedXPosition, lerpedYPosition, 0);
    }

    void CameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, xLimit.x, xLimit.y);
        yRotation = Mathf.Clamp(yRotation, yLimit.x, yLimit.y);

        fpsCamera.rotation = Quaternion.Euler(xRotation, yRotation, 0);
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

                if(Input.GetMouseButtonDown(0))
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
