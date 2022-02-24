using ProtoBuf;
using System.Net;

static class Program
{
    public static void Main()
    {
        Stream a = new MemoryStream();
        Serializer.Serialize<Root1>(a, new Root1());
        string Registered = Convert.ToBase64String(a.ToByteArray());
        var Cookies = CookiesKnuddels(Registered);
        Console.WriteLine("Waiting 20 sec");
        Thread.Sleep(20000);
        Serializer.Serialize<Root3>(a, new Root3());
        string Registered1 = Convert.ToBase64String(a.ToByteArray());
        Console.WriteLine(RegisterKnuddels(Registered1, Cookies));
        Console.ReadLine();
    }
    static CookieCollection CookiesKnuddels(string Register)
    {
        var url = "https://www.knuddels.de/registration/registration_submit.html";

        var httpRequest = (HttpWebRequest)WebRequest.Create(url);
        httpRequest.Method = "POST";
        httpRequest.Accept = "application/x-protobuf+base64";
        httpRequest.Referer = "https://www.knuddels.de/";
        httpRequest.ContentType = "application/x-protobuf+base64; charset=UTF-8";
        httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36 OPR/83.0.4254.46";
        httpRequest.CookieContainer = new CookieContainer();
        var data = Register;
        httpRequest.AllowAutoRedirect = true;
        httpRequest.KeepAlive = true;
        using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
        {
            streamWriter.Write(data);
        }

        var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            Console.WriteLine(httpResponse.StatusCode);
            Console.WriteLine(result);
            Console.WriteLine("Cookies: "+httpResponse.Cookies.Count);
            for (int i = 0; i != httpResponse.Cookies.Count; i++)
            {
                Console.WriteLine("Got Cookie: [" + httpResponse.Cookies[i].Name + $"]=[{httpResponse.Cookies[i].Value}]");
            }
            return httpResponse.Cookies;
        }
    }
    static string RegisterKnuddels(string Register, CookieCollection cookies)
    {
        var url = "https://www.knuddels.de/registration/registration_submit.html";

        var httpRequest = (HttpWebRequest)WebRequest.Create(url);
        httpRequest.Method = "POST";
        httpRequest.Accept = "application/x-protobuf+base64";
        httpRequest.Referer = "https://www.knuddels.de/";
        httpRequest.ContentType = "application/x-protobuf+base64; charset=UTF-8";
        httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36 OPR/83.0.4254.46";
        httpRequest.CookieContainer = new CookieContainer();
        httpRequest.CookieContainer.Add(cookies);
        for (int i = 0; i != cookies.Count; i++)
        {
            Console.WriteLine("Added Cookie: ["+cookies[i].Name+$"]=[{cookies[i].Value}]");
        }
        httpRequest.CookieContainer.Add(new Cookie("shared___deviceIdentifier", "1b68267-959a-4911-8c62-8d584366054b", "/registration/registration_submit.html", "knuddels.de"));
        httpRequest.CookieContainer.Add(new Cookie("ksgagc", "%2CRegStart", "/registration/registration_submit.html", "knuddels.de"));
        var data = Register;
        httpRequest.AllowAutoRedirect = true;
        httpRequest.KeepAlive = true;
        using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
        {
            streamWriter.Write(data);
        }

        var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            Console.WriteLine(httpResponse.StatusCode);
            return result;
        }
    }
    public static byte[] ToByteArray(this Stream stream)
    {
        if (stream is MemoryStream)
            return ((MemoryStream)stream).ToArray();
        else
        {
            using MemoryStream ms = new();
            stream.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
#region EndSequenz
[ProtoContract]
class Root3
{
    [ProtoMember(1)]
    public RegisterObject3 root = new RegisterObject3();
}
[ProtoContract]
class RegisterObject3
{
    [ProtoMember(1)]
    public string Command = "/registration.RegistrationRequest";
    [ProtoMember(2)]
    public AccountInformations3 Informations = new AccountInformations3();
}
[ProtoContract]
class AccountInformations3
{
    [ProtoMember(1)]
    public int Action = 7; //7 is register 
    [ProtoMember(2)]
    public string Username = "Not-Blank-6";
    [ProtoMember(3)]
    public int Age = 60;
    [ProtoMember(4)]
    public int Gender = 1; //1 = Male 2 = Female
    [ProtoMember(8)]
    public string Password = "";
    [ProtoMember(9)]
    public int Category = 7; //7 is Default idk more about it
    [ProtoMember(10)]
    public string NullString = ""; //seems like Extra Tags or smth going to investigate it
    [ProtoMember(11)]
    public string SharedHWID = "1b68267-959a-4911-8c62-8d584366054b"; //Generate a Random HWID to be undetected
    [ProtoMember(14)]
    public string AGB_Accept =  "Stimmst du den (AGB|https://www.knuddels.de/agb) und der Verarbeitung deiner Daten im Rahmen unsere (Datenschutzerklärung|https://www.knuddels.de/legal/privacy-policy.html) zu?";
    [ProtoMember(15)]
    public string Captcha = "09AGEo2mNF6k7CZUbN89DD9Bk74UTcw9lLEiDWqHjFHI8n_1vuk-YJQ3q4Ds4auzQOKsXhvUrKm2hbFXUVnTDzb8QuSQ";
}
#endregion
#region FirstSequenz
[ProtoContract]
class Root1
{
    [ProtoMember(1)]
    public RegisterObject1 root = new RegisterObject1();
}
[ProtoContract]
class RegisterObject1
{
    [ProtoMember(1)]
    public string Command = "/registration.RegistrationRequest";
    [ProtoMember(2)]
    public AccountInformations1 Informations = new AccountInformations1();
}
[ProtoContract]
class AccountInformations1
{
    [ProtoMember(1)]
    public int Action = 1; //7 is register 1 is Check Username
    [ProtoMember(2)]
    public string Username = "Not-Blank-6";
    [ProtoMember(3)]
    public int Age = 60;
    [ProtoMember(4)]
    public int Gender = 1; //1 = Male 2 = Female
    [ProtoMember(9)]
    public int Category = 7; //7 is Default idk more about it
}
#endregion