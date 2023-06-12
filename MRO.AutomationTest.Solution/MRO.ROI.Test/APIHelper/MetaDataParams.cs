using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Test
{
    public static class MetaDataParams
    { 
        public static string RequesterRef = "External Ref\t";
        public static string FirstName = "First Name\t";
        public static string LastName = "Last Name\t";
        public static string DOB = "DOB\t";
        public static string PAN = "PAN\t";
        public static string DOS = "DOS\t";
        public static string DOSEND = "DOS End\t";
        public static string ROIFacilityCode = "ROI Facility Code\t";
        public static string LocationCode = "LocationCode\t";
        public static string RequesterID = "Requester ID\n";

    }
    public static class MetaDataValues
    {
        public static string RequesterRefValue = $"rajesh{GenerateRandomNumber.RandomNumberGenerator()}\t";
        public static string FirstNameValue = "Change\t";
        public static string LastNameValue = "API2\t";
        public static string DOBValue = "1/1/1991\t";
        public static string PANValue = "123pan\t";
        public static string DOSValue = "6/1/2020\t";
        public static string DOSENDValue = "6/6/2020\t";
        public static string ROIFacilityCodeValue = "ROIT\t";
        public static string LocationCodeValue = "KOP\t";
        public static string RequesterIDValue = "4160";


    }

    public static class GenerateRandomNumber
    {
        public static int RandomNumberGenerator()
        {
            int result;
            try
            {
                Random rand = new Random();

                 result = rand.Next(0, 1000);

            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }
    }

}
