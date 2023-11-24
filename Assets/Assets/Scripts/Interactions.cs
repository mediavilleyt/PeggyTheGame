using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    [HideInInspector] public Transform distanceActivator, lookAtActivator;
    public float distance = 3;
    [HideInInspector] public Transform activator;
    [HideInInspector] public bool activeState = false;
    [HideInInspector] public CanvasGroup target;
    [HideInInspector] public float alpha;
    [HideInInspector] public TMP_Text text;

    public virtual void Start()
    {
        distanceActivator = GetComponent<Transform>();
        target = GetComponentInChildren<CanvasGroup>();
        alpha = activeState ? 1 : -1;
        activator = Camera.main.transform;
        foreach (var child in GetComponentsInChildren<TMP_Text>())
        {
            if (child.name == "text")
            {
                text = child;
                break;
            }
        }
    }

    public virtual void Update()
    {
        if (!activeState)
        {
            if (IsTargetNear())
            {
                alpha = 1;
                activeState = true;
            }
        }
        else
        {
            if (!IsTargetNear())
            {
                alpha = -1;
                activeState = false;
            }
        }
        target.alpha = Mathf.Clamp01(target.alpha + alpha * Time.deltaTime);

        if (activeState)
        {
            ActiveState();
        }
    }

    public bool IsTargetNear()
    {
        var distanceDelta = distanceActivator.position - activator.position;
        if (distanceDelta.sqrMagnitude < distance * distance)
        {
            if (lookAtActivator != null)
            {
                var lookAtActivatorDelta = lookAtActivator.position - activator.position;
                if (Vector3.Dot(activator.forward, lookAtActivatorDelta.normalized) > 0.95f)
                    return true;
            }
            var lookAtDelta = target.transform.position - activator.position;
            if (Vector3.Dot(activator.forward, lookAtDelta.normalized) > 0.95f)
                return true;
        }
        return false;
    }

    public virtual void ActiveState()
    {

    }
}
