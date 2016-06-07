#r "Microsoft.WindowsAzure.Storage"
#r "Newtonsoft.Json"

using System.Net;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json; 

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, IQueryable<Item> tableBinding, TraceWriter log)
{
    
    string rowKey = req.GetQueryNameValuePairs()
      .FirstOrDefault(q => string.Compare(q.Key, "id", true) == 0)
      .Value;
  
    var query = from p in tableBinding.Where(p => p.RowKey == rowKey) select new Relic {id = p.RowKey, name = p.Name, epitaph = p.Epitaph };
    
    return await Task<HttpResponseMessage>.Factory.StartNew(() =>
    {
        return query == null
        ? req.CreateResponse(HttpStatusCode.OK, new JsonError {link="http://serverless.azurewebsites.net/api/GetRelic", data=null} ,"application/json")
        : req.CreateResponse(HttpStatusCode.OK, query.Take(1).First() ,"application/json");
        
    });
}

public class Item : TableEntity
{
    public string Name { get; set; }
    public string Epitaph { get; set; }
}

public class Relic 
{
    public string id { get; set; }
    public string name { get; set; }
    public string epitaph { get; set; }
}

public class JsonError {
    public string link { get; set;}
    public object data { get; set;}
    public JsonError() {}
}
