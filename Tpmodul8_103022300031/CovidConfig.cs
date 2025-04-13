using System.Text.Json;

public class CovidConfig
{
    public string satuan_suhu { get; set; }
    public int batas_hari_deman { get; set; }
    public string pesan_ditolak { get; set; }
    public string pesan_diterima { get; set; }

    private const string configPath = "covid_config.json";

    private static bool isInitialized = false;

    public CovidConfig()
    {
        if (isInitialized) return;
        isInitialized = true;

        try
        {
            string json = File.ReadAllText(configPath);
            var config = JsonSerializer.Deserialize<CovidConfig>(json);
            satuan_suhu = config.satuan_suhu;
            batas_hari_deman = config.batas_hari_deman;
            pesan_ditolak = config.pesan_ditolak;
            pesan_diterima = config.pesan_diterima;
        }
        catch
        {
            SetDefault();
            SaveConfig();
        }
    }


    private void SetDefault()
    {
        satuan_suhu = "celcius";
        batas_hari_deman = 14;
        pesan_ditolak = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
        pesan_diterima = "Anda dipersilahkan untuk masuk ke dalam gedung ini";
    }

    private void SaveConfig()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(this, options);
        File.WriteAllText(configPath, json);
    }

    public void UbahSatuan()
    {
        if (satuan_suhu.ToLower() == "celcius")
            satuan_suhu = "fahrenheit";
        else
            satuan_suhu = "celcius";

        SaveConfig();
    }
}