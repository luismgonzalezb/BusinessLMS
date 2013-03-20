using Syncfusion.Mvc.Diagram;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{
	[Authorize]
	public class DashboardController : BaseWebController
	{
		public ActionResult Index()
		{
			List<Company> model = new CompanyDataContext().Company;
			return View(model);
		}
	}

	public class Company
	{
		public string DeptId { get; set; }
		public string DeptName { get; set; }
		public string HeadDept { get; set; }
		public Shapes Shape { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
	}

	public class CompanyDataContext
	{
		public List<Company> Company
		{
			get
			{
				List<Company> company = new List<Company>();
				company.Add(new Company() { DeptId = "Client", DeptName = "Client", HeadDept = "0", Height = 50, Width = 150, Shape = Shapes.RoundedRectangle });
				company.Add(new Company() { DeptId = "Company", DeptName = "Company", HeadDept = "Client", Height = 50, Width = 150, Shape = Shapes.RoundedRectangle });
				company.Add(new Company() { DeptId = "Plant", DeptName = "Plant", HeadDept = "Company", Height = 50, Width = 150, Shape = Shapes.RoundedRectangle });
				company.Add(new Company() { DeptId = "Sales", DeptName = "Sales Organisation", HeadDept = "Company", Height = 50, Width = 150, Shape = Shapes.RoundedRectangle });
				company.Add(new Company() { DeptId = "Purchase", DeptName = "Purchase Organisation", HeadDept = "Company", Height = 50, Width = 150, Shape = Shapes.RoundedRectangle });
				company.Add(new Company() { DeptId = "Shipping", DeptName = "Shipping Point", HeadDept = "Plant", Height = 50, Width = 150, Shape = Shapes.RoundedRectangle });
				company.Add(new Company() { DeptId = "WholeSale", DeptName = "WholeSale Distribution", HeadDept = "Sales", Height = 50, Width = 150, Shape = Shapes.RoundedRectangle });
				company.Add(new Company() { DeptId = "Internet", DeptName = "Internet Distribution", HeadDept = "Sales", Height = 50, Width = 150, Shape = Shapes.RoundedRectangle });
				company.Add(new Company() { DeptId = "Division1", DeptName = "Division", HeadDept = "WholeSale", Height = 50, Width = 150, Shape = Shapes.RoundedRectangle });
				company.Add(new Company() { DeptId = "Division2", DeptName = "Division", HeadDept = "WholeSale", Height = 50, Width = 150, Shape = Shapes.RoundedRectangle });
				company.Add(new Company() { DeptId = "Division3", DeptName = "Division", HeadDept = "Internet", Height = 50, Width = 150, Shape = Shapes.RoundedRectangle });
				company.Add(new Company() { DeptId = "Division4", DeptName = "Division", HeadDept = "Internet", Height = 50, Width = 150, Shape = Shapes.RoundedRectangle });
				return company;
			}
		}
	}
}