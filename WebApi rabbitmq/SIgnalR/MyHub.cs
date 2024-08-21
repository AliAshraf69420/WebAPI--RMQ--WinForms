using Microsoft.AspNet.SignalR;
using System.Runtime.CompilerServices;

namespace WebApi_rabbitmq.SIgnalR
{
    public class MyHub : Hub
    {
        public async IAsyncEnumerable<int> Counter(
            int count,
                int delay,
                [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            for (var i = 0; i < count; i++) {
                cancellationToken.ThrowIfCancellationRequested();
            yield return i;
                await Task.Delay(delay).ConfigureAwait(false);
            }

        }
    }
}
