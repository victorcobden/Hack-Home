using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using Android.Graphics;

namespace HackAtHome.Client
{
    [Activity(Label = "Hack@Home", Icon = "@drawable/icon")]
    public class EvidenceDetailActivity : Activity
    {
        DataDetailComplex _data;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EvidenceDetail);

            var LabID = Intent.GetLongExtra("LabID", 0);
            var Token = Intent.GetStringExtra("Token");

            

            var title = FindViewById<TextView>(Resource.Id.DetailTitle);
            var description = FindViewById<WebView>(Resource.Id.DetailDescription);
            var name = FindViewById<TextView>(Resource.Id.DetailFullName);
            var status = FindViewById<TextView>(Resource.Id.DetailStatus);
            var image = FindViewById<ImageView>(Resource.Id.DetailImage);

            _data = (DataDetailComplex)this.FragmentManager.FindFragmentByTag("Data");
            if (_data == null)
            {
                _data = new DataDetailComplex();
                var FragmentTransaction = this.FragmentManager.BeginTransaction();
                FragmentTransaction.Add(_data, "Data");
                FragmentTransaction.Commit();
            }

            if (_data.Detail == null)
            {
                _data.Detail = await HackAtHome.SAL.HackAtHomeService.GetEvidenceByIDAsync(Token, (int)LabID);
            }

            title.Text = Intent.GetStringExtra("EvidenceTitle");
            status.Text = Intent.GetStringExtra("EvidenceStatus");
            description.LoadDataWithBaseURL(null,$"<span style='color:white !important'>{_data.Detail.Description}</span>","text/html","utf-8",null);
            name.Text = Intent.GetStringExtra("Name");
            Koush.UrlImageViewHelper.SetUrlDrawable(image, _data.Detail.Url);

            description.SetBackgroundColor(Color.Transparent);
        }
    }
}