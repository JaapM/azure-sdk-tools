﻿// Copyright Microsoft Corporation
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

using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Commands.MediaServices;
using Microsoft.WindowsAzure.Commands.Test.Utilities.Common;
using Microsoft.WindowsAzure.Commands.Utilities.MediaServices;
using Moq;

namespace Microsoft.WindowsAzure.Commands.Test.MediaServices
{
    [TestClass]
    public class RemoveMediaServicesAccountTests : TestBase
    {
        [TestMethod]
        public void ProcessRemoveMediaServicesAccountTest()
        {
            // Setup
            var clientMock = new Mock<IMediaServicesClient>();

            string expectedName = "testacc";

            clientMock.Setup(f => f.DeleteAzureMediaServiceAccountAsync(expectedName)).Returns(Task.Factory.StartNew(() => { return true; }));

            // Test
            var command = new RemoveAzureMediaServiceCommand
            {
                CommandRuntime = new MockCommandRuntime(),
                Name = expectedName,
                MediaServicesClient = clientMock.Object,
            };

            command.ExecuteCmdlet();
            Assert.AreEqual(1, ((MockCommandRuntime) command.CommandRuntime).OutputPipeline.Count);
            var deleted = (bool) ((MockCommandRuntime) command.CommandRuntime).OutputPipeline.FirstOrDefault();
            Assert.IsTrue(deleted);
        }
    }
}