﻿using HackAtHome.Entities;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAtHome.SAL
{
    public class MicrosoftService
    {
        MobileServiceClient Client;
        private IMobileServiceTable<LabItem> LabItemTable;
        public async Task SendEvidence(LabItem userEvidence)
        {
            Client = new MobileServiceClient(@"http://xamarin-diplomado.azurewebsites.net/");
            LabItemTable = Client.GetTable<LabItem>();
            await LabItemTable.InsertAsync(userEvidence);
        }
    }
}
