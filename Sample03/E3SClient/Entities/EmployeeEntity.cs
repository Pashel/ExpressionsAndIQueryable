using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample03.E3SClient.Entities
{
	public class Skills
	{
		[JsonProperty]
		public string nativespeaker { get; set; }

		[JsonProperty]
		public string expert { get; set; }

		[JsonProperty]
		public string advanced { get; set; }

		[JsonProperty]
		public string intermediate { get; set; }

		[JsonProperty]
		public string novice { get; set; }

		[JsonProperty]
		public string position { get; set; }

		[JsonProperty]
		public string os { get; set; }

		[JsonProperty]
		public string db { get; set; }

		[JsonProperty]
		public string platform { get; set; }

		[JsonProperty]
		public string industry { get; set; }

		[JsonProperty]
		public string proglang { get; set; }

		[JsonProperty]
		public string language { get; set; }

		[JsonProperty]
		public string other { get; set; }

		[JsonProperty]
		public string primary { get; set; }

		[JsonProperty]
		public string category { get; set; }
	}

	public class WorkHistory
	{
		[JsonProperty]
		public string terms { get; set; }

		[JsonProperty]
		public string company { get; set; }

		[JsonProperty]
		public string companyurl { get; set; }

		[JsonProperty]
		public string position { get; set; }

		[JsonProperty]
		public string role { get; set; }

		[JsonProperty]
		public string project { get; set; }

		[JsonProperty]
		public string participation { get; set; }

		[JsonProperty]
		public string team { get; set; }

		[JsonProperty]
		public string database { get; set; }

		[JsonProperty]
		public string tools { get; set; }

		[JsonProperty]
		public string technologies { get; set; }

		[JsonProperty]
		public string startdate { get; set; }

		[JsonProperty]
		public string enddate { get; set; }

		[JsonProperty]
		public bool isepam { get; set; }

		[JsonProperty]
		public string epamproject { get; set; }
	}

	public class Recognition
	{
		[JsonProperty]
		public string nomination { get; set; }

		[JsonProperty]
		public string description { get; set; }

		[JsonProperty]
		public string recognitiondate { get; set; }

		[JsonProperty]
		public string points { get; set; }


	}

    [E3SMetaType("meta:people-suite:people-api:com.epam.e3s.app.people.api.data.pluggable.EmployeeEntity")]
	public class EmployeeEntity : E3SEntity
	{
		[JsonProperty]
		List<string> phone;

		[JsonProperty]
		Skills skill { get; set; }

		[JsonProperty]
		List<string> fullname { get; set; }

		[JsonProperty]
		List<string> country { get; set; }

		[JsonProperty]
		List<string> city { get; set; }

		[JsonProperty]
		List<string> email { get; set; }

		[JsonProperty]
		List<string> skype { get; set; }

		[JsonProperty]
		public string startworkdate { get; set; }

		[JsonProperty]
		public string unit { get; set; }

		[JsonProperty]
		public string office { get; set; }

		[JsonProperty]
		public string birthday { get; set; }

		[JsonProperty]
		public string workstation { get; set; }

		[JsonProperty]
		public string nativename { get; set; }
	}
}
