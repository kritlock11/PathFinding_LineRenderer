
using UnityEngine;
using UnityEngine.AI;

namespace Tests
{
    public class TestNavMesh : MonoBehaviour
    {
	    public Transform TargetFirst;
	    public Transform TargetSecond;

	    private NavMeshPath _path;
	    private float _elapsed = 0;

	    private void Start()
	    {
			_path = new NavMeshPath();
		    _elapsed = 0;
		}

	    private void Update()
	    {
		    //_elapsed += Time.deltaTime;
		    //if (_elapsed > 1)
		    //{
			   // _elapsed = -1;
			   // NavMesh.CalculatePath(TargetFirst.position, TargetSecond.position, NavMesh.AllAreas, _path);
		    //}

		    //for (var i = 0; i < _path.corners.Length - 1; i++)
		    //{
			   // Debug.DrawLine(_path.corners[i], _path.corners[i+1], Color.red);
		    //}
	    }
    }
}
