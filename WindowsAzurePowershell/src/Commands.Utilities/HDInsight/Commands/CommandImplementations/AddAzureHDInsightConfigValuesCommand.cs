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
namespace Microsoft.WindowsAzure.Management.HDInsight.Cmdlet.Commands.CommandImplementations
{
    using System.Collections;
    using System.Threading.Tasks;
    using CommandInterfaces;
    using DataObjects;
    using GetAzureHDInsightClusters;
    using GetAzureHDInsightClusters.Extensions;

    internal class AddAzureHDInsightConfigValuesCommand : AzureHDInsightCommand<AzureHDInsightConfig>, IAddAzureHDInsightConfigValuesCommand
    {
        public AddAzureHDInsightConfigValuesCommand()
        {
            this.Config = new AzureHDInsightConfig();
            this.Core = new Hashtable();
            this.Hdfs = new Hashtable();
            this.MapReduce = new AzureHDInsightMapReduceConfiguration();
            this.Hive = new AzureHDInsightHiveConfiguration();
            this.Oozie = new AzureHDInsightOozieConfiguration();
        }

        public AzureHDInsightConfig Config { get; set; }

        public Hashtable Core { get; set; }

        public Hashtable Hdfs { get; set; }

        public AzureHDInsightHiveConfiguration Hive { get; set; }

        public AzureHDInsightMapReduceConfiguration MapReduce { get; set; }

        public AzureHDInsightOozieConfiguration Oozie { get; set; }

        public override Task EndProcessing()
        {
            this.Config.CoreConfiguration.AddRange(this.Core.ToKeyValuePairs());
            this.Config.HdfsConfiguration.AddRange(this.Hdfs.ToKeyValuePairs());
            this.Config.MapReduceConfiguration.ConfigurationCollection.AddRange(this.MapReduce.Configuration.ToKeyValuePairs());
            this.Config.MapReduceConfiguration.CapacitySchedulerConfigurationCollection.AddRange(
                this.MapReduce.CapacitySchedulerConfiguration.ToKeyValuePairs());
            this.Config.HiveConfiguration.ConfigurationCollection.AddRange(this.Hive.Configuration.ToKeyValuePairs());
            this.Config.OozieConfiguration.ConfigurationCollection.AddRange(this.Oozie.Configuration.ToKeyValuePairs());

            if (this.Hive.AdditionalLibraries != null)
            {
                this.Config.HiveConfiguration.AdditionalLibraries = this.Hive.AdditionalLibraries.ToWabStorageAccountConfiguration();
            }

            if (this.Oozie.AdditionalSharedLibraries != null)
            {
                this.Config.OozieConfiguration.AdditionalSharedLibraries = this.Oozie.AdditionalSharedLibraries.ToWabStorageAccountConfiguration();
            }

            if (this.Oozie.AdditionalActionExecutorLibraries != null)
            {
                this.Config.OozieConfiguration.AdditionalActionExecutorLibraries =
                    this.Oozie.AdditionalActionExecutorLibraries.ToWabStorageAccountConfiguration();
            }

            this.Output.Add(this.Config);
            return TaskEx.FromResult(0);
        }
    }
}
