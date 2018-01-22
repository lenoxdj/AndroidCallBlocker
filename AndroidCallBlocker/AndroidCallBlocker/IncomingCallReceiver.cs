using System;
using Android.Content;
using Android.Runtime;
using Android.Telephony;

namespace AndroidCallBlocker
{
    [BroadcastReceiver]
    public class IncomingCallReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var tm = (TelephonyManager)context.GetSystemService(Context.TelephonyService);
            String incomingNumber = intent.Extras.GetString("incoming_number");

            if (MainActivity.BlockSimilarNumbers && (incomingNumber != null) && IsPhoneNumberSimilar(tm.Line1Number, incomingNumber))
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

            for(int i = 0; i < 7; i++)
            {
                if(myNum[i] == incomingNum[i])
                {
                    numSameDigits++;
                }
            }

            return numSameDigits > MaxAllowedSameDigits;
        }
    }
}