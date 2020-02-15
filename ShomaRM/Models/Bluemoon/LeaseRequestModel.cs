using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Models.Bluemoon
{
    public class LeaseRequestModel
    {
        public string ADDRESS { get; set; }
        //<Length>54</Length>
        public DateTime? DATE_OF_LEASE { get; set; }
        public DateTime? LEASE_BEGIN_DATE { get; set; }
        public DateTime? LEASE_END_DATE { get; set; }
        public int? MAXIMUM_GUEST_STAY { get; set; }
        //<Length>2</Length>
        public string OCCUPANT_1 { get; set; }
        //<Length>75</Length>
        public string OCCUPANT_2 { get; set; }
        //<Length>75</Length>
        public string OCCUPANT_3 { get; set; }
        //<Length>75</Length>
        public string OCCUPANT_4 { get; set; }
        //<Length>75</Length>
        public string OCCUPANT_5 { get; set; }
        //<Length>75</Length>
        public string OCCUPANT_6 { get; set; }
        //<Length>75</Length>
        public float? PRORATED_RENT { get; set; }
        //<Length>8</Length>

            //date format MM-dd-yyyy
        public DateTime? PRORATED_RENT_DUE_DATE { get; set; }
        public string PRORATED_RENT_DUE_FIRST_MONTH { get; set; }
        //<Type>enum</Type>
        public float? RENT { get; set; }
        //<Length>8</Length>
        public string RESIDENT_1 { get; set; }
        //<Length>75</Length>
        public string RESIDENT_2 { get; set; }
        //<Length>75</Length>
        public string RESIDENT_3 { get; set; }
        //<Length>75</Length>
        public string RESIDENT_4 { get; set; }
        //<Length>75</Length>
        public string RESIDENT_5 { get; set; }
        //<Length>75</Length>
        public string RESIDENT_6 { get; set; }
        //<Length>75</Length>
        public float? SECURITY_DEPOSIT { get; set; }
        //<Length>8</Length>
        public string UNIT_NUMBER { get; set; }
        //<Length>15</Length>
        public string APARTMENT_MANAGER_ADDRESS { get; set; }
        //<Length>80</Length>
        public string APARTMENT_MANAGER_NAME { get; set; }
        //<Length>75</Length>
        public string APARTMENT_OWNER_OR_MANAGER { get; set; }
        //<Type>enum</Type>
        public string LEASE_TERMINATION_NOTICE_ADDRESS { get; set; }
        //<Length>70</Length>
        public bool? SECURITY_DEPOSIT_COMMINGLED_ACCOUNT { get; set; }
        public string SECURITY_DEPOSIT_COMMINGLED_BANK_ADDRESS { get; set; }
        //<Length>80</Length>
        public string SECURITY_DEPOSIT_COMMINGLED_BANK_NAME { get; set; }
        //<Length>35</Length>
        public bool? SECURITY_DEPOSIT_INTEREST_ACCOUNT { get; set; }
        public string SECURITY_DEPOSIT_INTEREST_BANK_ADDRESS { get; set; }
        //<Length>80</Length>
        public string SECURITY_DEPOSIT_INTEREST_BANK_NAME { get; set; }
        //<Length>50</Length>
        public bool? SECURITY_DEPOSIT_NONINTEREST_ACCOUNT { get; set; }
        public string SECURITY_DEPOSIT_NONINTEREST_BANK_ADDRESS { get; set; }
        //<Length>80</Length>
        public string SECURITY_DEPOSIT_NONINTEREST_BANK_NAME { get; set; }
        //<Length>50</Length>
        public string DAYS_REQUIRED_FOR_NOTICE_OF_LEASE_TERMINATION { get; set; }
        //<Length>3</Length>
        public float? LATE_CHARGE_DAILY_CHARGE { get; set; }
        //<Length>6</Length>
        public string LATE_CHARGE_INITIAL_CHARGE { get; set; }
        //<Length>6</Length>
        public float? LIQUIDATED_DAMAGES { get; set; }
        //<Length>8</Length>
        public int? NUMBER_OF_APARTMENT_KEYS { get; set; }
        //<Length>2</Length>
        public int? NUMBER_OF_MAIL_KEYS { get; set; }
        //<Length>2</Length>
        public int? NUMBER_OF_OTHER_KEYS { get; set; }
        //<Length>2</Length>
        public string OTHER_KEY_TYPE { get; set; }
        //<Length>29</Length>
        public string RENT_DUE_DATE { get; set; }
        //<Type>day</Type>
        public string RENTERS_INSURANCE_REQUIREMENT { get; set; }
        //<Type>enum</Type>
        public float? RETURNED_CHECK_CHARGE { get; set; }
        //<Length>6</Length>
        public bool? UTILITIES_CABLE_TV { get; set; }
        public bool? UTILITIES_ELECTRICITY { get; set; }
        public bool? UTILITIES_GAS { get; set; }
        public bool? UTILITIES_MASTER_ANTENNA { get; set; }
        public bool? UTILITIES_OTHER { get; set; }
        public string UTILITIES_OTHER_DESCRIPTION { get; set; }
        //<Length>35</Length>
        public bool? UTILITIES_TRASH { get; set; }
        public bool? UTILITIES_WASTEWATER { get; set; }
        public bool? UTILITIES_WATER { get; set; }
        public string FIRE_PROTECTION_AVAILABLE { get; set; }//1 or 2
        //<Type>enum</Type>
        public bool? FIRE_PROTECTION_CARBON_MONOXIDE_DETECTOR { get; set; }
        public bool? FIRE_PROTECTION_FIRE_EXTINGUISHER { get; set; }
        public bool? FIRE_PROTECTION_OTHER { get; set; }
        public string FIRE_PROTECTION_OTHER_DESCRIPTION { get; set; }
        //<Length>26</Length>
        public bool? FIRE_PROTECTION_SMOKE_DETECTOR { get; set; }
        public bool? FIRE_PROTECTION_SPRINKLER_IN_APARTMENT { get; set; }
        public bool? FIRE_PROTECTION_SPRINKLER_IN_COMMON_AREAS { get; set; }
        public string PAY_RENT_ADDRESS { get; set; }
        //<Length>140</Length>
        public bool? PAY_RENT_AT_ONLINE_SITE { get; set; }
        public bool? PAY_RENT_AT_OTHER_LOCATION { get; set; }
        public bool? PAY_RENT_ON_SITE { get; set; }
        public string SPECIAL_PROVISIONS { get; set; }
        //<Length>1530</Length>
        public string UNIT_FURNISHED { get; set; } // 1 0r 2
        //<Type>enum</Type>
        public string LOCATOR_ADDRESS { get; set; }
        //<Length>30</Length>
        public string LOCATOR_CITY_STATE_ZIP { get; set; }
        //<Length>30</Length>
        public string LOCATOR_NAME { get; set; }
        //<Length>30</Length>
        public string OWNERS_REPRESENTATIVE_ADDRESS_1 { get; set; }
        //<Length>35</Length>
        public string OWNERS_REPRESENTATIVE_ADDRESS_2 { get; set; }
        //<Length>35</Length>
        public string OWNERS_REPRESENTATIVE_TELEPHONE { get; set; }
        //<Type>phone</Type>
        public string PET_2_AGE { get; set; }
        //<Length>12</Length>
        public string PET_2_BREED { get; set; }
        //<Length>24</Length>
        public string PET_2_COLOR { get; set; }
        //<Length>24</Length>
        public DateTime? PET_2_DATE_OF_LAST_SHOTS { get; set; }
        public string PET_2_HOUSE_BROKEN { get; set; }
        //<Length>24</Length>
        public string PET_2_LICENSE_CITY { get; set; }
        //<Length>24</Length>
        public string PET_2_LICENSE_NUMBER { get; set; }
        //<Length>24</Length>
        public string PET_2_NAME { get; set; }
        //<Length>24</Length>
        public string PET_2_OWNERS_NAME { get; set; }
        //<Length>75</Length>
        public string PET_2_TYPE { get; set; }
        //<Length>24</Length>
        public string PET_2_WEIGHT { get; set; }
        //<Length>8</Length>
        public float? PET_ADDITIONAL_RENT { get; set; }
        //<Length>7</Length>
        public string PET_AGE { get; set; }
        //<Length>12</Length>
        public string PET_BREED { get; set; }
        //<Length>24</Length>
        public string PET_COLOR { get; set; }
        //<Length>24</Length>
        public DateTime? PET_DATE_OF_LAST_SHOTS { get; set; }
        public string PET_DR_ADDRESS { get; set; }
        //<Length>25</Length>
        public string PET_DR_CITY { get; set; }
        //<Length>25</Length>
        public string PET_DR_NAME { get; set; }
        //<Length>25</Length>
        public string PET_DR_TELEPHONE { get; set; }
        //<Type>phone</Type>
        public string PET_HOUSE_BROKEN { get; set; }
        //<Length>24</Length>
        public string PET_LICENSE_CITY { get; set; }
        //<Length>24</Length>
        public string PET_LICENSE_NUMBER { get; set; }
        //<Length>24</Length>
        public string PET_NAME { get; set; }
        //<Length>24</Length>
        public float? PET_ONE_TIME_FEE { get; set; }
        //<Length>7</Length>
        public string PET_OWNERS_NAME { get; set; }
        //<Length>75</Length>
        public float? PET_SECURITY_DEPOSIT { get; set; }
        //<Length>7</Length>
        public string PET_SECURITY_DEPOSIT_SCOPE { get; set; }
        //<Type>enum</Type>
        public string PET_SPECIAL_PROVISIONS { get; set; }
        //<Length>750</Length>
        public string PET_TYPE { get; set; }
        //<Length>24</Length>
        public string PET_URINATE_INSIDE_AREAS { get; set; }
        //<Length>49</Length>
        public string PET_URINATE_OUTSIDE_AREAS { get; set; }
        //<Length>49</Length>
        public string PET_WEIGHT { get; set; }
        //<Length>8</Length>
        public string SECURITY_DEPOSIT_INCLUDES_ADD_PET_RENT { get; set; }
        //<Type>enum</Type>
        public string SECURITY_DEPOSIT_INCLUDES_ANIMAL_DEPOSIT { get; set; }
        //<Type>enum</Type>
        public string EARLY_TERMINATION_CHOICE_OF_DAMAGES { get; set; }
        //<Type>enum</Type>
        public float? EARLY_TERMINATION_FEE { get; set; }
        //<Length>8</Length>
        public bool? GARAGE_ADDENDUM_CARPORT_PROVIDED { get; set; }
        public string GARAGE_ADDENDUM_CARPORT_SPACE_NO { get; set; }
        //<Length>12</Length>
        public bool? GARAGE_ADDENDUM_GARAGE_OR_CARPORT_PROVIDED { get; set; }
        public bool? GARAGE_ADDENDUM_GARAGE_PROVIDED { get; set; }
        public string GARAGE_ADDENDUM_GARAGE_SPACE_NO { get; set; }
        //<Length>12</Length>
        public string GARAGE_ADDENDUM_SPECIAL_PROVISIONS { get; set; }
        //<Length>750</Length>
        public string GARAGE_ADDENDUM_STORAGE_UNIT_NO { get; set; }
        //<Length>12</Length>
        public bool? GARAGE_ADDENDUM_STORAGE_UNIT_PROVIDED { get; set; }
        public string NO_SMOKING_ADDENDUM_SMOKING_AREAS { get; set; }
        //<Length>150</Length>
        public string NO_SMOKING_ADDENDUM_SMOKING_DISTANCE { get; set; }
        //<Length>4</Length>
        public string NO_SMOKING_ADDENDUM_SMOKING_POLICY { get; set; }
        //<Type>enum</Type>
        public string NO_SMOKING_SPECIAL_PROVISIONS { get; set; }
        //<Length>750</Length>
        public string RAMF_POLICY_CONTACT_PERSON { get; set; }
        //<Length>40</Length>
        public string RAMF_POLICY_CONTACT_PHONE_AND_ADDRESS { get; set; }
        //<Length>145</Length>
        public string SATELLITE_ADDENDUM_DEPOSIT_SCOPE { get; set; }
        //<Type>enum</Type>
        public float? SATELLITE_ADDENDUM_INSURANCE { get; set; }
        //<Length>10</Length>
        public int? SATELLITE_ADDENDUM_NO_DISHES { get; set; }
        //<Length>2</Length>
        public float? SATELLITE_ADDENDUM_SECURITY_DEPOSIT { get; set; }
        //<Length>8</Length>
        public string SATELLITE_ADDENDUM_SPECIAL_PROVISIONS { get; set; }
        //<Length>750</Length>
        public string SECURITY_DEPOSIT_INCLUDES_SATELLITE_DEP { get; set; }
        //<Type>enum</Type>
        public float? UTILITY_ADDENDUM_ADMINISTRATION_FEE { get; set; }
        //<Length>6</Length>
        public float? UTILITY_ADDENDUM_BILLING_FEE_LIMIT { get; set; }
        //<Length>6</Length>
        public bool? UTILITY_ADDENDUM_CABLE_TV_BILLING_CO { get; set; }
        public string UTILITY_ADDENDUM_CABLE_TV_BILLING_CO_DESC { get; set; }
        //<Length>54</Length>
        public bool? UTILITY_ADDENDUM_CABLE_TV_FLAT_RATE_SELECTED { get; set; }
        public string UTILITY_ADDENDUM_CABLE_TV_FORMULA { get; set; }
        //<Length>6</Length>
        public string UTILITY_ADDENDUM_CABLE_TV_PAID_BY { get; set; }
        //<Type>enum</Type>
        public float? UTILITY_ADDENDUM_CABLE_TV_RATE { get; set; }
        //<Length>8</Length>
        public int? UTILITY_ADDENDUM_DAYS_TO_PAY_BILL { get; set; }
        //<Length>2</Length>
        public bool? UTILITY_ADDENDUM_ELECTRIC_BILLING_CO { get; set; }

        public string UTILITY_ADDENDUM_ELECTRIC_BILLING_CO_DESC { get; set; }
        //<Length>54</Length>

        public bool? UTILITY_ADDENDUM_ELECTRIC_FLAT_RATE_SELECTED { get; set; }

        public string UTILITY_ADDENDUM_ELECTRIC_FORMULA { get; set; }
        //<Length>8</Length>
        public string UTILITY_ADDENDUM_ELECTRIC_PAID_BY { get; set; }
        //<Type>enum</Type>
        public float? UTILITY_ADDENDUM_ELECTRIC_RATE { get; set; }
        //<Length>8</Length>
        public float? UTILITY_ADDENDUM_FINAL_BILL_FEE { get; set; }
        //<Length>6</Length>
        public float? UTILITY_ADDENDUM_FINAL_BILL_FEE_LIMIT { get; set; }
        //<Length>6</Length>
        public bool? UTILITY_ADDENDUM_GAS_BILLING_CO { get; set; }
        public string UTILITY_ADDENDUM_GAS_BILLING_CO_DESC { get; set; }
        //<Length>54</Length>
        public bool? UTILITY_ADDENDUM_GAS_FLAT_RATE_SELECTED { get; set; }
        public string UTILITY_ADDENDUM_GAS_FORMULA { get; set; }
        //<Length>8</Length>
        public string UTILITY_ADDENDUM_GAS_PAID_BY { get; set; }
        //<Type>enum</Type>
        public float? UTILITY_ADDENDUM_GAS_RATE { get; set; }
        //<Length>8</Length>
        public bool? UTILITY_ADDENDUM_INTERNET_BILLING_CO { get; set; }
        public string UTILITY_ADDENDUM_INTERNET_BILLING_CO_DESC { get; set; }
        //<Length>54</Length>
        public bool? UTILITY_ADDENDUM_INTERNET_FLAT_RATE_SELECTED { get; set; }
        public string UTILITY_ADDENDUM_INTERNET_FORMULA { get; set; }
        //<Length>8</Length>
        public string UTILITY_ADDENDUM_INTERNET_PAID_BY { get; set; }
        //<Type>enum</Type>
        public float? UTILITY_ADDENDUM_INTERNET_RATE { get; set; }
        //<Length>8</Length>
        public float? UTILITY_ADDENDUM_LATE_FEE { get; set; }
        //<Length>6</Length>
        public float? UTILITY_ADDENDUM_LATE_FEE_LIMIT { get; set; }
        //<Length>6</Length>
        public bool? UTILITY_ADDENDUM_MASTER_ANTENNA_BILLING_CO { get; set; }
        public string UTILITY_ADDENDUM_MASTER_ANTENNA_BILLING_CO_DESC { get; set; }
        //<Length>54</Length>
        public bool? UTILITY_ADDENDUM_MASTER_ANTENNA_FLAT_RATE_SELECTED { get; set; }
        public string UTILITY_ADDENDUM_MASTER_ANTENNA_FORMULA { get; set; }
        //<Length>6</Length>
        public string UTILITY_ADDENDUM_MASTER_ANTENNA_PAID_BY { get; set; }
        //<Type>enum</Type>
        public float? UTILITY_ADDENDUM_MASTER_ANTENNA_RATE { get; set; }
        //<Length>8</Length>
        public string UTILITY_ADDENDUM_OTHER { get; set; }
        //<Length>28</Length>
        public string UTILITY_ADDENDUM_OTHER_2 { get; set; }
        //<Length>28</Length>
        public bool? UTILITY_ADDENDUM_OTHER_BILLING_CO { get; set; }
        public bool? UTILITY_ADDENDUM_OTHER_BILLING_CO_2 { get; set; }
        public string UTILITY_ADDENDUM_OTHER_BILLING_CO_DESC { get; set; }
        //<Length>54</Length>
        public string UTILITY_ADDENDUM_OTHER_BILLING_CO_DESC_2 { get; set; }
        //<Length>54</Length>
        public float? UTILITY_ADDENDUM_OTHER_BILLING_FEE { get; set; }
        //<Length>6</Length>
        public bool? UTILITY_ADDENDUM_OTHER_FLAT_RATE_SELECTED { get; set; }
        public bool? UTILITY_ADDENDUM_OTHER_FLAT_RATE_SELECTED_2 { get; set; }
        public string UTILITY_ADDENDUM_OTHER_FORMULA { get; set; }
        //<Length>6</Length>
        public string UTILITY_ADDENDUM_OTHER_FORMULA_2 { get; set; }
        //<Length>6</Length>
        public string UTILITY_ADDENDUM_OTHER_PAID_BY { get; set; }
        //<Type>enum</Type>
        public string UTILITY_ADDENDUM_OTHER_PAID_BY_2 { get; set; }
        //<Type>enum</Type>
        public float? UTILITY_ADDENDUM_OTHER_RATE { get; set; }
        //<Length>8</Length>
        public float? UTILITY_ADDENDUM_OTHER_RATE_2 { get; set; }
        //<Length>8</Length>
        public bool? UTILITY_ADDENDUM_PEST_CONTROL_BILLING_CO { get; set; }
        public string UTILITY_ADDENDUM_PEST_CONTROL_BILLING_CO_DESC { get; set; }
        //<Length>54</Length>
        public bool? UTILITY_ADDENDUM_PEST_CONTROL_FLAT_RATE_SELECTED { get; set; }
        public string UTILITY_ADDENDUM_PEST_CONTROL_FORMULA { get; set; }
        //<Length>6</Length>
        public string UTILITY_ADDENDUM_PEST_CONTROL_PAID_BY { get; set; }
        //<Type>enum</Type>
        public float? UTILITY_ADDENDUM_PEST_CONTROL_RATE { get; set; }
        //<Length>8</Length>
        public float? UTILITY_ADDENDUM_SET_UP_FEE { get; set; }
        //<Length>6</Length>
        public float? UTILITY_ADDENDUM_SET_UP_FEE_LIMIT { get; set; }
        //<Length>6</Length>
        public bool? UTILITY_ADDENDUM_SEWER_BILLING_CO { get; set; }
        public string UTILITY_ADDENDUM_SEWER_BILLING_CO_DESC { get; set; }
        //<Length>54</Length>
        public bool? UTILITY_ADDENDUM_SEWER_FLAT_RATE_SELECTED { get; set; }
        public string UTILITY_ADDENDUM_SEWER_FORMULA { get; set; }
        //<Length>8</Length>
        public string UTILITY_ADDENDUM_SEWER_PAID_BY { get; set; }
        //<Type>enum</Type>
        public float? UTILITY_ADDENDUM_SEWER_RATE { get; set; }
        //<Length>8</Length>
        public string UTILITY_ADDENDUM_SPECIAL_PROVISIONS { get; set; }
        //<Length>2048</Length>
        public bool? UTILITY_ADDENDUM_STORMWATER_BILLING_CO { get; set; }
        public string UTILITY_ADDENDUM_STORMWATER_BILLING_CO_DESC { get; set; }
        //<Length>54</Length>
        public bool? UTILITY_ADDENDUM_STORMWATER_FLAT_RATE_SELECTED { get; set; }
        public string UTILITY_ADDENDUM_STORMWATER_FORMULA { get; set; }
        //<Length>6</Length>
        public string UTILITY_ADDENDUM_STORMWATER_PAID_BY { get; set; }
        //<Type>enum</Type>
        public float? UTILITY_ADDENDUM_STORMWATER_RATE { get; set; }
        //<Length>8</Length>
        public bool? UTILITY_ADDENDUM_TRASH_BILLING_CO { get; set; }
        public string UTILITY_ADDENDUM_TRASH_BILLING_CO_DESC { get; set; }
        //<Length>54</Length>
        public bool? UTILITY_ADDENDUM_TRASH_FLAT_RATE_SELECTED { get; set; }
        public string UTILITY_ADDENDUM_TRASH_FORMULA { get; set; }
        //<Length>8</Length>
        public string UTILITY_ADDENDUM_TRASH_PAID_BY { get; set; }
        //<Type>enum</Type>
        public float? UTILITY_ADDENDUM_TRASH_RATE { get; set; }
        //<Length>8</Length>
        public bool? UTILITY_ADDENDUM_WATER_BILLING_CO { get; set; }
        public string UTILITY_ADDENDUM_WATER_BILLING_CO_DESC { get; set; }
        //<Length>54</Length>
        public bool? UTILITY_ADDENDUM_WATER_FLAT_RATE_SELECTED { get; set; }
        public string UTILITY_ADDENDUM_WATER_FORMULA { get; set; }
        //<Length>8</Length>
        public string UTILITY_ADDENDUM_WATER_PAID_BY { get; set; }
        //<Type>enum</Type>
        public float? UTILITY_ADDENDUM_WATER_RATE { get; set; }
        //<Length>8</Length>
        public DateTime? PARKING_BEGIN_DATE { get; set; }
        public string PARKING_DUE_DATE { get; set; }
        //<Type>day</Type>
        public DateTime? PARKING_END_DATE { get; set; }
        public float? PARKING_MONTHLY_CHARGE { get; set; }
        //<Length>8</Length>
        public string PARKING_MONTHLY_CHARGE_DATE { get; set; }
        //<Type>day</Type>
        public string PARKING_NSF_FEE { get; set; }
        //<Length>8</Length>
        public DateTime? PARKING_ONE_TIME_DATE { get; set; }
        public string PARKING_ONE_TIME_FEE { get; set; }
        //<Length>8</Length>
        public string PARKING_SPACE_NUMBER_1 { get; set; }
        //<Length>12</Length>
        public string PARKING_SPACE_NUMBER_2 { get; set; }
        //<Length>12</Length>
        public string PARKING_SPACE_NUMBER_3 { get; set; }
        //<Length>12</Length>
        public string PARKING_SPECIAL_PROVISIONS { get; set; }
        //<Length>750</Length>
        public string VEHICLE_LICENSE_NUMBER_1 { get; set; }
        //<Length>12</Length>
        public string VEHICLE_LICENSE_NUMBER_2 { get; set; }
        //<Length>12</Length>
        public string VEHICLE_LICENSE_NUMBER_3 { get; set; }
        //<Length>12</Length>
        public string VEHICLE_MAKE_1 { get; set; }
        //<Length>28</Length>
        public string VEHICLE_MAKE_2 { get; set; }
        //<Length>28</Length>
        public string VEHICLE_MAKE_3 { get; set; }
        //<Length>28</Length>
        public string VEHICLE_MODEL_YEAR_1 { get; set; }
        //<Length>25</Length>
        public string VEHICLE_MODEL_YEAR_2 { get; set; }
        //<Length>25</Length>
        public string VEHICLE_MODEL_YEAR_3 { get; set; }
        //<Length>25</Length>
        public string VEHICLE_OWNER_NUMBER_1 { get; set; }
        //<Type>phone</Type>
        public string VEHICLE_OWNER_NUMBER_2 { get; set; }
        //<Type>phone</Type>
        public string VEHICLE_OWNER_NUMBER_3 { get; set; }
        //<Type>phone</Type>
        public string VEHICLE_PERMIT_NUMBER_1 { get; set; }
        //<Length>15</Length>
        public string VEHICLE_PERMIT_NUMBER_2 { get; set; }
        //<Length>15</Length>
        public string VEHICLE_PERMIT_NUMBER_3 { get; set; }
        //<Length>15</Length>
        public string VEHICLE_STATE_1 { get; set; }
        //<Length>15</Length>
        public string VEHICLE_STATE_2 { get; set; }
        //<Length>15</Length>
        public string VEHICLE_STATE_3 { get; set; }
        //<Length>15</Length>
        public string COMMUNITY_BUSINESS_CENTER_AVAILABILITY { get; set; }
        //<Type>enum</Type>
        public int? COMMUNITY_BUSINESS_CENTER_COMPUTER_TIME_LIMIT { get; set; }
        //<Length>3</Length>
        public int? COMMUNITY_FIRE_HAZARDS_GRILL_DISTANCE { get; set; }
        //<Length>4</Length>
        public string COMMUNITY_FITNESS_CENTER_AVAILABILITY { get; set; }
        //<Type>enum</Type>
        public string COMMUNITY_FITNESS_CENTER_CARD_1 { get; set; }
        //<Length>12</Length>
        public string COMMUNITY_FITNESS_CENTER_CARD_2 { get; set; }
        //<Length>12</Length>
        public string COMMUNITY_FITNESS_CENTER_CARD_3 { get; set; }
        //<Length>12</Length>
        public string COMMUNITY_FITNESS_CENTER_CARD_4 { get; set; }
        //<Length>12</Length>
        public string COMMUNITY_FITNESS_CENTER_CARD_5 { get; set; }
        //<Length>12</Length>
        public string COMMUNITY_FITNESS_CENTER_CARD_6 { get; set; }
        //<Length>12</Length>
        public string COMMUNITY_PACKAGE_RELEASE_POLICY { get; set; }
        //<Type>enum</Type>
        public string COMMUNITY_POLICIES_SPECIAL_PROVISIONS { get; set; }
        //<Length>600</Length>
        public string COMMUNITY_POOL_AVAILABILITY { get; set; }
        //<Type>enum</Type>
        public int? COMMUNITY_VEHICLES_MAXIMUM_ALLOWED { get; set; }
        //<Length>1</Length>
        public int? COMMUNITY_VEHICLES_TOWING_NOTICE { get; set; }
        //<Length>2</Length>
        public float? ADDENDUM_RENT_CONCESSION_AMOUNT { get; set; }
        //<Length>8</Length>
        public string ADDENDUM_RENT_CONCESSION_DESCRIPTION { get; set; }
        //<Length>170</Length>
        public bool? ADDENDUM_RENT_CONCESSION_MONTHLY_DISCOUNT { get; set; }
        public bool? ADDENDUM_RENT_CONCESSION_ONE_TIME { get; set; }
        public float? ADDENDUM_RENT_CONCESSION_ONE_TIME_AMOUNT { get; set; }
        public string ADDENDUM_RENT_CONCESSION_ONE_TIME_MONTHS { get; set; }
        public bool? ADDENDUM_RENT_CONCESSION_OTHER_DISCOUNT { get; set; }
        public bool? ADDENDUM_RENT_CONCESSION_REPAY_CONCESSIONS { get; set; }
        public bool? ADDENDUM_RENT_CONCESSION_REPAY_DISCOUNTS { get; set; }
        public string ADDENDUM_RENT_CONCESSION_SPECIAL_PROVISIONS { get; set; }
        //<Length>750</Length>
        public bool? CONCESSION_NONMONETARY { get; set; }
        public string CONCESSION_NONMONETARY_DESC { get; set; }
        //<Length>175</Length>
        public float? RENTERS_INSURANCE_LIABILITY_LIMIT { get; set; }
        //<Length>10</Length>
        public float? RENTERS_INSURANCE_MINIMUM_COVERAGE { get; set; }
        //<Length>10</Length>
        public string RENTERS_INSURANCE_PROVIDER { get; set; }
        //<Length>55</Length>
        public string RENTERS_INSURANCE_SPECIAL_PROVISIONS { get; set; }
        //<Length>750</Length>
        public string PACKAGE_ACCEPTANCE_SPECIAL_PROVISIONS { get; set; }
        //<Length>750</Length>
        public string PACKAGE_ACCEPTANCE_TIME_LIMIT { get; set; }
        //<Length>5</Length>
        public float? REMOTE_CARD_CODE_ADDENDUM_ADD_CARD_FEE { get; set; }
        //<Length>6</Length>
        public float? REMOTE_CARD_CODE_ADDENDUM_ADD_REMOTE_FEE { get; set; }
        //<Length>6</Length>
        public bool? REMOTE_CARD_CODE_ADDENDUM_CARD { get; set; }
        public bool? REMOTE_CARD_CODE_ADDENDUM_CODE { get; set; }
        public bool? REMOTE_CARD_CODE_ADDENDUM_CODE_CHANGE { get; set; }
        public bool? REMOTE_CARD_CODE_ADDENDUM_LOST_CARD { get; set; }
        public float? REMOTE_CARD_CODE_ADDENDUM_LOST_CARD_DEDUCT { get; set; }
        //<Length>6</Length>
        public bool? REMOTE_CARD_CODE_ADDENDUM_LOST_REMOTE { get; set; }
        public float? REMOTE_CARD_CODE_ADDENDUM_LOST_REMOTE_DEDUCT { get; set; }
        //<Length>6</Length>
        public bool? REMOTE_CARD_CODE_ADDENDUM_REMOTE { get; set; }
        public float? REMOTE_CARD_CODE_ADDENDUM_REPLACE_CARD_FEE { get; set; }
        //<Length>6</Length>
        public float? REMOTE_CARD_CODE_ADDENDUM_REPLACE_REMOTE_FEE { get; set; }
        //<Length>6</Length>
        public string REMOTE_CARD_CODE_SPECIAL_PROVISIONS { get; set; }
        //<Length>750</Length>
        public string AIRBNB_SPECIAL_PROVISIONS { get; set; }
        //<Length>750</Length>
        public string BED_BUG_SPECIAL_PROVISIONS { get; set; }
        //<Length>750</Length>
        public string CRIME_SPECIAL_PROVISIONS { get; set; }
        //<Length>750</Length>
        public string INVENTORY_SPECIAL_PROVISIONS { get; set; }
        //<Length>750</Length>
        public string MIXED_USE_SPECIAL_PROVISIONS { get; set; }
        //<Length>750</Length>
        public string MOLD_ADDENDUM_SPECIAL_PROVISIONS { get; set; }
        //<Length>750</Length>
        public string PHOTO_RELEASE_ADDENDUM_SPECIAL_PROVISIONS { get; set; }
        //<Length>750</Length>
        public string SUPPORT_ANIMAL_SPECIAL_PROVISIONS { get; set; }
        //<Length>750</Length>
        public bool? INVENTORY_MOVE_IN { get; set; }
        public DateTime? INVENTORY_MOVE_IN_DATE { get; set; }
        public bool? INVENTORY_MOVE_OUT { get; set; }
        public DateTime? INVENTORY_MOVE_OUT_DATE { get; set; }
    }
}