using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace BlockSimilarNumbers
{
    [Activity(Label = "BlockSimilarNumbers", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public static bool BlockSimilarNumbers;
        private const string AppName = "BlockSimilarNumbers";
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
            HandlServiceLifeCycle();

            // Handle changes to checked state
            similarNumberChk.Click += (o, e) =>
            {
                BlockSimilarNumbers = similarNumberChk.Checked;
                WriteBlockSimilarNumbersToPreferences();
                HandlServiceLifeCycle();
            };
        }

        private void HandlServiceLifeCycle()
        {
            if (BlockSimilarNumbers)
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
            prefEditor.PutBoolean(BlockSimilarNumbersKeyName, BlockSimilarNumbers);
            prefEditor.Commit();
        }
    }
}