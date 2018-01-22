using Android.App;
using Android.Content;
using Android.OS;

namespace AndroidCallBlocker
{
    [Service]
    public class BlockCallsService : Service
    {
        private bool isStarted = false;

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            isStarted = true;

            return StartCommandResult.NotSticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnDestroy()
        {
            isStarted = false;
        }
    }
}