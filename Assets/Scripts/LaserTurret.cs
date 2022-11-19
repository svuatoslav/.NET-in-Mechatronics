using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class LaserTurret : MonoBehaviour
{
    private List<Vector3> _vertex = new();
    private List<int> _triangl = new();
    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;
    private float _funHeight = 0f;
    private float _summAngle = 0f;
    private float _r = 0.5f;
    private const int _around = 360;
    private int _numbDivisions = 30;
    private delegate float _x_t(float a, float phi);
    private delegate float _z_t(float b, float phi);
    private delegate float _y_t(float phi);
    private delegate float _z_t_vert(float phi);

    private void Start()
    {
        //Generate();
        //GenerateHeadWeapon();
    }
    private void Generate()
    {
        _mesh = new();
        GetComponent<MeshFilter>().mesh = _mesh;
        GetComponent<MeshCollider>().sharedMesh = _mesh;
        _mesh.name = "Turret";
        _x_t xt = X_t;
        _z_t zt = Z_t;
        _vertex.Add(new Vector3(0f, _funHeight, 0f));
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_r, _summAngle), _funHeight, zt(_r, _summAngle)));
        }
        _funHeight += 0.1f;
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_r, _summAngle), _funHeight, zt(_r, _summAngle)));
        }
        _funHeight += 0.1f;
        _r -= 0.1f;
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_r, _summAngle), _funHeight, zt(_r, _summAngle)));
        }
        _funHeight += 0.5f;
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_r, _summAngle), _funHeight, zt(_r, _summAngle)));
        }
        _vertex.Add(new Vector3(0f, _funHeight, 0f));
        Debug.Log(_vertex.Count);
        //
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
        for (int i = 1; i < _numbDivisions; i++)
        {
        _triangl.Add(i);
        _triangl.Add(_numbDivisions+i);
        _triangl.Add(_numbDivisions+i+1);
        _triangl.Add(i);
        _triangl.Add(_numbDivisions+i+1);
        _triangl.Add(i+1);
        }
        _triangl.Add(_numbDivisions);
        _triangl.Add(_numbDivisions*2);
        _triangl.Add(_numbDivisions+1);
        _triangl.Add(_numbDivisions);
        _triangl.Add(_numbDivisions+1);
        _triangl.Add(1);
        for (int i = _numbDivisions + 1; i < _numbDivisions*2; i++)
        {
        _triangl.Add(i);
        _triangl.Add(_numbDivisions+i);
        _triangl.Add(_numbDivisions+i+1);
        _triangl.Add(i);
        _triangl.Add(_numbDivisions+i+1);
        _triangl.Add(i+1);
        }
        _triangl.Add(_numbDivisions*2);
        _triangl.Add(_numbDivisions*3);
        _triangl.Add(_numbDivisions*2+1);
        _triangl.Add(_numbDivisions*2);
        _triangl.Add(_numbDivisions*2+1);
        _triangl.Add(_numbDivisions+1);
        for (int i = _numbDivisions*2 + 1; i < _numbDivisions*3; i++)
        {
        _triangl.Add(i);
        _triangl.Add(_numbDivisions+i);
        _triangl.Add(_numbDivisions+i+1);
        _triangl.Add(i);
        _triangl.Add(_numbDivisions+i+1);
        _triangl.Add(i+1);
        }
        _triangl.Add(_numbDivisions*3);
        _triangl.Add(_numbDivisions*4);
        _triangl.Add(_numbDivisions*3+1);
        _triangl.Add(_numbDivisions*3);
        _triangl.Add(_numbDivisions*3+1);
        _triangl.Add(_numbDivisions*2+1);

        for (int i = _numbDivisions*3; i < _numbDivisions*4 - 1; i++)
        {
            _triangl.Add(i + 2);
            _triangl.Add(i + 1);
            _triangl.Add(_vertex.Count-1);
        }
        _triangl.Add(_numbDivisions*3+1);
        _triangl.Add(_vertex.Count-2);
        _triangl.Add(_vertex.Count-1);
        
        _triangles = new int[_triangl.Count];
        for (int i = 0; i < _triangl.Count; i++)
        {
            _triangles[i] = _triangl[i];
        }
        _mesh.triangles = _triangles;
        _mesh.vertices = _vertices;
        _mesh.RecalculateNormals();
        AssetDatabase.CreateAsset(_mesh, "Assets/Meshs/Turret.asset");
    }
    private void GenerateHeadWeapon()
    {
        _vertex = new();
        _triangl = new();
        _numbDivisions = 20;
        _mesh = new();
        _mesh.name = "TurretHead";
        _x_t xt = X_t;
        _z_t zt = Z_t;
        _y_t yt = Y_t;
        _z_t_vert ztvert = Z_t_vert;
        GetComponent<MeshFilter>().mesh = _mesh;
        GetComponent<MeshCollider>().sharedMesh = _mesh;
        // Head
        _vertex.Add(new Vector3(0f, _funHeight, 0f));
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_r, _summAngle), _funHeight, zt(_r, _summAngle)));
        }
        _funHeight += 0.1f;
        _r += 0.1f;
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_r, _summAngle), _funHeight, zt(_r, _summAngle)));
        }
        _funHeight += 0.3f;
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_r, _summAngle), _funHeight, zt(_r, _summAngle)));
        }
        _funHeight += 0.1f;
        _r -= 0.1f;
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_r, _summAngle), _funHeight, zt(_r, _summAngle)));
        }
        _vertex.Add(new Vector3(0f, _funHeight, 0f));
        //weapon
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(0.25f, yt(_summAngle) + _funHeight - 0.25f, ztvert(_summAngle)));
        }
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(1.25f, yt(_summAngle) + _funHeight - 0.25f, ztvert(_summAngle)));
        }
        _vertex.Add(new Vector3(1.25f, _funHeight - 0.25f, 0f));
        //
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
        for (int i = 1; i < _numbDivisions; i++)
        {
        _triangl.Add(i);
        _triangl.Add(_numbDivisions+i);
        _triangl.Add(_numbDivisions+i+1);
        _triangl.Add(i);
        _triangl.Add(_numbDivisions+i+1);
        _triangl.Add(i+1);
        }
        _triangl.Add(_numbDivisions);
        _triangl.Add(_numbDivisions*2);
        _triangl.Add(_numbDivisions+1);
        _triangl.Add(_numbDivisions);
        _triangl.Add(_numbDivisions+1);
        _triangl.Add(1);
        for (int i = _numbDivisions + 1; i < _numbDivisions*2; i++)
        {
        _triangl.Add(i);
        _triangl.Add(_numbDivisions+i);
        _triangl.Add(_numbDivisions+i+1);
        _triangl.Add(i);
        _triangl.Add(_numbDivisions+i+1);
        _triangl.Add(i+1);
        }
        _triangl.Add(_numbDivisions*2);
        _triangl.Add(_numbDivisions*3);
        _triangl.Add(_numbDivisions*2+1);
        _triangl.Add(_numbDivisions*2);
        _triangl.Add(_numbDivisions*2+1);
        _triangl.Add(_numbDivisions+1);
        for (int i = _numbDivisions*2 + 1; i < _numbDivisions*3; i++)
        {
        _triangl.Add(i);
        _triangl.Add(_numbDivisions+i);
        _triangl.Add(_numbDivisions+i+1);
        _triangl.Add(i);
        _triangl.Add(_numbDivisions+i+1);
        _triangl.Add(i+1);
        }
        _triangl.Add(_numbDivisions*3);
        _triangl.Add(_numbDivisions*4);
        _triangl.Add(_numbDivisions*3+1);
        _triangl.Add(_numbDivisions*3);
        _triangl.Add(_numbDivisions*3+1);
        _triangl.Add(_numbDivisions * 2 + 1);
        for (int i = _numbDivisions*3; i < _numbDivisions*4 - 1; i++)
        {
            _triangl.Add(i + 2);
            _triangl.Add(i + 1);
            _triangl.Add(_numbDivisions*4+1);
        }
        _triangl.Add(_numbDivisions*3+1);
        _triangl.Add(_numbDivisions*4);
        _triangl.Add(_numbDivisions*4+1);

        for (int i = _numbDivisions * 4+2; i < _numbDivisions * 5+1; i++)
        {
            _triangl.Add(i);
            _triangl.Add(_numbDivisions + i);
            _triangl.Add(_numbDivisions + i + 1);
            _triangl.Add(i);
            _triangl.Add(_numbDivisions + i + 1);
            _triangl.Add(i + 1);
        }
        _triangl.Add(_numbDivisions * 5 + 1);
        _triangl.Add(_numbDivisions * 6 + 1);
        _triangl.Add(_numbDivisions * 5 + 2);
        _triangl.Add(_numbDivisions * 5 + 1);
        _triangl.Add(_numbDivisions * 5 + 2);
        _triangl.Add(_numbDivisions * 4 + 2);

        for (int i = _numbDivisions * 5 + 1; i < _numbDivisions * 6 + 1; i++)
        {
            _triangl.Add(i + 2);
            _triangl.Add(i + 1);
            _triangl.Add(_vertex.Count-1);
        }
        _triangl.Add(_numbDivisions * 5 + 2);
        _triangl.Add(_vertex.Count - 2);
        _triangl.Add(_vertex.Count - 1);

        _triangles = new int[_triangl.Count];
        for (int i = 0; i < _triangl.Count; i++)
        {
            _triangles[i] = _triangl[i];
        }
        _mesh.triangles = _triangles;
        _mesh.vertices = _vertices;
        _mesh.RecalculateNormals();
        AssetDatabase.CreateAsset(_mesh, "Assets/Meshs/TurretHead.asset");
    }
    private float X_t(float a, float t) => a * Mathf.Cos(t * Mathf.Deg2Rad);
    private float Z_t(float b, float t) => b * Mathf.Sin(t * Mathf.Deg2Rad);
    private float Z_t_vert(float t) => 0.1f * Mathf.Cos(t * Mathf.Deg2Rad);
    private float Y_t(float t) => 0.1f * Mathf.Sin(t * Mathf.Deg2Rad);


    //private void OnDrawGizmos()
    //{
    //    if (_vertex == null)
    //        return;
    //    Gizmos.color = Color.red;
    //    for (int i = 0; i < _vertex.Count; i++)
    //    {
    //        Gizmos.DrawSphere(_vertex[i], 0.01f);
    //    }
    //}
}
