// Copyright 2009, Google Inc. All Rights Reserved.
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
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Xml;

namespace com.google.api.adwords.lib.util {
  /// <summary>
  /// Defines data utility functions for the client library.
  /// </summary>
  public class DataUtilities {
    /// <summary>
    /// Gets all the category codes that can be used with
    /// SiteSuggestionService.getSitesByCategoryName.
    /// See <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_categories.html">this page</a> for details.
    /// </summary>
    /// <returns>A list of Category objects.</returns>
    public static List<Category> GetAllCategories() {
      string fullPath = GetDataPath() + "\\categories.csv";
      CsvFile reader = new CsvFile();
      reader.Read(fullPath, true);
      List<Category> retVal = new List<Category>();

      foreach (string[] item in reader.Records) {
        Category category;
        category.path = item[0];
        category.category = item[1];
        retVal.Add(category);
      }
      return retVal;
    }

    /// <summary>
    /// Gets all the country and territory codes you can use for targeting
    /// your ads. See <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_countries.html">this page</a> for details.
    /// </summary>
    /// <returns>A list of Country objects.</returns>
    public static List<Country> GetAllCountries() {
      string fullPath = GetDataPath() + "\\countries.csv";
      CsvFile reader = new CsvFile();
      reader.Read(fullPath, true);
      List<Country> retVal = new List<Country>();

      foreach (string[] item in reader.Records) {
        Country country;
        country.name = item[0];
        country.code = item[1];
        retVal.Add(country);
      }
      return retVal;
    }

    /// <summary>
    /// Gets all the currency codes you can use to specify the currency of
    /// monetary values. See <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_currency.html">this page</a> for details.
    /// </summary>
    /// <returns>A list of Currency objects.</returns>
    public static List<Currency> GetAllCurrencies() {
      string fullPath = GetDataPath() + "\\currencies.csv";
      CsvFile reader = new CsvFile();
      reader.Read(fullPath, true);
      List<Currency> retVal = new List<Currency>();

      foreach (string[] item in reader.Records) {
        Currency currency;
        currency.code = item[0];
        currency.name = item[1];
        retVal.Add(currency);
      }
      return retVal;
    }

    /// <summary>
    /// Gets all the language codes you can use while targeting ads. You can
    /// also use this list to set the preferred language for the AdWords web
    /// interface for a user. See <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_languages.html">this page</a> for details.
    /// </summary>
    /// <returns>A list of Language objects.</returns>
    public static List<Language> GetAllLanguages() {
      string fullPath = GetDataPath() + "\\languages.csv";
      CsvFile reader = new CsvFile();
      reader.Read(fullPath, true);
      List<Language> retVal = new List<Language>();

      foreach (string[] item in reader.Records) {
        Language language;
        language.name = item[0];
        language.criteriaCode = item[1];
        language.displayCode = item[2];
        retVal.Add(language);
      }
      return retVal;
    }

    /// <summary>
    /// Gets the cost in units for all operations available in AdWords API.
    /// See <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_ratesheet.html">this page</a> for details.
    /// </summary>
    /// <returns>A list of OpRates objects.</returns>
    public static List<OpRates> GetAllOpRates() {
      string fullPath = GetDataPath() + "\\ops_rates.csv";
      CsvFile reader = new CsvFile();
      reader.Read(fullPath, true);
      List<OpRates> retVal = new List<OpRates>();

      foreach (string[] item in reader.Records) {
        OpRates rates;
        rates.serviceName = item[0];
        rates.methodName = item[1];
        rates.rate = int.Parse(item[2]);
        rates.isPerItem = bool.Parse(item[3]);
        retVal.Add(rates);
      }
      return retVal;
    }

    /// <summary>
    /// Gets the list of time zone codes used by AccountInfo.timeZoneId.
    /// See <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_timezones.html">this page</a> for details.
    /// </summary>
    /// <returns>A list of OpRates objects.</returns>
    public static List<Timezone> GetAllTimezones() {
      string fullPath = GetDataPath() + "\\timezones.csv";
      CsvFile reader = new CsvFile();
      reader.Read(fullPath, true);
      List<Timezone> retVal = new List<Timezone>();

      foreach (string[] item in reader.Records) {
        Timezone rates;
        rates.code = item[0];
        retVal.Add(rates);
      }
      return retVal;
    }

    /// <summary>
    /// Gets the values you can use for targeting ads at cities in the
    /// United States. See <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_us_cities.html">this page</a> for details.
    /// </summary>
    /// <returns>A list of OpRates objects.</returns>
    public static List<UsCity> GetAllUsCities() {
      string fullPath = GetDataPath() + "\\us_cities.csv";
      CsvFile reader = new CsvFile();
      reader.Read(fullPath, true);
      List<UsCity> retVal = new List<UsCity>();

      foreach (string[] item in reader.Records) {
        UsCity city;
        city.state = item[0];
        city.code = item[1];
        retVal.Add(city);
      }
      return retVal;
    }

