using System.IO.Compression;
using System.Text;
using RequestReader;


/*   * Instructions
     First of all you need to uncomment ExractZip method to get access.log file 
     and from there you can read the specific file and uncomment Main method. */

// * Settings
string fileToRead = "access" + ".log";
Directory.SetCurrentDirectory(@"..\..\..\Files");

// * Main Program
//ExtractZip();
//Main();

// * Main Methods
void ExtractZip()
{
    try
    {
        ZipFile.ExtractToDirectory("access.zip", ".");
        Console.WriteLine("Done!");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
void Main()
{
    try
    {
        List<string> requestsString = File.ReadAllLines(fileToRead).ToList();
        List<Request> requests = new List<Request>();
        Request request;

        foreach (var @string in requestsString)
        {
            //Check that the requests are correct
            if (@string[0] == '#')
            {
                continue;
            }

            //Separate the parts of the request
            string[] requestParts = @string.Split(' ', StringSplitOptions.RemoveEmptyEntries).Where(x => x != "-").ToArray();//0 1 2 6
            if (requestPartsIsWrong(requestParts))
                continue;


            //Chech if the request features are correct
            int[] ip = requestParts[2].Split(".", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            if (requestPartsIsWrong(requestParts))
                continue;

            request = GetRequest(requests, ip, int.Parse(requestParts[6]));

            //Adding requests to the list or increasing their number of appearances
            if (requests.Contains(request))
            {
                request.IncreaseCount();
            }
            else
            {
                requests.Add(request);
            }
        }

        //List manipulation
        requests.Sort();
        requests.Reverse();

        //List for printing
        List<string> report = Report(requests);
        File.WriteAllLines("report.csv", report);
        Console.WriteLine("Done!");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }

}



// * Methods
bool requestPartsIsWrong(string[] requestParts)
{
    int port = int.Parse(requestParts[6]);
    string ipString = requestParts[2];
    if (ipString == "Periodic-Log")
    {
        return true;
    }

    int[] ip = requestParts[2].Split(".", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    if (ip.Length < 3 || ip[0] == 207 && ip[1] == 114 || port != 80)
    {
        return true;
    }
    return false;
}

List<string> Report(List<Request> requests)
{
    List<string> report = new List<string>();
    for (int i = 0; i < requests.Count; i++)
    {
        string reportString = $"{requests[i].Count},  \"{string.Join(".", requests[i].IP)}\"";
        report.Add(reportString);
    }
    return report;
}

Request GetRequest(List<Request> requests, int[] ip, int port)
{
    foreach (var request in requests)
    {
        if (string.Join(".", request.IP) == string.Join(".", ip))
        {
            return request;
        }
    }
    return new Request(ip, port);
}