using ShomaRM.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace ShomaRM.Models.Bluemoon
{
    public class BluemoonService
    {
        public string BaseUrl = "https://www.bluemoonforms.com/services/";
        private XmlDocument CreateXMLDocument(string Body)
        {
            XmlDocument body = new XmlDocument();
            try
            {
                //SOAP Body Request  
                body.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                                    <SOAP-ENV:Envelope 
                                    xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/""
                                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                                    xmlns:ns1=""https://www.bluemoonforms.com/services/lease.php"">
                                    <SOAP-ENV:Body>
                                        " + Body + @"
                                    </SOAP-ENV:Body>
                                    </SOAP-ENV:Envelope>");
            }
            catch (Exception ex)
            {

            }

            return body;
        }

        private string LeaseXMLData(LeaseRequestModel leaseRequestModel)
        {
            string leaseData = @"<LeaseXMLData>
            	    &lt;BLUEMOON&gt;
                    &lt;LEASE&gt;
                    &lt;STANDARD&gt;
                    " + (leaseRequestModel.ADDRESS == null ? "&lt;ADDRESS/&gt;" : "&lt;ADDRESS&gt;" + leaseRequestModel.ADDRESS + @"&lt;/ADDRESS&gt;") + @" 
                    " + (leaseRequestModel.DATE_OF_LEASE == null ? "&lt;DATE-OF-LEASE/&gt;" : "&lt;DATE-OF-LEASE&gt;" + leaseRequestModel.DATE_OF_LEASE + @"&lt;/DATE-OF-LEASE&gt;") + @"
                    " + (leaseRequestModel.LEASE_BEGIN_DATE == null ? "&lt;LEASE-BEGIN-DATE/&gt;" : "&lt;LEASE-BEGIN-DATE&gt;" + leaseRequestModel.LEASE_BEGIN_DATE + @"&lt;/LEASE-BEGIN-DATE&gt;") + @"
                    " + (leaseRequestModel.LEASE_END_DATE == null ? "&lt;LEASE-END-DATE/&gt;" : "&lt;LEASE-END-DATE&gt;" + leaseRequestModel.LEASE_END_DATE + @"&lt;/LEASE-END-DATE&gt;") + @"
                    " + (leaseRequestModel.MAXIMUM_GUEST_STAY == null ? "&lt;MAXIMUM-GUEST-STAY/&gt;" : "&lt;MAXIMUM-GUEST-STAY&gt;" + leaseRequestModel.MAXIMUM_GUEST_STAY + @"&lt;/MAXIMUM-GUEST-STAY&gt;") + @"
                    " + (leaseRequestModel.OCCUPANT_1 == null ? "&lt;OCCUPANT-1/&gt;" : "&lt;OCCUPANT-1&gt;" + leaseRequestModel.OCCUPANT_1 + @"&lt;/OCCUPANT-1&gt;") + @"
                    " + (leaseRequestModel.OCCUPANT_2 == null ? "&lt;OCCUPANT-2/&gt;" : "&lt;OCCUPANT-2&gt;" + leaseRequestModel.OCCUPANT_2 + @"&lt;/OCCUPANT-2&gt;") + @"
                    " + (leaseRequestModel.OCCUPANT_3 == null ? "&lt;OCCUPANT-3/&gt;" : "&lt;OCCUPANT-3&gt;" + leaseRequestModel.OCCUPANT_3 + @"&lt;/OCCUPANT-3&gt;") + @"
                    " + (leaseRequestModel.OCCUPANT_4 == null ? "&lt;OCCUPANT-4/&gt;" : "&lt;OCCUPANT-4&gt;" + leaseRequestModel.OCCUPANT_4 + @"&lt;/OCCUPANT-4&gt;") + @"
                    " + (leaseRequestModel.OCCUPANT_5 == null ? "&lt;OCCUPANT-5/&gt;" : "&lt;OCCUPANT-5&gt;" + leaseRequestModel.OCCUPANT_5 + @"&lt;/OCCUPANT-5&gt;") + @"
                    " + (leaseRequestModel.OCCUPANT_6 == null ? "&lt;OCCUPANT-6/&gt;" : "&lt;OCCUPANT-6&gt;" + leaseRequestModel.OCCUPANT_6 + @"&lt;/OCCUPANT-6&gt;") + @"
                    " + (leaseRequestModel.PRORATED_RENT == null ? "&lt;PRORATED-RENT/&gt;" : "&lt;PRORATED-RENT&gt;" + leaseRequestModel.PRORATED_RENT + @"&lt;/PRORATED-RENT&gt;") + @"
                    " + (leaseRequestModel.PRORATED_RENT_DUE_DATE == null ? "&lt;PRORATED-RENT-DUE-DATE/&gt;" : "&lt;PRORATED-RENT-DUE-DATE&gt;" + leaseRequestModel.PRORATED_RENT_DUE_DATE + @"&lt;/PRORATED-RENT-DUE-DATE&gt;") + @"
                    " + (leaseRequestModel.PRORATED_RENT_DUE_FIRST_MONTH == null ? "&lt;PRORATED-RENT-DUE-FIRST-MONTH/&gt;" : "&lt;PRORATED-RENT-DUE-FIRST-MONTH&gt;" + leaseRequestModel.PRORATED_RENT_DUE_FIRST_MONTH + @"&lt;/PRORATED-RENT-DUE-FIRST-MONTH&gt;") + @"
                    " + (leaseRequestModel.RENT == null ? "&lt;RENT/&gt;" : "&lt;RENT&gt;" + leaseRequestModel.RENT + @"&lt;/RENT&gt;") + @"
                    " + (leaseRequestModel.RESIDENT_1 == null ? "&lt;RESIDENT-1/&gt;" : "&lt;RESIDENT-1&gt;" + leaseRequestModel.RESIDENT_1 + @"&lt;/RESIDENT-1&gt;") + @"
                    " + (leaseRequestModel.RESIDENT_2 == null ? "&lt;RESIDENT-2/&gt;" : "&lt;RESIDENT-2&gt;" + leaseRequestModel.RESIDENT_2 + @"&lt;/RESIDENT-2&gt;") + @"
                    " + (leaseRequestModel.RESIDENT_3 == null ? "&lt;RESIDENT-3/&gt;" : "&lt;RESIDENT-3&gt;" + leaseRequestModel.RESIDENT_3 + @"&lt;/RESIDENT-3&gt;") + @"
                    " + (leaseRequestModel.RESIDENT_4 == null ? "&lt;RESIDENT-4/&gt;" : "&lt;RESIDENT-4&gt;" + leaseRequestModel.RESIDENT_4 + @"&lt;/RESIDENT-4&gt;") + @"
                    " + (leaseRequestModel.RESIDENT_5 == null ? "&lt;RESIDENT-5/&gt;" : "&lt;RESIDENT-5&gt;" + leaseRequestModel.RESIDENT_5 + @"&lt;/RESIDENT-5&gt;") + @"
                    " + (leaseRequestModel.RESIDENT_6 == null ? "&lt;RESIDENT-6/&gt;" : "&lt;RESIDENT-6&gt;" + leaseRequestModel.RESIDENT_6 + @"&lt;/RESIDENT-6&gt;") + @"
                    " + (leaseRequestModel.SECURITY_DEPOSIT == null ? "&lt;SECURITY-DEPOSIT/&gt;" : "&lt;SECURITY-DEPOSIT&gt;" + leaseRequestModel.SECURITY_DEPOSIT + @"&lt;/SECURITY-DEPOSIT&gt;") + @"
                    " + (leaseRequestModel.UNIT_NUMBER == null ? "&lt;ADDRESS/&gt;" : "&lt;UNIT-NUMBER&gt;" + leaseRequestModel.UNIT_NUMBER + @"&lt;/UNIT-NUMBER&gt;") + @"
                    " + (leaseRequestModel.APARTMENT_MANAGER_ADDRESS == null ? "&lt;APARTMENT-MANAGER-ADDRESS/&gt;" : "&lt;APARTMENT-MANAGER-ADDRESS&gt;" + leaseRequestModel.APARTMENT_MANAGER_ADDRESS + @"&lt;/APARTMENT-MANAGER-ADDRESS&gt;") + @"
                    " + (leaseRequestModel.APARTMENT_MANAGER_NAME == null ? "&lt;APARTMENT-MANAGER-NAME/&gt;" : "&lt;APARTMENT-MANAGER-NAME&gt;" + leaseRequestModel.APARTMENT_MANAGER_NAME + @"&lt;/APARTMENT-MANAGER-NAME&gt;") + @"
                    " + (leaseRequestModel.APARTMENT_OWNER_OR_MANAGER == null ? "&lt;APARTMENT-OWNER-OR-MANAGER/&gt;" : "&lt;APARTMENT-OWNER-OR-MANAGER&gt;" + leaseRequestModel.APARTMENT_OWNER_OR_MANAGER + @"&lt;/APARTMENT-OWNER-OR-MANAGER&gt;") + @"
                    " + (leaseRequestModel.LEASE_TERMINATION_NOTICE_ADDRESS == null ? "&lt;LEASE-TERMINATION-NOTICE-ADDRESS/&gt;" : "&lt;LEASE-TERMINATION-NOTICE-ADDRESS&gt;" + leaseRequestModel.LEASE_TERMINATION_NOTICE_ADDRESS + @"&lt;/LEASE-TERMINATION-NOTICE-ADDRESS&gt;") + @"
                    " + (leaseRequestModel.SECURITY_DEPOSIT_COMMINGLED_ACCOUNT == null ? "&lt;SECURITY-DEPOSIT-COMMINGLED-ACCOUNT/&gt;" : "&lt;SECURITY-DEPOSIT-COMMINGLED-ACCOUNT&gt;" + leaseRequestModel.SECURITY_DEPOSIT_COMMINGLED_ACCOUNT + @"&lt;/SECURITY-DEPOSIT-COMMINGLED-ACCOUNT&gt;") + @"
                    " + (leaseRequestModel.SECURITY_DEPOSIT_COMMINGLED_BANK_ADDRESS == null ? "&lt;SECURITY-DEPOSIT-COMMINGLED-BANK-ADDRESS/&gt;" : "&lt;SECURITY-DEPOSIT-COMMINGLED-BANK-ADDRESS&gt;" + leaseRequestModel.SECURITY_DEPOSIT_COMMINGLED_BANK_ADDRESS + @"&lt;/SECURITY-DEPOSIT-COMMINGLED-BANK-ADDRESS&gt;") + @"
                    " + (leaseRequestModel.SECURITY_DEPOSIT_COMMINGLED_BANK_NAME == null ? "&lt;SECURITY-DEPOSIT-COMMINGLED-BANK-NAME/&gt;" : "&lt;SECURITY-DEPOSIT-COMMINGLED-BANK-NAME&gt;" + leaseRequestModel.SECURITY_DEPOSIT_COMMINGLED_BANK_NAME + @"&lt;/SECURITY-DEPOSIT-COMMINGLED-BANK-NAME&gt;") + @"
                    " + (leaseRequestModel.SECURITY_DEPOSIT_INTEREST_ACCOUNT == null ? "&lt;SECURITY-DEPOSIT-INTEREST-ACCOUNT/&gt;" : "&lt;SECURITY-DEPOSIT-INTEREST-ACCOUNT&gt;" + leaseRequestModel.SECURITY_DEPOSIT_INTEREST_ACCOUNT + @"&lt;/SECURITY-DEPOSIT-INTEREST-ACCOUNT&gt;") + @"
                    " + (leaseRequestModel.SECURITY_DEPOSIT_INTEREST_BANK_ADDRESS == null ? "&lt;SECURITY-DEPOSIT-INTEREST-BANK-ADDRESS/&gt;" : "&lt;SECURITY-DEPOSIT-INTEREST-BANK-ADDRESS&gt;" + leaseRequestModel.SECURITY_DEPOSIT_INTEREST_BANK_ADDRESS + @"&lt;/SECURITY-DEPOSIT-INTEREST-BANK-ADDRESS&gt;") + @"
                    " + (leaseRequestModel.SECURITY_DEPOSIT_INTEREST_BANK_NAME == null ? "&lt;SECURITY-DEPOSIT-INTEREST-BANK-NAME/&gt;" : "&lt;SECURITY-DEPOSIT-INTEREST-BANK-NAME&gt;" + leaseRequestModel.SECURITY_DEPOSIT_INTEREST_BANK_NAME + @"&lt;/SECURITY-DEPOSIT-INTEREST-BANK-NAME&gt;") + @"
                    " + (leaseRequestModel.SECURITY_DEPOSIT_NONINTEREST_ACCOUNT == null ? "&lt;SECURITY-DEPOSIT-NONINTEREST-ACCOUNT/&gt;" : "&lt;SECURITY-DEPOSIT-NONINTEREST-ACCOUNT&gt;" + leaseRequestModel.SECURITY_DEPOSIT_NONINTEREST_ACCOUNT + @"&lt;/SECURITY-DEPOSIT-NONINTEREST-ACCOUNT&gt;") + @"
                    " + (leaseRequestModel.SECURITY_DEPOSIT_NONINTEREST_BANK_ADDRESS == null ? "&lt;SECURITY-DEPOSIT-NONINTEREST-BANK-ADDRESS/&gt;" : "&lt;SECURITY-DEPOSIT-NONINTEREST-BANK-ADDRESS&gt;" + leaseRequestModel.SECURITY_DEPOSIT_NONINTEREST_BANK_ADDRESS + @"&lt;/SECURITY-DEPOSIT-NONINTEREST-BANK-ADDRESS&gt;") + @"
                    " + (leaseRequestModel.SECURITY_DEPOSIT_NONINTEREST_BANK_NAME == null ? "&lt;SECURITY-DEPOSIT-NONINTEREST-BANK-NAMES/&gt;" : "&lt;SECURITY-DEPOSIT-NONINTEREST-BANK-NAMES&gt;" + leaseRequestModel.SECURITY_DEPOSIT_NONINTEREST_BANK_NAME + @"&lt;/SECURITY-DEPOSIT-NONINTEREST-BANK-NAME&gt;") + @"
                    " + (leaseRequestModel.DAYS_REQUIRED_FOR_NOTICE_OF_LEASE_TERMINATION == null ? "&lt;DAYS-REQUIRED-FOR-NOTICE-OF-LEASE-TERMINATIONS/&gt;" : "&lt;DAYS-REQUIRED-FOR-NOTICE-OF-LEASE-TERMINATIONS&gt;" + leaseRequestModel.DAYS_REQUIRED_FOR_NOTICE_OF_LEASE_TERMINATION + @"&lt;/DAYS-REQUIRED-FOR-NOTICE-OF-LEASE-TERMINATION&gt;") + @"
                    " + (leaseRequestModel.LATE_CHARGE_DAILY_CHARGE == null ? "&lt;LATE-CHARGE-DAILY-CHARGE/&gt;" : "&lt;LATE-CHARGE-DAILY-CHARGE&gt;" + leaseRequestModel.LATE_CHARGE_DAILY_CHARGE + @"&lt;/LATE-CHARGE-DAILY-CHARGE&gt;") + @"
                    " + (leaseRequestModel.LATE_CHARGE_INITIAL_CHARGE == null ? "&lt;LATE-CHARGE-INITIAL-CHARGE/&gt;" : "&lt;LATE-CHARGE-INITIAL-CHARGE&gt;" + leaseRequestModel.LATE_CHARGE_INITIAL_CHARGE + @"&lt;/LATE-CHARGE-INITIAL-CHARGE&gt;") + @"
                    " + (leaseRequestModel.LIQUIDATED_DAMAGES == null ? "&lt;LIQUIDATED-DAMAGES/&gt;" : "&lt;LIQUIDATED-DAMAGES&gt;" + leaseRequestModel.LIQUIDATED_DAMAGES + @"&lt;/LIQUIDATED-DAMAGES&gt;") + @"
                    " + (leaseRequestModel.NUMBER_OF_APARTMENT_KEYS == null ? "&lt;NUMBER-OF-APARTMENT-KEYS/&gt;" : "&lt;NUMBER-OF-APARTMENT-KEYS&gt;" + leaseRequestModel.NUMBER_OF_APARTMENT_KEYS + @"&lt;/NUMBER-OF-APARTMENT-KEYS&gt;") + @"
                    " + (leaseRequestModel.NUMBER_OF_MAIL_KEYS == null ? "&lt;NUMBER-OF-MAIL-KEYS/&gt;" : "&lt;NUMBER-OF-MAIL-KEYS&gt;" + leaseRequestModel.NUMBER_OF_MAIL_KEYS + @"&lt;/NUMBER-OF-MAIL-KEYS&gt;") + @"
                    " + (leaseRequestModel.NUMBER_OF_OTHER_KEYS == null ? "&lt;NUMBER-OF-OTHER-KEYS/&gt;" : "&lt;NUMBER-OF-OTHER-KEYS&gt;" + leaseRequestModel.NUMBER_OF_OTHER_KEYS + @"&lt;/NUMBER-OF-OTHER-KEYS&gt;") + @"
                    " + (leaseRequestModel.OTHER_KEY_TYPE == null ? "&lt;OTHER-KEY-TYPE/&gt;" : "&lt;OTHER-KEY-TYPE&gt;" + leaseRequestModel.OTHER_KEY_TYPE + @"&lt;/OTHER-KEY-TYPE&gt;") + @"
                    " + (leaseRequestModel.RENT_DUE_DATE == null ? "&lt;RENT-DUE-DATE/&gt;" : "&lt;RENT-DUE-DATE&gt;" + leaseRequestModel.RENT_DUE_DATE + @"&lt;/RENT-DUE-DATE&gt;") + @"
                    " + (leaseRequestModel.RENTERS_INSURANCE_REQUIREMENT == null ? "&lt;RENTERS-INSURANCE-REQUIREMENT/&gt;" : "&lt;RENTERS-INSURANCE-REQUIREMENT&gt;" + leaseRequestModel.RENTERS_INSURANCE_REQUIREMENT + @"&lt;/RENTERS-INSURANCE-REQUIREMENT&gt;") + @"
                    " + (leaseRequestModel.RETURNED_CHECK_CHARGE == null ? "&lt;RETURNED-CHECK-CHARGE/&gt;" : "&lt;RETURNED-CHECK-CHARGE&gt;" + leaseRequestModel.RETURNED_CHECK_CHARGE + @"&lt;/RETURNED-CHECK-CHARGE&gt;") + @"
                    " + (leaseRequestModel.UTILITIES_CABLE_TV == null ? "&lt;UTILITIES-CABLE-TV/&gt;" : "&lt;UTILITIES-CABLE-TV&gt;" + leaseRequestModel.UTILITIES_CABLE_TV + @"&lt;/UTILITIES-CABLE-TV&gt;") + @"
                    " + (leaseRequestModel.UTILITIES_ELECTRICITY == null ? "&lt;UTILITIES-ELECTRICITY/&gt;" : "&lt;UTILITIES-ELECTRICITY&gt;" + leaseRequestModel.UTILITIES_ELECTRICITY + @"&lt;/UTILITIES-ELECTRICITY&gt;") + @"
                    " + (leaseRequestModel.UTILITIES_GAS == null ? "&lt;UTILITIES-GAS/&gt;" : "&lt;UTILITIES-GAS&gt;" + leaseRequestModel.UTILITIES_GAS + @"&lt;/UTILITIES-GAS&gt;") + @"
                    " + (leaseRequestModel.UTILITIES_MASTER_ANTENNA == null ? "&lt;UTILITIES-MASTER-ANTENNA/&gt;" : "&lt;UTILITIES-MASTER-ANTENNA&gt;" + leaseRequestModel.UTILITIES_MASTER_ANTENNA + @"&lt;/UTILITIES-MASTER-ANTENNA&gt;") + @"
                    " + (leaseRequestModel.UTILITIES_OTHER == null ? "&lt;UTILITIES-OTHER/&gt;" : "&lt;UTILITIES-OTHER&gt;" + leaseRequestModel.UTILITIES_OTHER + @"&lt;/UTILITIES-OTHER&gt;") + @"
                    " + (leaseRequestModel.UTILITIES_OTHER_DESCRIPTION == null ? "&lt;UTILITIES-OTHER-DESCRIPTION/&gt;" : "&lt;UTILITIES-OTHER-DESCRIPTION&gt;" + leaseRequestModel.UTILITIES_OTHER_DESCRIPTION + @"&lt;/UTILITIES-OTHER-DESCRIPTION&gt;") + @"
                    " + (leaseRequestModel.UTILITIES_TRASH == null ? "&lt;UTILITIES-TRASH/&gt;" : "&lt;UTILITIES-TRASH&gt;" + leaseRequestModel.UTILITIES_TRASH + @"&lt;/UTILITIES-TRASH&gt;") + @"
                    " + (leaseRequestModel.UTILITIES_WASTEWATER == null ? "&lt;UTILITIES-WASTEWATER/&gt;" : "&lt;UTILITIES-WASTEWATER&gt;" + leaseRequestModel.UTILITIES_WASTEWATER + @"&lt;/UTILITIES-WASTEWATER&gt;") + @"
                    " + (leaseRequestModel.UTILITIES_WATER == null ? "&lt;UTILITIES-WATER/&gt;" : "&lt;UTILITIES-WATER&gt;" + leaseRequestModel.UTILITIES_WATER + @"&lt;/UTILITIES-WATER&gt;") + @"
                    " + (leaseRequestModel.FIRE_PROTECTION_AVAILABLE == null ? "&lt;FIRE-PROTECTION-AVAILABLE/&gt;" : "&lt;FIRE-PROTECTION-AVAILABLE&gt;" + leaseRequestModel.FIRE_PROTECTION_AVAILABLE + @"&lt;/FIRE-PROTECTION-AVAILABLE&gt;") + @"
                    " + (leaseRequestModel.FIRE_PROTECTION_CARBON_MONOXIDE_DETECTOR == null ? "&lt;FIRE-PROTECTION-CARBON-MONOXIDE-DETECTOR/&gt;" : "&lt;FIRE-PROTECTION-CARBON-MONOXIDE-DETECTOR&gt;" + leaseRequestModel.FIRE_PROTECTION_CARBON_MONOXIDE_DETECTOR + @"&lt;/FIRE-PROTECTION-CARBON-MONOXIDE-DETECTOR&gt;") + @"
                    " + (leaseRequestModel.FIRE_PROTECTION_FIRE_EXTINGUISHER == null ? "&lt;FIRE-PROTECTION-FIRE-EXTINGUISHER/&gt;" : "&lt;FIRE-PROTECTION-FIRE-EXTINGUISHER&gt;" + leaseRequestModel.FIRE_PROTECTION_FIRE_EXTINGUISHER + @"&lt;/FIRE-PROTECTION-FIRE-EXTINGUISHER&gt;") + @"
                    " + (leaseRequestModel.FIRE_PROTECTION_OTHER == null ? "&lt;FIRE-PROTECTION-OTHER/&gt;" : "&lt;FIRE-PROTECTION-OTHER&gt;" + leaseRequestModel.FIRE_PROTECTION_OTHER + @"&lt;/FIRE-PROTECTION-OTHER&gt;") + @"
                    " + (leaseRequestModel.FIRE_PROTECTION_OTHER_DESCRIPTION == null ? "&lt;FIRE-PROTECTION-OTHER-DESCRIPTION/&gt;" : "&lt;FIRE-PROTECTION-OTHER-DESCRIPTION&gt;" + leaseRequestModel.FIRE_PROTECTION_OTHER_DESCRIPTION + @"&lt;/FIRE-PROTECTION-OTHER-DESCRIPTION&gt;") + @"
                    " + (leaseRequestModel.FIRE_PROTECTION_SMOKE_DETECTOR == null ? "&lt;FIRE-PROTECTION-SMOKE-DETECTOR/&gt;" : "&lt;FIRE-PROTECTION-SMOKE-DETECTOR&gt;" + leaseRequestModel.FIRE_PROTECTION_SMOKE_DETECTOR + @"&lt;/FIRE-PROTECTION-SMOKE-DETECTOR&gt;") + @"
                    " + (leaseRequestModel.FIRE_PROTECTION_SPRINKLER_IN_APARTMENT == null ? "&lt;FIRE-PROTECTION-SPRINKLER-IN-APARTMENT/&gt;" : "&lt;FIRE-PROTECTION-SPRINKLER-IN-APARTMENT&gt;" + leaseRequestModel.FIRE_PROTECTION_SPRINKLER_IN_APARTMENT + @"&lt;/FIRE-PROTECTION-SPRINKLER-IN-APARTMENT&gt;") + @"
                    " + (leaseRequestModel.FIRE_PROTECTION_SPRINKLER_IN_COMMON_AREAS == null ? "&lt;FIRE-PROTECTION-SPRINKLER-IN-COMMON-AREAS/&gt;" : "&lt;FIRE-PROTECTION-SPRINKLER-IN-COMMON-AREAS&gt;" + leaseRequestModel.FIRE_PROTECTION_SPRINKLER_IN_COMMON_AREAS + @"&lt;/FIRE-PROTECTION-SPRINKLER-IN-COMMON-AREAS&gt;") + @"
                    " + (leaseRequestModel.PAY_RENT_ADDRESS == null ? "&lt;PAY-RENT-ADDRESS/&gt;" : "&lt;PAY-RENT-ADDRESS&gt;" + leaseRequestModel.PAY_RENT_ADDRESS + @"&lt;/PAY-RENT-ADDRESS&gt;") + @"
                    " + (leaseRequestModel.PAY_RENT_AT_ONLINE_SITE == null ? "&lt;PAY-RENT-AT-ONLINE-SITE/&gt;" : "&lt;PAY-RENT-AT-ONLINE-SITE&gt;" + leaseRequestModel.PAY_RENT_AT_ONLINE_SITE + @"&lt;/PAY-RENT-AT-ONLINE-SITE&gt;") + @"
                    " + (leaseRequestModel.PAY_RENT_AT_OTHER_LOCATION == null ? "&lt;PAY-RENT-AT-OTHER-LOCATION/&gt;" : "&lt;PAY-RENT-AT-OTHER-LOCATION&gt;" + leaseRequestModel.PAY_RENT_AT_OTHER_LOCATION + @"&lt;/PAY-RENT-AT-OTHER-LOCATION&gt;") + @"
                    " + (leaseRequestModel.PAY_RENT_ON_SITE == null ? "&lt;PAY-RENT-ON-SITE/&gt;" : "&lt;PAY-RENT-ON-SITE&gt;" + leaseRequestModel.PAY_RENT_ON_SITE + @"&lt;/PAY-RENT-ON-SITE&gt;") + @"
                    " + (leaseRequestModel.SPECIAL_PROVISIONS == null ? "&lt;SPECIAL-PROVISIONS/&gt;" : "&lt;SPECIAL-PROVISIONS&gt;" + leaseRequestModel.SPECIAL_PROVISIONS + @"&lt;/SPECIAL-PROVISIONS&gt;") + @"
                    " + (leaseRequestModel.UNIT_FURNISHED == null ? "&lt;UNIT-FURNISHED/&gt;" : "&lt;UNIT-FURNISHED&gt;" + leaseRequestModel.UNIT_FURNISHED + @"&lt;/UNIT-FURNISHED&gt;") + @"
                    " + (leaseRequestModel.LOCATOR_ADDRESS == null ? "&lt;LOCATOR-ADDRESS/&gt;" : "&lt;LOCATOR-ADDRESS&gt;" + leaseRequestModel.LOCATOR_ADDRESS + @"&lt;/LOCATOR-ADDRESS&gt;") + @"
                    " + (leaseRequestModel.LOCATOR_CITY_STATE_ZIP == null ? "&lt;LOCATOR-CITY-STATE-ZIP/&gt;" : "&lt;LOCATOR-CITY-STATE-ZIP&gt;" + leaseRequestModel.LOCATOR_CITY_STATE_ZIP + @"&lt;/LOCATOR-CITY-STATE-ZIP&gt;") + @"
                    " + (leaseRequestModel.LOCATOR_NAME == null ? "&lt;LOCATOR-NAME/&gt;" : "&lt;LOCATOR-NAME&gt;" + leaseRequestModel.LOCATOR_NAME + @"&lt;/LOCATOR-NAME&gt;") + @"
                    " + (leaseRequestModel.OWNERS_REPRESENTATIVE_ADDRESS_1 == null ? "&lt;OWNERS-REPRESENTATIVE-ADDRESS-1/&gt;" : "&lt;OWNERS-REPRESENTATIVE-ADDRESS-1&gt;" + leaseRequestModel.OWNERS_REPRESENTATIVE_ADDRESS_1 + @"&lt;/OWNERS-REPRESENTATIVE-ADDRESS-1&gt;") + @"
                    " + (leaseRequestModel.OWNERS_REPRESENTATIVE_ADDRESS_2 == null ? "&lt;OWNERS-REPRESENTATIVE-ADDRESS-2/&gt;" : "&lt;OWNERS-REPRESENTATIVE-ADDRESS-2&gt;" + leaseRequestModel.OWNERS_REPRESENTATIVE_ADDRESS_2 + @"&lt;/OWNERS-REPRESENTATIVE-ADDRESS-2&gt;") + @"
                    " + (leaseRequestModel.OWNERS_REPRESENTATIVE_TELEPHONE == null ? "&lt;OWNERS-REPRESENTATIVE-TELEPHONE/&gt;" : "&lt;OWNERS-REPRESENTATIVE-TELEPHONE&gt;" + leaseRequestModel.OWNERS_REPRESENTATIVE_TELEPHONE + @"&lt;/OWNERS-REPRESENTATIVE-TELEPHONE&gt;") + @"
                    " + (leaseRequestModel.PET_2_AGE == null ? "&lt;PET-2-AGE/&gt;" : "&lt;PET-2-AGE&gt;" + leaseRequestModel.PET_2_AGE + @"&lt;/PET-2-AGE&gt;") + @"
                    " + (leaseRequestModel.PET_2_BREED == null ? "&lt;PET-2-BREED/&gt;" : "&lt;PET-2-BREED&gt;" + leaseRequestModel.PET_2_BREED + @"&lt;/PET-2-BREED&gt;") + @"
                    " + (leaseRequestModel.PET_2_COLOR == null ? "&lt;PET-2-COLOR/&gt;" : "&lt;PET-2-COLOR&gt;" + leaseRequestModel.PET_2_COLOR + @"&lt;/PET-2-COLOR&gt;") + @"
                    " + (leaseRequestModel.PET_2_DATE_OF_LAST_SHOTS == null ? "&lt;PET-2-DATE-OF-LAST-SHOTS/&gt;" : "&lt;PET-2-DATE-OF-LAST-SHOTS&gt;" + leaseRequestModel.PET_2_DATE_OF_LAST_SHOTS + @"&lt;/PET-2-DATE-OF-LAST-SHOTS&gt;") + @"
                    " + (leaseRequestModel.PET_2_HOUSE_BROKEN == null ? "&lt;PET-2-HOUSE-BROKEN/&gt;" : "&lt;PET-2-HOUSE-BROKEN&gt;" + leaseRequestModel.PET_2_HOUSE_BROKEN + @"&lt;/PET-2-HOUSE-BROKEN&gt;") + @"
                    " + (leaseRequestModel.PET_2_LICENSE_CITY == null ? "&lt;PET-2-LICENSE-CITY/&gt;" : "&lt;PET-2-LICENSE-CITY&gt;" + leaseRequestModel.PET_2_LICENSE_CITY + @"&lt;/PET-2-LICENSE-CITY&gt;") + @"
                    " + (leaseRequestModel.PET_2_LICENSE_NUMBER == null ? "&lt;PET-2-LICENSE-NUMBERS/&gt;" : "&lt;PET-2-LICENSE-NUMBER&gt;" + leaseRequestModel.PET_2_LICENSE_NUMBER + @"&lt;/PET-2-LICENSE-NUMBER&gt;") + @"
                    " + (leaseRequestModel.PET_2_NAME == null ? "&lt;PET-2-NAME/&gt;" : "&lt;PET-2-NAME&gt;" + leaseRequestModel.PET_2_NAME + @"&lt;/PET-2-NAME&gt;") + @"
                    " + (leaseRequestModel.PET_2_OWNERS_NAME == null ? "&lt;PET-2-OWNERS-NAME/&gt;" : "&lt;PET-2-OWNERS-NAME&gt;" + leaseRequestModel.PET_2_OWNERS_NAME + @"&lt;/PET-2-OWNERS-NAME&gt;") + @"
                    " + (leaseRequestModel.PET_2_TYPE == null ? "&lt;PET-2-TYPE/&gt;" : "&lt;PET-2-TYPE&gt;" + leaseRequestModel.PET_2_TYPE + @"&lt;/PET-2-TYPE&gt;") + @"
                    " + (leaseRequestModel.PET_2_WEIGHT == null ? "&lt;PET-2-WEIGHT/&gt;" : "&lt;PET-2-WEIGHT&gt;" + leaseRequestModel.PET_2_WEIGHT + @"&lt;/PET-2-WEIGHT&gt;") + @"
                    " + (leaseRequestModel.PET_ADDITIONAL_RENT == null ? "&lt;PET-ADDITIONAL-RENT/&gt;" : "&lt;PET-ADDITIONAL-RENT&gt;" + leaseRequestModel.PET_ADDITIONAL_RENT + @"&lt;/PET-ADDITIONAL-RENT&gt;") + @"
                    " + (leaseRequestModel.PET_AGE == null ? "&lt;PET-AGE/&gt;" : "&lt;PET-AGE&gt;" + leaseRequestModel.PET_AGE + @"&lt;/PET-AGE&gt;") + @"
                    " + (leaseRequestModel.PET_BREED == null ? "&lt;PET-BREED/&gt;" : "&lt;PET-BREED&gt;" + leaseRequestModel.PET_BREED + @"&lt;/PET-BREED&gt;") + @"
                    " + (leaseRequestModel.PET_COLOR == null ? "&lt;PET-COLOR/&gt;" : "&lt;PET-COLOR&gt;" + leaseRequestModel.PET_COLOR + @"&lt;/PET-COLOR&gt;") + @"
                    " + (leaseRequestModel.PET_DATE_OF_LAST_SHOTS == null ? "&lt;PET-DATE-OF-LAST-SHOTS/&gt;" : "&lt;PET-DATE-OF-LAST-SHOTS&gt;" + leaseRequestModel.PET_DATE_OF_LAST_SHOTS + @"&lt;/PET-DATE-OF-LAST-SHOTS&gt;") + @"
                    " + (leaseRequestModel.PET_DR_ADDRESS == null ? "&lt;PET-DR-ADDRESS/&gt;" : "&lt;PET-DR-ADDRESS&gt;" + leaseRequestModel.PET_DR_ADDRESS + @"&lt;/PET-DR-ADDRESS&gt;") + @"
                    " + (leaseRequestModel.PET_DR_CITY == null ? "&lt;PET-DR-CITY/&gt;" : "&lt;PET-DR-CITY&gt;" + leaseRequestModel.PET_DR_CITY + @"&lt;/PET-DR-CITY&gt;") + @"
                    " + (leaseRequestModel.PET_DR_NAME == null ? "&lt;PET-DR-NAME/&gt;" : "&lt;PET-DR-NAME&gt;" + leaseRequestModel.PET_DR_NAME + @"&lt;/PET-DR-NAME&gt;") + @"
                    " + (leaseRequestModel.PET_DR_TELEPHONE == null ? "&lt;PET-DR-TELEPHONE/&gt;" : "&lt;PET-DR-TELEPHONE&gt;" + leaseRequestModel.PET_DR_TELEPHONE + @"&lt;/PET-DR-TELEPHONE&gt;") + @"
                    " + (leaseRequestModel.PET_HOUSE_BROKEN == null ? "&lt;PET-HOUSE-BROKEN/&gt;" : "&lt;PET-HOUSE-BROKEN&gt;" + leaseRequestModel.PET_HOUSE_BROKEN + @"&lt;/PET-HOUSE-BROKEN&gt;") + @"
                    " + (leaseRequestModel.PET_LICENSE_CITY == null ? "&lt;PET-LICENSE-CITY/&gt;" : "&lt;PET-LICENSE-CITY&gt;" + leaseRequestModel.PET_LICENSE_CITY + @"&lt;/PET-LICENSE-CITY&gt;") + @"
                    " + (leaseRequestModel.PET_LICENSE_NUMBER == null ? "&lt;PET-LICENSE-NUMBER/&gt;" : "&lt;PET-LICENSE-NUMBER&gt;" + leaseRequestModel.PET_LICENSE_NUMBER + @"&lt;/PET-LICENSE-NUMBER&gt;") + @"
                    " + (leaseRequestModel.PET_NAME == null ? "&lt;PET-NAME/&gt;" : "&lt;PET-NAME&gt;" + leaseRequestModel.PET_NAME + @"&lt;/PET-NAME&gt;") + @"
                    " + (leaseRequestModel.PET_ONE_TIME_FEE == null ? "&lt;PET-ONE-TIME-FEE/&gt;" : "&lt;PET-ONE-TIME-FEE&gt;" + leaseRequestModel.PET_ONE_TIME_FEE + @"&lt;/PET-ONE-TIME-FEE&gt;") + @"
                    " + (leaseRequestModel.PET_OWNERS_NAME == null ? "&lt;PET-OWNERS-NAME/&gt;" : "&lt;PET-OWNERS-NAME&gt;" + leaseRequestModel.PET_OWNERS_NAME + @"&lt;/PET-OWNERS-NAME&gt;") + @"
                    " + (leaseRequestModel.PET_SECURITY_DEPOSIT == null ? "&lt;PET-SECURITY-DEPOSIT/&gt;" : "&lt;PET-SECURITY-DEPOSIT&gt;" + leaseRequestModel.PET_SECURITY_DEPOSIT + @"&lt;/PET-SECURITY-DEPOSIT&gt;") + @"
                    " + (leaseRequestModel.PET_SECURITY_DEPOSIT_SCOPE == null ? "&lt;PET-SECURITY-DEPOSIT-SCOPE/&gt;" : "&lt;PET-SECURITY-DEPOSIT-SCOPE&gt;" + leaseRequestModel.PET_SECURITY_DEPOSIT_SCOPE + @"&lt;/PET-SECURITY-DEPOSIT-SCOPE&gt;") + @"
                    " + (leaseRequestModel.PET_SPECIAL_PROVISIONS == null ? "&lt;PET-SPECIAL-PROVISIONS/&gt;" : "&lt;PET-SPECIAL-PROVISIONS&gt;" + leaseRequestModel.PET_SPECIAL_PROVISIONS + @"&lt;/PET-SPECIAL-PROVISIONS&gt;") + @"
                    " + (leaseRequestModel.PET_TYPE == null ? "&lt;PET-TYPE/&gt;" : "&lt;PET-TYPE&gt;" + leaseRequestModel.PET_TYPE + @"&lt;/PET-TYPE&gt;") + @"
                    " + (leaseRequestModel.PET_URINATE_INSIDE_AREAS == null ? "&lt;PET-URINATE-INSIDE-AREAS/&gt;" : "&lt;PET-URINATE-INSIDE-AREAS&gt;" + leaseRequestModel.PET_URINATE_INSIDE_AREAS + @"&lt;/PET-URINATE-INSIDE-AREAS&gt;") + @"
                    " + (leaseRequestModel.PET_URINATE_OUTSIDE_AREAS == null ? "&lt;PET-URINATE-OUTSIDE-AREAS/&gt;" : "&lt;PET-URINATE-OUTSIDE-AREAS&gt;" + leaseRequestModel.PET_URINATE_OUTSIDE_AREAS + @"&lt;/PET-URINATE-OUTSIDE-AREAS&gt;") + @"
                    " + (leaseRequestModel.PET_WEIGHT == null ? "&lt;PET-WEIGHT/&gt;" : "&lt;PET-WEIGHT&gt;" + leaseRequestModel.PET_WEIGHT + @"&lt;/PET-WEIGHT&gt;") + @"
                    " + (leaseRequestModel.SECURITY_DEPOSIT_INCLUDES_ADD_PET_RENT == null ? "&lt;SECURITY-DEPOSIT-INCLUDES-ADD-PET-RENT/&gt;" : "&lt;SECURITY-DEPOSIT-INCLUDES-ADD-PET-RENT&gt;" + leaseRequestModel.SECURITY_DEPOSIT_INCLUDES_ADD_PET_RENT + @"&lt;/SECURITY-DEPOSIT-INCLUDES-ADD-PET-RENT&gt;") + @"
                    " + (leaseRequestModel.SECURITY_DEPOSIT_INCLUDES_ANIMAL_DEPOSIT == null ? "&lt;SECURITY-DEPOSIT-INCLUDES-ANIMAL-DEPOSIT/&gt;" : "&lt;SECURITY-DEPOSIT-INCLUDES-ANIMAL-DEPOSIT&gt;" + leaseRequestModel.SECURITY_DEPOSIT_INCLUDES_ANIMAL_DEPOSIT + @"&lt;/SECURITY-DEPOSIT-INCLUDES-ANIMAL-DEPOSIT&gt;") + @"
                    " + (leaseRequestModel.EARLY_TERMINATION_CHOICE_OF_DAMAGES == null ? "&lt;EARLY-TERMINATION-CHOICE-OF-DAMAGES/&gt;" : "&lt;EARLY-TERMINATION-CHOICE-OF-DAMAGES&gt;" + leaseRequestModel.EARLY_TERMINATION_CHOICE_OF_DAMAGES + @"&lt;/EARLY-TERMINATION-CHOICE-OF-DAMAGES&gt;") + @"
                    " + (leaseRequestModel.EARLY_TERMINATION_FEE == null ? "&lt;EARLY-TERMINATION-FEE/&gt;" : "&lt;EARLY-TERMINATION-FEE&gt;" + leaseRequestModel.EARLY_TERMINATION_FEE + @"&lt;/EARLY-TERMINATION-FEE&gt;") + @"
                    " + (leaseRequestModel.GARAGE_ADDENDUM_CARPORT_PROVIDED == null ? "&lt;GARAGE-ADDENDUM-CARPORT-PROVIDED/&gt;" : "&lt;GARAGE-ADDENDUM-CARPORT-PROVIDED&gt;" + leaseRequestModel.GARAGE_ADDENDUM_CARPORT_PROVIDED + @"&lt;/GARAGE-ADDENDUM-CARPORT-PROVIDED&gt;") + @"
                    " + (leaseRequestModel.GARAGE_ADDENDUM_CARPORT_SPACE_NO == null ? "&lt;GARAGE-ADDENDUM-CARPORT-SPACE-NO/&gt;" : "&lt;GARAGE-ADDENDUM-CARPORT-SPACE-NO&gt;" + leaseRequestModel.GARAGE_ADDENDUM_CARPORT_SPACE_NO + @"&lt;/GARAGE-ADDENDUM-CARPORT-SPACE-NO&gt;") + @"
                    " + (leaseRequestModel.GARAGE_ADDENDUM_GARAGE_OR_CARPORT_PROVIDED == null ? "&lt;GARAGE-ADDENDUM-GARAGE-OR-CARPORT-PROVIDED/&gt;" : "&lt;GARAGE-ADDENDUM-GARAGE-OR-CARPORT-PROVIDED&gt;" + leaseRequestModel.GARAGE_ADDENDUM_GARAGE_OR_CARPORT_PROVIDED + @"&lt;/GARAGE-ADDENDUM-GARAGE-OR-CARPORT-PROVIDED&gt;") + @"
                    " + (leaseRequestModel.GARAGE_ADDENDUM_GARAGE_PROVIDED == null ? "&lt;GARAGE-ADDENDUM-GARAGE-PROVIDED/&gt;" : "&lt;GARAGE-ADDENDUM-GARAGE-PROVIDED&gt;" + leaseRequestModel.GARAGE_ADDENDUM_GARAGE_PROVIDED + @"&lt;/GARAGE-ADDENDUM-GARAGE-PROVIDED&gt;") + @"
                    " + (leaseRequestModel.GARAGE_ADDENDUM_GARAGE_SPACE_NO == null ? "&lt;GARAGE-ADDENDUM-GARAGE-SPACE-NO/&gt;" : "&lt;GARAGE-ADDENDUM-GARAGE-SPACE-NO&gt;" + leaseRequestModel.GARAGE_ADDENDUM_GARAGE_SPACE_NO + @"&lt;/GARAGE-ADDENDUM-GARAGE-SPACE-NO&gt;") + @"
                    " + (leaseRequestModel.GARAGE_ADDENDUM_SPECIAL_PROVISIONS == null ? "&lt;GARAGE-ADDENDUM-SPECIAL-PROVISIONS/&gt;" : "&lt;GARAGE-ADDENDUM-SPECIAL-PROVISIONS&gt;" + leaseRequestModel.GARAGE_ADDENDUM_SPECIAL_PROVISIONS + @"&lt;/GARAGE-ADDENDUM-SPECIAL-PROVISIONS&gt;") + @"
                    " + (leaseRequestModel.GARAGE_ADDENDUM_STORAGE_UNIT_NO == null ? "&lt;GARAGE-ADDENDUM-STORAGE-UNIT-NO/&gt;" : "&lt;GARAGE-ADDENDUM-STORAGE-UNIT-NO&gt;" + leaseRequestModel.GARAGE_ADDENDUM_STORAGE_UNIT_NO + @"&lt;/GARAGE-ADDENDUM-STORAGE-UNIT-NO&gt;") + @"
                    " + (leaseRequestModel.GARAGE_ADDENDUM_STORAGE_UNIT_PROVIDED == null ? "&lt;GARAGE-ADDENDUM-STORAGE-UNIT-PROVIDED/&gt;" : "&lt;GARAGE-ADDENDUM-STORAGE-UNIT-PROVIDED&gt;" + leaseRequestModel.GARAGE_ADDENDUM_STORAGE_UNIT_PROVIDED + @"&lt;/GARAGE-ADDENDUM-STORAGE-UNIT-PROVIDED&gt;") + @"
                    " + (leaseRequestModel.NO_SMOKING_ADDENDUM_SMOKING_AREAS == null ? "&lt;NO-SMOKING-ADDENDUM-SMOKING-AREAS/&gt;" : "&lt;NO-SMOKING-ADDENDUM-SMOKING-AREAS&gt;" + leaseRequestModel.NO_SMOKING_ADDENDUM_SMOKING_AREAS + @"&lt;/NO-SMOKING-ADDENDUM-SMOKING-AREAS&gt;") + @"
                    " + (leaseRequestModel.NO_SMOKING_ADDENDUM_SMOKING_DISTANCE == null ? "&lt;NO-SMOKING-ADDENDUM-SMOKING-DISTANCE/&gt;" : "&lt;NO-SMOKING-ADDENDUM-SMOKING-DISTANCE&gt;" + leaseRequestModel.NO_SMOKING_ADDENDUM_SMOKING_DISTANCE + @"&lt;/NO-SMOKING-ADDENDUM-SMOKING-DISTANCE&gt;") + @"
                    " + (leaseRequestModel.NO_SMOKING_ADDENDUM_SMOKING_POLICY == null ? "&lt;NO-SMOKING-ADDENDUM-SMOKING-POLICY/&gt;" : "&lt;NO-SMOKING-ADDENDUM-SMOKING-POLICY&gt;" + leaseRequestModel.NO_SMOKING_ADDENDUM_SMOKING_POLICY + @"&lt;/NO-SMOKING-ADDENDUM-SMOKING-POLICY&gt;") + @"
                    " + (leaseRequestModel.NO_SMOKING_SPECIAL_PROVISIONS == null ? "&lt;NO-SMOKING-SPECIAL-PROVISIONS/&gt;" : "&lt;NO-SMOKING-SPECIAL-PROVISIONS&gt;" + leaseRequestModel.NO_SMOKING_SPECIAL_PROVISIONS + @"&lt;/NO-SMOKING-SPECIAL-PROVISIONS&gt;") + @"
                    " + (leaseRequestModel.RAMF_POLICY_CONTACT_PERSON == null ? "&lt;RAMF-POLICY-CONTACT-PERSON/&gt;" : "&lt;RAMF-POLICY-CONTACT-PERSON&gt;" + leaseRequestModel.RAMF_POLICY_CONTACT_PERSON + @"&lt;/RAMF-POLICY-CONTACT-PERSON&gt;") + @"
                    " + (leaseRequestModel.RAMF_POLICY_CONTACT_PHONE_AND_ADDRESS == null ? "&lt;RAMF-POLICY-CONTACT-PHONE-AND-ADDRESS/&gt;" : "&lt;RAMF-POLICY-CONTACT-PHONE-AND-ADDRESS&gt;" + leaseRequestModel.RAMF_POLICY_CONTACT_PHONE_AND_ADDRESS + @"&lt;/RAMF-POLICY-CONTACT-PHONE-AND-ADDRES&gt;") + @"
                    " + (leaseRequestModel.SATELLITE_ADDENDUM_DEPOSIT_SCOPE == null ? "&lt;SATELLITE-ADDENDUM-DEPOSIT-SCOPE/&gt;" : "&lt;SATELLITE-ADDENDUM-DEPOSIT-SCOPE&gt;" + leaseRequestModel.SATELLITE_ADDENDUM_DEPOSIT_SCOPE + @"&lt;/SATELLITE-ADDENDUM-DEPOSIT-SCOPE&gt;") + @"
                    " + (leaseRequestModel.SATELLITE_ADDENDUM_INSURANCE == null ? "&lt;SATELLITE-ADDENDUM-INSURANCE/&gt;" : "&lt;SATELLITE-ADDENDUM-INSURANCE&gt;" + leaseRequestModel.SATELLITE_ADDENDUM_INSURANCE + @"&lt;/SATELLITE-ADDENDUM-INSURANCE&gt;") + @"
                    " + (leaseRequestModel.SATELLITE_ADDENDUM_NO_DISHES == null ? "&lt;SATELLITE-ADDENDUM-NO-DISHES/&gt;" : "&lt;SATELLITE-ADDENDUM-NO-DISHES&gt;" + leaseRequestModel.SATELLITE_ADDENDUM_NO_DISHES + @"&lt;/SATELLITE-ADDENDUM-NO-DISHES&gt;") + @"
                    " + (leaseRequestModel.SATELLITE_ADDENDUM_SECURITY_DEPOSIT == null ? "&lt;SATELLITE-ADDENDUM-SECURITY-DEPOSIT/&gt;" : "&lt;SATELLITE-ADDENDUM-SECURITY-DEPOSIT&gt;" + leaseRequestModel.SATELLITE_ADDENDUM_SECURITY_DEPOSIT + @"&lt;/SATELLITE-ADDENDUM-SECURITY-DEPOSIT&gt;") + @"
                    " + (leaseRequestModel.SATELLITE_ADDENDUM_SPECIAL_PROVISIONS == null ? "&lt;SATELLITE-ADDENDUM-SPECIAL-PROVISIONS/&gt;" : "&lt;SATELLITE-ADDENDUM-SPECIAL-PROVISIONS&gt;" + leaseRequestModel.SATELLITE_ADDENDUM_SPECIAL_PROVISIONS + @"&lt;/SATELLITE-ADDENDUM-SPECIAL-PROVISIONS&gt;") + @"
                    " + (leaseRequestModel.SECURITY_DEPOSIT_INCLUDES_SATELLITE_DEP == null ? "&lt;SECURITY-DEPOSIT-INCLUDES-SATELLITE-DEP/&gt;" : "&lt;SECURITY-DEPOSIT-INCLUDES-SATELLITE-DEP&gt;" + leaseRequestModel.SECURITY_DEPOSIT_INCLUDES_SATELLITE_DEP + @"&lt;/SECURITY-DEPOSIT-INCLUDES-SATELLITE-DEP&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_ADMINISTRATION_FEE == null ? "&lt;UTILITY-ADDENDUM-ADMINISTRATION-FEE/&gt;" : "&lt;UTILITY-ADDENDUM-ADMINISTRATION-FEE&gt;" + leaseRequestModel.UTILITY_ADDENDUM_ADMINISTRATION_FEE + @"&lt;/UTILITY-ADDENDUM-ADMINISTRATION-FEE&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_BILLING_FEE_LIMIT == null ? "&lt;UTILITY-ADDENDUM-BILLING-FEE-LIMIT/&gt;" : "&lt;UTILITY-ADDENDUM-BILLING-FEE-LIMIT&gt;" + leaseRequestModel.UTILITY_ADDENDUM_BILLING_FEE_LIMIT + @"&lt;/UTILITY-ADDENDUM-BILLING-FEE-LIMIT&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_CABLE_TV_BILLING_CO == null ? "&lt;UTILITY-ADDENDUM-CABLE-TV-BILLING-CO/&gt;" : "&lt;UTILITY-ADDENDUM-CABLE-TV-BILLING-CO&gt;" + leaseRequestModel.UTILITY_ADDENDUM_CABLE_TV_BILLING_CO + @"&lt;/UTILITY-ADDENDUM-CABLE-TV-BILLING-CO&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_CABLE_TV_BILLING_CO_DESC == null ? "&lt;UTILITY-ADDENDUM-CABLE-TV-BILLING-CO-DESC/&gt;" : "&lt;UTILITY-ADDENDUM-CABLE-TV-BILLING-CO-DESC&gt;" + leaseRequestModel.UTILITY_ADDENDUM_CABLE_TV_BILLING_CO_DESC + @"&lt;/UTILITY-ADDENDUM-CABLE-TV-BILLING-CO-DESC&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_CABLE_TV_FLAT_RATE_SELECTED == null ? "&lt;UTILITY-ADDENDUM-CABLE-TV-FLAT-RATE-SELECTED/&gt;" : "&lt;UTILITY-ADDENDUM-CABLE-TV-FLAT-RATE-SELECTED&gt;" + leaseRequestModel.UTILITY_ADDENDUM_CABLE_TV_FLAT_RATE_SELECTED + @"&lt;/UTILITY-ADDENDUM-CABLE-TV-FLAT-RATE-SELECTED&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_CABLE_TV_FORMULA == null ? "&lt;UTILITY-ADDENDUM-CABLE-TV-FORMULA/&gt;" : "&lt;UTILITY-ADDENDUM-CABLE-TV-FORMULA&gt;" + leaseRequestModel.UTILITY_ADDENDUM_CABLE_TV_FORMULA + @"&lt;/UTILITY-ADDENDUM-CABLE-TV-FORMULA&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_CABLE_TV_PAID_BY == null ? "&lt;UTILITY-ADDENDUM-CABLE-TV-PAID-BY/&gt;" : "&lt;UTILITY-ADDENDUM-CABLE-TV-PAID-BY&gt;" + leaseRequestModel.UTILITY_ADDENDUM_CABLE_TV_PAID_BY + @"&lt;/UTILITY-ADDENDUM-CABLE-TV-PAID-BY&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_CABLE_TV_RATE == null ? "&lt;UTILITY-ADDENDUM-CABLE-TV-RATE/&gt;" : "&lt;UTILITY-ADDENDUM-CABLE-TV-RATE&gt;" + leaseRequestModel.UTILITY_ADDENDUM_CABLE_TV_RATE + @"&lt;/UTILITY-ADDENDUM-CABLE-TV-RATE&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_DAYS_TO_PAY_BILL == null ? "&lt;UTILITY-ADDENDUM-DAYS-TO-PAY-BILL/&gt;" : "&lt;UTILITY-ADDENDUM-DAYS-TO-PAY-BILL&gt;" + leaseRequestModel.UTILITY_ADDENDUM_DAYS_TO_PAY_BILL + @"&lt;/UTILITY-ADDENDUM-DAYS-TO-PAY-BILL&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_ELECTRIC_BILLING_CO == null ? "&lt;UTILITY-ADDENDUM-ELECTRIC-BILLING-CO/&gt;" : "&lt;UTILITY-ADDENDUM-ELECTRIC-BILLING-CO&gt;" + leaseRequestModel.UTILITY_ADDENDUM_ELECTRIC_BILLING_CO + @"&lt;/UTILITY-ADDENDUM-ELECTRIC-BILLING-CO&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_ELECTRIC_BILLING_CO_DESC == null ? "&lt;UTILITY-ADDENDUM-ELECTRIC-BILLING-CO-DESC/&gt;" : "&lt;UTILITY-ADDENDUM-ELECTRIC-BILLING-CO-DESC&gt;" + leaseRequestModel.UTILITY_ADDENDUM_ELECTRIC_BILLING_CO_DESC + @"&lt;/UTILITY-ADDENDUM-ELECTRIC-BILLING-CO-DESC&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_ELECTRIC_FLAT_RATE_SELECTED == null ? "&lt;UTILITY-ADDENDUM-ELECTRIC-FLAT-RATE-SELECTED/&gt;" : "&lt;UTILITY-ADDENDUM-ELECTRIC-FLAT-RATE-SELECTED&gt;" + leaseRequestModel.UTILITY_ADDENDUM_ELECTRIC_FLAT_RATE_SELECTED + @"&lt;/UTILITY-ADDENDUM-ELECTRIC-FLAT-RATE-SELECTED&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_ELECTRIC_FORMULA == null ? "&lt;UTILITY-ADDENDUM-ELECTRIC-FORMULA/&gt;" : "&lt;UTILITY-ADDENDUM-ELECTRIC-FORMULA&gt;" + leaseRequestModel.UTILITY_ADDENDUM_ELECTRIC_FORMULA + @"&lt;/UTILITY-ADDENDUM-ELECTRIC-FORMULA&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_ELECTRIC_PAID_BY == null ? "&lt;UTILITY-ADDENDUM-ELECTRIC-PAID-BY/&gt;" : "&lt;UTILITY-ADDENDUM-ELECTRIC-PAID-BY&gt;" + leaseRequestModel.UTILITY_ADDENDUM_ELECTRIC_PAID_BY + @"&lt;/UTILITY-ADDENDUM-ELECTRIC-PAID-BY&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_ELECTRIC_RATE == null ? "&lt;UTILITY-ADDENDUM-ELECTRIC-RATE/&gt;" : "&lt;UTILITY-ADDENDUM-ELECTRIC-RATE&gt;" + leaseRequestModel.UTILITY_ADDENDUM_ELECTRIC_RATE + @"&lt;/UTILITY-ADDENDUM-ELECTRIC-RATE&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_FINAL_BILL_FEE == null ? "&lt;UTILITY-ADDENDUM-FINAL-BILL-FEE/&gt;" : "&lt;UTILITY-ADDENDUM-FINAL-BILL-FEE&gt;" + leaseRequestModel.UTILITY_ADDENDUM_FINAL_BILL_FEE + @"&lt;/UTILITY-ADDENDUM-FINAL-BILL-FEE&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_FINAL_BILL_FEE_LIMIT == null ? "&lt;UTILITY-ADDENDUM-FINAL-BILL-FEE-LIMIT/&gt;" : "&lt;UTILITY-ADDENDUM-FINAL-BILL-FEE-LIMIT&gt;" + leaseRequestModel.UTILITY_ADDENDUM_FINAL_BILL_FEE_LIMIT + @"&lt;/UTILITY-ADDENDUM-FINAL-BILL-FEE-LIMIT&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_GAS_BILLING_CO == null ? "&lt;UTILITY-ADDENDUM-GAS-BILLING-CO/&gt;" : "&lt;UTILITY-ADDENDUM-GAS-BILLING-CO&gt;" + leaseRequestModel.UTILITY_ADDENDUM_GAS_BILLING_CO + @"&lt;/UTILITY-ADDENDUM-GAS-BILLING-CO&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_GAS_BILLING_CO_DESC == null ? "&lt;UTILITY-ADDENDUM-GAS-BILLING-CO-DESC/&gt;" : "&lt;UTILITY-ADDENDUM-GAS-BILLING-CO-DESC&gt;" + leaseRequestModel.UTILITY_ADDENDUM_GAS_BILLING_CO_DESC + @"&lt;/UTILITY-ADDENDUM-GAS-BILLING-CO-DESC&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_GAS_FLAT_RATE_SELECTED == null ? "&lt;UTILITY-ADDENDUM-GAS-FLAT-RATE-SELECTED/&gt;" : "&lt;UTILITY-ADDENDUM-GAS-FLAT-RATE-SELECTED&gt;" + leaseRequestModel.UTILITY_ADDENDUM_GAS_FLAT_RATE_SELECTED + @"&lt;/UTILITY-ADDENDUM-GAS-FLAT-RATE-SELECTED&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_GAS_FORMULA == null ? "&lt;UTILITY-ADDENDUM-GAS-FORMULA/&gt;" : "&lt;UTILITY-ADDENDUM-GAS-FORMULA&gt;" + leaseRequestModel.UTILITY_ADDENDUM_GAS_FORMULA + @"&lt;/UTILITY-ADDENDUM-GAS-FORMULA&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_GAS_PAID_BY == null ? "&lt;UTILITY-ADDENDUM-GAS-PAID-BY/&gt;" : "&lt;UTILITY-ADDENDUM-GAS-PAID-BY&gt;" + leaseRequestModel.UTILITY_ADDENDUM_GAS_PAID_BY + @"&lt;/UTILITY-ADDENDUM-GAS-PAID-BY&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_GAS_RATE == null ? "&lt;UTILITY-ADDENDUM-GAS-RATE/&gt;" : "&lt;UTILITY-ADDENDUM-GAS-RATE&gt;" + leaseRequestModel.UTILITY_ADDENDUM_GAS_RATE + @"&lt;/UTILITY-ADDENDUM-GAS-RATE&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_INTERNET_BILLING_CO == null ? "&lt;UTILITY-ADDENDUM-INTERNET-BILLING-CO/&gt;" : "&lt;UTILITY-ADDENDUM-INTERNET-BILLING-CO&gt;" + leaseRequestModel.UTILITY_ADDENDUM_INTERNET_BILLING_CO + @"&lt;/UTILITY-ADDENDUM-INTERNET-BILLING-CO&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_INTERNET_BILLING_CO_DESC == null ? "&lt;UTILITY-ADDENDUM-INTERNET-BILLING-CO-DESC/&gt;" : "&lt;UTILITY-ADDENDUM-INTERNET-BILLING-CO-DESC&gt;" + leaseRequestModel.UTILITY_ADDENDUM_INTERNET_BILLING_CO_DESC + @"&lt;/UTILITY-ADDENDUM-INTERNET-BILLING-CO-DESC&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_INTERNET_FLAT_RATE_SELECTED == null ? "&lt;UTILITY-ADDENDUM-INTERNET-FLAT-RATE-SELECTED/&gt;" : "&lt;UTILITY-ADDENDUM-INTERNET-FLAT-RATE-SELECTED&gt;" + leaseRequestModel.UTILITY_ADDENDUM_INTERNET_FLAT_RATE_SELECTED + @"&lt;/UTILITY-ADDENDUM-INTERNET-FLAT-RATE-SELECTED&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_INTERNET_FORMULA == null ? "&lt;UTILITY-ADDENDUM-INTERNET-FORMULA/&gt;" : "&lt;UTILITY-ADDENDUM-INTERNET-FORMULA&gt;" + leaseRequestModel.UTILITY_ADDENDUM_INTERNET_FORMULA + @"&lt;/UTILITY-ADDENDUM-INTERNET-FORMULA&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_INTERNET_PAID_BY == null ? "&lt;UTILITY-ADDENDUM-INTERNET-PAID-BY/&gt;" : "&lt;UTILITY-ADDENDUM-INTERNET-PAID-BY&gt;" + leaseRequestModel.UTILITY_ADDENDUM_INTERNET_PAID_BY + @"&lt;/UTILITY-ADDENDUM-INTERNET-PAID-BY&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_INTERNET_RATE == null ? "&lt;UTILITY-ADDENDUM-INTERNET-RATE/&gt;" : "&lt;UTILITY-ADDENDUM-INTERNET-RATE&gt;" + leaseRequestModel.UTILITY_ADDENDUM_INTERNET_RATE + @"&lt;/UTILITY-ADDENDUM-INTERNET-RATE&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_LATE_FEE == null ? "&lt;UTILITY-ADDENDUM-LATE-FEE/&gt;" : "&lt;UTILITY-ADDENDUM-LATE-FEE&gt;" + leaseRequestModel.UTILITY_ADDENDUM_LATE_FEE + @"&lt;/UTILITY-ADDENDUM-LATE-FEE&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_LATE_FEE_LIMIT == null ? "&lt;UTILITY-ADDENDUM-LATE-FEE-LIMIT/&gt;" : "&lt;UTILITY-ADDENDUM-LATE-FEE-LIMIT&gt;" + leaseRequestModel.UTILITY_ADDENDUM_LATE_FEE_LIMIT + @"&lt;/UTILITY-ADDENDUM-LATE-FEE-LIMIT&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_MASTER_ANTENNA_BILLING_CO == null ? "&lt;UTILITY-ADDENDUM-MASTER-ANTENNA-BILLING-CO/&gt;" : "&lt;UTILITY-ADDENDUM-MASTER-ANTENNA-BILLING-CO&gt;" + leaseRequestModel.UTILITY_ADDENDUM_MASTER_ANTENNA_BILLING_CO + @"&lt;/UTILITY-ADDENDUM-MASTER-ANTENNA-BILLING-CO&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_MASTER_ANTENNA_BILLING_CO_DESC == null ? "&lt;UTILITY-ADDENDUM-MASTER-ANTENNA-BILLING-CO-DESC/&gt;" : "&lt;UTILITY-ADDENDUM-MASTER-ANTENNA-BILLING-CO-DESC&gt;" + leaseRequestModel.UTILITY_ADDENDUM_MASTER_ANTENNA_BILLING_CO_DESC + @"&lt;/UTILITY-ADDENDUM-MASTER-ANTENNA-BILLING-CO-DESC&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_MASTER_ANTENNA_FLAT_RATE_SELECTED == null ? "&lt;UTILITY-ADDENDUM-MASTER-ANTENNA-FLAT-RATE-SELECTED/&gt;" : "&lt;UTILITY-ADDENDUM-MASTER-ANTENNA-FLAT-RATE-SELECTED&gt;" + leaseRequestModel.UTILITY_ADDENDUM_MASTER_ANTENNA_FLAT_RATE_SELECTED + @"&lt;/UTILITY-ADDENDUM-MASTER-ANTENNA-FLAT-RATE-SELECTED&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_MASTER_ANTENNA_FORMULA == null ? "&lt;UTILITY-ADDENDUM-MASTER-ANTENNA-FORMULA/&gt;" : "&lt;UTILITY-ADDENDUM-MASTER-ANTENNA-FORMULA&gt;" + leaseRequestModel.UTILITY_ADDENDUM_MASTER_ANTENNA_FORMULA + @"&lt;/UTILITY-ADDENDUM-MASTER-ANTENNA-FORMULA&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_MASTER_ANTENNA_PAID_BY == null ? "&lt;UTILITY-ADDENDUM-MASTER-ANTENNA-PAID-BY/&gt;" : "&lt;UTILITY-ADDENDUM-MASTER-ANTENNA-PAID-BY&gt;" + leaseRequestModel.UTILITY_ADDENDUM_MASTER_ANTENNA_PAID_BY + @"&lt;/UTILITY-ADDENDUM-MASTER-ANTENNA-PAID-BY&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_MASTER_ANTENNA_RATE == null ? "&lt;UTILITY-ADDENDUM-MASTER-ANTENNA-RATE/&gt;" : "&lt;UTILITY-ADDENDUM-MASTER-ANTENNA-RATE&gt;" + leaseRequestModel.UTILITY_ADDENDUM_MASTER_ANTENNA_RATE + @"&lt;/UTILITY-ADDENDUM-MASTER-ANTENNA-RATE&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_OTHER == null ? "&lt;UTILITY-ADDENDUM-OTHER/&gt;" : "&lt;UTILITY-ADDENDUM-OTHER&gt;" + leaseRequestModel.UTILITY_ADDENDUM_OTHER + @"&lt;/UTILITY-ADDENDUM-OTHER&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_OTHER_2 == null ? "&lt;UTILITY-ADDENDUM-OTHER-2/&gt;" : "&lt;UTILITY-ADDENDUM-OTHER-2&gt;" + leaseRequestModel.UTILITY_ADDENDUM_OTHER_2 + @"&lt;/UTILITY-ADDENDUM-OTHER-2&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_OTHER_BILLING_CO == null ? "&lt;UTILITY-ADDENDUM-OTHER-BILLING-CO/&gt;" : "&lt;UTILITY-ADDENDUM-OTHER-BILLING-CO&gt;" + leaseRequestModel.UTILITY_ADDENDUM_OTHER_BILLING_CO + @"&lt;/UTILITY-ADDENDUM-OTHER-BILLING-CO&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_OTHER_BILLING_CO_2 == null ? "&lt;UTILITY-ADDENDUM-OTHER-BILLING-CO-2/&gt;" : "&lt;UTILITY-ADDENDUM-OTHER-BILLING-CO-2&gt;" + leaseRequestModel.UTILITY_ADDENDUM_OTHER_BILLING_CO_2 + @"&lt;/UTILITY-ADDENDUM-OTHER-BILLING-CO-2&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_OTHER_BILLING_CO_DESC == null ? "&lt;UTILITY-ADDENDUM-OTHER-BILLING-CO-DESC/&gt;" : "&lt;UTILITY-ADDENDUM-OTHER-BILLING-CO-DESC&gt;" + leaseRequestModel.UTILITY_ADDENDUM_OTHER_BILLING_CO_DESC + @"&lt;/UTILITY-ADDENDUM-OTHER-BILLING-CO-DESC&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_OTHER_BILLING_CO_DESC_2 == null ? "&lt;UTILITY-ADDENDUM-OTHER-BILLING-CO-DESC-2/&gt;" : "&lt;UTILITY-ADDENDUM-OTHER-BILLING-CO-DESC-2&gt;" + leaseRequestModel.UTILITY_ADDENDUM_OTHER_BILLING_CO_DESC_2 + @"&lt;/UTILITY-ADDENDUM-OTHER-BILLING-CO-DESC-2&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_OTHER_BILLING_FEE == null ? "&lt;UTILITY-ADDENDUM-OTHER-BILLING-FEE/&gt;" : "&lt;UTILITY-ADDENDUM-OTHER-BILLING-FEE&gt;" + leaseRequestModel.UTILITY_ADDENDUM_OTHER_BILLING_FEE + @"&lt;/UTILITY-ADDENDUM-OTHER-BILLING-FEE&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_OTHER_FLAT_RATE_SELECTED == null ? "&lt;UTILITY-ADDENDUM-OTHER-FLAT-RATE-SELECTED/&gt;" : "&lt;UTILITY-ADDENDUM-OTHER-FLAT-RATE-SELECTED&gt;" + leaseRequestModel.UTILITY_ADDENDUM_OTHER_FLAT_RATE_SELECTED + @"&lt;/UTILITY-ADDENDUM-OTHER-FLAT-RATE-SELECTED&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_OTHER_FLAT_RATE_SELECTED_2 == null ? "&lt;UTILITY-ADDENDUM-OTHER-FLAT-RATE-SELECTED-2/&gt;" : "&lt;UTILITY-ADDENDUM-OTHER-FLAT-RATE-SELECTED-2&gt;" + leaseRequestModel.UTILITY_ADDENDUM_OTHER_FLAT_RATE_SELECTED_2 + @"&lt;/UTILITY-ADDENDUM-OTHER-FLAT-RATE-SELECTED-2&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_OTHER_FORMULA == null ? "&lt;UTILITY-ADDENDUM-OTHER-FORMULA/&gt;" : "&lt;UTILITY-ADDENDUM-OTHER-FORMULA&gt;" + leaseRequestModel.UTILITY_ADDENDUM_OTHER_FORMULA + @"&lt;/UTILITY-ADDENDUM-OTHER-FORMULA&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_OTHER_FORMULA_2 == null ? "&lt;UTILITY-ADDENDUM-OTHER-FORMULA-2/&gt;" : "&lt;UTILITY-ADDENDUM-OTHER-FORMULA-2&gt;" + leaseRequestModel.UTILITY_ADDENDUM_OTHER_FORMULA_2 + @"&lt;/UTILITY-ADDENDUM-OTHER-FORMULA-2&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_OTHER_PAID_BY == null ? "&lt;UTILITY-ADDENDUM-OTHER-PAID-BY/&gt;" : "&lt;UTILITY-ADDENDUM-OTHER-PAID-BY&gt;" + leaseRequestModel.UTILITY_ADDENDUM_OTHER_PAID_BY + @"&lt;/UTILITY-ADDENDUM-OTHER-PAID-BY&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_OTHER_PAID_BY_2 == null ? "&lt;UTILITY-ADDENDUM-OTHER-PAID-BY-2/&gt;" : "&lt;UTILITY-ADDENDUM-OTHER-PAID-BY-2&gt;" + leaseRequestModel.UTILITY_ADDENDUM_OTHER_PAID_BY_2 + @"&lt;/UTILITY-ADDENDUM-OTHER-PAID-BY-2&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_OTHER_RATE == null ? "&lt;UTILITY-ADDENDUM-OTHER-RATE/&gt;" : "&lt;UTILITY-ADDENDUM-OTHER-RATE&gt;" + leaseRequestModel.UTILITY_ADDENDUM_OTHER_RATE + @"&lt;/UTILITY-ADDENDUM-OTHER-RATE&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_OTHER_RATE_2 == null ? "&lt;UTILITY-ADDENDUM-OTHER-RATE-2/&gt;" : "&lt;UTILITY-ADDENDUM-OTHER-RATE-2&gt;" + leaseRequestModel.UTILITY_ADDENDUM_OTHER_RATE_2 + @"&lt;/UTILITY-ADDENDUM-OTHER-RATE-2&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_PEST_CONTROL_BILLING_CO == null ? "&lt;UTILITY-ADDENDUM-PEST-CONTROL-BILLING-CO/&gt;" : "&lt;UTILITY-ADDENDUM-PEST-CONTROL-BILLING-CO&gt;" + leaseRequestModel.UTILITY_ADDENDUM_PEST_CONTROL_BILLING_CO + @"&lt;/UTILITY-ADDENDUM-PEST-CONTROL-BILLING-CO&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_PEST_CONTROL_BILLING_CO_DESC == null ? "&lt;UTILITY-ADDENDUM-PEST-CONTROL-BILLING-CO-DESC/&gt;" : "&lt;UTILITY-ADDENDUM-PEST-CONTROL-BILLING-CO-DESC&gt;" + leaseRequestModel.UTILITY_ADDENDUM_PEST_CONTROL_BILLING_CO_DESC + @"&lt;/UTILITY-ADDENDUM-PEST-CONTROL-BILLING-CO-DESC&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_PEST_CONTROL_FLAT_RATE_SELECTED == null ? "&lt;UTILITY-ADDENDUM-PEST-CONTROL-FLAT-RATE-SELECTED/&gt;" : "&lt;UTILITY-ADDENDUM-PEST-CONTROL-FLAT-RATE-SELECTED&gt;" + leaseRequestModel.UTILITY_ADDENDUM_PEST_CONTROL_FLAT_RATE_SELECTED + @"&lt;/UTILITY-ADDENDUM-PEST-CONTROL-FLAT-RATE-SELECTED&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_PEST_CONTROL_FORMULA == null ? "&lt;UTILITY-ADDENDUM-PEST-CONTROL-FORMULA/&gt;" : "&lt;UTILITY-ADDENDUM-PEST-CONTROL-FORMULA&gt;" + leaseRequestModel.UTILITY_ADDENDUM_PEST_CONTROL_FORMULA + @"&lt;/UTILITY-ADDENDUM-PEST-CONTROL-FORMULA&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_PEST_CONTROL_PAID_BY == null ? "&lt;UTILITY-ADDENDUM-PEST-CONTROL-PAID-BY/&gt;" : "&lt;UTILITY-ADDENDUM-PEST-CONTROL-PAID-BY&gt;" + leaseRequestModel.UTILITY_ADDENDUM_PEST_CONTROL_PAID_BY + @"&lt;/UTILITY-ADDENDUM-PEST-CONTROL-PAID-BY&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_PEST_CONTROL_RATE == null ? "&lt;UTILITY-ADDENDUM-PEST-CONTROL-RATE/&gt;" : "&lt;UTILITY-ADDENDUM-PEST-CONTROL-RATE&gt;" + leaseRequestModel.UTILITY_ADDENDUM_PEST_CONTROL_RATE + @"&lt;/UTILITY-ADDENDUM-PEST-CONTROL-RATE&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_SET_UP_FEE == null ? "&lt;UTILITY-ADDENDUM-SET-UP-FEE/&gt;" : "&lt;UTILITY-ADDENDUM-SET-UP-FEE&gt;" + leaseRequestModel.UTILITY_ADDENDUM_SET_UP_FEE + @"&lt;/UTILITY-ADDENDUM-SET-UP-FEE&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_SET_UP_FEE_LIMIT == null ? "&lt;UTILITY-ADDENDUM-SET-UP-FEE-LIMIT/&gt;" : "&lt;UTILITY-ADDENDUM-SET-UP-FEE-LIMIT&gt;" + leaseRequestModel.UTILITY_ADDENDUM_SET_UP_FEE_LIMIT + @"&lt;/UTILITY-ADDENDUM-SET-UP-FEE-LIMIT&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_SEWER_BILLING_CO == null ? "&lt;UTILITY-ADDENDUM-SEWER-BILLING-CO/&gt;" : "&lt;UTILITY-ADDENDUM-SEWER-BILLING-CO&gt;" + leaseRequestModel.UTILITY_ADDENDUM_SEWER_BILLING_CO + @"&lt;/UTILITY-ADDENDUM-SEWER-BILLING-CO&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_SEWER_BILLING_CO_DESC == null ? "&lt;UTILITY-ADDENDUM-SEWER-BILLING-CO-DESC/&gt;" : "&lt;UTILITY-ADDENDUM-SEWER-BILLING-CO-DESC&gt;" + leaseRequestModel.UTILITY_ADDENDUM_SEWER_BILLING_CO_DESC + @"&lt;/UTILITY-ADDENDUM-SEWER-BILLING-CO-DESC&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_SEWER_FLAT_RATE_SELECTED == null ? "&lt;UTILITY-ADDENDUM-SEWER-FLAT-RATE-SELECTED/&gt;" : "&lt;UTILITY-ADDENDUM-SEWER-FLAT-RATE-SELECTED&gt;" + leaseRequestModel.UTILITY_ADDENDUM_SEWER_FLAT_RATE_SELECTED + @"&lt;/UTILITY-ADDENDUM-SEWER-FLAT-RATE-SELECTED&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_SEWER_FORMULA == null ? "&lt;UTILITY-ADDENDUM-SEWER-FORMULA/&gt;" : "&lt;UTILITY-ADDENDUM-SEWER-FORMULA&gt;" + leaseRequestModel.UTILITY_ADDENDUM_SEWER_FORMULA + @"&lt;/UTILITY-ADDENDUM-SEWER-FORMULA&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_SEWER_PAID_BY == null ? "&lt;UTILITY-ADDENDUM-SEWER-PAID-BY/&gt;" : "&lt;UTILITY-ADDENDUM-SEWER-PAID-BY&gt;" + leaseRequestModel.UTILITY_ADDENDUM_SEWER_PAID_BY + @"&lt;/UTILITY-ADDENDUM-SEWER-PAID-BY&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_SEWER_RATE == null ? "&lt;UTILITY-ADDENDUM-SEWER-RATE/&gt;" : "&lt;UTILITY-ADDENDUM-SEWER-RATE&gt;" + leaseRequestModel.UTILITY_ADDENDUM_SEWER_RATE + @"&lt;/UTILITY-ADDENDUM-SEWER-RATE&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_SPECIAL_PROVISIONS == null ? "&lt;UTILITY-ADDENDUM-SPECIAL-PROVISIONS/&gt;" : "&lt;UTILITY-ADDENDUM-SPECIAL-PROVISIONS&gt;" + leaseRequestModel.UTILITY_ADDENDUM_SPECIAL_PROVISIONS + @"&lt;/UTILITY-ADDENDUM-SPECIAL-PROVISIONS&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_STORMWATER_BILLING_CO == null ? "&lt;UTILITY-ADDENDUM-STORMWATER-BILLING-CO/&gt;" : "&lt;UTILITY-ADDENDUM-STORMWATER-BILLING-CO&gt;" + leaseRequestModel.UTILITY_ADDENDUM_STORMWATER_BILLING_CO + @"&lt;/UTILITY-ADDENDUM-STORMWATER-BILLING-CO&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_STORMWATER_BILLING_CO_DESC == null ? "&lt;UTILITY-ADDENDUM-STORMWATER-BILLING-CO-DESC/&gt;" : "&lt;UTILITY-ADDENDUM-STORMWATER-BILLING-CO-DESC&gt;" + leaseRequestModel.UTILITY_ADDENDUM_STORMWATER_BILLING_CO_DESC + @"&lt;/UTILITY-ADDENDUM-STORMWATER-BILLING-CO-DESC&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_STORMWATER_FLAT_RATE_SELECTED == null ? "&lt;UTILITY-ADDENDUM-STORMWATER-FLAT-RATE-SELECTED/&gt;" : "&lt;UTILITY-ADDENDUM-STORMWATER-FLAT-RATE-SELECTED&gt;" + leaseRequestModel.UTILITY_ADDENDUM_STORMWATER_FLAT_RATE_SELECTED + @"&lt;/UTILITY-ADDENDUM-STORMWATER-FLAT-RATE-SELECTED&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_STORMWATER_FORMULA == null ? "&lt;UTILITY-ADDENDUM-STORMWATER-FORMULA/&gt;" : "&lt;UTILITY-ADDENDUM-STORMWATER-FORMULA&gt;" + leaseRequestModel.UTILITY_ADDENDUM_STORMWATER_FORMULA + @"&lt;/UTILITY-ADDENDUM-STORMWATER-FORMULA&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_STORMWATER_PAID_BY == null ? "&lt;UTILITY-ADDENDUM-STORMWATER-PAID-BY/&gt;" : "&lt;UTILITY-ADDENDUM-STORMWATER-PAID-BY&gt;" + leaseRequestModel.UTILITY_ADDENDUM_STORMWATER_PAID_BY + @"&lt;/UTILITY-ADDENDUM-STORMWATER-PAID-BY&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_STORMWATER_RATE == null ? "&lt;UTILITY-ADDENDUM-STORMWATER-RATE/&gt;" : "&lt;UTILITY-ADDENDUM-STORMWATER-RATE&gt;" + leaseRequestModel.UTILITY_ADDENDUM_STORMWATER_RATE + @"&lt;/UTILITY-ADDENDUM-STORMWATER-RATE&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_TRASH_BILLING_CO == null ? "&lt;UTILITY-ADDENDUM-TRASH-BILLING-CO/&gt;" : "&lt;UTILITY-ADDENDUM-TRASH-BILLING-CO&gt;" + leaseRequestModel.UTILITY_ADDENDUM_TRASH_BILLING_CO + @"&lt;/UTILITY-ADDENDUM-TRASH-BILLING-CO&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_TRASH_BILLING_CO_DESC == null ? "&lt;UTILITY-ADDENDUM-TRASH-BILLING-CO-DESC/&gt;" : "&lt;UTILITY-ADDENDUM-TRASH-BILLING-CO-DESC&gt;" + leaseRequestModel.UTILITY_ADDENDUM_TRASH_BILLING_CO_DESC + @"&lt;/UTILITY-ADDENDUM-TRASH-BILLING-CO-DESC&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_TRASH_FLAT_RATE_SELECTED == null ? "&lt;UTILITY-ADDENDUM-TRASH-FLAT-RATE-SELECTED/&gt;" : "&lt;UTILITY-ADDENDUM-TRASH-FLAT-RATE-SELECTED&gt;" + leaseRequestModel.UTILITY_ADDENDUM_TRASH_FLAT_RATE_SELECTED + @"&lt;/UTILITY-ADDENDUM-TRASH-FLAT-RATE-SELECTED&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_TRASH_FORMULA == null ? "&lt;UTILITY-ADDENDUM-TRASH-FORMULA/&gt;" : "&lt;UTILITY-ADDENDUM-TRASH-FORMULA&gt;" + leaseRequestModel.UTILITY_ADDENDUM_TRASH_FORMULA + @"&lt;/UTILITY-ADDENDUM-TRASH-FORMULA&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_TRASH_PAID_BY == null ? "&lt;UTILITY-ADDENDUM-TRASH-PAID-BY/&gt;" : "&lt;UTILITY-ADDENDUM-TRASH-PAID-BY&gt;" + leaseRequestModel.UTILITY_ADDENDUM_TRASH_PAID_BY + @"&lt;/UTILITY-ADDENDUM-TRASH-PAID-BY&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_TRASH_RATE == null ? "&lt;UTILITY-ADDENDUM-TRASH-RATE/&gt;" : "&lt;UTILITY-ADDENDUM-TRASH-RATE&gt;" + leaseRequestModel.UTILITY_ADDENDUM_TRASH_RATE + @"&lt;/UTILITY-ADDENDUM-TRASH-RATE&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_WATER_BILLING_CO == null ? "&lt;UTILITY-ADDENDUM-WATER-BILLING-CO/&gt;" : "&lt;UTILITY-ADDENDUM-WATER-BILLING-CO&gt;" + leaseRequestModel.UTILITY_ADDENDUM_WATER_BILLING_CO + @"&lt;/UTILITY-ADDENDUM-WATER-BILLING-CO&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_WATER_BILLING_CO_DESC == null ? "&lt;UTILITY-ADDENDUM-WATER-BILLING-CO-DESC/&gt;" : "&lt;UTILITY-ADDENDUM-WATER-BILLING-CO-DESC&gt;" + leaseRequestModel.UTILITY_ADDENDUM_WATER_BILLING_CO_DESC + @"&lt;/UTILITY-ADDENDUM-WATER-BILLING-CO-DESC&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_WATER_FLAT_RATE_SELECTED == null ? "&lt;UTILITY-ADDENDUM-WATER-FLAT-RATE-SELECTED/&gt;" : "&lt;UTILITY-ADDENDUM-WATER-FLAT-RATE-SELECTED&gt;" + leaseRequestModel.UTILITY_ADDENDUM_WATER_FLAT_RATE_SELECTED + @"&lt;/UTILITY-ADDENDUM-WATER-FLAT-RATE-SELECTED&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_WATER_FORMULA == null ? "&lt;UTILITY-ADDENDUM-WATER-FORMULA/&gt;" : "&lt;UTILITY-ADDENDUM-WATER-FORMULA&gt;" + leaseRequestModel.UTILITY_ADDENDUM_WATER_FORMULA + @"&lt;/UTILITY-ADDENDUM-WATER-FORMULA&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_WATER_PAID_BY == null ? "&lt;UTILITY-ADDENDUM-WATER-PAID-BY/&gt;" : "&lt;UTILITY-ADDENDUM-WATER-PAID-BY&gt;" + leaseRequestModel.UTILITY_ADDENDUM_WATER_PAID_BY + @"&lt;/UTILITY-ADDENDUM-WATER-PAID-BY&gt;") + @"
                    " + (leaseRequestModel.UTILITY_ADDENDUM_WATER_RATE == null ? "&lt;UTILITY-ADDENDUM-WATER-RATE/&gt;" : "&lt;UTILITY-ADDENDUM-WATER-RATE&gt;" + leaseRequestModel.UTILITY_ADDENDUM_WATER_RATE + @"&lt;/UTILITY-ADDENDUM-WATER-RATE&gt;") + @"
                    " + (leaseRequestModel.PARKING_BEGIN_DATE == null ? "&lt;PARKING-BEGIN-DATE/&gt;" : "&lt;PARKING-BEGIN-DATE&gt;" + leaseRequestModel.PARKING_BEGIN_DATE + @"&lt;/PARKING-BEGIN-DATE&gt;") + @"
                    " + (leaseRequestModel.PARKING_DUE_DATE == null ? "&lt;PARKING-DUE-DATE/&gt;" : "&lt;PARKING-DUE-DATE&gt;" + leaseRequestModel.PARKING_DUE_DATE + @"&lt;/PARKING-DUE-DATE&gt;") + @"
                    " + (leaseRequestModel.PARKING_END_DATE == null ? "&lt;PARKING-END-DATE/&gt;" : "&lt;PARKING-END-DATE&gt;" + leaseRequestModel.PARKING_END_DATE + @"&lt;/PARKING-END-DATE&gt;") + @"
                    " + (leaseRequestModel.PARKING_MONTHLY_CHARGE == null ? "&lt;PARKING-MONTHLY-CHARGE/&gt;" : "&lt;PARKING-MONTHLY-CHARGE&gt;" + leaseRequestModel.PARKING_MONTHLY_CHARGE + @"&lt;/PARKING-MONTHLY-CHARGE&gt;") + @"
                    " + (leaseRequestModel.PARKING_MONTHLY_CHARGE_DATE == null ? "&lt;PARKING-MONTHLY-CHARGE-DATE/&gt;" : "&lt;PARKING-MONTHLY-CHARGE-DATE&gt;" + leaseRequestModel.PARKING_MONTHLY_CHARGE_DATE + @"&lt;/PARKING-MONTHLY-CHARGE-DATE&gt;") + @"
                    " + (leaseRequestModel.PARKING_NSF_FEE == null ? "&lt;PARKING-NSF-FEE/&gt;" : "&lt;PARKING-NSF-FEE&gt;" + leaseRequestModel.PARKING_NSF_FEE + @"&lt;/PARKING-DUE-DATE&gt;") + @"
                    " + (leaseRequestModel.PARKING_ONE_TIME_DATE == null ? "&lt;PARKING-ONE-TIME-DATE/&gt;" : "&lt;PARKING-ONE-TIME-DATE&gt;" + leaseRequestModel.PARKING_ONE_TIME_DATE + @"&lt;/PARKING-ONE-TIME-DATE&gt;") + @"
                    " + (leaseRequestModel.PARKING_ONE_TIME_FEE == null ? "&lt;PARKING-ONE-TIME-FEE/&gt;" : "&lt;PARKING-ONE-TIME-FEE&gt;" + leaseRequestModel.PARKING_ONE_TIME_FEE + @"&lt;/PARKING-ONE-TIME-FEE&gt;") + @"
                    " + (leaseRequestModel.PARKING_SPACE_NUMBER_1 == null ? "&lt;PARKING-SPACE-NUMBER-1/&gt;" : "&lt;PARKING-SPACE-NUMBER-1&gt;" + leaseRequestModel.PARKING_SPACE_NUMBER_1 + @"&lt;/PARKING-SPACE-NUMBER-1&gt;") + @"
                    " + (leaseRequestModel.PARKING_SPACE_NUMBER_2 == null ? "&lt;PARKING-SPACE-NUMBER-2/&gt;" : "&lt;PARKING-SPACE-NUMBER-2&gt;" + leaseRequestModel.PARKING_SPACE_NUMBER_2 + @"&lt;/PARKING-SPACE-NUMBER-2&gt;") + @"
                    " + (leaseRequestModel.PARKING_SPACE_NUMBER_3 == null ? "&lt;PARKING-SPACE-NUMBER-3/&gt;" : "&lt;PARKING-SPACE-NUMBER-3&gt;" + leaseRequestModel.PARKING_SPACE_NUMBER_3 + @"&lt;/PARKING-SPACE-NUMBER-3&gt;") + @"
                    " + (leaseRequestModel.PARKING_SPECIAL_PROVISIONS == null ? "&lt;PARKING-SPECIAL-PROVISIONS/&gt;" : "&lt;PARKING-SPECIAL-PROVISIONS&gt;" + leaseRequestModel.PARKING_SPECIAL_PROVISIONS + @"&lt;/PARKING-SPECIAL-PROVISIONS&gt;") + @"
                    " + (leaseRequestModel.VEHICLE_LICENSE_NUMBER_1 == null ? "&lt;VEHICLE-LICENSE-NUMBER-1/&gt;" : "&lt;VEHICLE-LICENSE-NUMBER-1&gt;" + leaseRequestModel.VEHICLE_LICENSE_NUMBER_1 + @"&lt;/VEHICLE-LICENSE-NUMBER-1&gt;") + @"
                    " + (leaseRequestModel.VEHICLE_LICENSE_NUMBER_2 == null ? "&lt;VEHICLE-LICENSE-NUMBER-2/&gt;" : "&lt;VEHICLE-LICENSE-NUMBER-2&gt;" + leaseRequestModel.VEHICLE_LICENSE_NUMBER_2 + @"&lt;/VEHICLE-LICENSE-NUMBER-2&gt;") + @"
                    " + (leaseRequestModel.VEHICLE_LICENSE_NUMBER_3 == null ? "&lt;VEHICLE-LICENSE-NUMBER-3/&gt;" : "&lt;VEHICLE-LICENSE-NUMBER-3&gt;" + leaseRequestModel.VEHICLE_LICENSE_NUMBER_3 + @"&lt;/VEHICLE-LICENSE-NUMBER-3&gt;") + @"
                    " + (leaseRequestModel.VEHICLE_MAKE_1 == null ? "&lt;VEHICLE-MAKE-1/&gt;" : "&lt;VEHICLE-MAKE-1&gt;" + leaseRequestModel.VEHICLE_MAKE_1 + @"&lt;/VEHICLE-MAKE-1&gt;") + @"
                    " + (leaseRequestModel.VEHICLE_MAKE_2 == null ? "&lt;VEHICLE-MAKE-2/&gt;" : "&lt;VEHICLE-MAKE-2&gt;" + leaseRequestModel.VEHICLE_MAKE_2 + @"&lt;/VEHICLE-MAKE-2&gt;") + @"
                    " + (leaseRequestModel.VEHICLE_MAKE_3 == null ? "&lt;VEHICLE-MAKE-3/&gt;" : "&lt;VEHICLE-MAKE-3&gt;" + leaseRequestModel.VEHICLE_MAKE_3 + @"&lt;/VEHICLE-MAKE-3&gt;") + @"
                    " + (leaseRequestModel.VEHICLE_MODEL_YEAR_1 == null ? "&lt;VEHICLE-MODEL-YEAR-1/&gt;" : "&lt;VEHICLE-MODEL-YEAR-1&gt;" + leaseRequestModel.VEHICLE_MODEL_YEAR_1 + @"&lt;/VEHICLE-MODEL-YEAR-1&gt;") + @"
                    " + (leaseRequestModel.VEHICLE_MODEL_YEAR_2 == null ? "&lt;VEHICLE-MODEL-YEAR-2/&gt;" : "&lt;VEHICLE-MODEL-YEAR-2&gt;" + leaseRequestModel.VEHICLE_MODEL_YEAR_2 + @"&lt;/VEHICLE-MODEL-YEAR-2&gt;") + @"
                    " + (leaseRequestModel.VEHICLE_MODEL_YEAR_3 == null ? "&lt;VEHICLE-MODEL-YEAR-3/&gt;" : "&lt;VEHICLE-MODEL-YEAR-3&gt;" + leaseRequestModel.VEHICLE_MODEL_YEAR_3 + @"&lt;/VEHICLE-MODEL-YEAR-3&gt;") + @"
                    " + (leaseRequestModel.VEHICLE_OWNER_NUMBER_1 == null ? "&lt;VEHICLE-OWNER-NUMBER-1/&gt;" : "&lt;VEHICLE-OWNER-NUMBER-1&gt;" + leaseRequestModel.VEHICLE_OWNER_NUMBER_1 + @"&lt;/VEHICLE-OWNER-NUMBER-1&gt;") + @"
                    " + (leaseRequestModel.VEHICLE_OWNER_NUMBER_2 == null ? "&lt;VEHICLE-OWNER-NUMBER-2/&gt;" : "&lt;VEHICLE-OWNER-NUMBER-2&gt;" + leaseRequestModel.VEHICLE_OWNER_NUMBER_2 + @"&lt;/VEHICLE-OWNER-NUMBER-2&gt;") + @"
                    " + (leaseRequestModel.VEHICLE_OWNER_NUMBER_3 == null ? "&lt;VEHICLE-OWNER-NUMBER-3/&gt;" : "&lt;VEHICLE-OWNER-NUMBER-3&gt;" + leaseRequestModel.VEHICLE_OWNER_NUMBER_3 + @"&lt;/VEHICLE-OWNER-NUMBER-3&gt;") + @"
                    " + (leaseRequestModel.VEHICLE_PERMIT_NUMBER_1 == null ? "&lt;VEHICLE-PERMIT-NUMBER-1/&gt;" : "&lt;VEHICLE-PERMIT-NUMBER-1&gt;" + leaseRequestModel.VEHICLE_PERMIT_NUMBER_1 + @"&lt;/VEHICLE-PERMIT-NUMBER-1&gt;") + @"
                    " + (leaseRequestModel.VEHICLE_PERMIT_NUMBER_2 == null ? "&lt;VEHICLE-PERMIT-NUMBER-2/&gt;" : "&lt;VEHICLE-PERMIT-NUMBER-2&gt;" + leaseRequestModel.VEHICLE_PERMIT_NUMBER_2 + @"&lt;/VEHICLE-PERMIT-NUMBER-2&gt;") + @"
                    " + (leaseRequestModel.VEHICLE_PERMIT_NUMBER_3 == null ? "&lt;VEHICLE-PERMIT-NUMBER-3/&gt;" : "&lt;VEHICLE-PERMIT-NUMBER-3&gt;" + leaseRequestModel.VEHICLE_PERMIT_NUMBER_3 + @"&lt;/VEHICLE-PERMIT-NUMBER-3&gt;") + @"
                    " + (leaseRequestModel.VEHICLE_STATE_1 == null ? "&lt;VEHICLE-STATE-1/&gt;" : "&lt;VEHICLE-STATE-1&gt;" + leaseRequestModel.VEHICLE_STATE_1 + @"&lt;/VEHICLE-STATE-1&gt;") + @"
                    " + (leaseRequestModel.VEHICLE_STATE_2 == null ? "&lt;VEHICLE-STATE-2/&gt;" : "&lt;VEHICLE-STATE-2&gt;" + leaseRequestModel.VEHICLE_STATE_2 + @"&lt;/VEHICLE-STATE-2&gt;") + @"
                    " + (leaseRequestModel.VEHICLE_STATE_3 == null ? "&lt;VEHICLE-STATE-3/&gt;" : "&lt;VEHICLE-STATE-3&gt;" + leaseRequestModel.VEHICLE_STATE_3 + @"&lt;/VEHICLE-STATE-3&gt;") + @"
                    " + (leaseRequestModel.COMMUNITY_BUSINESS_CENTER_AVAILABILITY == null ? "&lt;COMMUNITY-BUSINESS-CENTER-AVAILABILITY/&gt;" : "&lt;COMMUNITY-BUSINESS-CENTER-AVAILABILITY&gt;" + leaseRequestModel.COMMUNITY_BUSINESS_CENTER_AVAILABILITY + @"&lt;/COMMUNITY-BUSINESS-CENTER-AVAILABILITY&gt;") + @"
                    " + (leaseRequestModel.COMMUNITY_BUSINESS_CENTER_COMPUTER_TIME_LIMIT == null ? "&lt;COMMUNITY-BUSINESS-CENTER-COMPUTER-TIME-LIMIT/&gt;" : "&lt;COMMUNITY-BUSINESS-CENTER-COMPUTER-TIME-LIMIT&gt;" + leaseRequestModel.COMMUNITY_BUSINESS_CENTER_COMPUTER_TIME_LIMIT + @"&lt;/COMMUNITY-BUSINESS-CENTER-COMPUTER-TIME-LIMIT&gt;") + @"
                    " + (leaseRequestModel.COMMUNITY_FIRE_HAZARDS_GRILL_DISTANCE == null ? "&lt;COMMUNITY-FIRE-HAZARDS-GRILL-DISTANCE/&gt;" : "&lt;COMMUNITY-FIRE-HAZARDS-GRILL-DISTANCE&gt;" + leaseRequestModel.COMMUNITY_FIRE_HAZARDS_GRILL_DISTANCE + @"&lt;/COMMUNITY-FIRE-HAZARDS-GRILL-DISTANCE&gt;") + @"
                    " + (leaseRequestModel.COMMUNITY_FITNESS_CENTER_AVAILABILITY == null ? "&lt;COMMUNITY-FITNESS-CENTER-AVAILABILITY/&gt;" : "&lt;COMMUNITY-FITNESS-CENTER-AVAILABILITY&gt;" + leaseRequestModel.COMMUNITY_FITNESS_CENTER_AVAILABILITY + @"&lt;/COMMUNITY-FITNESS-CENTER-AVAILABILITY&gt;") + @"
                    " + (leaseRequestModel.COMMUNITY_FITNESS_CENTER_CARD_1 == null ? "&lt;COMMUNITY-FITNESS-CENTER-CARD-1/&gt;" : "&lt;COMMUNITY-FITNESS-CENTER-CARD-1&gt;" + leaseRequestModel.COMMUNITY_FITNESS_CENTER_CARD_1 + @"&lt;/COMMUNITY-FITNESS-CENTER-CARD-1&gt;") + @"
                    " + (leaseRequestModel.COMMUNITY_FITNESS_CENTER_CARD_2 == null ? "&lt;COMMUNITY-FITNESS-CENTER-CARD-2/&gt;" : "&lt;COMMUNITY-FITNESS-CENTER-CARD-2&gt;" + leaseRequestModel.COMMUNITY_FITNESS_CENTER_CARD_2 + @"&lt;/COMMUNITY-FITNESS-CENTER-CARD-2&gt;") + @"
                    " + (leaseRequestModel.COMMUNITY_FITNESS_CENTER_CARD_3 == null ? "&lt;COMMUNITY-FITNESS-CENTER-CARD-3/&gt;" : "&lt;COMMUNITY-FITNESS-CENTER-CARD-3&gt;" + leaseRequestModel.COMMUNITY_FITNESS_CENTER_CARD_3 + @"&lt;/COMMUNITY-FITNESS-CENTER-CARD-3&gt;") + @"
                    " + (leaseRequestModel.COMMUNITY_FITNESS_CENTER_CARD_4 == null ? "&lt;COMMUNITY-FITNESS-CENTER-CARD-4/&gt;" : "&lt;COMMUNITY-FITNESS-CENTER-CARD-4&gt;" + leaseRequestModel.COMMUNITY_FITNESS_CENTER_CARD_4 + @"&lt;/COMMUNITY-FITNESS-CENTER-CARD-4&gt;") + @"
                    " + (leaseRequestModel.COMMUNITY_FITNESS_CENTER_CARD_5 == null ? "&lt;COMMUNITY-FITNESS-CENTER-CARD-5/&gt;" : "&lt;COMMUNITY-FITNESS-CENTER-CARD-5&gt;" + leaseRequestModel.COMMUNITY_FITNESS_CENTER_CARD_5 + @"&lt;/COMMUNITY-FITNESS-CENTER-CARD-5&gt;") + @"
                    " + (leaseRequestModel.COMMUNITY_FITNESS_CENTER_CARD_6 == null ? "&lt;COMMUNITY-FITNESS-CENTER-CARD-6/&gt;" : "&lt;COMMUNITY-FITNESS-CENTER-CARD-6&gt;" + leaseRequestModel.COMMUNITY_FITNESS_CENTER_CARD_6 + @"&lt;/COMMUNITY-FITNESS-CENTER-CARD-6&gt;") + @"
                    " + (leaseRequestModel.COMMUNITY_PACKAGE_RELEASE_POLICY == null ? "&lt;COMMUNITY-PACKAGE-RELEASE-POLICY/&gt;" : "&lt;COMMUNITY-PACKAGE-RELEASE-POLICY&gt;" + leaseRequestModel.COMMUNITY_PACKAGE_RELEASE_POLICY + @"&lt;/COMMUNITY-PACKAGE-RELEASE-POLICY&gt;") + @"
                    " + (leaseRequestModel.COMMUNITY_POLICIES_SPECIAL_PROVISIONS == null ? "&lt;COMMUNITY-POLICIES-SPECIAL-PROVISIONS/&gt;" : "&lt;COMMUNITY-POLICIES-SPECIAL-PROVISIONS&gt;" + leaseRequestModel.COMMUNITY_POLICIES_SPECIAL_PROVISIONS + @"&lt;/COMMUNITY-POLICIES-SPECIAL-PROVISIONS&gt;") + @"
                    " + (leaseRequestModel.COMMUNITY_POOL_AVAILABILITY == null ? "&lt;COMMUNITY-POOL-AVAILABILITY/&gt;" : "&lt;COMMUNITY-POOL-AVAILABILITY&gt;" + leaseRequestModel.COMMUNITY_POOL_AVAILABILITY + @"&lt;/COMMUNITY-POOL-AVAILABILITY&gt;") + @"
                    " + (leaseRequestModel.COMMUNITY_VEHICLES_MAXIMUM_ALLOWED == null ? "&lt;COMMUNITY-VEHICLES-MAXIMUM-ALLOWED/&gt;" : "&lt;COMMUNITY-VEHICLES-MAXIMUM-ALLOWED&gt;" + leaseRequestModel.COMMUNITY_VEHICLES_MAXIMUM_ALLOWED + @"&lt;/COMMUNITY-VEHICLES-MAXIMUM-ALLOWED&gt;") + @"
                    " + (leaseRequestModel.COMMUNITY_VEHICLES_TOWING_NOTICE == null ? "&lt;COMMUNITY-VEHICLES-TOWING-NOTICE/&gt;" : "&lt;COMMUNITY-VEHICLES-TOWING-NOTICE&gt;" + leaseRequestModel.COMMUNITY_VEHICLES_TOWING_NOTICE + @"&lt;/COMMUNITY-VEHICLES-TOWING-NOTICE&gt;") + @"
                    " + (leaseRequestModel.ADDENDUM_RENT_CONCESSION_AMOUNT == null ? "&lt;ADDENDUM-RENT-CONCESSION-AMOUNT/&gt;" : "&lt;ADDENDUM-RENT-CONCESSION-AMOUNT&gt;" + leaseRequestModel.ADDENDUM_RENT_CONCESSION_AMOUNT + @"&lt;/ADDENDUM-RENT-CONCESSION-AMOUNT&gt;") + @"
                    " + (leaseRequestModel.ADDENDUM_RENT_CONCESSION_DESCRIPTION == null ? "&lt;ADDENDUM-RENT-CONCESSION-DESCRIPTION/&gt;" : "&lt;ADDENDUM-RENT-CONCESSION-DESCRIPTION&gt;" + leaseRequestModel.ADDENDUM_RENT_CONCESSION_DESCRIPTION + @"&lt;/ADDENDUM-RENT-CONCESSION-DESCRIPTION&gt;") + @"
                    " + (leaseRequestModel.ADDENDUM_RENT_CONCESSION_MONTHLY_DISCOUNT == null ? "&lt;ADDENDUM-RENT-CONCESSION-MONTHLY-DISCOUNT/&gt;" : "&lt;ADDENDUM-RENT-CONCESSION-MONTHLY-DISCOUNT&gt;" + leaseRequestModel.ADDENDUM_RENT_CONCESSION_MONTHLY_DISCOUNT + @"&lt;/ADDENDUM-RENT-CONCESSION-MONTHLY-DISCOUNT&gt;") + @"
                    " + (leaseRequestModel.ADDENDUM_RENT_CONCESSION_ONE_TIME == null ? "&lt;ADDENDUM-RENT-CONCESSION-ONE-TIME/&gt;" : "&lt;ADDENDUM-RENT-CONCESSION-ONE-TIME&gt;" + leaseRequestModel.ADDENDUM_RENT_CONCESSION_ONE_TIME + @"&lt;/ADDENDUM-RENT-CONCESSION-ONE-TIME&gt;") + @"
                    " + (leaseRequestModel.ADDENDUM_RENT_CONCESSION_ONE_TIME_AMOUNT == null ? "&lt;ADDENDUM-RENT-CONCESSION-ONE-TIME-AMOUNT/&gt;" : "&lt;ADDENDUM-RENT-CONCESSION-ONE-TIME-AMOUNT&gt;" + leaseRequestModel.ADDENDUM_RENT_CONCESSION_ONE_TIME_AMOUNT + @"&lt;/ADDENDUM-RENT-CONCESSION-ONE-TIME-AMOUNT&gt;") + @"
                    " + (leaseRequestModel.ADDENDUM_RENT_CONCESSION_ONE_TIME_MONTHS == null ? "&lt;ADDENDUM-RENT-CONCESSION-ONE-TIME-MONTHS/&gt;" : "&lt;ADDENDUM-RENT-CONCESSION-ONE-TIME-MONTHS&gt;" + leaseRequestModel.ADDENDUM_RENT_CONCESSION_ONE_TIME_MONTHS + @"&lt;/ADDENDUM-RENT-CONCESSION-ONE-TIME-MONTHS&gt;") + @"
                    " + (leaseRequestModel.ADDENDUM_RENT_CONCESSION_OTHER_DISCOUNT == null ? "&lt;ADDENDUM-RENT-CONCESSION-OTHER-DISCOUNT/&gt;" : "&lt;ADDENDUM-RENT-CONCESSION-OTHER-DISCOUNT&gt;" + leaseRequestModel.ADDENDUM_RENT_CONCESSION_OTHER_DISCOUNT + @"&lt;/ADDENDUM-RENT-CONCESSION-OTHER-DISCOUNT&gt;") + @"
                    " + (leaseRequestModel.ADDENDUM_RENT_CONCESSION_REPAY_CONCESSIONS == null ? "&lt;ADDENDUM-RENT-CONCESSION-REPAY-CONCESSIONS/&gt;" : "&lt;ADDENDUM-RENT-CONCESSION-REPAY-CONCESSIONS&gt;" + leaseRequestModel.ADDENDUM_RENT_CONCESSION_REPAY_CONCESSIONS + @"&lt;/ADDENDUM-RENT-CONCESSION-REPAY-CONCESSIONS&gt;") + @"
                    " + (leaseRequestModel.ADDENDUM_RENT_CONCESSION_REPAY_DISCOUNTS == null ? "&lt;ADDENDUM-RENT-CONCESSION-REPAY-DISCOUNTS/&gt;" : "&lt;ADDENDUM-RENT-CONCESSION-REPAY-DISCOUNTS&gt;" + leaseRequestModel.ADDENDUM_RENT_CONCESSION_REPAY_DISCOUNTS + @"&lt;/ADDENDUM-RENT-CONCESSION-REPAY-DISCOUNTS&gt;") + @"
                    " + (leaseRequestModel.ADDENDUM_RENT_CONCESSION_SPECIAL_PROVISIONS == null ? "&lt;ADDENDUM-RENT-CONCESSION-SPECIAL-PROVISIONS/&gt;" : "&lt;ADDENDUM-RENT-CONCESSION-SPECIAL-PROVISIONS&gt;" + leaseRequestModel.ADDENDUM_RENT_CONCESSION_SPECIAL_PROVISIONS + @"&lt;/ADDENDUM-RENT-CONCESSION-SPECIAL-PROVISIONS&gt;") + @"
                    " + (leaseRequestModel.CONCESSION_NONMONETARY == null ? "&lt;CONCESSION-NONMONETARY/&gt;" : "&lt;CONCESSION-NONMONETARY&gt;" + leaseRequestModel.CONCESSION_NONMONETARY + @"&lt;/CONCESSION-NONMONETARY&gt;") + @"
                    " + (leaseRequestModel.CONCESSION_NONMONETARY_DESC == null ? "&lt;CONCESSION-NONMONETARY-DESC/&gt;" : "&lt;CONCESSION-NONMONETARY-DESC&gt;" + leaseRequestModel.CONCESSION_NONMONETARY_DESC + @"&lt;/CONCESSION-NONMONETARY-DESC&gt;") + @"
                    " + (leaseRequestModel.RENTERS_INSURANCE_LIABILITY_LIMIT == null ? "&lt;RENTERS-INSURANCE-LIABILITY-LIMIT/&gt;" : "&lt;RENTERS-INSURANCE-LIABILITY-LIMIT&gt;" + leaseRequestModel.RENTERS_INSURANCE_LIABILITY_LIMIT + @"&lt;/RENTERS-INSURANCE-LIABILITY-LIMIT&gt;") + @"
                    " + (leaseRequestModel.RENTERS_INSURANCE_MINIMUM_COVERAGE == null ? "&lt;RENTERS-INSURANCE-MINIMUM-COVERAGE/&gt;" : "&lt;RENTERS-INSURANCE-MINIMUM-COVERAGE&gt;" + leaseRequestModel.RENTERS_INSURANCE_MINIMUM_COVERAGE + @"&lt;/RENTERS-INSURANCE-MINIMUM-COVERAGE&gt;") + @"
                    " + (leaseRequestModel.RENTERS_INSURANCE_PROVIDER == null ? "&lt;RENTERS-INSURANCE-PROVIDER/&gt;" : "&lt;RENTERS-INSURANCE-PROVIDER&gt;" + leaseRequestModel.RENTERS_INSURANCE_PROVIDER + @"&lt;/RENTERS-INSURANCE-PROVIDER&gt;") + @"
                    " + (leaseRequestModel.RENTERS_INSURANCE_REQUIREMENT == null ? "&lt;RENTERS-INSURANCE-REQUIREMENT/&gt;" : "&lt;RENTERS-INSURANCE-REQUIREMENT&gt;" + leaseRequestModel.RENTERS_INSURANCE_REQUIREMENT + @"&lt;/RENTERS-INSURANCE-REQUIREMENT&gt;") + @"
                    " + (leaseRequestModel.RENTERS_INSURANCE_SPECIAL_PROVISIONS == null ? "&lt;RENTERS-INSURANCE-SPECIAL-PROVISIONS/&gt;" : "&lt;RENTERS-INSURANCE-SPECIAL-PROVISIONS&gt;" + leaseRequestModel.RENTERS_INSURANCE_SPECIAL_PROVISIONS + @"&lt;/RENTERS-INSURANCE-SPECIAL-PROVISIONS&gt;") + @"
                    " + (leaseRequestModel.PACKAGE_ACCEPTANCE_SPECIAL_PROVISIONS == null ? "&lt;PACKAGE-ACCEPTANCE-SPECIAL-PROVISIONS/&gt;" : "&lt;PACKAGE-ACCEPTANCE-SPECIAL-PROVISIONS&gt;" + leaseRequestModel.PACKAGE_ACCEPTANCE_SPECIAL_PROVISIONS + @"&lt;/PACKAGE-ACCEPTANCE-SPECIAL-PROVISIONS&gt;") + @"
                    " + (leaseRequestModel.PACKAGE_ACCEPTANCE_TIME_LIMIT == null ? "&lt;PACKAGE-ACCEPTANCE-TIME-LIMIT/&gt;" : "&lt;PACKAGE-ACCEPTANCE-TIME-LIMIT&gt;" + leaseRequestModel.PACKAGE_ACCEPTANCE_TIME_LIMIT + @"&lt;/PACKAGE-ACCEPTANCE-TIME-LIMIT&gt;") + @"
                    " + (leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_ADD_CARD_FEE == null ? "&lt;REMOTE-CARD-CODE-ADDENDUM-ADD-CARD-FEE/&gt;" : "&lt;REMOTE-CARD-CODE-ADDENDUM-ADD-CARD-FEE&gt;" + leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_ADD_CARD_FEE + @"&lt;/REMOTE-CARD-CODE-ADDENDUM-ADD-CARD-FEE&gt;") + @"
                    " + (leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_ADD_REMOTE_FEE == null ? "&lt;REMOTE-CARD-CODE-ADDENDUM-ADD-REMOTE-FEE/&gt;" : "&lt;REMOTE-CARD-CODE-ADDENDUM-ADD-REMOTE-FEE&gt;" + leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_ADD_REMOTE_FEE + @"&lt;/REMOTE-CARD-CODE-ADDENDUM-ADD-REMOTE-FEE&gt;") + @"
                    " + (leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_CARD == null ? "&lt;REMOTE-CARD-CODE-ADDENDUM-CARD/&gt;" : "&lt;REMOTE-CARD-CODE-ADDENDUM-CARD&gt;" + leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_CARD + @"&lt;/REMOTE-CARD-CODE-ADDENDUM-CARD&gt;") + @"
                    " + (leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_CODE == null ? "&lt;REMOTE-CARD-CODE-ADDENDUM-CODE/&gt;" : "&lt;REMOTE-CARD-CODE-ADDENDUM-CODE&gt;" + leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_CODE + @"&lt;/REMOTE-CARD-CODE-ADDENDUM-CODE&gt;") + @"
                    " + (leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_CODE_CHANGE == null ? "&lt;REMOTE-CARD-CODE-ADDENDUM-CODE-CHANGE/&gt;" : "&lt;REMOTE-CARD-CODE-ADDENDUM-CODE-CHANGE&gt;" + leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_CODE_CHANGE + @"&lt;/REMOTE-CARD-CODE-ADDENDUM-CODE-CHANGE&gt;") + @"
                    " + (leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_LOST_CARD == null ? "&lt;REMOTE-CARD-CODE-ADDENDUM-LOST-CARD/&gt;" : "&lt;REMOTE-CARD-CODE-ADDENDUM-LOST-CARD&gt;" + leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_LOST_CARD + @"&lt;/REMOTE-CARD-CODE-ADDENDUM-LOST-CARD&gt;") + @"
                    " + (leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_LOST_CARD_DEDUCT == null ? "&lt;REMOTE-CARD-CODE-ADDENDUM-LOST-CARD-DEDUCT/&gt;" : "&lt;REMOTE-CARD-CODE-ADDENDUM-LOST-CARD-DEDUCT&gt;" + leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_LOST_CARD_DEDUCT + @"&lt;/REMOTE-CARD-CODE-ADDENDUM-LOST-CARD-DEDUCT&gt;") + @"
                    " + (leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_LOST_REMOTE == null ? "&lt;REMOTE-CARD-CODE-ADDENDUM-LOST-REMOTE/&gt;" : "&lt;REMOTE-CARD-CODE-ADDENDUM-LOST-REMOTE&gt;" + leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_LOST_REMOTE + @"&lt;/REMOTE-CARD-CODE-ADDENDUM-LOST-REMOTE&gt;") + @"
                    " + (leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_LOST_REMOTE_DEDUCT == null ? "&lt;REMOTE-CARD-CODE-ADDENDUM-LOST-REMOTE-DEDUCT/&gt;" : "&lt;REMOTE-CARD-CODE-ADDENDUM-LOST-REMOTE-DEDUCT&gt;" + leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_LOST_REMOTE_DEDUCT + @"&lt;/REMOTE-CARD-CODE-ADDENDUM-LOST-REMOTE-DEDUCT&gt;") + @"
                    " + (leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_REMOTE == null ? "&lt;REMOTE-CARD-CODE-ADDENDUM-REMOTE/&gt;" : "&lt;REMOTE-CARD-CODE-ADDENDUM-REMOTE&gt;" + leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_REMOTE + @"&lt;/REMOTE-CARD-CODE-ADDENDUM-REMOTE&gt;") + @"
                    " + (leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_REPLACE_CARD_FEE == null ? "&lt;REMOTE-CARD-CODE-ADDENDUM-REPLACE-CARD-FEE/&gt;" : "&lt;REMOTE-CARD-CODE-ADDENDUM-REPLACE-CARD-FEE&gt;" + leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_REPLACE_CARD_FEE + @"&lt;/REMOTE-CARD-CODE-ADDENDUM-REPLACE-CARD-FEE&gt;") + @"
                    " + (leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_REPLACE_REMOTE_FEE == null ? "&lt;REMOTE-CARD-CODE-ADDENDUM-REPLACE-REMOTE-FEE/&gt;" : "&lt;REMOTE-CARD-CODE-ADDENDUM-REPLACE-REMOTE-FEE&gt;" + leaseRequestModel.REMOTE_CARD_CODE_ADDENDUM_REPLACE_REMOTE_FEE + @"&lt;/REMOTE-CARD-CODE-ADDENDUM-REPLACE-REMOTE-FEE&gt;") + @"
                    " + (leaseRequestModel.REMOTE_CARD_CODE_SPECIAL_PROVISIONS == null ? "&lt;REMOTE-CARD-CODE-SPECIAL-PROVISIONS/&gt;" : "&lt;REMOTE-CARD-CODE-SPECIAL-PROVISIONS&gt;" + leaseRequestModel.REMOTE_CARD_CODE_SPECIAL_PROVISIONS + @"&lt;/REMOTE-CARD-CODE-SPECIAL-PROVISIONS&gt;") + @"
                    " + (leaseRequestModel.AIRBNB_SPECIAL_PROVISIONS == null ? "&lt;AIRBNB-SPECIAL-PROVISIONS/&gt;" : "&lt;AIRBNB-SPECIAL-PROVISIONS&gt;" + leaseRequestModel.AIRBNB_SPECIAL_PROVISIONS + @"&lt;/AIRBNB-SPECIAL-PROVISIONS&gt;") + @"
                    " + (leaseRequestModel.BED_BUG_SPECIAL_PROVISIONS == null ? "&lt;BED-BUG-SPECIAL-PROVISIONS/&gt;" : "&lt;BED-BUG-SPECIAL-PROVISIONS&gt;" + leaseRequestModel.BED_BUG_SPECIAL_PROVISIONS + @"&lt;/BED-BUG-SPECIAL-PROVISIONSS&gt;") + @"
                    " + (leaseRequestModel.CRIME_SPECIAL_PROVISIONS == null ? "&lt;CRIME-SPECIAL-PROVISIONS/&gt;" : "&lt;CRIME-SPECIAL-PROVISIONS&gt;" + leaseRequestModel.CRIME_SPECIAL_PROVISIONS + @"&lt;/CRIME-SPECIAL-PROVISIONSS&gt;") + @"
                    " + (leaseRequestModel.INVENTORY_SPECIAL_PROVISIONS == null ? "&lt;INVENTORY-SPECIAL-PROVISIONS/&gt;" : "&lt;INVENTORY-SPECIAL-PROVISIONS&gt;" + leaseRequestModel.INVENTORY_SPECIAL_PROVISIONS + @"&lt;/INVENTORY-SPECIAL-PROVISIONS&gt;") + @"
                    " + (leaseRequestModel.MIXED_USE_SPECIAL_PROVISIONS == null ? "&lt;MIXED-USE-SPECIAL-PROVISIONS/&gt;" : "&lt;MIXED-USE-SPECIAL-PROVISIONS&gt;" + leaseRequestModel.MIXED_USE_SPECIAL_PROVISIONS + @"&lt;/MIXED-USE-SPECIAL-PROVISIONS&gt;") + @"
                    " + (leaseRequestModel.MOLD_ADDENDUM_SPECIAL_PROVISIONS == null ? "&lt;MOLD-ADDENDUM-SPECIAL-PROVISIONS/&gt;" : "&lt;MOLD-ADDENDUM-SPECIAL-PROVISIONS&gt;" + leaseRequestModel.MOLD_ADDENDUM_SPECIAL_PROVISIONS + @"&lt;/MOLD-ADDENDUM-SPECIAL-PROVISIONS&gt;") + @"
                    " + (leaseRequestModel.PHOTO_RELEASE_ADDENDUM_SPECIAL_PROVISIONS == null ? "&lt;PHOTO-RELEASE-ADDENDUM-SPECIAL-PROVISIONS/&gt;" : "&lt;PHOTO-RELEASE-ADDENDUM-SPECIAL-PROVISIONS&gt;" + leaseRequestModel.PHOTO_RELEASE_ADDENDUM_SPECIAL_PROVISIONS + @"&lt;/PHOTO-RELEASE-ADDENDUM-SPECIAL-PROVISIONS&gt;") + @"
                    " + (leaseRequestModel.SUPPORT_ANIMAL_SPECIAL_PROVISIONS == null ? "&lt;SUPPORT-ANIMAL-SPECIAL-PROVISIONS/&gt;" : "&lt;SUPPORT-ANIMAL-SPECIAL-PROVISIONS&gt;" + leaseRequestModel.SUPPORT_ANIMAL_SPECIAL_PROVISIONS + @"&lt;/SUPPORT-ANIMAL-SPECIAL-PROVISIONS&gt;") + @"
                    " + (leaseRequestModel.INVENTORY_MOVE_IN == null ? "&lt;INVENTORY-MOVE-IN/&gt;" : "&lt;INVENTORY-MOVE-IN&gt;" + leaseRequestModel.INVENTORY_MOVE_IN + @"&lt;/INVENTORY-MOVE-IN&gt;") + @"
                    " + (leaseRequestModel.INVENTORY_MOVE_IN_DATE == null ? "&lt;INVENTORY-MOVE-IN-DATE/&gt;" : "&lt;INVENTORY-MOVE-IN-DATE&gt;" + leaseRequestModel.INVENTORY_MOVE_IN_DATE + @"&lt;/INVENTORY-MOVE-IN-DATE&gt;") + @"
                    " + (leaseRequestModel.INVENTORY_MOVE_OUT == null ? "&lt;INVENTORY-MOVE-OUT/&gt;" : "&lt;INVENTORY-MOVE-OUT&gt;" + leaseRequestModel.INVENTORY_MOVE_OUT + @"&lt;/INVENTORY-MOVE-OUT&gt;") + @"
                    " + (leaseRequestModel.INVENTORY_MOVE_OUT_DATE == null ? "&lt;INVENTORY-MOVE-OUT-DATE/&gt;" : "&lt;INVENTORY-MOVE-OUT-DATE&gt;" + leaseRequestModel.INVENTORY_MOVE_OUT_DATE + @"&lt;/INVENTORY-MOVE-OUT-DATE&gt;") + @"
                    &lt;/STANDARD&gt;
                    &lt;/LEASE&gt;
                    &lt;/BLUEMOON&gt;
                </LeaseXMLData>";

            return leaseData;
        }

        private XmlDocument CreateXMLForLease(LeaseRequestModel leaseRequestModel, string propertyId, string sessionId)
        {
            string leaseXmlStr = "";
            try
            {
                leaseXmlStr = @" <ns1:CreateLease>
                <SessionId>" + sessionId + @"</SessionId>" + LeaseXMLData(leaseRequestModel) +
                (propertyId == null ? @" <PropertyId xsi:nil=""true""/>" : "<PropertyId>" + propertyId + @"</PropertyId>") + @"
                <LeaseId xsi:nil=""true""/>
                </ns1:CreateLease>";
            }
            catch (Exception ex)
            {

            }
            var bodyForLease = CreateXMLDocument(leaseXmlStr);


            return bodyForLease;
        }

        private XmlDocument EditXMLForLease(LeaseRequestModel leaseRequestModel, string leaseId, string sessionId)
        {
            string leaseXmlStr = "";
            try
            {
                leaseXmlStr = @" <ns1:EditLease>
            <SessionId>" + sessionId + @"</SessionId>
            <LeaseId>" + leaseId + @"</LeaseId>
            	" + LeaseXMLData(leaseRequestModel) + @"
            </ns1:EditLease>";
            }
            catch (Exception ex)
            {

            }
            var bodyForLease = CreateXMLDocument(leaseXmlStr);


            return bodyForLease;
        }

        public async Task<LeaseResponseModel> CreateLease(LeaseRequestModel leaseRequestModel, string sessionId, string PropertyId)
        {
            LeaseResponseModel leaseResponseModel = new LeaseResponseModel();
            string leaseId = "";


            var bodyForLease = CreateXMLForLease(leaseRequestModel, PropertyId, sessionId);

            var result = await AquatraqHelper.Post<List<XElement>>(BaseUrl + "lease.php#CreateLease", bodyForLease);
            if (result != null)
            {
                var resultlease = result
                 .Descendants("CreateLeaseResult")
                 .ToList();
                leaseId = resultlease[0].Value;
            }



            leaseResponseModel.LeaseId = leaseId;
            leaseResponseModel.SessionId = sessionId;


            return leaseResponseModel;
        }

        public async Task<LeaseResponseModel> EditLease(LeaseRequestModel leaseRequestModel, string sessionId, string leaseId)
        {
            LeaseResponseModel leaseResponseModel = new LeaseResponseModel();

            var bodyForLease = EditXMLForLease(leaseRequestModel, leaseId, sessionId);

            var result = await AquatraqHelper.Post<List<XElement>>(BaseUrl + "lease.php#EditLease", bodyForLease);
            if (result != null)
            {
                var resultlease = result
                 .Descendants("EditLeaseResult")
                 .ToList();
                leaseResponseModel.Success = Convert.ToBoolean(resultlease[0].Value);
            }


            leaseResponseModel.LeaseId = leaseId;
            leaseResponseModel.SessionId = sessionId;
            return leaseResponseModel;
        }

        public async Task<LeaseResponseModel> CreateSession()
        {
            LeaseResponseModel leaseResponseModel = new LeaseResponseModel();
            string sessionId = "";
            string leaseId = "";
            var bodyForAuth = CreateXMLDocument(@" <ns1:AuthenticateUser>
                                                    <SerialNumber>FL19100803</SerialNumber>
                                                    <UserId>MasmarMgmt</UserId>
                                                    <Password>$homa123</Password>
                                                </ns1:AuthenticateUser>");

            var resultAuth = await AquatraqHelper.Post<List<XElement>>(BaseUrl + "lease.php#AuthenticateUser", bodyForAuth);
            if (resultAuth != null)
            {
                // for attribute
                var resultOrderDetail = resultAuth
                 .Descendants("AuthenticateUserResult")
                 .ToList();

                if (Convert.ToBoolean(resultOrderDetail[0].Value) == true)
                {
                    var bodyForSession = CreateXMLDocument(@"   <ns1:CreateSession>
                                                                    <SerialNumber>FL19100803</SerialNumber>
                                                                    <UserId>MasmarMgmt</UserId>
                                                                    <Password>$homa123</Password>
                                                                </ns1:CreateSession>");

                    var resultSession = await AquatraqHelper.Post<List<XElement>>(BaseUrl + "lease.php#CreateSession", bodyForSession);
                    if (resultSession != null)
                    {
                        // for attribute
                        var resultSessionDetails = resultSession
                         .Descendants("CreateSessionResult")
                         .ToList();

                        sessionId = resultSessionDetails[0].Value;
                    }
                }

            }
            leaseResponseModel.SessionId = sessionId;
            return leaseResponseModel;
        }

        public async Task<LeaseResponseModel> CloseSession(string sessionId)
        {
            LeaseResponseModel leaseResponseModel = new LeaseResponseModel();
            var bodyForCloseSession = CreateXMLDocument(@"<ns1:CloseSession>
                                                        <SessionId>" + sessionId + @"</SessionId>
                                                     </ns1:CloseSession>");

            var resultCloseSession = await AquatraqHelper.Post<List<XElement>>(BaseUrl + "lease.php#CloseSession", bodyForCloseSession);
            if (resultCloseSession != null)
            {
                // for attribute
                var resultCloseSessionDetails = resultCloseSession
                 .Descendants("CloseSessionResult")
                 .ToList();
            }

            return null;
        }

        /// <summary>
        /// Get Esignature Data
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>

        public async Task<LeaseResponseModel> GetEsignnatureDetails(string SessionId, string EsignatureId)
        {
            LeaseResponseModel esignatureDetailsModel = new LeaseResponseModel();
            var body = CreateXMLDocument(@"<ns1:GetEsignatureData>
                                                        <SessionId>" + SessionId + @"</SessionId>
                                                        <EsignatureId>" + EsignatureId + @"</EsignatureId>
                                                     </ns1:GetEsignatureData>");

            var result = await AquatraqHelper.Post<List<XElement>>(BaseUrl + "lease.php#GetEsignatureData", body);
            if (result != null)
            {
                // for attribute
                var keys = result
                 .Descendants("Key")
                 .ToList();

                var emails = result
        .Descendants("Email")
        .ToList();

                var DateSigned = result
       .Descendants("DateSigned")
       .ToList();

                esignatureDetailsModel.EsignatureId = EsignatureId;
                int i = 0;
                foreach (var item in keys)
                {
                    esignatureDetailsModel.EsigneResidents.Add(new KeyModel
                    {
                        Key = item.Value.ToString(), // unique key for user // url will be e.g https://www-new.bluemoonforms.com/esignature/{key}
                        DateSigned = Convert.ToString(DateSigned[i].Value), // date signed will be blank if resident not signed 
                        Email = Convert.ToString(emails[i].Value),

                    });
                    ;
                    i++;
                }
            }

            return esignatureDetailsModel;
        }


        public async Task<LeaseResponseModel> RequestEsignature(string sessionId, string leaseId, List<EsignatureParty> esignatureParties)
        {
            var owner = esignatureParties.Where(a => a.IsOwner == true).FirstOrDefault();

            string requestEsignStr = @"<ns1:RequestEsignature>
          <SessionId>" + sessionId + @"</SessionId>
            <LeaseId>" + leaseId + @"</LeaseId>
            <OwnerRep>
                <Name>" + owner.Name + @"</Name>
                <Email>" + owner.Email + @"</Email>
                <Phone>" + owner.Phone + @"</Phone>
            </OwnerRep><Residents>";

            foreach (var item in esignatureParties.Where(a => a.IsOwner == false))
            {
                requestEsignStr += @"<item>
                    <Name>" + item.Name + @"</Name>
                    <Email>" + item.Email + @"</Email>
                    <Phone>" + item.Phone + @"</Phone>
                </item>";
            }

            requestEsignStr += @"</Residents>
                                    <LeaseForms>
    	                                                <item><Id>RENTCON2</Id></item>
                                                        <item><Id>UTILITY</Id></item>
                                                        <item><Id>PETAGREE</Id></item>
                                                        <item><Id>APTLEASE</Id></item>
                                                        <item><Id>SUPPORTANIMAL</Id></item>
                                                        <item><Id>BEDBUGADD</Id></item>
                                                        <item><Id>FLBUYOUT</Id></item>
                                                        <item><Id>POLICIES</Id></item>
                                                        <item><Id>CRIMEDRUG</Id></item>
                                                        <item><Id>ENCGARA2</Id></item>
                                                        <item><Id>MIXEDUSE</Id></item>
                                                        <item><Id>MOLDADDN</Id></item>
                                                        <item><Id>NOSMOKE</Id></item>
                                                        <item><Id>PKGACCEPT</Id></item>
                                                        <item><Id>PARKINGADD</Id></item>
                                                        <item><Id>PHOTORELS</Id></item>
                                                        <item><Id>RAMFPOLICY</Id></item>
                                                        <item><Id>ADDGATE2</Id></item>
                                                        <item><Id>RENTINS2</Id></item>
                                                        <item><Id>SATELIT2</Id></item>
                                                        <item><Id>AIRBNB</Id></item>
                                         </LeaseForms>
                                        <SendOwnerRepNotices xsi:nil=""true""/>
                                    </ns1:RequestEsignature>
                                  ";


            //requestEsignStr += @"</Residents>
            //                        <LeaseForms>

            //                                            <item><Id>PETAGREE</Id></item>

            //                             </LeaseForms>
            //                            <SendOwnerRepNotices xsi:nil=""true""/>
            //                        </ns1:RequestEsignature>
            //                      ";

            LeaseResponseModel leaseResponseModel = new LeaseResponseModel();
            var bodyForCloseSession = CreateXMLDocument(requestEsignStr);


            var resultRequestEsign = await AquatraqHelper.Post<List<XElement>>(BaseUrl + "lease.php#RequestEsignature", bodyForCloseSession);
            if (resultRequestEsign != null)
            {
                // for attribute
                var resultCloseSessionDetails = resultRequestEsign
                 .Descendants("RequestEsignatureResult")
                 .ToList();




                var bodyEsignatureData = CreateXMLDocument(@"<ns1:GetEsignatureData>
                                                        <SessionId>" + sessionId + @"</SessionId>
                                                        <EsignatureId>" + resultCloseSessionDetails[0].Value + @"</EsignatureId>
                                                     </ns1:GetEsignatureData>");


                var resultEsignatureData = await AquatraqHelper.Post<List<XElement>>(BaseUrl + "lease.php#GetEsignatureData", bodyEsignatureData);
                if (resultEsignatureData != null)
                {
                    //// for attribute
                    //var resultGetEsignatureDetails = resultEsignatureData
                    // .Descendants("GetEsignatureDataResult")
                    // .ToList();

                    var keys = resultEsignatureData
                  .Descendants("Key")
                  .ToList();


                    var emails = resultEsignatureData
              .Descendants("Email")
              .ToList();

                    int i = 0;
                    foreach (var item in keys)
                    {
                        var bodyEsignResendRequest = CreateXMLDocument(@" <ns1:ResendEsignatureRequest>
                                                                        <SessionId>" + sessionId + @"</SessionId>
                                                                         <SignerKey>" + item.Value + @"</SignerKey>
                                                                        <Email>" + emails[i].Value + @"</Email>
                                                                        </ns1:ResendEsignatureRequest>");

                        var resultEsignatureResendRequestData = await AquatraqHelper.Post<List<XElement>>(BaseUrl + "lease.php#ResendEsignatureRequest", bodyEsignResendRequest);
                        if (resultEsignatureResendRequestData != null)
                        {
                            // for attribute
                            var resultGetEsignaturePdfDetails = resultEsignatureResendRequestData
                             .Descendants("ResendEsignatureRequestResult")
                             .ToList();

                            leaseResponseModel.EsignatureId = resultCloseSessionDetails[0].Value;

                        }
                        i++;
                    }
                }
            }


            leaseResponseModel.LeaseId = leaseId;
            leaseResponseModel.SessionId = sessionId;

            return leaseResponseModel;
        }

        public async Task<LeaseResponseModel> GetLeaseDocumentWithEsignature(string SessionId, string EsignatureId)
        {
            LeaseResponseModel leaseResponseModel = new LeaseResponseModel();
            var bodyEsignaturePdfData = CreateXMLDocument(@"<ns1:GetEsignaturePDF>
                                                                        <SessionId>" + SessionId + @"</SessionId>
                                                                        <EsignatureId>" + EsignatureId + @"</EsignatureId>
                                                                    </ns1:GetEsignaturePDF>");

            var resultEsignaturePdfData = await AquatraqHelper.Post<List<XElement>>(BaseUrl + "lease.php#GetEsignaturePDF", bodyEsignaturePdfData);
            if (resultEsignaturePdfData != null)
            {
                // for attribute
                var resultGetEsignaturePdfDetails = resultEsignaturePdfData
                 .Descendants("GetEsignaturePDFResult")
                 .ToList();

                byte[] bytes = Convert.FromBase64String(resultGetEsignaturePdfDetails[0].Value.ToString());
                leaseResponseModel.leasePdf = bytes;
            }

            leaseResponseModel.SessionId = SessionId;
            return leaseResponseModel;
        }


        public async Task<LeaseResponseModel> ExecuteLease(string SessionId, string EsignatureId, string SignatureFullText, string SignatureInitials, string SignatureTitle)
        {
            LeaseResponseModel leaseResponseModel = new LeaseResponseModel();

            try
            {
                var executeLeaseBoxy = CreateXMLDocument(@"<ns1:ExecuteLease>
                                                <SessionId>" + SessionId + @"</SessionId>
                                                <EsignatureId>" + EsignatureId + @"</EsignatureId>
                                                <OwnerRepSignature>" + SignatureFullText + @"</OwnerRepSignature>
                                                <OwnerRepInitials>" + SignatureInitials + @"</OwnerRepInitials>
                                                <OwnerRepTitle>" + SignatureTitle + @"</OwnerRepTitle>
                                                <OwnerRepClientIP xsi:nil=""true""/>
                                                </ns1:ExecuteLease>");

                var resultExecuteLease = await AquatraqHelper.Post<List<XElement>>(BaseUrl + "lease.php#ExecuteLease", executeLeaseBoxy);
                if (resultExecuteLease != null)
                {
                    // for attribute
                    var ExecuteLeaseResult = resultExecuteLease
                     .Descendants("ExecuteLeaseResult")
                     .ToList();

                    bool success = Convert.ToBoolean(ExecuteLeaseResult[0].Value);
                    leaseResponseModel.Success = success;
                }

                leaseResponseModel.SessionId = SessionId;
            }
            catch(Exception ex)
            {
                leaseResponseModel.Success = false;

            }
            return leaseResponseModel;

        }






        public async Task<LeaseResponseModel> GenerateLeasePdf(string sessionId, string leaseId)
        {
            LeaseResponseModel leaseResponseModel = new LeaseResponseModel();
            var bodyForAuth = CreateXMLDocument(@"<ns1:GetLeasePDF>
                                                  <SessionId>" + sessionId + @"</SessionId>
                                                  <LeaseId>" + leaseId + @"</LeaseId>
                                                  <LeaseForms>
    	                                                <item><Id>RENTCON2</Id></item>
                                                        <item><Id>UTILITY</Id></item>
                                                        <item><Id>PETAGREE</Id></item>
                                                        <item><Id>APTLEASE</Id></item>
                                                        <item><Id>SUPPORTANIMAL</Id></item>
                                                        <item><Id>BEDBUGADD</Id></item>
                                                        <item><Id>FLBUYOUT</Id></item>
                                                        <item><Id>POLICIES</Id></item>
                                                        <item><Id>CRIMEDRUG</Id></item>
                                                        <item><Id>ENCGARA2</Id></item>
                                                        <item><Id>INVENTRY</Id></item>
                                                        <item><Id>MIXEDUSE</Id></item>
                                                        <item><Id>MOLDADDN</Id></item>
                                                        <item><Id>NOSMOKE</Id></item>
                                                        <item><Id>PKGACCEPT</Id></item>
                                                        <item><Id>PARKINGADD</Id></item>
                                                        <item><Id>PHOTORELS</Id></item>
                                                        <item><Id>RAMFPOLICY</Id></item>
                                                        <item><Id>ADDGATE2</Id></item>
                                                        <item><Id>RENTINS2</Id></item>
                                                        <item><Id>SATELIT2</Id></item>
                                                        <item><Id>AIRBNB</Id></item>
                                                    </LeaseForms>
                                                    <Preview xsi:nil=""true""/>
                                                  </ns1:GetLeasePDF>");

            var resultAuth = await AquatraqHelper.Post<List<XElement>>(BaseUrl + "lease.php#GetLeasePDF", bodyForAuth);
            if (resultAuth != null)
            {
                // for attribute
                var resultOrderDetail = resultAuth
                 .Descendants("GetLeasePDFResult")
                 .ToList();

                byte[] bytes = Convert.FromBase64String(resultOrderDetail[0].Value.ToString());
                leaseResponseModel.leasePdf = bytes;
            }

            leaseResponseModel.LeaseId = leaseId;
            leaseResponseModel.SessionId = sessionId;
            return leaseResponseModel;
        }

    }
}