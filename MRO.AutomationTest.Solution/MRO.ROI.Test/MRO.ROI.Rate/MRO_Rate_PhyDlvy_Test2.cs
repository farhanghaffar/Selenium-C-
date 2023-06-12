using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using MRO.ROI.Test.Utilities;
namespace MRO.ROI.Test
{
    class MRO_ROI_Rate_Test2

    {

        //  static int[] id = new int[8];
        // [DataSource]


        [TestClass]
        public class RoiAdminTest : ROITestBase
        {
            public RoiAdminTest() : base(ROITestArea.ROIAdmin)
            {

            }


            [TestMethod]
            //[TestCategory(ROITestCategory.Regression)]
            public void MRO_Rate_PhyDlvy_Test2()
            {
                MRODBConnection.SampleDBConnection();
            }
        }
    }
}
