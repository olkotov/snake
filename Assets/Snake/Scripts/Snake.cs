using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float speed;
    private float currentSpeed;

    public GameObject partPrefab;
    public GameObject conePrefab;

    private float distanceBetween;
    private List<Transform> parts;

    private float nailTransformTimer;
    private Camera currentCamera;
    private float cameraToHeadDistance;
    private Color color;

    private bool isFeverActive;
    private float feverTimer;
    private int feverComboCount;
    private float lastCrystalEatingTime;

    void Start()
    {
        currentSpeed = speed;
        distanceBetween = partPrefab.transform.localScale.z;
        parts = new List<Transform>();
        nailTransformTimer = distanceBetween;
        currentCamera = Camera.main;

        isFeverActive = false;
        feverTimer = 0.0f;
        feverComboCount = 0;
        lastCrystalEatingTime = 0.0f;

        Transform head = Instantiate(partPrefab, transform).transform;
        head.gameObject.tag = "SnakeHead";
        Instantiate(conePrefab, head);
        parts.Add(head);

        cameraToHeadDistance = Vector3.Distance(currentCamera.transform.position, head.position);

        for (int i = 0; i < 2; i++)
        {
            AddPart();
        }

        ChangeColor(new Color(0.3254f, 1.0f, 0.0f));
    }

    void FixedUpdate()
    {
        UpdateFever();
        UpdateNailTransforms();
        Move();
        CheckFinish();
    }

    public void AddPart()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, parts.Count * -distanceBetween);
        Transform part = Instantiate(partPrefab, spawnPosition, Quaternion.identity, transform).transform;

        Renderer rendererComponent = part.GetComponent<Renderer>();
        rendererComponent.material.SetColor("_Color", color);

        parts.Add(part);
    }

    void UpdateNailTransforms()
    {
        nailTransformTimer += Time.deltaTime;

        if (nailTransformTimer >= distanceBetween / (currentSpeed + float.Epsilon))
        {
            for (int i = 0; i < parts.Count; i++)
            {
                parts[i].GetComponent<SnakePart>().UpdateTransform();
            }

            nailTransformTimer = 0.0f;
        }
    }

    void Move()
    {
        // Move snake head

        Vector3 lookAtVector = parts[0].position + Vector3.forward;

        if (isFeverActive)
        {
            lookAtVector.x = 0.0f;
        }
        else
        {
            // Mobile movement

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                Vector3 screenPosition = new Vector3(touch.position.x, touch.position.y, cameraToHeadDistance);
                Vector3 worldPosition = currentCamera.ScreenToWorldPoint(screenPosition);

                lookAtVector.x = Mathf.Clamp(worldPosition.x, -1.45f, 1.45f);
            }

            // PC movement

            //Vector3 screenPosition = Input.mousePosition;
            //screenPosition.z = cameraToHeadDistance;

            //Vector3 worldPosition = currentCamera.ScreenToWorldPoint(screenPosition);

            //lookAtVector.x = Mathf.Clamp(worldPosition.x, -1.45f, 1.45f);
        }

        parts[0].LookAt(lookAtVector);
        parts[0].Translate(new Vector3(0.0f, 0.0f, currentSpeed * Time.deltaTime));

        // Move snake tail

        if (parts.Count > 1)
        {
            for (int i = 1; i < parts.Count; i++)
            {
                SnakePart partComponent = parts[i - 1].GetComponent<SnakePart>();
                parts[i].position = partComponent.GetPreviousPosition();
                parts[i].rotation = partComponent.GetPreviousRotation();
            }
        }
    }

    public void ChangeColor(Color color)
    {
        this.color = color;

        for (int i = 0; i < parts.Count; i++)
        {
            Renderer rendererComponent = parts[i].GetComponent<Renderer>();
            rendererComponent.material.SetColor("_Color", color);
        }
    }

    public Color GetColor()
    {
        return color;
    }

    void SetFeverActive(bool isActive)
    {
        if (isActive)
        {
            isFeverActive = true;
            currentSpeed = speed * 3.0f;
        }
        else
        {
            isFeverActive = false;
            currentSpeed = speed;
        }
    }

    public bool IsFeverActive()
    {
        return isFeverActive;
    }

    public void CrystalEating()
    {
        if (IsFeverActive())
        {
            return;
        }

        if (feverComboCount == 0)
        {
            feverComboCount++;
            lastCrystalEatingTime = Time.time;
            return;
        }

        //------------------------------------------------------------------------------

        float currentCrystalEatingTime = Time.time;

        float eatingDeltaTime = currentCrystalEatingTime - lastCrystalEatingTime;

        if (eatingDeltaTime <= 0.31f)
        {
            feverComboCount++;
            lastCrystalEatingTime = currentCrystalEatingTime;
        }
        else
        {
            feverComboCount = 0;
            lastCrystalEatingTime = currentCrystalEatingTime;
            return;
        }

        //------------------------------------------------------------------------------

        if (feverComboCount > 3)
        {
            SetFeverActive(true);
            feverComboCount = 0;
        }
    }

    void UpdateFever()
    {
        if (isFeverActive)
        {
            feverTimer += Time.deltaTime;

            if (feverTimer >= 5.0f)
            {
                SetFeverActive(false);
                GameStats.ClearCrystalCounter();
                feverTimer = 0.0f;
            }
        }
    }

    void CheckFinish()
    {
        if (parts[0].position.z > 274.0f)
        {
            Application.Quit();
        }
    }
}

