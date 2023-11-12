using System.IO;
using System.Net.Http;

namespace Gw2AddonManagement.Exception;

public class GitHubRequestFailedException : System.Exception
{
    private readonly string _response;

    public GitHubRequestFailedException(HttpResponseMessage response) : base($"request to github failed with {response.StatusCode}.")
    {
        using var stream = response.Content.ReadAsStream();
        using var reader = new StreamReader(stream);
        _response = reader.ReadToEnd();
    }

    public override string ToString()
    {
        return $"{nameof(GitHubRequestFailedException)} - {_response}";
    }
}