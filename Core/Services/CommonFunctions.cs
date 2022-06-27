using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Common;
using Domain.Constants;
using Domain.Enums;
using PhoneNumbers;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Services
{
    public class CommonFunctions : ICommonFunctions
    {
        public bool IsDualConsentRecording(string targetPhoneNumber, Dictionary<string, string> areaPhoneCodes)
        {
            try
            {
                if (string.IsNullOrEmpty(targetPhoneNumber))
                {
                    //safety check
                    return false;
                }
                if (targetPhoneNumber.StartsWith("+") && !targetPhoneNumber.StartsWith("+1"))
                {
                    bool IsEuropean = IsNumberEuropean(targetPhoneNumber);
                    if (IsEuropean)
                    {
                        /* And, we're done here. */
                        return true;
                    }
                }
                if (!targetPhoneNumber.StartsWith("+"))
                {
                    string code = "+";
                    if (!targetPhoneNumber.StartsWith("1"))
                    {
                        code = "+1";
                    }
                    targetPhoneNumber = code + targetPhoneNumber;
                }

                if (targetPhoneNumber.Length > 5)
                {
                    if (targetPhoneNumber.StartsWith("+1"))
                    {
                        string areaCode = targetPhoneNumber.Substring(2, 3);
                        if (areaPhoneCodes.ContainsKey(areaCode))
                        {
                            if (Enum.TryParse<DisallowedConsentStates>(areaPhoneCodes.Get(areaCode)?.Trim(), out var value))
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw new NotFoundException("Error Parsing phone number : {0} ", targetPhoneNumber);
                throw new NotFoundException("Exception in parsing phone number: { 0} ", e);
            }
            return false;
        }

        private bool IsNumberEuropean(string targetPhoneNumber)
        {
            bool IsTrue = false;
            try
            {
                PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
                PhoneNumbers.PhoneNumber.Builder phNumber = new PhoneNumbers.PhoneNumber.Builder();
                string updatedPhoneNo = string.Empty;
                string countryName = GetCountryName(targetPhoneNumber, string.Empty, phoneUtil, phNumber, out updatedPhoneNo);
                if (countryName.Any())
                {
                    bool isName = Enum.TryParse<EuropeanCountries>(countryName?.Trim(), true, out var value);
                    if (isName)
                    {
                        IsTrue = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Error in checking if the number is European: {0} Exception Message: " + ex.ToString(), targetPhoneNumber);
            }

            return IsTrue;
        }

        private static string GetCountryName(string phoneNumber, string country, PhoneNumberUtil phoneUtil, PhoneNumbers.PhoneNumber.Builder phNumber, out string updatedPhonenumber)
        {
            string countryName = null;
            updatedPhonenumber = phoneNumber;
            int typeOfException = 1;
            try
            {
                bool checkForCountryCode = false;
                bool CountryDerived = false;
                //string isoCode = GetDefaultISO2(country);
                phoneNumber = new System.Text.RegularExpressions.Regex(@"[(,),\s]").Replace(phoneNumber, string.Empty);
                if (phoneNumber.StartsWith("+"))
                {
                    PhoneNumbers.PhoneNumber.Builder phNumberTemp = phNumber.Clone();
                    var phoneMetadata = PhoneNumbers.PhoneMetadata.DefaultInstance;
                    StringBuilder nationalNumber = new StringBuilder();
                    int countryCode = phoneUtil.MaybeExtractCountryCode(phoneNumber, phoneMetadata, nationalNumber, true, phNumber); //country code derived from Phone Number
                    countryName = GetCountryByCode(countryCode.ToString());
                }
                else if (!countryName.Any())
                {
                    string tempPhoneNumber = "+1" + phoneNumber;
                    // Logger.Info(String.Format("Going through +1 logic for phoneNumber: {0}", phoneNumber));
                    PhoneNumbers.PhoneNumber.Builder phNumberTemp = phNumber.Clone();
                    phoneUtil.Parse(tempPhoneNumber, country, phNumberTemp);
                    bool isValid = phoneUtil.IsValidNumber(phNumberTemp.Build());
                    if (isValid)
                    {
                        CountryDerived = true;
                    }
                }
                phoneNumber = updatedPhonenumber;
                if (!countryName.Any() && !phoneNumber.StartsWith("0") && !CountryDerived)
                {
                    typeOfException = 2;
                    //Remove special charcater from phone number 
                    phoneNumber = Regex.Replace(phoneNumber, @"[^0-9A-Za-z]", "");
                    phoneNumber = '+' + phoneNumber; // Add '+' to phonenumber
                    PhoneNumbers.PhoneNumber.Builder phNumberTemp = phNumber.Clone();
                    phoneUtil.Parse(phoneNumber, country, phNumberTemp);
                    bool isValid = phoneUtil.IsValidNumber(phNumberTemp.Build());
                    if (isValid)
                    {
                        var phoneMetadata = PhoneNumbers.PhoneMetadata.DefaultInstance;
                        StringBuilder nationalNumber = new StringBuilder();
                        int countryCode = phoneUtil.MaybeExtractCountryCode(phoneNumber, phoneMetadata, nationalNumber, true, phNumber);
                        countryName = GetCountryByCode(countryCode.ToString());
                    }
                    else
                    {
                        throw new InvalidValueException("Invalid phone number is provided {0}", phoneNumber);
                        //countryName = GetCountryByISOCode(isoCode);
                    }
                }
                if (countryName.Any())
                {
                    updatedPhonenumber = phoneNumber;
                }
            }
            catch (Exception ex)
            {
                //if (typeOfException == 1)
                //Logger.Info(string.Format("Failed to get country name for number : {0}", phoneNumber));
                throw new NotFoundException("Failed to get country name for number : {0}", phoneNumber);
            }
            return (!countryName.Any() ? GetCountryByCode("1") : countryName);
        }

        private static string GetCountryByCode(string countryCode)
        {
            string countryName = string.Empty;
            try
            {
                countryName = CountryInfoProvider.GetCountryInfo().Where(x => string.Equals(x.Code, countryCode)).Select(x => x.Name).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Failed to get country name for code : {0}", countryCode);
            }
            return (countryName == null) ? string.Empty : countryName;
        }
    }
}
