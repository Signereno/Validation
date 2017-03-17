//######################################################################//
//            This example uses the following nuget packages:           //
//                                                                      //
//            - IdentiSign.Validation.Client                            //
//            - IdentiSign.Validation.Models                            //
//                                                                      //              
//######################################################################//


using System;
using System.IO;
using IdentiSign.Validation.Client;
using IdentiSign.Validation.Client.ValidationClient.NO;
using IdentiSign.Validation.Models.Constants;
using IdentiSign.Validation.Models.NO.BankID;

namespace ValidationExampleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //If you have an account insert your credentials here, you can do up to 5 requests per day without an account
            Guid? accountId = null;
            string oauthClientId = null;
            string oauthSecret = null;


            //#################################################################################################//
            //                                    SDO (Norwegian bankid)                                       //      
            //                                ---------------------------------                                //          
            //#################################################################################################//
            


            //Create new client for norwegain bank id
            INoBankidValidationClient noClient = new NoBankidValidationClient(accountId, oauthClientId, oauthSecret);

            var fileData = File.ReadAllBytes("App_Data/signedtxt.sdo");
            var originalText = File.ReadAllBytes("App_Data/originaltxt.txt"); // The original of the signed text document (You can include this in the request (dataToValidate) 
                                                                              // to check that the original text matches the signed text)

            //  Validate SDO                                   
            //--------------------                                       

            Console.WriteLine("Validating SDO document, please wait...");
            var request = new ValidateSDORequest()
            {
                sdoData = Convert.ToBase64String(fileData),
                ExternalReference = "Some reference",
                dataToValidate = Convert.ToBase64String(originalText) //Optional
            };
            
            var result = noClient.ValidateSdo(request, isProd: false);

            Console.WriteLine(Extensions.SerializeAndFormat(result));

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            // Validate and Parse SDO                                 
            //---------------------------------  
            Console.WriteLine("Parsing SDO document, please wait...");

            var parseRequest = new ParseSDORequest()
            {
                sdoData = Convert.ToBase64String(fileData),
                ExternalReference = "Some reference",
            };

            var parseResult = noClient.ValidateAndParseSdo(parseRequest, isProd: false);

            Console.WriteLine(Extensions.SerializeAndFormat(parseResult));

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }
    }
}
