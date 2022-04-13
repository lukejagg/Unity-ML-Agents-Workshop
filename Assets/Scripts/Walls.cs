using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    [SerializeField] private Material badColor;
    [SerializeField] private Material goodColor;
    
    private MeshRenderer[] _wallMeshes;
    // Start is called before the first frame update
    private void Awake()
    {
        _wallMeshes = GetComponentsInChildren<MeshRenderer>();
    }

    public void SetWalls(bool success)
    {
        foreach (var wallMesh in _wallMeshes)
            wallMesh.material = success ? goodColor : badColor;
    }
}
