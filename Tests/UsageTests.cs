using System;
using TestableDateTimeProvider;
using Xunit;

namespace Tests {
    public class UsageTests {
        [Fact]
        public void SimpleUsage() {
            var now = new DateTime(2019, 2, 27);
            using (new DateTimeProviderContext(now)) {
                Assert.Equal(now, DateTimeProvider.Instance.Now);
            }

            Assert.NotEqual(now, DateTimeProvider.Instance.Now);
        }

        [Fact]
        public void NestedUsage() {
            var now = new DateTime(2019, 2, 27);
            using (var context = new DateTimeProviderContext(now)) {
                Assert.Equal(now, DateTimeProvider.Instance.Now);
                var newNow = new DateTime(2019, 2, 28);
                using (new DateTimeProviderContext(newNow)) {
                    Assert.Equal(newNow, DateTimeProvider.Instance.Now);
                }

                Assert.Equal(now, DateTimeProvider.Instance.Now);
            }

            Assert.NotEqual(now, DateTimeProvider.Instance.Now);
        }
    }
}
