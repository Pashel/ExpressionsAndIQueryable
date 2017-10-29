using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample03.E3SClient
{
	public class FTSRequestGenerator
	{
		private readonly UriTemplate FTSSearchTemplate = new UriTemplate(@"data/searchFts?metaType={metaType}&query={query}&fields={fields}");
		private readonly Uri BaseAddress;

		public FTSRequestGenerator(string baseAddres) : this(new Uri(baseAddres))
		{
		}

		public FTSRequestGenerator(Uri baseAddress)
		{
			BaseAddress = baseAddress;
		}

		public Uri GenerateRequestUrl<T>(List<string> queryList, int start = 0, int limit = 10)
		{
			return GenerateRequestUrl(typeof(T), queryList, start, limit);
		}

		public Uri GenerateRequestUrl(Type type, List<string> queryList, int start = 0, int limit = 10)
		{
			string metaTypeName = GetMetaTypeName(type);

			var ftsQueryRequest = new FTSQueryRequest
			{
				Statements = new List<Statement>() {new Statement {Query = "*"}},
				Start = start,
				Limit = limit
			};

		    foreach (var query in queryList)
		    {
                ftsQueryRequest.Statements.Add(new Statement { Query = query });
            }

			var ftsQueryRequestString = JsonConvert.SerializeObject(ftsQueryRequest);

			var uri = FTSSearchTemplate.BindByName(BaseAddress,
				new Dictionary<string, string>()
				{
					{ "metaType", metaTypeName },
					{ "query", ftsQueryRequestString }
				});

			return uri;
		}

	    //private string GenerateQuery(List<string> queryList)
	    //{
	    //    if (queryList.Count == 0)
	    //    {
	    //        return "{\"query\":\"*\"}";
	    //    }

	    //    string result = "";
	    //    foreach (var query in queryList)
	    //    {
	    //        result += 
	    //    }
	    //}

		private string GetMetaTypeName(Type type)
		{
			var attributes = type.GetCustomAttributes(typeof(E3SMetaTypeAttribute), false);

			if (attributes.Length == 0)
				throw new Exception(string.Format("Entity {0} do not have attribute E3SMetaType", type.FullName));

			return ((E3SMetaTypeAttribute)attributes[0]).Name;
		}

	}
}
