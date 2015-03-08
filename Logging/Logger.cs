using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Barjees.Logging
{
    /// <summary>
    /// Logger class
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Log file name
        /// </summary>
        static readonly string fileName;

        /// <summary>
        /// Default static constructor
        /// </summary>
        static Logger()
        {
            fileName = GetLogFolder() + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".csv";
            Log("Type", "Player", "Pawn", "Cell", "Description", "Outcome");            
        }

        /// <summary>
        /// Log a message
        /// </summary>
        /// <param name="logType">Log record type</param>
        /// <param name="player">Current player if exists</param>
        /// <param name="pawn">Current pawn if exists</param>
        /// <param name="cell">Current cell if exists</param>
        /// <param name="description">Record description</param>
        /// <param name="outcome">The outcome of the action</param>
        public static void Log(string logType, string player, string pawn, string cell, string description, string outcome)
        {
            string message = string.Format("{0},{1},{2},{3},{4},{5}", logType, player, pawn, cell, description, outcome);
            File.AppendAllLines(fileName, new string[] { message });
        }
        /// <summary>
        /// Return the log file contents
        /// </summary>
        /// <returns>Log lines</returns>
        public static string[] GetContents()
        {
            string[] contents = File.ReadAllLines(fileName);
            return contents.Skip(1).ToArray();
        }
        /// <summary>
        /// Returns the log files directory
        /// </summary>
        /// <returns>Log directory path</returns>
        public static string GetLogFolder()
        {
            return Directory.GetCurrentDirectory() + @"\Log\";
        }
    }
}
