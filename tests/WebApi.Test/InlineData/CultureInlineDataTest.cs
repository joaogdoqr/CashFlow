using System.Collections;

namespace WebApi.Tests.InlineData
{
    public class CultureInlineDataTest : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new [] { "fr" };
            yield return new [] { "pt-PT" };
            yield return new [] { "pt-BR" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
