using System;
using System.Collections;
using System.Threading;

namespace TestableDateTimeProvider {
    public class DateTimeProviderContext : IDisposable {
        static readonly ThreadLocal<Stack> threadScopedStack;

        static DateTimeProviderContext()
            => threadScopedStack = new ThreadLocal<Stack>(() => new Stack());

        public DateTimeProviderContext(DateTime now) {
            Now = now;
            threadScopedStack.Value.Push(this);
        }

        public DateTime Now { get; }

        public static DateTimeProviderContext Current {
            get {
                if (threadScopedStack.Value.Count == 0) {
                    return null;
                }

                return (DateTimeProviderContext)threadScopedStack.Value.Peek();
            }
        }

        public void Dispose() => threadScopedStack.Value.Pop();
    }
}
