using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FeatureTest.Features.Home;

namespace FeatureTest
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			ConfigureWindsor();
			ConfigureViews();
		}

		private void ConfigureViews()
		{
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new FeatureRazorViewEngine());
		}








		private static void ConfigureWindsor()
		{
			var container = new WindsorContainer();
			container.Register(Classes.FromAssemblyContaining<HomeController>()
				.BasedOn<IController>()
				.LifestyleTransient());

			ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container.Kernel));
		}
	}
}