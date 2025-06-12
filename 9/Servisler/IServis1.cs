namespace Benimsite2.Servisler
{
    public interface IServis1
    {
        //ASP.NET Core'da bağımlılık enjeksiyonu (Dependency Injection, DI), bir sınıfın ihtiyaç duyduğu
        //bağımlılıkları dışarıdan sağlamak için kullanılan bir tasarım desenidir. Bu, sınıfların kendi bağımlılıklarını oluşturmak yerine,
        //bu bağımlılıkların dışarıdan bir container tarafından sağlanmasını ifade eder.
        //Bağımlılık enjeksiyonu için kullanılan interface
        //Avantajları: Test Edilebilirlik, esneklik ve bakım kolaylığı, sınıfların birbirine olan bağımlılığnı azaltma, yeniden kullanılabililik
        string Merhaba();
    }
}
