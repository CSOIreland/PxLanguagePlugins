
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
using PxLanguagePlugin.ga;
using System.Net;
using System.Net.Http;

namespace TestGaPlugin
{
    [TestClass]
    public class TestGa
    {
        private TestContext testContextInstance;


        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        public TestGa() { }

        [TestMethod]
        public void TestGetLabelValuesBasic()
        {
            string translation;
            using (HttpClient client = new HttpClient())
            {
                translation = client.GetStringAsync("https://cdn.jsdelivr.net/gh/CSOIreland/PxLanguagePlugins@2.2.0/server/src/en/PxLanguagePlugin/Resources/language.json").Result;
            }
            Language elp = new Language(translation);
            Language glp = new Language(translation);
            dynamic result = glp.GetLabelValues();
            Assert.IsFalse(result.Equals(null));
        }


        [TestMethod]
        public void SanitizeBasicNoSanitize()
        {
            string translation;
            using (HttpClient client = new HttpClient())
            {
                translation = client.GetStringAsync("https://cdn.jsdelivr.net/gh/CSOIreland/PxLanguagePlugins@2.2.0/server/src/en/PxLanguagePlugin/Resources/language.json").Result;
            }
            Language glp = new Language(translation);
            string testWordInput = "Is teist é seo";
            string testWordsResult = glp.Sanitize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals(testWordInput));
        }

        [TestMethod]
        public void SanitizeBasicRemoveCurlyBraces()
        {
            string translation;
            using (HttpClient client = new HttpClient())
            {
                translation = client.GetStringAsync("https://cdn.jsdelivr.net/gh/CSOIreland/PxLanguagePlugins@2.2.0/server/src/en/PxLanguagePlugin/Resources/language.json").Result;
            }
            Language glp = new Language(translation);
            string testWordInput = "Is teist {é} seo";
            string testWordsResult = glp.Sanitize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("Is teist é seo"));
        }

        [TestMethod]
        public void SanitizeBasicRemoveHtmlStuff()
        {
            string translation;
            using (HttpClient client = new HttpClient())
            {
                translation = client.GetStringAsync("https://cdn.jsdelivr.net/gh/CSOIreland/PxLanguagePlugins@2.2.0/server/src/en/PxLanguagePlugin/Resources/language.json").Result;
            }
            Language glp = new Language();
            string testWordInput = "Is teist <é> seo";
            string testWordsResult = glp.Sanitize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("Is teist é seo"));
        }

        [TestMethod]
        public void SingularizeBasic()
        {
            string translation;
            using (HttpClient client = new HttpClient())
            {
                translation = client.GetStringAsync("https://cdn.jsdelivr.net/gh/CSOIreland/PxLanguagePlugins@2.2.0/server/src/en/PxLanguagePlugin/Resources/language.json").Result;
            }
            Language glp = new Language(translation);
            string testWordInput = "tithe";
            string testWordsResult = glp.Singularize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("teach"));
        }

        [TestMethod]
        public void SingularizeLenition()
        {
            string translation;
            using (HttpClient client = new HttpClient())
            {
                translation = client.GetStringAsync("https://cdn.jsdelivr.net/gh/CSOIreland/PxLanguagePlugins@2.2.0/server/src/en/PxLanguagePlugin/Resources/language.json").Result;
            }
            Language glp = new Language(translation);
            string testWordInput = "dteach";
            string testWordsResult = glp.Singularize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("teach"));
        }

        [TestMethod]
        public void SingularizeAspiration()
        {
            string translation;
            using (HttpClient client = new HttpClient())
            {
                translation = client.GetStringAsync("https://cdn.jsdelivr.net/gh/CSOIreland/PxLanguagePlugins@2.2.0/server/src/en/PxLanguagePlugin/Resources/language.json").Result;
            }
            Language glp = new Language(translation);
            string testWordInput = "theach";
            string testWordsResult = glp.Singularize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("teach"));
        }

        [TestMethod]
        public void SingularizeIrregular()
        {
            string translation;
            using (HttpClient client = new HttpClient())
            {
                translation = client.GetStringAsync("https://cdn.jsdelivr.net/gh/CSOIreland/PxLanguagePlugins@2.2.0/server/src/en/PxLanguagePlugin/Resources/language.json").Result;
            }
            Language glp = new Language(translation);
            string testWordInput = "mná";
            string testWordsResult = glp.Singularize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("bean"));
        }

        [TestMethod]
        public void SingularizeNotFound()
        {
            string translation;
            using (HttpClient client = new HttpClient())
            {
                translation = client.GetStringAsync("https://cdn.jsdelivr.net/gh/CSOIreland/PxLanguagePlugins@2.2.0/server/src/en/PxLanguagePlugin/Resources/language.json").Result;
            }
            Language glp = new Language(translation);
            string testWordInput = "xxxx";
            string testWordsResult = glp.Singularize(testWordInput);
            Assert.IsTrue(testWordsResult.Equals("xxxx"));
        }

        [TestMethod]
        public void GetLabelsBasic()
        {
            string translation;
            using (HttpClient client = new HttpClient())
            {
                translation = client.GetStringAsync("https://cdn.jsdelivr.net/gh/CSOIreland/PxLanguagePlugins@2.2.0/server/src/en/PxLanguagePlugin/Resources/language.json").Result;
            }
            Language glp = new Language(translation);
            var result = glp.GetLabelValues();
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void SynonymBasic()
        {
            string translation;
            using (HttpClient client = new HttpClient())
            {
                translation = client.GetStringAsync("https://cdn.jsdelivr.net/gh/CSOIreland/PxLanguagePlugins@2.2.0/server/src/en/PxLanguagePlugin/Resources/language.json").Result;
            }
            Language glp = new Language(translation);
            var result = glp.GetSynonyms("sochaí");
            Assert.IsTrue(result.Contains("slua"));
        }

        [TestMethod]
        public void SynonymNotFound()
        {
            string translation;
            using (HttpClient client = new HttpClient())
            {
                translation = client.GetStringAsync("https://cdn.jsdelivr.net/gh/CSOIreland/PxLanguagePlugins@2.2.0/server/src/en/PxLanguagePlugin/Resources/language.json").Result;
            }
            Language glp = new Language(translation);
            var result = glp.GetSynonyms("xxxxx");
            Assert.IsTrue(result.Count() == 0);
        }

        [TestMethod]
        public void ExcludedTerms()
        {
            string translation;
            using (HttpClient client = new HttpClient())
            {
                translation = client.GetStringAsync("https://cdn.jsdelivr.net/gh/CSOIreland/PxLanguagePlugins@2.2.0/server/src/en/PxLanguagePlugin/Resources/language.json").Result;
            }
            Language glp = new Language(translation);
            var result = glp.GetExcludedTerms();
            Assert.IsTrue(result.Contains("agus"));
            Assert.IsTrue(result.Contains("do"));
        }
        [TestMethod]
        public void DoNotAmend()
        {
            string translation;
            using (HttpClient client = new HttpClient())
            {
                translation = client.GetStringAsync("https://cdn.jsdelivr.net/gh/CSOIreland/PxLanguagePlugins@2.2.0/server/src/en/PxLanguagePlugin/Resources/language.json").Result;
            }
            Language glp = new Language(translation);
            var result = glp.GetDoNotAmend();
            Assert.IsTrue(result.Contains("méid"));
            Assert.IsTrue(result.Contains("mhéid"));
        }
    }
}
