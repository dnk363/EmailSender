using EmailSender.ViewModels;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace EmailSender.Services
{
    public class DataIOService
    {
        private readonly string PATH;

        public DataIOService(string path)
        {
            PATH = path;
        }

        public BindingList<ViewSettings> LoadData()
        {
            if (!File.Exists(PATH))
            {
                Logger.Debug($"File not found - {PATH}");
                File.CreateText(PATH);
                return new BindingList<ViewSettings>();
            }
            using (var reader = File.OpenText(PATH))
            {
                return JsonSerializer.Deserialize<BindingList<ViewSettings>>(reader.ReadToEnd());
            }
        }

        public void SaveData(object dataSettingsList)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                writer.Write(JsonSerializer.Serialize<object>(dataSettingsList));
            }
        }
    }
}
