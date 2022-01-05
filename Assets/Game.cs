using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshMakerNamespace;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public GameObject Target;//要裁剪的板子
    public GameObject Brush;//裁剪的笔刷


    GameObject newPanel;//裁剪后的板子
    CSG csgPanel;

    GameObject newCube;//被裁剪出来的方块
    CSG csgCube;

    void Start () {
        CSG.EPSILON = 1e-5f;
        csgPanel = getCsg(CSG.Operation.Subtract);//裁剪后的板子
        csgCube = getCsg(CSG.Operation.Intersection);//被裁剪出来的方块
    }

    //几何布尔运算
    CSG getCsg (CSG.Operation OperationType) {
        CSG csg;
        csg = new CSG();
        csg.Brush = Brush;
        csg.Target = Target;
        csg.OperationType = OperationType;
        csg.customMaterial = new Material(Shader.Find("Standard")); // 材质
        csg.useCustomMaterial = false; // 使用上面的材质来填充切口
        csg.hideGameObjects = true; // 操作后隐藏目标和画笔对象
        csg.keepSubmeshes = true; // 保持原始的网格和材质
        return csg;
    }

    void Update () {
        if (Input.GetMouseButtonDown(0)) {
            CreateNewObj();
        }
    }

    void CreateNewObj () {

        if (newPanel != null) {
            Destroy(newPanel);
        }
        //生成裁剪后的板子
        newPanel = csgPanel.PerformCSG();
        newPanel.name = "newPanel";
        newPanel.transform.SetParent(transform.parent);
        csgPanel.Target = newPanel;

        //生成被裁剪出来的方块
        newCube = csgCube.PerformCSG();
        newCube.name = "newCube";
        newCube.AddComponent<MeshCollider>().convex = true;
        newCube.GetComponent<Rigidbody>().isKinematic = false;
        newCube.transform.SetParent(transform.parent);
        csgCube.Target = newPanel;


        Brush.SetActive(true);
    }
}