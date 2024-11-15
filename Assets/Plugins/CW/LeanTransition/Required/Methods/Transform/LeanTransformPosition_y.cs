using System;
using System.Collections.Generic;
using Lean.Transition.Method;
using UnityEngine;
using UnityEngine.Serialization;
using TARGET = UnityEngine.Transform;

namespace Lean.Transition.Method
{
    /// <summary>This component allows you to transition the Transform's position.y value.</summary>
    [HelpURL(LeanTransition.HelpUrlPrefix + "LeanTransformPosition_Y")]
    [AddComponentMenu(LeanTransition.MethodsMenuPrefix + "Transform/Transform.position.y" +
                      LeanTransition.MethodsMenuSuffix + "(LeanTransformPosition_Y)")]
    public class LeanTransformPosition_Y : LeanMethodWithStateAndTarget
    {
        public State Data;

        public override Type GetTargetType()
        {
            return typeof(TARGET);
        }

        public override void Register()
        {
            PreviousState = Register(GetAliasedTarget(Data.Target), Data.Value, Data.Duration, Data.Ease);
        }

        public static LeanState Register(TARGET target, float value, float duration, LeanEase ease = LeanEase.Smooth)
        {
            var state = LeanTransition.SpawnWithTarget(State.Pool, target);

            state.Value = value;

            state.Ease = ease;

            return LeanTransition.Register(state, duration);
        }

        [Serializable]
        public class State : LeanStateWithTarget<TARGET>
        {
            public static Stack<State> Pool = new();

            [Tooltip("The position value will transition to this.")] [FormerlySerializedAs("Position")]
            public float Value;

            [Tooltip("This allows you to control how the transition will look.")]
            public LeanEase Ease = LeanEase.Smooth;

            [NonSerialized] private float oldValue;

            public override int CanFill => Target != null && Target.position.y != Value ? 1 : 0;

            public override void FillWithTarget()
            {
                Value = Target.position.y;
            }

            public override void BeginWithTarget()
            {
                oldValue = Target.position.y;
            }

            public override void UpdateWithTarget(float progress)
            {
                var vector = Target.position;

                vector.y = Mathf.LerpUnclamped(oldValue, Value, Smooth(Ease, progress));

                Target.position = vector;
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
        public static TARGET positionTransition_y(this TARGET target, float value, float duration,
            LeanEase ease = LeanEase.Smooth)
        {
            LeanTransformPosition_Y.Register(target, value, duration, ease);
            return target;
        }
    }
}