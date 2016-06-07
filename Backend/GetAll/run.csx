#r "Microsoft.WindowsAzure.Storage"

using System.Net;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, IQueryable<Item> tableBinding, TraceWriter log)
{
    var query = from p in tableBinding select new Relic {id = p.RowKey, name = p.Name, epitaph = p.Epitaph };

    return await Task<HttpResponseMessage>.Factory.StartNew(() =>
    {
        return query == null
        ? req.CreateResponse(HttpStatusCode.OK, new JsonError {link="http://serverless.azurewebsites.net/api/test", data=null} ,"application/json")
        : req.CreateResponse(HttpStatusCode.OK, query.ToArray(),"application/json");
        
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
