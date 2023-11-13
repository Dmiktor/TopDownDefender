using System;
using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private float timeBetweenPathTick = 0.5f;
    [SerializeField] private Transform aiTargetTransform;
    public event Action OnPathTick;

    private Coroutine pathAlgorithmTick;

    public Transform AiTargetTransform => aiTargetTransform;

    public void Init()
    {
        pathAlgorithmTick = StartCoroutine(PathAlgorithmTick());
    }

    public void Exit()
    {
       StopCoroutine(pathAlgorithmTick);
    }

    private IEnumerator PathAlgorithmTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenPathTick);
            OnPathTick?.Invoke();
        }
 
    }
}
