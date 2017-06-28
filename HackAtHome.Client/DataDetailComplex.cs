using Android.App;
using Android.OS;
using HackAtHome.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAtHome.Client
{
    public class DataDetailComplex : Fragment
    {
        public EvidenceDetail Detail { get; set; }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RetainInstance = true;
        }
    }
}
