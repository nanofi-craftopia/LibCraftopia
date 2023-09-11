using Cysharp.Threading.Tasks;
using LibCraftopia.Container;
using LibCraftopia.Utils;
using Oc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCraftopia.Initialize
{
    public class InitializeManager : SingletonMonoBehaviour<InitializeManager>
    {
        public const string RegistryInit = "RegistryInit";
        public const string ModInit = "ModInit";
        public const string RegistryApply = "RegistryApply";

        private readonly List<string> tags = new List<string>();
        public IReadOnlyCollection<string> Tags { get => tags; }
        private readonly Dictionary<string, List<IInitializeHandler>> handlers = new Dictionary<string, List<IInitializeHandler>>();

        internal InitializeContext Context { get; private set; } = new InitializeContext();

        protected override void OnUnityAwake()
        {
            AddTagLast(RegistryInit);
            AddTagLast(ModInit);
            AddTagLast(RegistryApply);
        }

        public void AddTagLast(string tag)
        {
            tags.Add(tag);
            handlers.Add(tag, new List<IInitializeHandler>());
        }
        public void AddTagBefore(string tag, string target)
        {
            var targetIdx = tags.IndexOf(target);
            tags.Insert(targetIdx, tag);
        }
        public void AddTagAfter(string tag, string target)
        {
            var targetIdx = tags.IndexOf(target);
            tags.Insert(targetIdx + 1, tag);
        }

        public void AddHandler(string tag, IInitializeHandler handler)
        {
            var list = handlers[tag];
            list.Add(handler);
        }

        private int countHandlers()
        {
            return handlers.Values.Sum(handler => handler.Count);
        }

        internal async UniTask InitAsync()
        {
            Context.HandlerCount = countHandlers();
            foreach (var tag in tags)
            {
                var list = handlers[tag];
                foreach (var handler in list)
                {
                    await handler.Init(Context);
                    Context.CurrentHandlerIndex += 1;
                }
            }
        }
    }
}
