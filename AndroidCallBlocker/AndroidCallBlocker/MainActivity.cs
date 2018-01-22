using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace AndroidCallBlocker
{
    [Activity(Label = "AndroidCallBlocker", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public static bool BlockSimilarNumbers;
        private const string AppName = "AndroidCallBlocker";
        private const string BlockSimilarNumbersKeyName = "BlockSimilarNumbers";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            CheckBox similarNumberChk = FindViewById<CheckBox>(Resource.Id.blockSimilarNumbersChk);

            // Get initial state of similarNumberChk
            similarNumberChk.Checked = Application.Context.GetSharedPreferences(AppName, FileCreationMode.Private)
                .GetBoolean(BlockSimilarNumbersKeyName, false);

            BlockSimilarNumbers = similarNumberChk.Checked;

            // Handle changes to checked state
            similarNumberChk.Click += (o, e) =>
            {
                BlockSimilarNumbers = similarNumberChk.Checked;
                var prefEditor = Application.Context.GetSharedPreferences(AppName, FileCreationMode.Private).Edit();
                prefEditor.PutBoolean(BlockSimilarNumbersKeyName, BlockSimilarNumbers);
                prefEditor.Commit();
            };
        }
    }
}