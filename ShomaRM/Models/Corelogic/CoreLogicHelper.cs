using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace ShomaRM.Models.Corelogic
{
    public class CoreLogicHelper
    {

        private string CreateXMLDocument(string Body)
        {
            string FinalStr = "";
            try
            {
                //SOAP Body Request  
                FinalStr = @"<?xml version=""1.0"" encoding=""utf - 8""?>
                                <ApplicantScreening xmlns: MITS = ""http://my-company.com/namespace"" >
                                <Request xmlns: xsi = ""http://www.w3.org/2001/XMLSchema-instance"">
                                <PropertyID>
                                <MITS:Identification Type = ""other"">
                                <MITS:PrimaryID />
                                <MITS:MarketingName />
                                <MITS:WebSite > https://office.yourwebsite.com</MITS:WebSite>
                                </ MITS:Identification >
                                </ PropertyID >
                             <RequestType > New </ RequestType>
                             <ReportOptions>
                             <ReportName> CRD </ReportName>
                             </ ReportOptions>
                             <OriginatorID > 501214 </OriginatorID>
                             <UserAccount > 1M898 </ UserAccount>
                             <UserName> APIShomatest </ UserName>
                             <UserPassword > Shomatest1! </UserPassword>
                             </ Request></ApplicantScreening>";
            }
            catch (Exception ex)
            {

            }

            return FinalStr;
        }
        public string PostCoreLogicData(LeaseTermsModel leaseRequestModel, Applicant applicant)
        {
           string leasebody= LeaseXMLData(leaseRequestModel);
           string applicatnt=  ApplicantXMLData(applicant);
          return  CreateXMLDocument(leasebody + applicatnt);
        }
        private string LeaseXMLData(LeaseTermsModel leaseRequestModel)
        {
            string leaseXmlStr = "";
            try
            {
                leaseXmlStr = @"<LeaseTerms>
                             <MonthlyRent>"+ leaseRequestModel.LeaseMonths+ @"</MonthlyRent>
                             <LeaseMonths>"+ leaseRequestModel.LeaseMonths+ @"</LeaseMonths>
		                      <SecurityDeposit>" + leaseRequestModel.LeaseMonths + @"</SecurityDeposit>
                               </LeaseTerms>";
            }
            catch (Exception ex)
            {

            }
           
            return leaseXmlStr;
        }
        private string ApplicantXMLData(Applicant applicant)
        {
            string ApplicantXmlStr = "";
            try
            {
                ApplicantXmlStr = @"<Applicant>
                                <Other>
                                      <CurrentRent>"+ applicant.CurrentRent + @" </CurrentRent>
                                      <ConsentObtained>YES</ConsentObtained>
                                </Other>
                                <Income>
                                       <EmploymentGrossIncome>" + applicant.EmploymentGrossIncome + @"</EmploymentGrossIncome>
                                </Income>
                                <AS_Information>
                                       <ApplicantIdentifier>" + applicant.EmploymentGrossIncome + @"</ApplicantIdentifier>
                                       <ApplicantType>" + applicant.ApplicantType + @"</ApplicantType>
			                           <Birthdate>" + applicant.Birthdate + @"</Birthdate>								 
			                           <SocSecNumber>" + applicant.SocSecNumber + @"</SocSecNumber>
                                </AS_Information>
                                <Customers>
                               <MITS:Customer Type=""applicant"">
                               <MITS:CustomerID> " + applicant.CustomerID + @" </MITS:CustomerID>     
                                <MITS:Name>      
                                         <MITS:FirstName > " + applicant.FirstName + @" </MITS:FirstName>           
                                         <MITS:MiddleName ></ MITS:MiddleName>              
                                         <MITS:LastName > " + applicant.LastName + @" </ MITS:LastName>                   
                                </MITS:Name>                    
                                  <MITS:Address Type = ""current"">                     
                                         <MITS:Address1>" + applicant.Address1 + @"</ MITS:Address1>                         
                                         <MITS:City> " + applicant.City + @" </ MITS:City>                              
                                         <MITS:State> " + applicant.State + @" </ MITS:State>                                   
                                         <MITS:PostalCode> " + applicant.State + @" </MITS:PostalCode>                                        
                                         <MITS:UnparsedAddress >" + applicant.UnparsedAddress + @"</ MITS:UnparsedAddress>                                            
                                    </ MITS:Address>                                             
                                  </ MITS:Customer>                                             
                                  </ Customers>                                             
                                </ Applicant> ";
            }
            catch (Exception ex)
            {

            }
            return ApplicantXmlStr;
        }

        public  async Task<List<XElement>> PostFormUrlEncoded<TResult>(string url, List<KeyValuePair<string, string>> postData)
        {
            using (var httpClient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(postData))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    HttpResponseMessage response = await httpClient.PostAsync(url, content);
                    string serializedString = await response.Content.ReadAsStringAsync();
                    var xDoc = XDocument.Parse(serializedString);

                    // for attribute
                    var resultOrderDetail = xDoc
                     .Descendants("<List<XElement>>")
                    
                     .ToList();
                    return resultOrderDetail;

                }
            }
        }


    }
}