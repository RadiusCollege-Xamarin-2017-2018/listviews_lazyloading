using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System.Collections.Generic;

namespace LazyLoading
{
    [Activity(Label = "LazyLoading", MainLauncher = true)]
    public class MainActivity : Activity
    {
        static string[] countryList = new string[]
            {
                "Afghanistan","Albania","Algeria","American Samoa","Andorra",
                "Angola","Anguilla","Antarctica","Antigua and Barbuda","Argentina",
                "Armenia","Aruba","Australia","Austria","Azerbaijan",
                "Bahrain","Bangladesh","Barbados","Belarus","Belgium",
                "Belize","Benin","Bermuda","Bhutan","Bolivia",
                "Bosnia and Herzegovina","Botswana","Bouvet Island","Brazil","British Indian Ocean Territory",
                "British Virgin Islands","Brunei","Bulgaria","Burkina Faso","Burundi",
                "Cote d'Ivoire","Cambodia","Cameroon","Canada","Cape Verde",
                "Cayman Islands","Central African Republic","Chad","Chile","China",
                "Christmas Island","Cocos (Keeling) Islands","Colombia","Comoros","Congo",
                "Cook Islands","Costa Rica","Croatia","Cuba","Cyprus","Czech Republic",
                "Democratic Republic of the Congo","Denmark","Djibouti","Dominica","Dominican Republic",
                "East Timor","Ecuador","Egypt","El Salvador","Equatorial Guinea","Eritrea",
                "Estonia","Ethiopia","Faeroe Islands","Falkland Islands","Fiji","Finland",
                "Former Yugoslav Republic of Macedonia","France","French Guiana","French Polynesia",
                "French Southern Territories","Gabon","Georgia","Germany","Ghana","Gibraltar",
                "Greece","Greenland","Grenada","Guadeloupe","Guam","Guatemala","Guinea","Guinea-Bissau",
                "Guyana","Haiti","Heard Island and McDonald Islands","Honduras","Hong Kong","Hungary",
                "Iceland","India","Indonesia","Iran","Iraq","Ireland","Israel","Italy","Jamaica",
                "Japan","Jordan","Kazakhstan","Kenya","Kiribati","Kuwait","Kyrgyzstan","Laos",
                "Latvia","Lebanon","Lesotho","Liberia","Libya","Liechtenstein","Lithuania","Luxembourg",
                "Macau","Madagascar","Malawi","Malaysia","Maldives","Mali","Malta","Marshall Islands",
                "Martinique","Mauritania","Mauritius","Mayotte","Mexico","Micronesia","Moldova",
                "Monaco","Mongolia","Montserrat","Morocco","Mozambique","Myanmar","Namibia",
                "Nauru","Nepal","Netherlands","Netherlands Antilles","New Caledonia","New Zealand",
                "Nicaragua","Niger","Nigeria","Niue","Norfolk Island","North Korea","Northern Marianas",
                "Norway","Oman","Pakistan","Palau","Panama","Papua New Guinea","Paraguay","Peru",
                "Philippines","Pitcairn Islands","Poland","Portugal","Puerto Rico","Qatar",
                "Reunion","Romania","Russia","Rwanda","Sqo Tome and Principe","Saint Helena",
                "Saint Kitts and Nevis","Saint Lucia","Saint Pierre and Miquelon",
                "Saint Vincent and the Grenadines","Samoa","San Marino","Saudi Arabia","Senegal",
                "Seychelles","Sierra Leone","Singapore","Slovakia","Slovenia","Solomon Islands",
                "Somalia","South Africa","South Georgia and the South Sandwich Islands","South Korea",
                "Spain","Sri Lanka","Sudan","Suriname","Svalbard and Jan Mayen","Swaziland","Sweden",
                "Switzerland","Syria","Taiwan","Tajikistan","Tanzania","Thailand","The Bahamas",
                "The Gambia","Togo","Tokelau","Tonga","Trinidad and Tobago","Tunisia","Turkey",
                "Turkmenistan","Turks and Caicos Islands","Tuvalu","Virgin Islands","Uganda",
                "Ukraine","United Arab Emirates","United Kingdom",
                "United States","United States Minor Outlying Islands","Uruguay","Uzbekistan",
                "Vanuatu","Vatican City","Venezuela","Vietnam","Wallis and Futuna","Western Sahara",
                "Yemen","Yugoslavia","Zambia","Zimbabwe"
            };
        
