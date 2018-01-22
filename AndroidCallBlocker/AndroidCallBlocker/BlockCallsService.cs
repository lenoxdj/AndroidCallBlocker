using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;

namespace AndroidCallBlocker
{
    [Service(Label = "BlockCallsService", DirectBootAware = true)]
    public class BlockCallsService : Service
    {
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            return StartCommandResult.Sticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            // This is a started service
            return null;
        }
    }
}