using Runtime.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction onEnableInput = delegate { };
        public UnityAction onDisableInput = delegate { };
        public UnityAction onFirstTimeTouchTaken = delegate { };
        public UnityAction onInputTaken = delegate { };
        public UnityAction onInputReleased = delegate { };
        public UnityAction<HorizontalInputParams> onInputDragged = delegate { };
    }
}