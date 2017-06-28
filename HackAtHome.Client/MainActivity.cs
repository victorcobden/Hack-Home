using Android.App;
using Android.Widget;
using Android.OS;
using HackAtHome.CustomAdapters;
using System.Collections.Generic;
using System;
using HackAtHome.Entities;

namespace HackAtHome.Client
{
    [Activity(Label = "Hack@Home", Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        DataComplex _data;
        EvidencesAdapter _adapter;
        string _token;
        string _name;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            var UserField = FindViewById<TextView>(Resource.Id.UserTextView);

            _name = Intent.GetStringExtra("Name");
            UserField.Text = _name;

            _token = Intent.GetStringExtra("Token");
            var token = _token;

            var EvidencesList = FindViewById<ListView>(Resource.Id.evidencesListView);

            #region Fragment
            _data = (DataComplex)this.FragmentManager.FindFragmentByTag("Data");
            if (_data == null)
            {
                _data = new DataComplex();
                var FragmentTransaction = this.FragmentManager.BeginTransaction();
                FragmentTransaction.Add(_data, "Data");
                FragmentTransaction.Commit();
            }

            if (_data.Data == null)
            {
                _data.Data = await HackAtHome.SAL.HackAtHomeService.GetEvidencesASync(token);
            }


            _adapter =
                new EvidencesAdapter(this,
                _data.Data,
                Resource.Layout.ListEvidences,
                Resource.Id.titleTextView,
                Resource.Id.statusTextView);

            EvidencesList.Adapter = _adapter;
            #endregion

            EvidencesList.ItemClick += SelectedLab;
        }

        private void SelectedLab(object sender, AdapterView.ItemClickEventArgs e)
        {
            var labID = _adapter.GetItemId(e.Position);

            var Intent = new Android.Content.Intent(this, typeof(EvidenceDetailActivity));
            var Evidence = _data.Data.Find(x => x.EvidenceID == labID);
            Intent.PutExtra("EvidenceTitle", Evidence.Title);
            Intent.PutExtra("EvidenceStatus", Evidence.Status);
            Intent.PutExtra("LabID", labID);
            Intent.PutExtra("Token", _token);
            Intent.PutExtra("Name", _name);
            StartActivity(Intent);
        }
    }
}