        int addedTimes = 0;
        int maxPerLoad = 30;
        List<string> history = new List<string>();

    protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            history.AddRange(countryList);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            LinearLayout LazyLoadingContainer = FindViewById<LinearLayout>(Resource.Id.LazyloadingContainer);
            ScrollView LazyScroller = new ScrollView(this);
            LinearLayout ScrollContainer = new LinearLayout(this);
            System.Lazy<string> items = new System.Lazy<string>();




            LazyLoadingContainer.AddView(LazyScroller);
            ScrollContainer.Orientation = Orientation.Vertical;
            LazyScroller.AddView(ScrollContainer);
            LazyScroller.ScrollChange += OnScroll;

            int startoffset = (addedTimes * maxPerLoad);
            int endoffset = ((addedTimes + 1) * maxPerLoad);

            for (int i = startoffset; i < endoffset; i++)
            {
                TextView text = new TextView(this);
                text.SetText(countryList[i], TextView.BufferType.Normal);

                ScrollContainer.AddView(text);
            }
            addedTimes += 1;

        }

        private void OnScroll(object sender, View.ScrollChangeEventArgs e)
        {
            try
            {
                ScrollView scroll = (ScrollView)sender;
                LinearLayout itemContainter = (LinearLayout)scroll.GetChildAt(0);
                

                if (scroll.ScrollY >= scroll.GetChildAt(0).Height - scroll.Height)
                {
                    if(addedTimes >= 1)
                    {
                        int startoffset = (addedTimes * maxPerLoad);
                        int endoffset = ((addedTimes + 1) * maxPerLoad);
                        if (!(startoffset > countryList.Length) && !(endoffset > countryList.Length) && startoffset < endoffset)
                        {
                            itemContainter.RemoveAllViewsInLayout();
                            for (int i = startoffset; i < endoffset; i++)
                            {
                                TextView text = new TextView(this);
                                text.SetText(countryList[i], TextView.BufferType.Normal);
                                itemContainter.AddView(text);
                            }
                            scroll.SmoothScrollTo(0, 5);
                            addedTimes += 1;
                        }
                    }
                }
                else if (scroll.ScrollY <= 0)
                {
                    if (addedTimes > 1)
                    {
                        int startoffset = ((addedTimes - 1) * maxPerLoad);
                        int endoffset = (addedTimes * maxPerLoad);
                        if (!(startoffset > countryList.Length) && !(endoffset > countryList.Length) && startoffset < endoffset)
                        {
                            itemContainter.RemoveAllViewsInLayout();
                            for (int i = startoffset; i < endoffset; i++)
                            {
                                TextView text = new TextView(this);
                                text.SetText(countryList[i], TextView.BufferType.Normal);
                                itemContainter.AddView(text);
                            }
                            scroll.SmoothScrollTo(0, scroll.GetChildAt(0).Height - scroll.Height - 5);
                            addedTimes -= 1;
                        }
                    }
                    else
                    {
                        int startoffset = ((addedTimes - 1) * maxPerLoad);
                        int endoffset = (addedTimes * maxPerLoad);
                        if (!(startoffset > countryList.Length) && !(endoffset > countryList.Length) && startoffset < endoffset)
                        {
                            itemContainter.RemoveAllViewsInLayout();
                            for (int i = startoffset; i < endoffset; i++)
                            {
                                TextView text = new TextView(this);
                                text.SetText(countryList[i], TextView.BufferType.Normal);
                                itemContainter.AddView(text);
                            }
                            addedTimes = 1;
                        }
                    }
                }
            }
            catch { }


            //View first = itemContainter.GetChildAt(0);
            //int fbottom = last.Bottom + scroll.PaddingBottom;
            //int fsy = scroll.ScrollY;
            //int fsh = scroll.Height;
            //int deltaFirst = fbottom - (fsy + fsh);

            //if (e.ScrollY >= (deltaLast * 3) - 20)
            //{
            //    
            //}
            //else if(e.ScrollY <= deltaFirst)
            //{

            //}
        }

    }
}

