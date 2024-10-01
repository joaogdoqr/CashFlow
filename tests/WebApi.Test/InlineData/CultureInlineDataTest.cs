using System.Collections;

namespace WebApi.Tests.InlineData
{
    public class CultureInlineDataTest : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new [] { "en" };
            yield return new [] { "pt-BR" };
            yield return new [] { "pt-PT" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
