/*

 * Projeye gerekli kütüphaneler eklenir.
 * System -> Temel işlemler için (input / output)
 * System.Net.Http -> HTTP istekleri için
 * System.Text.Json -> JSON işlemleri için
 * System.Text.Json.Serialization -> JSON serileştirme (veri grubunu JSON formatına çevirmek) işlemleri için
 * System.Threading.Tasks -> Asenkron programlama işlemleri için

*/
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

// Proje adı tanımlanır.
namespace WeatherApp
{
    // Ana sınıf tanımlanır.
    internal class Program
    {
        // Asenkron main metodu (ana metod) tanımlanır.
        static async Task Main(string[] args)
        {
            // HttpClient nesnesi oluşturulur.
            HttpClient client = new HttpClient();
            // Nesneye API'lerin bulunduğu kök url atanır.
            client.BaseAddress = new Uri("https://goweather.herokuapp.com/weather/");
            // Şehir adlarından oluşan dizi tanımlanır.
            string[] cities = { "istanbul", "izmir", "ankara" };
            // Her bir şehir için döngü oluşturulur.
            foreach (string city in cities)
            {
                // Şehir adına göre API isteği gönderilir. Gelen cevap response nesnesine atanır.
                HttpResponseMessage response = await client.GetAsync(city);
                // İstek başarılı ise çalışır.
                if (response.IsSuccessStatusCode)
                {
                    // JSON içeriği okunur ve content değişkenine atanır.
                    string content = await response.Content.ReadAsStringAsync();
                    // JSON içeriği havadurumu sınıfındaki alanlara çevrilir.
                    WeatherData weatherData = JsonSerializer.Deserialize<WeatherData>(content);
                    // Şehir isimlerinin her bir karakteri büyük olacak şekilde ekrana yazdırılır.
                    Console.WriteLine(city.ToUpper());
                    // Şehir için veriler yazdırılır.
                    Console.WriteLine("Sıcaklık: " + weatherData.Temperature + " Rüzgar: " + weatherData.Wind + " Açıklama: " + weatherData.Description);
                    // Ekrana Tahminler yazdırılır.
                    Console.WriteLine("Tahminler:");
                    /*                     
                     * Gün verileri weatherData.Forecast dizisinden çekilir ve ForecastData nesnesine dönüştürülür.
                     * forecast nesnesi ile erişmek istenilen verilere erişilir.
                    */
                    foreach (ForecastData forecast in weatherData.Forecast)
                    {
                        // Şehir için önümüzdeki günlerin tahminleri ekrana yazdırılır.
                        Console.WriteLine(forecast.Day + ". Gün Sıcaklık: " + forecast.Temperature + " Rüzgar: " + forecast.Wind);
                    }
                    // Her bir şehir bilgisi için verilerin arasına bir satırlık boşluk bırakılır.
                    Console.WriteLine();
                }
                // İstek başarısız ise çalışır.
                else
                {
                    // Ekrana hata mesajı yazdırılır.
                    Console.WriteLine("İstek Başarısız: " + response.ReasonPhrase);
                }
            }
            // Programın anında kapanmaması için 10 saniye program bekletilir.
            System.Threading.Thread.Sleep(10000);
        }
    }

    // Havadurumu sınıfı tanımlanır.
    public class WeatherData
    {
        /*
         * JSON belgesinde bulunan alan isimlerine göre değişkenler oluşturulur.
         * Değişkenlerin [JsonPropertyName("")] ile JSON başlıkları olacağı belirtilir.
         * JSON içeriği sıcaklık, rüzgar hızı, tanım ve gün bilgileri dizisidir.
         * Bu sınıf kök JSON verisidir.
        */

        [JsonPropertyName("temperature")]
        public string Temperature { get; set; }
        [JsonPropertyName("wind")]
        public string Wind { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("forecast")]
        public ForecastData[] Forecast { get; set; }
    }

    // Gün bilgisi sınıfı tanımlanır.
    public class ForecastData
    {
        /*
         * JSON belgesinde bulunan alan isimlerine göre değişkenler oluşturulur.
         * Değişkenlerin [JsonPropertyName("")] ile JSON başlıkları olacağı belirtilir.
         * JSON içeriğinde gün verileri gün, sıcaklık ve rüzgar hızı bilgileridir.
         * Bu sınıf gün bilgilerini tutan sınıftır ve WeatherData için gereklidir.
        */

        [JsonPropertyName("day")]
        public string Day { get; set; }
        [JsonPropertyName("temperature")]
        public string Temperature { get; set; }
        [JsonPropertyName("wind")]
        public string Wind { get; set; }
    }
}
