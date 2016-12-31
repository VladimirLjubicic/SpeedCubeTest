using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using System;
using System.Diagnostics;

namespace SpeedCubeInterviewTest
{   
    [CodedUITest]
    public class CodedUITest1
    {
        public CodedUITest1()
        {
        }

        static Process proc = null;

        [ClassInitialize]
        public static void initializeTest(TestContext context)  // launching browser before the test
        {
            Playback.Initialize();
            var bw = BrowserWindow.Launch();
            proc = bw.Process;
            bw.CloseOnPlaybackCleanup = false;
        }

        [ClassCleanup]                                          // closing browser after the test is finishe
        public static void CleanUp()
        {
            BrowserWindow bw = BrowserWindow.FromProcess(proc);
            bw.Close();
        }


        [TestMethod]
        [DeploymentItem("data.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\data.csv", "data#csv", DataAccessMethod.Sequential)]
        public void CodedUITestMethod1()
        {
            BrowserWindow browser = BrowserWindow.FromProcess(proc);
            browser.NavigateToUrl(new Uri("http://speedcube.rs"));                  // step 1 - go to speedcube website                   
            ClickAllCubes(browser, "Sve Kocke");                                    // step 2 - Sve Kocke button selected from the home page
            ClickAddToCart(browser, TestContext.DataRow["cubetag"].ToString());     // step 3 - Adds cube to cart
            ClickConfirmOrder(browser, "naručujem");                                // step 4 - Confirm order button click
            EnterText(browser, "email", TestContext.DataRow["email"].ToString());       //
            EnterText(browser, "name", TestContext.DataRow["name"].ToString());        //
            EnterText(browser, "address", TestContext.DataRow["address"].ToString()); // steps 5-11 populate all of
            EnterText(browser, "postnum", TestContext.DataRow["postnum"].ToString()); // the text fields
            EnterText(browser, "city", TestContext.DataRow["city"].ToString());        //
            EnterText(browser, "phone", TestContext.DataRow["phone"].ToString());       //
            EnterComment(browser, "comment", TestContext.DataRow["comment"].ToString()); //
            ClickCheckBox(browser, "terms");                                             // step 12 - Ticks the checkbox

        }
         
        void ClickAllCubes(UITestControl parent, string innerText)
        {
            var link = new HtmlHyperlink(parent);
            link.SearchProperties.Add(HtmlHyperlink.PropertyNames.InnerText, innerText);
            Mouse.Click(link);
        }

        void ClickAddToCart(UITestControl parent, string tagInstance)
        {
            var button = new HtmlImage(parent);
            button.SearchProperties.Add(HtmlImage.PropertyNames.TagInstance, tagInstance);
            Mouse.Click(button);
        }

        void ClickConfirmOrder(UITestControl parent, string alt)
        {
            var button = new HtmlImage(parent);
            button.SearchProperties.Add(HtmlImage.PropertyNames.Alt, alt);
            Mouse.Click(button);
        }

        void EnterText(UITestControl parent, string name, string value)
        {
            var edit = new HtmlEdit(parent);
            edit.SearchProperties.Add(HtmlEdit.PropertyNames.Name, name);
            edit.Text = value;
        }

        void EnterComment(UITestControl parent, string name, string value)
        {
            var edit = new HtmlTextArea(parent);
            edit.SearchProperties.Add(HtmlTextArea.PropertyNames.Name, name);
            edit.Text = value;
        }

        void ClickCheckBox(UITestControl parent, string id)
        {
            var checkBox = new HtmlCheckBox(parent);
            checkBox.SearchProperties.Add(HtmlCheckBox.PropertyNames.Id, id);
            Mouse.Click(checkBox);
        }        

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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
        private TestContext testContextInstance;
    }
}
