#r "Microsoft.WindowsAzure.Storage"

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;
using System;
using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, CloudTable tableBinding, TraceWriter log)
{
  string rowKey = req.GetQueryNameValuePairs()
      .FirstOrDefault(q => string.Compare(q.Key, "id", true) == 0)
      .Value;

 string name = req.GetQueryNameValuePairs()
      .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
      .Value;
      
  string epitaph = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "epitaph", true) == 0)
        .Value;

  try {
 
    TableOperation retrieveOperation = TableOperation.Retrieve<Item>("Relic", rowKey);

    Item retrievedRelic = (Item)tableBinding.Execute(retrieveOperation).Result;
	
	retrievedRelic.Name = name;
	retrievedRelic.Epitaph = epitaph;
	
	TableOperation updateOperation = TableOperation.Replace(retrievedRelic);

    tableBinding.Execute(updateOperation);
	
	
  }
  catch 
  {}
  
  return await Task<HttpResponseMessage>.Factory.StartNew(() =>
    {
        return rowKey == null
        ? req.CreateResponse(HttpStatusCode.BadRequest,"Parameter 'id' must be specified!" ,"application/json")
        : req.CreateResponse(HttpStatusCode.OK);
        
    });
}

public class Item : TableEntity
{
    public string Name { get; set; }
    public string Epitaph { get; set; }
}
