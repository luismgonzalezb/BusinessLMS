using System;

namespace BusinessLMS.Models
{
	public partial class Step
	{
		public int stepId { get; set; }
		public Nullable<int> parentStepId { get; set; }
		public int stepOrder { get; set; }
		public string iconClass { get; set; }
		public string action { get; set; }
		public string controller { get; set; }
	}
}
