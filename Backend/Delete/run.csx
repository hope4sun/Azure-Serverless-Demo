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

  try {
    var entity = new DynamicTableEntity("Relic", rowKey);
    entity.ETag = "*";
    tableBinding.Execute(TableOperation.Delete(entity));
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
