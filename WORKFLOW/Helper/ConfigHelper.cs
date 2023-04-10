namespace WORKFLOW.Helper
{
    public class ConfigHelper
    {
        public static T loadConfig<T>(ConfigurationBuilder configurationBuilder, string envName) where T : new()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string text = Path.Combine(currentDirectory, ".env");
            Console.WriteLine(text.ToString());
            if (File.Exists(text)) {
                string[] array = File.ReadAllLines(text);
                foreach (string text2 in array) {
                    int num = text2.IndexOf('=');
                    if (num > 0) {
                        string variable = text2.Substring(0, num).Trim();
                        string value = "";
                        if (text2.Length > num + 1) {
                            value = text2.Substring(num + 1, text2.Length - (num + 1)).Trim();
                        }

                        Environment.SetEnvironmentVariable(variable, value);
                    }
                }
            }

            IConfigurationBuilder configurationBuilder2 = new ConfigurationBuilder().SetBasePath(currentDirectory).AddEnvironmentVariables();
            text = Path.Combine(currentDirectory, "appsettings." + envName + ".json");
            Console.WriteLine(text.ToString());
            if (File.Exists(text)) {
                configurationBuilder2.AddJsonFile(text);
            }

            text = Path.Combine(currentDirectory, "appsettings.json");
            Console.WriteLine(text.ToString());
            if (File.Exists(text)) {
                configurationBuilder2.AddJsonFile(text);
            }

            IConfigurationRoot configurationRoot = configurationBuilder2.Build();
            T val = new T();
            configurationRoot.GetSection(val.GetType().Name).Bind(val);
            configurationRoot.Bind(val);
            return val;
        }
    }
}
