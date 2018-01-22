using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace AndroidCallBlocker
{
    [Activity(Label = "AndroidCallBlocker", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private bool blockSimilarNumbers;
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

            blockSimilarNumbers = similarNumberChk.Checked;
            HandlServiceLifeCycle();

            // Handle changes to checked state
            similarNumberChk.Click += (o, e) =>
            {
                blockSimilarNumbers = similarNumberChk.Checked;
                WriteBlockSimilarNumbersToPreferences();
                HandlServiceLifeCycle();
            };
        }

        private void HandlServiceLifeCycle()
        {
            if (blockSimilarNumbers)
            {
                StartBlockCallsService();
            }
            else
            {
                StopBlockCallsService();
            }
        }

        private void StartBlockCallsService()
        {
            StartService(new Intent(this, typeof(BlockCallsService)));
        }

        private void StopBlockCallsService()
        {
            StopService(new Intent(this, typeof(BlockCallsService)));
        }

        private void WriteBlockSimilarNumbersToPreferences()
        {
            var prefEditor = Application.Context.GetSharedPreferences(AppName, FileCreationMode.Private).Edit();
            prefEditor.PutBoolean(BlockSimilarNumbersKeyName, blockSimilarNumbers);
            prefEditor.Commit();
        }
    }
}