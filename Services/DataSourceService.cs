using System.Collections.Generic;

namespace CustomersCanvasSampleMVC.Services
{
    public class DataSourceService
    {
        public Dictionary<string, string> GetDataForTemplate()
        {
            return new Dictionary<string, string>()
            {
                {"Name",  "TestName (predefined in userInfo)"},
                {"Email", "test@mail.com (predefined in userInfo)"},
                {"Your Name", "TestName (predefined in userInfo)"},
                {"Position", "Position (predefined in userInfo)"},
                {"Phone Number", "000-000-000 (predefined in userInfo)"},
                {"Your address", "Test address (predefined in userInfo)"},
                {"Your Email", "test@mail.com (predefined in userInfo)"},
            };
        }
    }
}
