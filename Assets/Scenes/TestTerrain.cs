using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestTerrain : MonoBehaviour
{

    public Terrain terrain;
    public Transform planeNormal;

    private Camera m_camera;
    private float[,] m_heightsRecord;

    // Start is called before the first frame update
    void Start()
    {
        m_heightsRecord = terrain.terrainData.GetHeights(0,0, terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution);
        m_camera = Camera.main;
        //TerrainUtil.Flatten(terrain,100);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) {
            Plane plane = new Plane(planeNormal.up, planeNormal.position);
            Vector3 point = CameraUtil.GetScreenRayCastToPlanePoint(Input.mousePosition, m_camera,plane);
            TerrainUtil.Sink(terrain, point, 100f, 30, true);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            terrain.terrainData.SetHeights(0,0, m_heightsRecord);
        }
    }

    private void OnDisable () {
        
    }
}
