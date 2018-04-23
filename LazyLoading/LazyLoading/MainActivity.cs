using Android.App;
using Android.Widget;
using Android.OS;

namespace LazyLoading
{
    [Activity(Label = "LazyLoading", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            LinearLayout LazyLoadingContainer = FindViewById<LinearLayout>(Resource.Id.LazyloadingContainer);
            ScrollView LazyScroller = new ScrollView(this);
            LinearLayout ScrollContainer = new LinearLayout(this);
            System.Lazy<string> items = new System.Lazy<string>();
                
            LazyLoadingContainer.AddView(LazyScroller);
            ScrollContainer.Orientation = Orientation.Vertical;
            LazyScroller.AddView(ScrollContainer);
            
            for (int i = 0; i < 100; i++)
            {
                


                TextView text = new TextView(this);
                text.SetText(i.ToString(), TextView.BufferType.Normal);

                ScrollContainer.AddView(text);
            }
        }
    }
}

