using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using ShomaRM.Data;

namespace ShomaRM.Models.Corelogic
{
    public class CoreLogicHelper
    {
        private string CreateXMLDocument(string Body, string Service, string TrsansactionId = "", bool IsNew = true)
        {
            string FinalStr = "";
            try
            {
                //SOAP Body Request  
                FinalStr = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                <ApplicantScreening xmlns:MITS=""http://my-company.com/namespace"" >
                                <Request xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
                                <PropertyID>
                                <MITS:Identification Type=""other"">
                                <MITS:PrimaryID/>
                                <MITS:MarketingName/>
                                <MITS:WebSite>https://office.yourwebsite.com</MITS:WebSite>
                                </MITS:Identification>
                                </PropertyID>
                             <RequestType>" + @"
                              " + (IsNew == true ? "New" : "View") + @"</RequestType>
                             <ReportOptions>
                            <ReportName>" + @"
                              " + Service + @"</ReportName>                           
                             </ReportOptions>" + @"
                              " + (IsNew == true ? "" : "<ReportID>" + TrsansactionId + "</ReportID>") + @"
                          
                             <OriginatorID>501214</OriginatorID>
                             <UserAccount>1M898</UserAccount>
                             <UserName>APIShomatest</UserName>
                             <UserPassword>Shomatest1!</UserPassword>
                             </Request>" + Body + @"</ApplicantScreening>";
            }
            catch (Exception ex)
            {

            }

            return FinalStr;
        }
        public string PostCoreLogicData(LeaseTermsModel leaseRequestModel, Applicant applicant, string ReportType = "CRD", string TransactionId = "", bool IsNew = true)
        {
            string leasebody = LeaseXMLData(leaseRequestModel);
            string applicatnt = ApplicantXMLData(applicant);
            string strxml = CreateXMLDocument(leasebody + applicatnt, ReportType, TransactionId, IsNew);
            //var keyValues = new List<KeyValuePair<string, string>>();
            //keyValues.Add(new KeyValuePair<string, string>("XMLPost", strxml));
            //Task.Run(() => PostFormUrlEncoded<List<XElement>>("https://vendors.residentscreening.net/b2b/demits.aspx", keyValues, ReportType, applicant.ApplyNowID, applicant.CustomerID, applicant.SocSecNumber));
            return strxml;
        }
        private string LeaseXMLData(LeaseTermsModel leaseRequestModel)
        {
            string leaseXmlStr = "";
            try
            {
                leaseXmlStr = @"<LeaseTerms>
                             <MonthlyRent>" + leaseRequestModel.MonthlyRent + @"</MonthlyRent>
                             <LeaseMonths>" + leaseRequestModel.LeaseMonths + @"</LeaseMonths>
		                      <SecurityDeposit>" + leaseRequestModel.SecurityDeposit + @"</SecurityDeposit>
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
                                      <CurrentRent>" + applicant.CurrentRent + @"</CurrentRent>
                                      <ConsentObtained>YES</ConsentObtained>
                                </Other>
                                <Income>
                                       <EmploymentGrossIncome>" + applicant.EmploymentGrossIncome + @"</EmploymentGrossIncome>
                                </Income>
                                <AS_Information>
                                       <ApplicantIdentifier>" + applicant.ApplicantIdentifier + @"</ApplicantIdentifier>
                                       <ApplicantType>" + applicant.ApplicantType + @"</ApplicantType>
			                           <Birthdate>" + applicant.Birthdate + @"</Birthdate>								 
			                           <SocSecNumber>" + applicant.SocSecNumber + @"</SocSecNumber>
                                </AS_Information>
                                <Customers>
                               <MITS:Customer Type=""applicant"">
                               <MITS:CustomerID>" + applicant.CustomerID + @" </MITS:CustomerID>     
                                <MITS:Name>      
                                         <MITS:FirstName>" + applicant.FirstName + @"</MITS:FirstName>           
                                         <MITS:MiddleName></MITS:MiddleName>              
                                         <MITS:LastName> " + applicant.LastName + @"</MITS:LastName>                   
                                </MITS:Name>                    
                                  <MITS:Address Type=""current"">                     
                                         <MITS:Address1>" + applicant.Address1 + @"</MITS:Address1>                         
                                         <MITS:City>" + applicant.City + @"</MITS:City>                              
                                         <MITS:State>" + applicant.State + @"</MITS:State>                                   
                                         <MITS:PostalCode>" + applicant.PostalCode + @"</MITS:PostalCode>                                        
                                         <MITS:UnparsedAddress>" + applicant.UnparsedAddress + @"</MITS:UnparsedAddress>                                            
                                    </MITS:Address>                                             
                                  </MITS:Customer>                                             
                                  </Customers>                                             
                                </Applicant>";
            }
            catch (Exception ex)
            {

            }
            return ApplicantXmlStr;
        }
        public async Task<string> PostFormUrlEncoded<TResult>(string url, List<KeyValuePair<string, string>> postData, string ReportType, string ApplyNowId, string ApplicationId, string SSN)
        {
            string result = "";
            using (var httpClient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(postData))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    HttpResponseMessage response = await httpClient.PostAsync(url, content);
                    string serializedString = await response.Content.ReadAsStringAsync();

                    var xDoc = XDocument.Parse(serializedString);

                    // <Response>
                    // <TransactionNumber>5825858 </TransactionNumber>
                    // <ReportDate>2020-06-24</ ReportDate>
                    // <ApplicantDecision>Accept with Conditions</ApplicantDeciīsion>
                    // <ApplicationDecision>Conditional</ApplicationDecision>
                    // <ApplicantScore>340</ApplicantScore>

                    string transactionNumber = "";
                    string reportDate = "";
                    string aplicantDecision = "";
                    string applicationDecision = "";
                    string applicantScore = "";

                    string sss = xDoc.ToString();


                    if (xDoc.ToString().Contains("TransactionNumber"))
                    {
                        try { transactionNumber = xDoc.Descendants("TransactionNumber").FirstOrDefault().Value; } catch { }
                        try { reportDate = xDoc.Descendants("ReportDate").FirstOrDefault().Value; } catch { }
                        try { aplicantDecision = xDoc.Descendants("ApplicantDecision").FirstOrDefault().Value; } catch { }
                        try { applicationDecision = xDoc.Descendants("ApplicationDecision").FirstOrDefault().Value; } catch { }
                        try { applicantScore = xDoc.Descendants("ApplicantScore").FirstOrDefault().Value; } catch { }

                        ShomaRMEntities db = new ShomaRMEntities();

                        int applynowid = Convert.ToInt32(ApplyNowId);
                        int applicantid = Convert.ToInt32(ApplicationId);

                        var bsExists = db.tbl_BackgroundScreening.Where(p => p.ApplyNowId == applynowid && p.ApplicantId== applicantid  && p.ReportType==ReportType).FirstOrDefault();
                        if (bsExists != null)
                        {
                            bsExists.ApplicantDecision = applicationDecision;
                            bsExists.ApplicationDecision = applicationDecision;
                            bsExists.ApplicantScore = applicantScore; 
                            db.SaveChanges();
                            result = applicationDecision;
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(transactionNumber))
                            {
                                int applicantID = Convert.ToInt32(ApplicationId);
                                int applyNowID = Convert.ToInt32(ApplyNowId);
                                string ssnEnc = new EncryptDecrypt().EncryptText(SSN);
                                var bcSave = new tbl_BackgroundScreening()
                                {
                                    ApplyNowId = applyNowID,
                                    ApplicantId = applicantID,
                                    ReportType = ReportType,
                                    SSN = ssnEnc,
                                    TransactionNumber = transactionNumber,
                                    ReportDate = reportDate,
                                    ApplicantDecision = aplicantDecision,
                                    ApplicationDecision = applicationDecision,
                                    ApplicantScore = applicantScore
                                };
                                db.tbl_BackgroundScreening.Add(bcSave);
                                db.SaveChanges();
                                result = applicationDecision;
                            }
                        }
                        db.Dispose();
                    }
                }
            }
            return result;
        }
    }
}