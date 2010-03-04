using System;
using System.Collections.Generic;
using System.IO;

namespace HenIT.Utilities
{
    public static class CommandLineUtils
    {
        #region GetDefault
        public static List<string> GetDefault(string[] args)
        {
            List<string> fileNames = new List<string>();
            foreach (string arg in args)
            {
                if (!(arg.StartsWith("-") || arg.StartsWith("/")))
                {
                    fileNames.Add(arg);                    
                }
            }
            return fileNames;
        }

        public static string GetDefault(string[] args, string defaultValue)
        {
            string commandValue = defaultValue;
            foreach (string arg in args)
            {
                if (arg.StartsWith("-") || arg.StartsWith("/"))
                    continue;
                else
                {
                    commandValue = arg;
                    break;
                }
            }
            return commandValue;
        }
        /// <summary>
        /// Get all file names passed as parameters
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static List<string> GetFileNames(string[] args)
        {
            List<string> fileNames = new List<string>();
            foreach (string arg in args)
            {
                if (!(arg.StartsWith("-") || arg.StartsWith("/")))
                {
                    if (File.Exists(arg) || Directory.Exists(arg))
                    {
                        fileNames.Add(arg);
                    }
                }
            }
            return fileNames;
        }
        #endregion

        #region GetCommand
        public static string GetCommand(string[] args, string defaultValue, params string[] commandSwitches)
        {
            string commandValue = defaultValue;
            bool foundValue = false;
            foreach (string arg in args)
            {
                foreach (string commandSwitch in commandSwitches)
                {
                    if (arg.ToUpper().StartsWith(commandSwitch.ToUpper()))
                    {
                        commandValue = arg.Substring(commandSwitch.Length);
                        foundValue = true;
                        break;
                    }
                }
                if (foundValue)
                    break;
            }
            return commandValue;
        }

        #endregion

        #region IsCommand
        public static bool IsCommand(string[] args, string commandSwitch)
        {
            string[] commandSwitches = commandSwitch.Split(',');
            return IsCommand(args, commandSwitches);
        }
        public static bool IsCommand(string[] args, params string[] commandSwitches)
        {
            foreach (string arg in args)
            {
                foreach (string commandSwitch in commandSwitches)
                {
                    if (arg.ToUpper() == commandSwitch.ToUpper())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
    }
}