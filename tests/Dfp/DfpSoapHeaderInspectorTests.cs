﻿// Copyright 2017, Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using NUnit.Framework;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Xml;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Headers;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Tests.Mocks;

namespace Google.Api.Ads.Dfp.Tests {
  /// <summary>
  /// Dfp SOAP header inspector tests.
  /// </summary>
  [TestFixture]
  public class DfpSoapHeaderInspectorTests {
    /// <summary>
    /// The service to test applying headers to.
    /// </summary>
    IClientChannel channel;

    /// <summary>
    /// The message to test applying headers to.
    /// </summary>
    Message message;

    readonly MessageVersion TestMessageVersion =
        MessageVersion.CreateVersion(EnvelopeVersion.Soap11);

    /// <summary>
    /// Initialize this test class instance.
    /// </summary>
    [SetUp]
    public void Init() {
      EndpointAddress endpoint = new EndpointAddress("http://www.google.com");
      BasicHttpBinding b = new BasicHttpBinding();
      this.channel =  new MockAdsService(b, endpoint).InnerChannel;
      this.message = Message.CreateMessage(TestMessageVersion, null);
    }

    /// <summary>
    /// Tests that setting a null header throws.
    /// </summary>
    [Test]
    public void TestNullHeaderInvalid() {
      DfpSoapHeaderInspector inspector = new DfpSoapHeaderInspector() {
        Config = new DfpAppConfig()
      };
      Assert.Throws(typeof(DfpApiException), delegate() {
        inspector.BeforeSendRequest(ref this.message, this.channel);
      }, "No exception was thrown for a null header");
    }

    /// <summary>
    /// Tests that the inspector requires a Config file.
    /// </summary>
    [Test]
    public void TestNullConfigInvalid() {
      DfpSoapHeaderInspector inspector = new DfpSoapHeaderInspector() {
        Config = null,
        RequestHeader = new RequestHeader() {
          networkCode = "12345"
        }
      };

      Assert.Throws(typeof(DfpApiException), delegate () {
        inspector.BeforeSendRequest(ref this.message, this.channel);
      }, "No exception was thrown for a null Config");
    }

    /// <summary>
    /// Tests that application names are required and not the defualt.
    /// </summary>
    [Test]
    public void TestApplicationNameRequired() {
      DfpSoapHeaderInspector inspector = new DfpSoapHeaderInspector() {
        Config = new DfpAppConfig()
      };
      RequestHeader header = new RequestHeader() {
        networkCode = "12345"
      };
      inspector.RequestHeader = header;

      Assert.Throws(typeof(DfpApiException), delegate() {
        inspector.BeforeSendRequest(ref this.message, this.channel);
      }, "No exception was thrown for a null application name");

      header.applicationName = DfpAppConfig.DEFAULT_APPLICATION_NAME;
      Assert.Throws(typeof(DfpApiException), delegate() {
        inspector.BeforeSendRequest(ref this.message, this.channel);
      }, "No exception was thrown for the default application name");

      header.applicationName = "";
      Assert.Throws(typeof(DfpApiException), delegate() {
        inspector.BeforeSendRequest(ref this.message, this.channel);
      }, "No exception was thrown for an empty string application name");
    }

    /// <summary>
    /// Tests that a valid header is applied.
    /// </summary>
    [Test]
    public void TestValidHeaderApplied() {
      DfpSoapHeaderInspector inspector = new DfpSoapHeaderInspector();
      RequestHeader header = new RequestHeader() {
        networkCode = "12345",
      };
      DfpAppConfig config = new DfpAppConfig();
      config.ApplicationName = "Unit test application";
      inspector.Config = config;
      inspector.RequestHeader = (RequestHeader) header.Clone();
      inspector.BeforeSendRequest(ref this.message, this.channel);
      Assert.AreEqual(1, this.message.Headers.Count);
      foreach(RequestHeader appliedHeader in this.message.Headers) {
        Assert.AreEqual("12345", appliedHeader.networkCode);
        Assert.AreEqual(config.GetUserAgent(), appliedHeader.applicationName);
      }
    }

    /// <summary>
    /// Tests that updates to the RequestHeader in a DfpSoapService are applied in the request.
    /// </summary>
    [Test]
    public void TestHeaderUpdatesApplied() {
      AdsServiceInspectorBehavior behavior = new AdsServiceInspectorBehavior();
      DfpSoapHeaderInspector inspector = new DfpSoapHeaderInspector();
      behavior.Add(inspector);

      DfpSoapClient<IMockAdsService> service = new DfpSoapClient<IMockAdsService>(
          new BasicHttpBinding(),
          new EndpointAddress("https://www.google.com"));
      service.Endpoint.EndpointBehaviors.Add(behavior);

      Assert.IsNull(service.RequestHeader);
      RequestHeader expected = new RequestHeader() {
        networkCode = "12345"
      };
      service.RequestHeader = expected;
      Assert.AreEqual(expected, inspector.RequestHeader);

      // Test removing a network code
      expected.networkCode = null;
      Assert.AreEqual(expected, inspector.RequestHeader);
    }
  }
}
  
