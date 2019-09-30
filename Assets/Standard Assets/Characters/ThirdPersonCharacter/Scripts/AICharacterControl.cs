using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        private Camera _camera;
        [SerializeField] private DestroySphere _point;
        private NavMeshPath _path;
        private NavMeshAgent _agent;
        private ThirdPersonCharacter _character;
        private RaycastHit _hit;
        private Queue<Vector3> _points;
        private readonly Color _colorRed = Color.red;
        private readonly Color _colorGreen = Color.green;
        private Vector3[] _concatArray;
        private Vector3 _startPoint;
        private LineRenderer _lineRenderer;
        private Queue<GameObject> _currentPoint;
        private void Start()
        {
            _camera = Camera.main;
            _agent = GetComponent<NavMeshAgent>();
            _character = GetComponent<ThirdPersonCharacter>();
            _path = new NavMeshPath();
            _startPoint = transform.position;
            _concatArray = new Vector3[0];
            _points = new Queue<Vector3>();
            _currentPoint = new Queue<GameObject>();

            _lineRenderer = new GameObject("LineRenderer").AddComponent<LineRenderer>();
            _lineRenderer.startWidth = 0.1F;
            _lineRenderer.endWidth = 0.1F;
            _lineRenderer.material = new Material(Shader.Find("Mobile/Particles/Additive"));
            _lineRenderer.startColor = _colorRed;
            _lineRenderer.endColor = _colorGreen;
        }

        private void Update()
        {
            MouseDraw();
        }

        private void MouseDraw()
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out _hit, 100f))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    var tempPoint = Instantiate(_point, _hit.point, Quaternion.identity);
                    _currentPoint.Enqueue(tempPoint.gameObject);
                    tempPoint.OnFinishCnange += DestroyPoint;
                    _startPoint = tempPoint.transform.position;

                    for (int i = 0; i < _path.corners.Length; i++)
                    {
                        _points.Enqueue(_path.corners[i]);
                    }
                }
            }

            NavMesh.CalculatePath(_startPoint, _hit.point, NavMesh.AllAreas, _path);
            _concatArray = ConcatArrays(_points.ToArray(), _path.corners);
            _lineRenderer.positionCount = _concatArray.Length;

            if (_points.Count > 0)
            {
                if (_points.Count > 1 && Vector3.Distance(_points.ToArray()[1], transform.position) <= .1f)
                {
                    _points.Dequeue();
                }
                _lineRenderer.SetPositions(_concatArray);
                _lineRenderer.SetPosition(0, transform.position);
            }
            else
            {
                _lineRenderer.positionCount = _path.corners.Length;
                _lineRenderer.SetPositions(_path.corners);
            }

            if (_points.Count > 1)
            {
                MoveToPoint(_points.ToArray()[1]);
            }
        }

        private void MoveToPoint(Vector3 pointTransform)
        {
            _agent.SetDestination(pointTransform);
            _character.Move(_agent.remainingDistance > _agent.stoppingDistance ? _agent.desiredVelocity : Vector3.zero, false, false);
        }

        private void DestroyPoint(GameObject GO)
        {
            var go = _currentPoint.Peek();
            if (go != null && go == GO)
            {
                go.GetComponent<DestroySphere>().OnFinishCnange -= DestroyPoint;
                _currentPoint.Dequeue();
                Destroy(go);
            }
        }

        public static T[] ConcatArrays<T>(params T[][] list)
        {
            var result = new T[list.Sum(a => a.Length)];
            int offset = 0;
            for (int x = 0; x < list.Length; x++)
            {
                list[x].CopyTo(result, offset);
                offset += list[x].Length;
            }
            return result;
        }
    }
}
