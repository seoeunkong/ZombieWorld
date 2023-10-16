using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    //몬스터 검색 기능 

    public float scanRange; //범위 
    public LayerMask targetLayer; //레이어
    public RaycastHit2D[] targets; //스캔 결과 배열
    public Transform nearestTarget; //가장 가까운 타겟

    void FixedUpdate()
    {
        //캐스팅 시작 위치, 원의 반지름, 캐스팅 방향, 캐스팅 길이, 대상 레이어
        //캐스팅을 쏘지 않을 예정이기 때문에 캐스팅 방향과 길이는 0으로 설정
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();
    }

    Transform GetNearest() //targets내에 플레이어와 가장 가까운 위치에 있는 몬스터 찾기 
    {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos); //myPos와 targetPos 간의 거리 계산

            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
}
