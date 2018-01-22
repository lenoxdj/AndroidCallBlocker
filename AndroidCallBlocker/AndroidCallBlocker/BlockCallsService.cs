using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using System;

namespace AndroidCallBlocker
{
    [Service(Label = "BlockCallsService", DirectBootAware = true)]
    [IntentFilter(new String[] { "com.AndroidCallBlocker.BlockCallService" })]
    public class BlockCallsService : Service
    {
        public const string BROADCASTFILTER = "com.AndroidCallBlocker.intent.action.IncomingCallReciever";

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Application.Context.SendBroadcast(new Intent(BlockCallsService.BROADCASTFILTER));

            return StartCommandResult.Sticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            // This is a started service
            return null;
        }

        [IntentFilter(new[] { BROADCASTFILTER })]
        class IncomingCallReceiver : BroadcastReceiver
        {
            BlockCallsService service;
            public IncomingCallReceiver(BlockCallsService service) : base()
            {
                this.service = service;
            }

            public override void OnReceive(Context context, Intent intent)
            {
                var tm = (TelephonyManager)context.GetSystemService(Context.TelephonyService);
                String incomingNumber = intent.Extras.GetString("incoming_number");

                if ((incomingNumber != null) && IsPhoneNumberSimilar(tm.Line1Number, incomingNumber))
                {
                    IntPtr TelephonyManager_getITelephony = JNIEnv.GetMethodID(
                       tm.Class.Handle,
                       "getITelephony",
                       "()Lcom/android/internal/telephony/ITelephony;");

                    IntPtr telephony = JNIEnv.CallObjectMethod(tm.Handle, TelephonyManager_getITelephony);
                    IntPtr ITelephony_class = JNIEnv.GetObjectClass(telephony);
                    IntPtr ITelephony_endCall = JNIEnv.GetMethodID(
                            ITelephony_class,
                            "endCall",
                            "()Z");

                    JNIEnv.CallBooleanMethod(telephony, ITelephony_endCall);
                    JNIEnv.DeleteLocalRef(telephony);
                    JNIEnv.DeleteLocalRef(ITelephony_class);
                }
            }

            private static bool IsPhoneNumberSimilar(String myNum, String incomingNum)
            {
                const int MaxAllowedSameDigits = 4;
                int numSameDigits = 0;

                for (int i = 0; i < 7; i++)
                {
                    if (myNum[i] == incomingNum[i])
                    {
                        numSameDigits++;
                    }
                }

                return numSameDigits > MaxAllowedSameDigits;
            }
        }
    }
}