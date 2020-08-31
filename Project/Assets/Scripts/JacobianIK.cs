using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//约束在（0， 0， 1）平面
public class JacobianIK : MonoBehaviour
{
    [SerializeField]
    private Transform[] _bones;
    [SerializeField]
    private Transform _controlPoint;

    private Vector3 _lastControlPointPos;

    private void Start()
    {
        _lastControlPointPos = _controlPoint.position;
        Matrix4x4 mat1 = new Matrix4x4(new Vector4(1, 2, 3, 4), Vector4.zero, Vector4.zero, Vector4.zero);
        Matrix4x4 mat2 = new Matrix4x4(new Vector4(1, 2, 3, 4), Vector4.zero, Vector4.zero, Vector4.zero);
        Debug.Log(mat1 * mat2);
    }

    private void Update()
    {
        Solve();
    }

    private void Solve()
    {
        //线速度
        Vector3 v = _controlPoint.position - _lastControlPointPos;
    }

    private void JacobianMatrix()
    {
        float[,] mat = new float[_bones.Length, 3];

        for (int i = 0; i < _bones.Length; i++)
        {
            Vector3 angularVelocity = Vector3.Cross(new Vector3(0, 0, 1), i == 0 ? (_controlPoint.position) : (_controlPoint.position - _bones[i].position));
            mat[i, 0] = angularVelocity.x;
            mat[i, 1] = angularVelocity.y;
            mat[i, 2] = angularVelocity.z;
        }
    }

    //广义逆
    private void GeneralizedInverseMatrix()
    {

    }

    private float[,] MatMultiply(float[,] mat1, float[,] mat2)
    {
        if(mat1.GetLength(0) != mat2.GetLength(1) || mat1.GetLength(1) != mat2.GetLength(0))
        {
            throw new System.Exception("矩阵无法相乘");
        }

        float[,] mat = new float[mat1.GetLength(0), mat2.GetLength(0)];
        for (int i = 0; i < mat1.GetLength(0); i++)
        {
            for (int j = 0; j < mat2.GetLength(0); j++)
            {

            }
        }

        return null;
    }
}
