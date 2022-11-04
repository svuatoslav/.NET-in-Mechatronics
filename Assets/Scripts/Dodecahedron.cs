using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class Dodecahedron : MonoBehaviour
{
    private List<Vector3> _vertex = new();
    private List<int> _triangl = new();
    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;
    private float _funHeight = 0.5f;
    private float _summAngle = 0f;
    private const int _around = 360;
    private const int _numbDivisions = 5;
    private const float _r = 1f;
    private delegate float _x_t(float a, float phi);
    private delegate float _z_t(float b, float phi);
    void Start()
    {
        Generate();
    }
    private void Generate()
    {
        _mesh = new();
        GetComponent<MeshFilter>().mesh = _mesh;
        _mesh.name = "Turret";
        _x_t xt = X_t;
        _z_t zt = Z_t;
        _vertex.Add(new Vector3(0f, 0f, 0f));
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_r, _summAngle), 0f, zt(_r, _summAngle)));
        }
        _vertices = new Vector3[_vertex.Count];
        for (int i = 0; i < _vertex.Count; i++)
        {
            _vertices[i] = _vertex[i];
        }
        _mesh.vertices = _vertices;
        for (int i = 0; i < _numbDivisions - 1; i++)
        {
            _triangl.Add(0);
            _triangl.Add(i + 1);
            _triangl.Add(i + 2);
        }
        _triangl.Add(0);
        _triangl.Add(_numbDivisions);
        _triangl.Add(1);
        _triangles = new int[_triangl.Count];
        for (int i = 0; i < _triangl.Count; i++)
        {
            _triangles[i] = _triangl[i];
        }
        _mesh.triangles = _triangles;
        _mesh.vertices = _vertices;
        GetComponent<MeshCollider>().sharedMesh = _mesh;
        //AssetDatabase.CreateAsset(_mesh, "Assets/Meshs/dodecahedron.asset");
        _mesh.RecalculateNormals();
    }
    private float X_t(float a, float t) => a * Mathf.Cos(t * Mathf.Deg2Rad);
    private float Z_t(float b, float t) => b * Mathf.Sin(t * Mathf.Deg2Rad);
    private void OnDrawGizmos()
    {
        if (_vertex == null)
            return;
        Gizmos.color = Color.red;
        for (int i = 0; i < _vertex.Count; i++)
        {
            Gizmos.DrawSphere(_vertex[i], 0.1f);
        }
    }
}
