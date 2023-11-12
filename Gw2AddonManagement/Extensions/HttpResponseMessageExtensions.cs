using System.IO;
using System.Net.Http;
using Newtonsoft.Json;

namespace Gw2AddonManagement.Extensions;

public static class HttpResponseMessageExtensions
{
    public static string GetResponseContentAsString(this HttpResponseMessage responseMessage)
    {
        using var stream = responseMessage.Content.ReadAsStream();
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    public static T GetContentAs<T>(this HttpResponseMessage responseMessage)
    {
        var content = JsonConvert.DeserializeObject<T>(responseMessage.GetResponseContentAsString());

        if (content != null)
        {
            return content;
        }
        else
        {
            throw new System.Exception("could not parse");
        }
    }
}