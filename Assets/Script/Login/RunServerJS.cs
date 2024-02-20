using UnityEngine;
using System.Diagnostics;
using System;

public class RunServerJS : MonoBehaviour
{
    void Start()
    {
        RunServer();
    }

    void RunServer()
    {
        // Find the npx and Node.js executable paths
        //string npxPath = FindExecutable("npx");
        string nodePath = @"C:\path\to\node.exe";
        string npxPath = @"C:\Program Files\nodejs\npx";
        string scriptPath = @"C:\path\to\your\LoginBackend-Cashflow\server.js";

        if (npxPath == null || nodePath == null)
        {
            UnityEngine.Debug.LogError("Could not find npx or node executable.");
            return;
        }

        // Provide the path to your server.js file (adjust as needed)
        //string scriptPath = "path/to/your/LoginBackend-Cashflow/server.js";

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = npxPath,
            Arguments = $"nodemon {scriptPath}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            WorkingDirectory = @"C:/path/to/your/LoginBackend-Cashflow"
        };

        Process process = new Process
        {
            StartInfo = psi
        };

        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        process.WaitForExit();

        // Log the output and error to Unity's console
        UnityEngine.Debug.Log("Output: " + output);
        UnityEngine.Debug.Log("Error: " + error);
        UnityEngine.Debug.LogError("PATH Environment Variable: " + Environment.GetEnvironmentVariable("PATH"));
    }

    // Function to find the executable path in the system's PATH
    private string FindExecutable(string executableName)
    {
        string pathVariable = Environment.GetEnvironmentVariable("PATH");
        foreach (string path in pathVariable.Split(';'))
        {
            string fullPath = System.IO.Path.Combine(path, executableName);
            if (System.IO.File.Exists(fullPath))
            {
                return fullPath;
            }
        }

        return null;
    }
}
