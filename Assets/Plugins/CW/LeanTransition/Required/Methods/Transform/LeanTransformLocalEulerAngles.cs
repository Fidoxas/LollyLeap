﻿using System;
using System.Collections.Generic;
using Lean.Transition.Method;
using UnityEngine;

namespace Lean.Transition.Method
{
    /// <summary>This component allows you to transition the specified Transform.localEulerAngles to the target value.</summary>
    [HelpURL(LeanTransition.HelpUrlPrefix + "LeanTransformLocalEulerAngles")]
    [AddComponentMenu(LeanTransition.MethodsMenuPrefix + "Transform/Transform.localEulerAngles" +
                      LeanTransition.MethodsMenuSuffix + "(LeanTransformLocalEulerAngles)")]
    public class LeanTransformLocalEulerAngles : LeanMethodWithStateAndTarget
    {
        public State Data;

        public override Type GetTargetType()
        {
            return typeof(Transform);
        }

        public override void Register()
        {
            PreviousState = Register(GetAliasedTarget(Data.Target), Data.Rotation, Data.Duration, Data.Ease);
        }

        public static LeanState Register(Transform target, Vector3 rotation, float duration,
            LeanEase ease = LeanEase.Smooth)
        {
            var state = LeanTransition.SpawnWithTarget(State.Pool, target);

            state.Rotation = rotation;
            state.Ease = ease;

            return LeanTransition.Register(state, duration);
        }

        [Serializable]
        public class State : LeanStateWithTarget<Transform>
        {
            public static Stack<State> Pool = new();

            [Tooltip("The rotation we will transition to.")]
            public Vector3 Rotation;

            [Tooltip("The ease method that will be used for the transition.")]
            public LeanEase Ease = LeanEase.Smooth;

            [NonSerialized] private Vector3 oldRotation;

            public override int CanFill => Target != null && Target.localEulerAngles != Rotation ? 1 : 0;

            public override void FillWithTarget()
            {
                Rotation = Target.localEulerAngles;
            }

            public override void BeginWithTarget()
            {
                oldRotation = Target.localEulerAngles;
            }

            public override void UpdateWithTarget(float progress)
            {
                var rotation = Vector3.LerpUnclamped(oldRotation, Rotation, Smooth(Ease, progress));

                Target.localRotation = Quaternion.Euler(rotation);
            }

            public override void Despawn()
            {
                Pool.Push(this);
            }
        }
    }
}

namespace Lean.Transition
{
    public static partial class LeanExtensions
    {
        public static Transform localEulerAnglesTransform(this Transform target, Vector3 position, float duration,
            LeanEase ease = LeanEase.Smooth)
        {
            LeanTransformLocalEulerAngles.Register(target, position, duration, ease);
            return target;
        }
    }
}