using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCDIK : MonoBehaviour
{
    [SerializeField]
    private Transform _controlPoint;
    [SerializeField]
    private Transform[] _bones;
    [SerializeField]
    private int _interations;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Solve(true);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Solve(false);
        }
    }

    private void OnGUI()
    {
        GUILayout.Space(10);
        GUILayout.Label("点击A查看debug");
        GUILayout.Label("点击空格进行一次解算");
    }

    private void Solve(bool flag)
    {
        //for (int j = 0; j < _interations; j++)
        //{

        //}
        for (int i = _bones.Length - 1; i >= 0; i--)
        {
            Transform bone = _bones[i];
            Vector3 adjustDir = _controlPoint.position - bone.position;
            Vector3 rotateAxis = i == _bones.Length - 1 ? bone.right : (_bones[_bones.Length - 1].position - _bones[i].position);
            Debug.DrawRay(bone.position, rotateAxis, Color.red, 1f);
            Debug.DrawRay(bone.position, adjustDir, Color.green, 1f);
            if(flag)
            {
                bone.localRotation *= Quaternion.FromToRotation(rotateAxis, adjustDir);
            }
        }
    }
}
