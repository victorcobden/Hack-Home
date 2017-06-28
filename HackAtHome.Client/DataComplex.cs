using Android.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.OS;
using HackAtHome.Entities;

namespace HackAtHome.Client
{
    public class DataComplex : Fragment
    {
        public List<Evidence> Data { get; set; }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RetainInstance = true;
        }
    }
}
