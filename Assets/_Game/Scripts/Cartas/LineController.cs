using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    [SerializeField] private Transform startPoint;
    private Transform endPoint;
    public int numberOfPoints = 50;
    public float height = 5f;

    private void Start()
    {
        lineRenderer.positionCount = numberOfPoints;
        endPoint = new GameObject().transform;
        DrawParabolicLine();
    }

    private void OnEnable()
    {
        lineRenderer.enabled = true;
        CardUISingleton.Instance.arrowInstance.SetActive(true);
    }

    private void OnDisable()
    {
        lineRenderer.enabled = false;
        CardUISingleton.Instance.arrowInstance.SetActive(false);
    }

    private void DrawParabolicLine()
    {
        Vector3[] linePositions = new Vector3[numberOfPoints];

        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = i / (float)(numberOfPoints - 1);
            float x = Mathf.Lerp(startPoint.position.x, endPoint.position.x, t);
            float z = Mathf.Lerp(startPoint.position.z, endPoint.position.z, t);
            float y = CalculateParabolicY(x - startPoint.position.x, height);

            Vector3 point = new Vector3(x, y, z);
            linePositions[i] = point;
        }

        linePositions[numberOfPoints - 1] = endPoint.position;
        lineRenderer.SetPositions(linePositions);
    }

    private float CalculateParabolicY(float x, float height)
    {
        return height + height * (-x * x / ((endPoint.position.x - startPoint.position.x) * (endPoint.position.x - startPoint.position.x)));
    }

    // Update is called whenever you want to change the parabolic line.
    private void Update()
    {
        DrawParabolicLine();
    }

    public void SetEndPoint(Vector3 pos)
    {
        endPoint.transform.position = pos;
    }
}
