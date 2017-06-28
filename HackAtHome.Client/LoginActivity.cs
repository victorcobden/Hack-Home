using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HackAtHome.SAL;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HackAtHome.Entities;

namespace HackAtHome.Client
{
    [Activity(Label = "Hack@Home", MainLauncher = true, Icon = "@drawable/icon")]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);

            var ButtonValidate = FindViewById<Button>(Resource.Id.ValidateButton);
            ButtonValidate.Click += ValidateUser;
        }

        private async void ValidateUser(object sender, EventArgs e)
        {
            var Button = FindViewById<Button>(Resource.Id.ValidateButton);
            Button.Enabled = false;

            var Email = FindViewById<EditText>(Resource.Id.EmailEditText);
            var Password = FindViewById<EditText>(Resource.Id.PasswordEditText);
            var Result = await HackAtHomeService.AutenticateAsync(Email.Text, Password.Text);

            try
            {
                if (Result.Status == Status.Success)
                {
                    var MicrosoftEvidence = new LabItem
                    {
                        Email = Email.Text,
                        Lab = "Hack@Home",
                        DeviceId = Android.Provider.Settings
                        .Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId)
                    };

                    var MicrosoftClient = new MicrosoftService();
                    await MicrosoftClient.SendEvidence(MicrosoftEvidence);

                    var Intent = new Android.Content.Intent(this, typeof(MainActivity));
                    Intent.PutExtra("Name", Result.FullName);
                    Intent.PutExtra("Token", Result.Token);
                    StartActivity(Intent);
                }
                else
                {
                    HelperMessage.MakeAlert(this,"Error", "Usuario no válido");
                }
            }
            catch (Exception ex)
            {
                HelperMessage.MakeAlert(this,"Error", ex.Message);
            }

            Button.Enabled = true;
        }
    }
}
