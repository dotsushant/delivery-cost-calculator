using System;
using System.Threading;
using System.Globalization;
using PageUp.CodeChallenge.Model;
using PageUp.CodeChallenge.Helpers;
using PageUp.CodeChallenge.Infrastructure;

namespace PageUp.CodeChallenge.Application
{
    /// <summary>
    /// Program to calculate the cost of delivery of parcel for postage
    /// </summary>
    class Program
    {
        /// <summary>
        /// Sets the culture for the whole application
        /// <remarks>The application is ready for localization</remarks>
        /// </summary>
        static Program()
        {
            var culture = new CultureInfo("en-au");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        /// <summary>
        /// Helper function to prompt the user
        /// </summary>
        /// <param name="message">Prompt message</param>
        /// <returns>User input as double value</returns>
        static double Prompt(object message)
        {
            Console.Write(message);

            double value = 0;
            double.TryParse(Console.ReadLine(), out value);
            return value;
        }

        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args">Command line arguments</param>
        static void Main(string[] args)
        {
            try
            {
                var weightInput = Prompt(ApplicationResource.WeightInput);

                var heightInput = Prompt(ApplicationResource.HeightInput);
                var widthInput = Prompt(ApplicationResource.WidthInput);
                var depthInput = Prompt(ApplicationResource.DepthInput);

                var parcelWeight = new Weight(weightInput);
                var parcelVolume = new Volume(heightInput, widthInput, depthInput);

                var parcel = new Parcel(parcelWeight, parcelVolume);

                var parcelValidator = new ParcelValidator();
                var parcelTag = parcelValidator.Validate(parcel);

                Console.WriteLine(ApplicationResource.CategoryFormat, ModelResource.ResourceManager.GetString(parcelTag.Category.ToString()));
                Console.WriteLine(ApplicationResource.CostFormat, parcelTag.DeliveryCost);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }

            Console.Write(ApplicationResource.EndProgramText);
            Console.ReadKey(true);
        }
    }
}