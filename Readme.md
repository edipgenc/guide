
<!-- ABOUT THE PROJECT -->
## About The Project

[![Projeyi basit bir kişi rehberi olarak tanımlayabiliriz, burada önemli olan clean architecture, SOLID, generic repository, Queue gibi sistemlerin micros servis mimarisinde nasıl çalıştığını göstermektir]](https://github.com/edipgenc/guide)


Projemiz iki adet micro servisten oluşmaktadır.
Database olarak PostgreSQL ve Mongo Db kullanılmıştır.
Queue için RabbitMQ kullanılmıştır.
Rabbit MQ Docker üzerindne çalışacaktır.
Diğer kullanılan teknolojiler ise Fluent Validation ve Auto Mapperdır.
Tasarım deseni olarak ta Reporsitory Pattern ve Generic Repository Pattern kullanılmıştır.
İlk micro servisimiz olan Guide.Book, Kişi kaydet, update et ve delete gibi crud işlemlerinin yanı sıra kişiye iletişim bilgisi ekle, sil ve güncelle gibi işlemleri yapmaktadır.
İki micro servisimiz ise Guide.Report'tur bu micro servis gelen rapor taleplerini alır ve Queue ya yazar, aynı zamanda tüm istenen rapor listesi, tek rapor detayı ve rapor sonucu gibi metodlarda içerir.
Servise bir rapor talebi gelince micro servis önce Mongo ya istek geldiğine dair bir kayıt atar daha sonra publisher gibi Queue ya bir event atar, ilk micro servisimizde çalışan background servis bir comsumer gibi gelen rapor isteklerine göre ilgili repository den bir rapor çalıştırır, ilgili rapor tamamlanınca rapor sonucu ikinci micro servisteki SaveReportResult a bir istek atar, burada data içinde hem rapor sonucu hemde gelen eventin rapor Id si yer alır bu sayede isteği olmayan raporlar Mongoya kaydedilmez.



### Built With

Below is a list of the technologies I have used to make this project possible.

* [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
* [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
* [EF Core](https://docs.microsoft.com/en-us/ef/core/)
* [AutoMapper](https://github.com/AutoMapper/AutoMapper)
* [FluentValidation](https://docs.fluentvalidation.net/en/latest/)
* [RabbitMQ](https://www.rabbitmq.com/)
* [PostgreSQL](https://www.postgresql.org/)
* [Docker](https://docker.com/)