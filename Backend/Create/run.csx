using System;
using System.Net;

public static void Run(HttpRequestMessage req, ICollector<Relic> tableBinding, TraceWriter log)
{
    
    // parse query parameter
    string name = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
        .Value;
		
    string epitaph = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "epitaph", true) == 0)
        .Value;

    if (name != "") {
        tableBinding.Add(
            new Relic() { 
                PartitionKey = "Relic", 
                RowKey = Guid.NewGuid().ToString(), 
                Name = name,
				Epitaph = epitaph  
				}
            );
    }

}

public class Relic
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public string Name { get; set; }
    public string Epitaph { get; set; }
}
