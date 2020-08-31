using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometricalAnalysisIK : MonoBehaviour
{
    [SerializeField]
    private Transform _bone01;
    [SerializeField]
    private Transform _bone02;
    [SerializeField]
    private Transform _controlPoint;
    [SerializeField]
    private bool _flip = false;

    private float l1;
    private float l2;
    // Start is called before the first frame update
    void Start()
    {
        l1 = Vector3.Distance(_bone02.position, _bone01.position);
        l2 = Vector3.Distance(_controlPoint.position, _bone02.position);
    }

    // Update is called once per frame
    void Update()
    {
        Solve();
    }

    private void Solve()
    {

        //控制点到根骨骼的向量（一般是第一根骨骼）
        Vector3 localTargetPosition = _controlPoint.position - _bone01.transform.position;
        localTargetPosition.z = 0f;

        float distanceMagnitude = localTargetPosition.magnitude;

        float angle0 = 0f;
        float angle1 = 0f;

        float sqrDistance = localTargetPosition.sqrMagnitude;//控制点到根骨骼的长度平方

        float sqrParentLength = (l1 * l1);//第一根骨骼的长度平方
        float sqrTargetLength = (l2 * l2);//第二根骨骼的长度平方

        float angle0Cos = (sqrDistance + sqrParentLength - sqrTargetLength) / (2f * l1 * distanceMagnitude);
        float angle1Cos = (sqrDistance - sqrParentLength - sqrTargetLength) / (2f * l1 * l2);

        if ((angle0Cos >= -1f && angle0Cos <= 1f) && (angle1Cos >= -1f && angle1Cos <= 1f))
        {
            angle0 = Mathf.Acos(angle0Cos) * Mathf.Rad2Deg;
            angle1 = Mathf.Acos(angle1Cos) * Mathf.Rad2Deg;
        }

        Vector3 rootBoneToTarget = _controlPoint.position - _bone01.transform.position;//Vector3.ProjectOnPlane(, _bone01.transform.forward);
        Debug.DrawRay(_controlPoint.position, rootBoneToTarget);
        float baseAngle = Mathf.Atan2(rootBoneToTarget.y, rootBoneToTarget.x) * Mathf.Rad2Deg;

        float flapSign = _flip ? -1 : 1;//控制关节的朝向
        _bone01.localRotation = Quaternion.Euler(0f, 0f, baseAngle + flapSign * -angle0);
        _bone02.localRotation = Quaternion.Euler(0f, 0f, flapSign * angle1);
        //Debug.Log(angle1);
    }
}
