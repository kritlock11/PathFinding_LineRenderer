using System.Collections.Generic;
using UnityEngine;

public class MovingPoints : MonoBehaviour
{
    [SerializeField] private Transform _agent;
    [SerializeField] private Transform _point;
    private Queue<Vector3> _points = new Queue<Vector3>();
    private readonly Color _color = Color.red;
    private readonly int lengthOfLineRenderer = 2;
    private LineRenderer _lineRenderer;
    private Camera _camera;

    //private void Start()
    //{
    //    _camera = FindObjectOfType<Camera>();
    //    _lineRenderer = new GameObject("LineRenderer").AddComponent<LineRenderer>();
    //    _lineRenderer.startWidth = 0.1F;
    //    _lineRenderer.endWidth = 0.1F;
    //    _lineRenderer.material = new Material(Shader.Find("Mobile/Particles/Additive"));
    //    _lineRenderer.startColor = _color;
    //    _lineRenderer.endColor = _color;
    //    _lineRenderer.positionCount = lengthOfLineRenderer;
    //    _lineRenderer.SetPosition(0, _agent.position);
    //}

    //private void Update()
    //{
    //    if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit))
    //    {
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            DrawPoint(hit.point);
    //        }
    //    }
    //    _lineRenderer.SetPosition(1, hit.point);
    //}
    private void DrawPoint(Vector3 position)
    {
        var tempPoint = Instantiate(_point, position, Quaternion.identity);
        _points.Enqueue(tempPoint.position);
        _lineRenderer.SetPosition(0, tempPoint.position);
    }
}