using Python.Runtime;

namespace Gui_Diplom.Helpers
{
    public static class PythonVenvHelper
    {
        public static dynamic Detector {get; set;}

        public static void InitPython()
        {
            string venvPath = @"D:\python\fire_diplom\.venv";
            string pythonDll = @"C:\Users\Raven\AppData\Local\Programs\Python\Python313\python313.dll";

            InitVenv(venvPath, pythonDll);

            using (Py.GIL())
            {
                dynamic sys = Py.Import("sys");
                sys.path.append(@"D:\python\fire_diplom\Version3(Mask cnn)");
                sys.path.append(@"D:\python\fire_diplom\.venv\Lib\site-packages");

                dynamic fireModule = Py.Import("fire_inference");
                dynamic FireDetector = fireModule.FireDetector;
                Detector = FireDetector(@"D:\python\fire_diplom\fire_detector_smoke_and_fireplace_added_model.pth");
            }
        }

        /// <summary>
        /// Initializes Python.NET with a virtual environment without using Activate.ps1.
        /// </summary>
        /// <param name="venvPath">Full path to the venv folder (contains Scripts, Lib, etc.)</param>
        /// <param name="basePythonDll">Full path to base python DLL (e.g., python313.dll)</param>
        private static void InitVenv(string venvPath, string basePythonDll)
        {
            if (!Directory.Exists(venvPath))
                throw new DirectoryNotFoundException($"Venv path not found: {venvPath}");
            if (!File.Exists(basePythonDll))
                throw new FileNotFoundException($"Python DLL not found: {basePythonDll}");

            // Determine key paths inside venv
            string libPath = Path.Combine(venvPath, "Lib");
            string sitePackages = Path.Combine(libPath, "site-packages");
            string scriptsPath = Path.Combine(venvPath, "Scripts");

            // Set environment variables like Activate.ps1 would
            Environment.SetEnvironmentVariable("VIRTUAL_ENV", venvPath);
            Environment.SetEnvironmentVariable("PYTHONHOME", venvPath);
            Environment.SetEnvironmentVariable("PYTHONPATH", $"{libPath};{sitePackages}");

            // Prepend Scripts to PATH so Python can find executables
            string oldPath = Environment.GetEnvironmentVariable("PATH") ?? "";
            Environment.SetEnvironmentVariable("PATH", $"{scriptsPath}{Path.PathSeparator}{oldPath}");

            // Tell Python.NET which DLL to load
            Runtime.PythonDLL = basePythonDll;

            // Initialize Python engine
            PythonEngine.Initialize();
            PythonEngine.BeginAllowThreads();
        }
    }
}
