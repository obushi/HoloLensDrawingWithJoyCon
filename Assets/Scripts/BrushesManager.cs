using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class BrushesManager : MonoBehaviour {

    GameObject brush;
    GameObject currentBrush;
    List<GameObject> brushes;

    LineRenderer lineRenderer;
    MeshRenderer meshRenderer;
    List<Vector3> vertexPositions;
    [SerializeField] uint maxVertexCount = 2000;
    Vector3 oldPosition;
    float hue = 0.0f;
    float brushSize = 0.01f;


    // Use this for initialization
    void Start () {
        brush = (GameObject)Resources.Load("Prefabs/Brush");
        brushes = new List<GameObject>();
        InitializeBrush();
    }
	
	// Update is called once per frame
	void Update () {
        if (vertexPositions.Count >= maxVertexCount)
        {
            vertexPositions.RemoveAt(0);
        }

        if (Vector3.Distance(oldPosition, currentBrush.transform.position) > 0.03f)
        {
            vertexPositions.Add(currentBrush.transform.position);
            lineRenderer.numPositions = vertexPositions.Count;
            lineRenderer.SetPositions(vertexPositions.ToArray());
            oldPosition = currentBrush.transform.position;
        }
    }

    void InitializeBrush()
    {
        currentBrush = Instantiate(brush, Camera.main.transform.position + Camera.main.transform.forward * 1f, Quaternion.identity, transform);
        lineRenderer = currentBrush.GetComponent<LineRenderer>();
        meshRenderer = currentBrush.GetComponent<MeshRenderer>();
        vertexPositions = new List<Vector3>();
        oldPosition = Camera.main.transform.forward;
        ApplyBrushColor();
        ApplyBrushSize();
        brushes.Add(currentBrush);
    }

    void DestroyBrush()
    {
        if (GazeManager.Instance.HitObject != null)
        {
            brushes.Remove(GazeManager.Instance.HitObject);
            Destroy(GazeManager.Instance.HitObject);
        }
    }

    void IncreaseBrushSize(float amount)
    {
        if (brushSize + amount >= 0.4f)
        {
            brushSize = 0.4f;
        }

        else
        {
            brushSize += amount;
        }

        ApplyBrushSize();
    }

    void DecreaseBrushSize(float amount)
    {
        if (brushSize - amount <= 0.02f)
        {
            brushSize = 0.02f;
        }

        else
        {
            brushSize -= amount;
        }

        ApplyBrushSize();
    }

    void ApplyBrushSize()
    {
        currentBrush.transform.localScale = brushSize * Vector3.one;
        lineRenderer.startWidth = lineRenderer.endWidth = brushSize * 0.5f;
    }

    void PickNextColor()
    {
        hue = hue + 0.1f > 1.0 ? 0.0f : hue + 0.1f;
        ApplyBrushColor();
    }

    void PickPreviousColor()
    {
        hue = hue - 0.1f < 0.0f ? 1.0f : hue - 0.1f;
        ApplyBrushColor();
    }

    void ApplyBrushColor()
    {
        Color brushColor = Color.HSVToRGB(hue, 1.0f, 1.0f);
        lineRenderer.startColor = lineRenderer.endColor = lineRenderer.material.color = brushColor;
        meshRenderer.material.color = brushColor;
    }
}
