using System;
using System.Collections;
using System.Collections.Generic;
using PlatformerCookbook.Scripts;
using Unity.Collections;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    public List<Transform> targetPoints;
    public float speed;
    public FloatingPlatformMode mode;
    public Color colorOfPathInEditor = Color.white;
    
    private int _currentPointIndex;
    private int _direction;

    private Rigidbody _rigidbody;

    private const float DistanceToSwitchPoint = 0.001f;
    private void Start()
    {
        InitPlatform();
    }

    private void InitPlatform()
    {
        _direction = mode == FloatingPlatformMode.Bounce ? -1 : 1;
        _rigidbody = GetComponent<Rigidbody>();
        
        if (!_rigidbody)
        {
            _rigidbody = gameObject.AddComponent<Rigidbody>();
        }
        _rigidbody.isKinematic = true;

        _currentPointIndex = 0;
        
        if (targetPoints.Count == 0) return;

        _rigidbody.position = targetPoints[_currentPointIndex].position;
    }

    private void FixedUpdate()
    {
        if (_currentPointIndex > targetPoints.Count - 1 || _currentPointIndex < 0) return;
        var currentPoint = targetPoints[_currentPointIndex];
        
        if (Vector3.Distance(_rigidbody.position, currentPoint.position) < DistanceToSwitchPoint)
        {
            
                if(mode==FloatingPlatformMode.Bounce)
                {
                    if (_currentPointIndex == targetPoints.Count - 1 || _currentPointIndex == 0)
                        _direction *= -1;

                    _currentPointIndex += _direction;
                }
                else
                {
                    _currentPointIndex += _direction;
                    _currentPointIndex %= targetPoints.Count;
                }
        }
        
        
        _rigidbody.MovePosition(Vector3.MoveTowards(
            _rigidbody.position,
            currentPoint.position, Time.fixedDeltaTime * speed));
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (targetPoints == null) return;
        if (targetPoints.Find(tp => tp == null)) {
            return;
        }

        Gizmos.color = colorOfPathInEditor;

        for (var i = 0; i < targetPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(targetPoints[i].position, targetPoints[i + 1].position);
        }

        if (mode != FloatingPlatformMode.Cycle) return;
        if (targetPoints.Count < 3) return;
        Gizmos.DrawLine(targetPoints[0].position, targetPoints[targetPoints.Count - 1].position);

        #endif
    }

    public void ToggleEnabled()
    {
        enabled = !enabled;
    }
}
