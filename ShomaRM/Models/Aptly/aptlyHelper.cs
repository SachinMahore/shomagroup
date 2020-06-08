using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ShomaRM.Models
{
    public class aptlyHelper
    {
        RequestProvider requestProvider;
        public aptlyHelper()
        {
            requestProvider = new RequestProvider();     
        }
        public async Task<string> PostAptlyAsync(aptlyModel model)
        {
            var result = requestProvider.PostAsync("https://app.getaptly.com/api/aptlet/2CvnMa2gDcpP8hPDP?x-token=7BQKWWux4dpASxq7w", model);
               aptlyResponse response = await Task.Run(() =>
                    JsonConvert.DeserializeObject<aptlyResponse>(result.ToString()));
            return response.response;
        }
    }
    public class aptlyResponse
    {
        public string response { get; set; }
    }
   public class aptlyModel
    {
  public string name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Building { get; set; }

        public string Unit { get; set; }
        public string Stage { get; set; }
        public string LeaseTerm { get; set; }
        public string MoveInDate { get; set; }
        public string DateSigned { get; set; }
    
    public string QuoteCreated { get; set; }
public string CreditPaid { get; set; }
     public string BackgroundCheckPaid { get; set; }


  
}
}