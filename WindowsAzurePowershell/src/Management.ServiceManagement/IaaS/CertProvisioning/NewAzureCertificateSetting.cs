﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.WindowsAzure.Management.ServiceManagement.IaaS
{
    using System;
    using System.Management.Automation;
    using WindowsAzure.ServiceManagement;

    [Cmdlet(VerbsCommon.New, "AzureCertificateSetting"), OutputType(typeof(CertificateSetting))]
    public class NewAzureCertificateSettingCommand : Cmdlet
    {
        public string StoreLocation
        {
            get { return "LocalMachine"; }
        }

        [Parameter(Position = 0, Mandatory = false, HelpMessage = "Store Name of the Certificate. Default is My.")]
        [ValidateSet("AddressBook", "AuthRoot", "CertificateAuthority", "Disallowed", "My", "Root", "TrustedPeople", "TrustedPublisher")]
        public string StoreName
        {
            get;
            set;
        }

        [Parameter(Position = 1, Mandatory = true, HelpMessage = "Certificate Thumbprint.")]
        [ValidateNotNullOrEmpty]
        public string Thumbprint
        {
            get;
            set;
        }

        internal void ExecuteCommand()
        {
            CertificateSetting certSettings = new CertificateSetting
            {
                StoreLocation = StoreLocation,
                StoreName = string.IsNullOrEmpty(StoreName) ? "My" : StoreName,
                Thumbprint = Thumbprint
            };

            WriteObject(certSettings, true);
        }

        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();
                ExecuteCommand();
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(ex, string.Empty, ErrorCategory.CloseError, null));
            }
        }
    }
}