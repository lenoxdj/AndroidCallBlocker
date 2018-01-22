using System;
using Android.Content;
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

            if((incomingNumber != null) && IsPhoneNumberSimilar(tm.Line1Number, incomingNumber))
            {

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