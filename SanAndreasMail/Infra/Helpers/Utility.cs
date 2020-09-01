using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanAndreasMail.Infra.Helpers
{
    public class Utility
    {
        /// <summary>
        /// Return the value of appsettings by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnectionString(string key)
        {
            // Defines the sources of configuration information for the
            // application.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            // Create the configuration object that the application will
            // use to retrieve configuration information.
            var configuration = builder.Build();
            // Retrieve the configuration information.
            var configValue = configuration[key];
            return configValue;
        }

        /// <summary>
        /// Method to read file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>StringBuilder with lines</returns>
        public static List<string> ReadFile(string filePath)
        {

            List<string> stringList = new List<string>();

            try
            {
                if (!File.Exists(filePath))
                    throw new IOException("Arquivo inexistente.");

                string line = "";

                // Open the text file using a stream reader.
                using (var sr = new StreamReader(filePath))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        stringList.Add(line);
                        //Console.WriteLine(line);
                    }
                }

            }
            catch (IOException e)
            {
                throw new IOException("O arquivo não pode ser lido: " + e.Message);
            }

            return stringList;
        }

        /// <summary>
        /// Method to write on file
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filePath"></param>
        public static void WriteFile(string content, string filePath)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(content);
                }

            }
            catch (IOException e)
            {
                throw new IOException("The file could not be written: " + e.Message);
            }
        }

    }
}
