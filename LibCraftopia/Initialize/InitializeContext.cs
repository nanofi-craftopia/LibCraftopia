using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LibCraftopia.Initialize
{
    public class InitializeContext
    {
        private string message = string.Empty;
        public string Message { get => message; set { message = value; onValueChanged(); } }
        private float progress = 0.0f;
        public float Progress { get => progress; set { progress = value; onValueChanged(); } }

        public int HandlerCount { get; internal set; } = 1;
        private int currentHandlerIndex = 0;
        public int CurrentHandlerIndex { get => currentHandlerIndex; set { currentHandlerIndex = value; progress = 0.0f; onValueChanged(); } }

        public event Action<string, float> OnValueChanged;

        private void onValueChanged()
        {
            var prog = (currentHandlerIndex + progress) / (float)HandlerCount;
            OnValueChanged?.Invoke(message, prog);
        }
    }
}
