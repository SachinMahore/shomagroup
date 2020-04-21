using ShomaRM.Models.Acutraq;
using System;
using System.Net.Http;
using System.Web.Mvc;

namespace ShomaRM.Controllers
{
    public class BackgroundScreeningController : ShomaAPIController
    {

        [HttpPost]       
        public HttpResponseMessage ReceiveRequestResult()
        {
            try
            {
				//var textansDesc = "<?xml version=\"1.0\" encoding=\"utf-8\"?><OrderXML><Method>PUSH RESULTS</Method><Authentication>"

				//		  + "<Username> pward </Username>"

				//		  + "< Password > Password1234! </ Password >"

				//		+ "</ Authentication >"

				//		+ "< ReportID > 173828 </ ReportID >"

				//		+ "< Order >"

				//		   + " < BillingReferenceCode > 000 - 0000 </ BillingReferenceCode >"

				//		  + "< Subject >"

				//			+ "< FirstName > Joe </ FirstName >"

				//		   + " < MiddleName > Rupram </ MiddleName >"

				//		   + " < LastName > Clean </ LastName >"

				//			+ "< Suffix ></ Suffix >"

				//			+ "< DOB > 1 / 1 / 1990 </ DOB >"

				//		   + " < SSN > 111223333 </ SSN >"

				//		 + " </ Subject >"


				//		  + "< ReportLink >< ![CDATA[https://orders.dciresources.com/webservice/getreport.cfm?reportID=173828&ReportKey=55DBCD75-A944-4355-B49B-3C8277CF0238]]></ReportLink>"


			 // + "< OrderDetail ServiceCode = 'Emp' OrderId = '123456' CRAorderId = '362801' >"

				//  + "< Status > COMPLETE </ Status >"


				//	 + "< CompanyName CurrentEmployer = 'Yes' > Thinkersteps </ CompanyName >"

				//	 + " < Position > Developer </ Position >"

				//	  + "< Salary period = 'Yearly' > 1, 234.00 </ Salary >"

				//	  + "< Manager > Test </ Manager >"

				//	  + "< Telephone > (123) 456 - 7890 </ Telephone >"

				//	  + "< EmployerCity > Miami </ EmployerCity >"

				//	 + " < EmployerState ></ EmployerState >"

				//	 + " < EmploymentDates >"

				//		 + " < StartDate > 10 / 1 / 2017 </ StartDate >"

				//		 + " < EndDate > 10 / 10 / 2019 </ EndDate >"

				//	 + " </ EmploymentDates >"

				//	  + " < ReasonForLeaving > Test </ ReasonForLeaving >"

				//	 + " < VerifiedDetails >"

				//		  + "< CompanyName ></ CompanyName >"

				//		  + "< CurrentEmployer > No </ CurrentEmployer >"

				//		 + " < Position ></ Position >"

				//		  + "< Salary ></ Salary >"

				//		 + " < Telephone ></ Telephone >"

				//		  + "< EmployerCity ></ EmployerCity >"

				//		  + "< EmployerState ></ EmployerState >"

				//		  + "< EmploymentDates >"

				//			 + " < StartDate ></ StartDate >"

				//			  + "< EndDate ></ EndDate >"

				//		 + " </ EmploymentDates >"

				//		  + "< ReasonForLeaving ></ ReasonForLeaving >"

				//	 + " </ VerifiedDetails >"

			 // + "</ OrderDetail >"


			 //+ " < OrderDetail ServiceCode = 'TentCredit' OrderId = '123456' CRAorderId = '362802' >"

				//  + "< Status > COMPLETE </ Status >"


				//	 + " < CreditReportDetails >< ![CDATA[]] ></ CreditReportDetails >"


			 // + "</ OrderDetail >"


			 //+ " < OrderDetail ServiceCode = 'MultiState' OrderId ='123456' CRAorderId = '362799' >"

				//  + "< Status > NO RECORD </ Status >"


				//	   + " < Result >"

				//		   + " < OffenderCount > 0 </ OffenderCount >"

				//		+ "</ Result >"


			 // + "</ OrderDetail >"


			 //+ " < OrderDetail ServiceCode = 'EviSea' OrderId = '123456' CRAorderId = '362800' >"

				//  + "< Status > NO RECORD </ Status >"


			 //+ " </ OrderDetail >"


				//	   + " </ Order >"

				//	   + "</ OrderXML > <? xml version = '1.0' encoding ='utf-8' ?>"

				//	 + " < OrderXML >"

				//		+ "< Method > PUSH RESULTS </ Method >"

				//		 + "< Authentication >"

				//		 + " < Username > pward </ Username >"

				//		  + "< Password > Password1234! </ Password >"

				//	   + " </ Authentication >"

				//		+ "< ReportID > 173828 </ ReportID >"

				//		+ "< Order >"

				//		   + " < BillingReferenceCode > 000 - 0000 </ BillingReferenceCode >"

				//		 + " < Subject >"

				//		   + " < FirstName > Joe </ FirstName >"

				//			+ "< MiddleName > Rupram </ MiddleName >"

				//		   + " < LastName > Clean </ LastName >"

				//		   + " < Suffix ></ Suffix >"

				//			+ "< DOB > 1 / 1 / 1990 </ DOB >"

				//		   + " < SSN > 111223333 </ SSN >"

				//		 + " </ Subject >"


				//		 + " < ReportLink >< ![CDATA[https://orders.dciresources.com/webservice/getreport.cfm?reportID=173828&ReportKey=55DBCD75-A944-4355-B49B-3C8277CF0238]]></ReportLink>"


			 // + "< OrderDetail ServiceCode = 'Emp' OrderId = '123456' CRAorderId = '362801' >"

				//  + "< Status > COMPLETE </ Status >"


				//	 + " < CompanyName CurrentEmployer = 'Yes' > Thinkersteps </ CompanyName >"

				//	 + " < Position > Developer </ Position >"

				//	 + " < Salary period = 'Yearly' > 1, 234.00 </ Salary >"

				//	 + " < Manager > Test </ Manager >"

				//	 + " < Telephone > (123) 456 - 7890 </ Telephone >"

				//	 + " < EmployerCity > Miami </ EmployerCity >"

				//	  + "< EmployerState ></ EmployerState >"

				//	 + " < EmploymentDates >"

				//		 + " < StartDate > 10 / 1 / 2017 </ StartDate >"

				//		 + " < EndDate > 10 / 10 / 2019 </ EndDate >"

				//	  + "</ EmploymentDates >"

				//	   + "< ReasonForLeaving > Test </ ReasonForLeaving >"

				//	 + " < VerifiedDetails >"

				//		 + " < CompanyName ></ CompanyName >"

				//		  + "< CurrentEmployer > No </ CurrentEmployer >"

				//		  + "< Position ></ Position >"

				//		 + " < Salary ></ Salary >"

				//		 + " < Telephone ></ Telephone >"

				//		  + "< EmployerCity ></ EmployerCity >"

				//		 + " < EmployerState ></ EmployerState >"

				//		 + " < EmploymentDates >"

				//			 + " < StartDate ></ StartDate >"

				//			  + "< EndDate ></ EndDate >"

				//		 + " </ EmploymentDates >"

				//		 + " < ReasonForLeaving ></ ReasonForLeaving >"

				//	  + "</ VerifiedDetails >"

			 // + "</ OrderDetail >"


			 // + "< OrderDetail ServiceCode = 'TentCredit' OrderId = '123456' CRAorderId = '362802' >"

				//  + "< Status > COMPLETE </ Status >"


				//	 + " < CreditReportDetails >< ![CDATA[]] ></ CreditReportDetails >"


			 //+ " </ OrderDetail >"


			 // + "< OrderDetail ServiceCode = 'MultiState' OrderId = '123456' CRAorderId = '362799' >"

				// + " < Status > NO RECORD </ Status >"


				//		+ "< Result >"

				//		   + " < OffenderCount > 0 </ OffenderCount >"

				//	   + " </ Result >"


			 // + "</ OrderDetail >"


			 // + "< OrderDetail ServiceCode = 'EviSea' OrderId = '123456' CRAorderId = '362800' >"

				// + " < Status > NO RECORD </ Status >"


			 //+ " </ OrderDetail >"


				//		+ "</ Order >"

				//	  + "</ OrderXML >";
				////var data = Server.HtmlEncode(textansDesc);
				//string EncodedString = System.Web.HttpUtility.HtmlEncode(textansDesc);

				var result = Request.Form["request"];                
               
            }
            catch (Exception ex)
            {
                
	 
					


            }
			return null;
        }
    }
}