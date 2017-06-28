using Android.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAtHome.Client
{
    public class HelperMessage
    {
        public static void MakeAlert(Activity activity,string title, string msge)
        {
            Android.App.AlertDialog.Builder Builder = new AlertDialog.Builder(activity);
            AlertDialog Alert = Builder.Create();
            Alert.SetTitle(title);
            Alert.SetIcon(Resource.Drawable.Icon);
            Alert.SetMessage($"{msge}");
            Alert.SetButton("Ok", (s, ev) => { });
            Alert.Show();
        }
    }
}
