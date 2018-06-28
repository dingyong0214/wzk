using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boat.Business.Common
{
    public class LongLatAddress
    {
        public int status { get; set; }
        public BaiduLongLat result { get; set; }
    }
    public class BaiduLongLat
    {
        public BaiduLocation location { get; set; }
        public string formatted_address { get; set; }

        public AddressComponent addressComponent { get; set; }
        public int precise { get; set; }
        public int confidence { get; set; }
        public string level { get; set; }
    }
    public class BaiduLocation
    {
        public double lng { get; set; }
        public double lat { get; set; }
    }

    public class AddressComponent
    {
        public string country { get; set; }
        public string country_code { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string adcode { get; set; }
        public string street { get; set; }
        public string street_number { get; set; }
        public string direction { get; set; }
        public string distance { get; set; }
    }
}
