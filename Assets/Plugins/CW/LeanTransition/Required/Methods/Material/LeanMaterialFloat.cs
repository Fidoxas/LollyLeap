﻿using System;
using System.Collections.Generic;
using Lean.Transition.Method;
using UnityEngine;

namespace Lean.Transition.Method
{
    /// <summary>This component allows you to transition the specified <b>Material</b>'s <b>float</b> to the target value.</summary>
    [HelpURL(LeanTransition.HelpUrlPrefix + "LeanMaterialFloat")]
    [AddComponentMenu(LeanTransition.MethodsMenuPrefix + "Material/Material float" + LeanTransition.MethodsMenuSuffix +
                      "(LeanMaterialFloat)")]
    public class LeanMaterialFloat : LeanMethodWithStateAndTarget
    {
        public State Data;

        public override Type GetTargetType()
        {
            return typeof(Material);
        }

        public override void Register()
        {
            PreviousState = Register(GetAliasedTarget(Data.Target), Data.Property, Data.Value, Data.Duration,
                Data.Ease);
        }

        public static LeanState Register(Material target, string property, float value, float duration,
            LeanEase ease = LeanEase.Smooth)
        {
            var state = LeanTransition.SpawnWithTarget(State.Pool, target);

            state.Property = property;
            state.Value = value;
            state.Ease = ease;

            return LeanTransition.Register(state, duration);
        }

        [Serializable]
        public class State : LeanStateWithTarget<Material>
        {
            public static Stack<State> Pool = new();

            [Tooltip("The name of the float property in the shader.")]
            public string Property;

            [Tooltip("The value we will transition to.")]
            public float Value;

            [Tooltip("The ease method that will be used for the transition.")]
            public LeanEase Ease = LeanEase.Smooth;

            [NonSerialized] private float oldValue;

            public override int CanFill => Target != null && Target.GetFloat(Property) != Value ? 1 : 0;

            public override void FillWithTarget()
            {
                Value = Target.GetFloat(Property);
            }

            public override void BeginWithTarget()
            {
                oldValue = Target.GetFloat(Property);
            }

            public override void UpdateWithTarget(float progress)
            {
                Target.SetFloat(Property, Mathf.LerpUnclamped(oldValue, Value, Smooth(Ease, progress)));
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
        public static Material floatTransition(this Material target, string property, float value, float duration,
            LeanEase ease = LeanEase.Smooth)
        {
            LeanMaterialFloat.Register(target, property, value, duration, ease);
            return target;
        }
    }
}