    /// <summary>
    /// Gets the the values you can use to target ads at metropolitan regions
    /// in the United States. See <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_us_metros.html">this page</a> for details.
    /// </summary>
    /// <returns>A list of UsMetro objects.</returns>
    public static List<UsMetro> GetAllUsMetros() {
      string fullPath = GetDataPath() + "\\us_metros.csv";
      CsvFile reader = new CsvFile();
      reader.Read(fullPath, true);
      List<UsMetro> retVal = new List<UsMetro>();

      foreach (string[] item in reader.Records) {
        UsMetro metro;
        metro.state = item[0];
        metro.metro = item[1];
        metro.code = item[2];
        retVal.Add(metro);
      }
      return retVal;
    }

    /// <summary>
    /// Gets the values you can use for targeting ads at cities across the
    /// world. See <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_cities.html">this page</a> for details.
    /// </summary>
    /// <returns>A list of WorldCity objects.</returns>
    public static List<WorldCity> GetAllWorldCities() {
      string fullPath = GetDataPath() + "\\world_cities.csv";
      CsvFile reader = new CsvFile();
      reader.Read(fullPath, true);
      List<WorldCity> retVal = new List<WorldCity>();

      foreach (string[] item in reader.Records) {
        WorldCity worldcity;
        worldcity.country = item[0];
        worldcity.code = item[1];
        retVal.Add(worldcity);
      }
      return retVal;
    }

    /// <summary>
    /// This page lists the region codes that you can use to target ads at
    /// specific geographical regions of the world.
    /// See <a href="http://code.google.com/apis/adwords/docs/developer/adwords_api_regions.html">this page</a> for details.
    /// </summary>
    /// <returns>A list of WorldCity objects.</returns>
    public static List<WorldRegion> GetAllWorldRegions() {
      string fullPath = GetDataPath() + "\\world_regions.csv";
      CsvFile reader = new CsvFile();
      reader.Read(fullPath, true);
      List<WorldRegion> retVal = new List<WorldRegion>();

      foreach (string[] item in reader.Records) {
        WorldRegion worldregion;
        worldregion.country = item[0];
        worldregion.code = item[1];
        worldregion.region = item[2];
        retVal.Add(worldregion);
      }
      return retVal;
    }

    /// <summary>
    /// Gets a version string for this assembly.
    /// </summary>
    /// <returns>The version string in format Major.Minor.Revision.</returns>
    public static string GetVersion() {
      Version version = Assembly.GetExecutingAssembly().GetName().Version;
      return string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Revision);
    }

    /// <summary>
    /// Saves the contents of a sandbox account into an XML.
    /// </summary>
    /// <param name="user">The AdWordsUser to be used for downloading sandbox contents.</param>
    /// <param name="fileName">The XML file to which the dump is to be
    /// saved.</param>
    public static void DownloadSandboxContents(AdWordsUser user, string fileName) {
      AccountManager manager = new AccountManager(user);
      ClientAccount[] allClients = manager.DownloadAllAccounts();

      XmlDocument xDoc = null;

      xDoc = new XmlDocument();
      xDoc.LoadXml("<accounts/>");

      Archiver archiver = new Archiver();

      foreach (ClientAccount account in allClients) {
        XmlElement xClient = xDoc.CreateElement("account");
        archiver.SerializeAccount(xClient, account);
        xDoc.DocumentElement.AppendChild(xClient);
      }
      xDoc.Save(fileName);
    }

    /// <summary>
    /// Restores the contents of a sandbox account from an XML.
    /// </summary>
    /// <param name="fileName">The XML file containing a sandbox dump.</param>
    /// <param name="user">The AdWordsUser to be used for uploading file contents to the sandbox.
    /// </param>
    public static void RestoreSandboxContents(AdWordsUser user, string fileName) {
      XmlDocument xDoc = new XmlDocument();
      xDoc.Load(fileName);

      Archiver archiver = new Archiver();
      List<ClientAccount> allClients = new List<ClientAccount>();

      XmlNodeList xClients = xDoc.SelectNodes("accounts/account");

      foreach (XmlElement xClient in xClients) {
        allClients.Add(archiver.DeSerializeAccount(xClient));
      }

      AccountManager manager = new AccountManager(user);
      manager.UploadAllAccounts(allClients.ToArray());
    }

    /// <summary>
    /// Get the data path. This is where the CSVs containing various
    /// codes are stored.
    /// </summary>
    /// <returns>The folder where the CSVs are stored.</returns>
    private static string GetDataPath() {
      Hashtable headers = (Hashtable) ConfigurationSettings.GetConfig("adwordsHeaders");

      return headers["dataPath"].ToString();
    }
  }
}
