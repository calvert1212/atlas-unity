using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimation : MonoBehaviour
{
    [SerializeField] private Animator handAnimator;
    [SerializeField] private InputActionReference gripAction;
    [SerializeField] private InputActionReference triggerAction;

    private static readonly int triggerAnimation = Animator.StringToHash("Trigger");
    private static readonly int gripAnimation = Animator.StringToHash("Grip");

    void Update()
    {
        handAnimator.SetFloat(triggerAnimation, triggerAction.action.ReadValue<float>());
        handAnimator.SetFloat(gripAnimation, gripAction.action.ReadValue<float>());
    }
}
