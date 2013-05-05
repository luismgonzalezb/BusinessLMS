using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMS.Models
{
	public partial class Step
	{
		[Display(Name = "Step ID")]
		public int stepId { get; set; }

		[Display(Name = "Parent Step Id")]
		public Nullable<int> parentStepId { get; set; }

		[Display(Name = "Step Order")]
		[DataType(DataType.Text)]
		public int stepOrder { get; set; }

		[Display(Name = "IconClass")]
		[DataType(DataType.Text)]
		public string iconClass { get; set; }

		[Display(Name = "Action")]
		[DataType(DataType.Text)]
		public string action { get; set; }

		[Display(Name = "Controller")]
		public string controller { get; set; }
	}
}
