using System.Collections.Generic;

namespace CustomersCanvasSampleMVC.Services
{
    public class DataSourceService
    {
        public Dictionary<string, string> GetDataForTemplate()
        {
            //Ensure that your template contains fields: "Name", "Email", "Position", "Phone Number", "Address".
            //Otherwise, Customer's Canvas will not be able to merge this data.
            //If you are facing any issues, make sure that fields names are correct. 
            //If your template contains several fields names for the same data, the data should be duplicated in the dataset.
            //You can get more info https://customerscanvas.com/dev/editors/iframe-api/howto/user-info.html

            return new Dictionary<string, string>()
            {
                {"Name",  "Name From Dataset"},
                {"Email", "email@from-dataset.com"},
                {"Position", "Position from Dataset"},
                {"Phone Number", "555.123.4567"},
                {"Address", "208 Dataset Dr.\nBanning, CA 92220"}
            };
        }
    }
}
