using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private const float DefaultTargetWeight = 1f;
    private const float DefaultTargetRadius = 2f;
    public CinemachineTargetGroup CinemachineTargetGroup;
    public SphereCollider SphereCollider;
    public List<Target> targets = new List<Target>();
    public Target currentTarget;

    public bool SelectTarget()
    {
        if (targets.Count == 0)
        {
            return false;
        }

        currentTarget = targets[0];
        CinemachineTargetGroup.AddMember(currentTarget.transform, DefaultTargetWeight, DefaultTargetRadius);
        return true;
    }

    public void CancelTarget()
    {
        CinemachineTargetGroup.RemoveMember(currentTarget.transform);
        currentTarget = null;
    }

    public void RemoveTarget(Target target)
    {
        if (target == currentTarget)
        {
            CancelTarget();
        }

        targets.Remove(target);
        target.OnTargetDestroyed -= RemoveTarget; // 타겟이 파괴될 때 RemoveTarget 메서드 호출
    }

    private void OnTriggerEnter(Collider other)
    {
        Target target = other.GetComponent<Target>();
        if (target == null)
        {
            return;
        }
        else
        {
            target.OnTargetDestroyed += RemoveTarget; // 타겟이 파괴될 때 RemoveTarget 메서드 호출
            targets.Add(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Target target = other.GetComponent<Target>();
        if (target == null)
        {
            return;
        }
        else
        {
            RemoveTarget(target);
        }
    }
}
