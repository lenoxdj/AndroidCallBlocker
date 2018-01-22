using Android.App;
using Android.Widget;
using Android.OS;

namespace AndroidCallBlocker
{
    [Activity(Label = "AndroidCallBlocker", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public bool BlockSimilarNumbers;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            CheckBox similarNumberChk = FindViewById<CheckBox>(Resource.Id.blockSimilarNumbersChk);

            similarNumberChk.Click += (o, e) =>
            {
                BlockSimilarNumbers = similarNumberChk.Checked;
            };
        }
    }
}