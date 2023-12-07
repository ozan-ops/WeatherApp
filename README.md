# WeatherApp

Bu proje, belirli şehirlerin hava durumu bilgilerini JSON dosyasından almak için bir HTTP istemcisi kullanır.

## Kullanılan Kütüphaneler

- System
- System.Net.Http
- System.Text.Json
- System.Text.Json.Serialization
- System.Threading.Tasks

## Kullanılan NuGet Paketleri

- System.Text.Json (Microsoft)

## Nasıl Çalışır

1. HttpClient nesnesi oluşturulur ve API'lerin bulunduğu kök url atanır.
2. Şehir adlarından oluşan bir dizi tanımlanır.
3. Her bir şehir için döngü oluşturulur ve şehir adına göre API isteği gönderilir.
4. İstek başarılı ise, JSON içeriği okunur ve havadurumu sınıfındaki alanlara çevrilir.
5. Şehir için veriler yazdırılır ve önümüzdeki günlerin tahminleri ekrana yazdırılır.

## Sınıflar

- WeatherData: JSON belgesinde bulunan alan isimlerine göre değişkenler oluşturulur. JSON içeriği sıcaklık, rüzgar hızı, tanım ve gün bilgileri dizisidir. Bu sınıf kök JSON verisidir.
- ForecastData: JSON belgesinde bulunan alan isimlerine göre değişkenler oluşturulur. JSON içeriği gün, sıcaklık ve rüzgar hızı bilgisidir.