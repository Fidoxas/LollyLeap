using System;
using System.Collections.Generic;
using Lean.Transition.Method;
using UnityEngine;
using TARGET = UnityEngine.AudioSource;

namespace Lean.Transition.Method
{
    /// <summary>This component allows you to transition the AudioSource's panStereo value.</summary>
    [HelpURL(LeanTransition.HelpUrlPrefix + "LeanAudioSourcePanStereo")]
    [AddComponentMenu(LeanTransition.MethodsMenuPrefix + "AudioSource/AudioSource.panStereo" +
                      LeanTransition.MethodsMenuSuffix + "(LeanAudioSourcePanStereo)")]
    public class LeanAudioSourcePanStereo : LeanMethodWithStateAndTarget
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

            [Tooltip("The panStereo value will transition to this.")] [Range(-1.0f, 1.0f)]
            public float Value;

            [Tooltip("This allows you to control how the transition will look.")]
            public LeanEase Ease = LeanEase.Smooth;

            [NonSerialized] private float oldValue;

            public override int CanFill => Target != null && Target.panStereo != Value ? 1 : 0;

            public override void FillWithTarget()
            {
                Value = Target.panStereo;
            }

            public override void BeginWithTarget()
            {
                oldValue = Target.panStereo;
            }

            public override void UpdateWithTarget(float progress)
            {
                Target.panStereo = Mathf.LerpUnclamped(oldValue, Value, Smooth(Ease, progress));
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
        public static TARGET panStereoTransition(this TARGET target, float value, float duration,
            LeanEase ease = LeanEase.Smooth)
        {
            LeanAudioSourcePanStereo.Register(target, value, duration, ease);
            return target;
        }
    }
}