using System.Web.Mvc;

namespace FeatureTest
{
	public class FeatureRazorViewEngine : RazorViewEngine
	{
		private const string ViewsPathTemplate = "~/Features/%1/Views/{0}.cshtml";
		private const string LayoutPathTemplate = "~/Features/Layout/{0}.cshtml";

		public FeatureRazorViewEngine() 
		{
			MasterLocationFormats = new[]
				{
					LayoutPathTemplate
				};

			ViewLocationFormats = new[]
			{
				ViewsPathTemplate
			};

			PartialViewLocationFormats = new[]
			{
				ViewsPathTemplate
			};
		}

		protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
		{
			return base.CreatePartialView(controllerContext, FeaturePath(controllerContext, partialPath));
		}

		protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
		{
			return base.CreateView(controllerContext, 
				FeaturePath(controllerContext, viewPath), FeaturePath(controllerContext, masterPath));
		}

		protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
		{
			return base.FileExists(controllerContext, FeaturePath(controllerContext, virtualPath));
		}

		private string FeatureName(ControllerContext controllerContext)
		{
			var namespaceName = controllerContext.Controller.GetType().Namespace;
			var lastDotIndex = namespaceName.LastIndexOf('.');
			var shortName = namespaceName.Substring(lastDotIndex + 1);
			return shortName;
		}

		private string FeaturePath(ControllerContext controllerContext, string virtualPath)
		{
			var path = virtualPath.Replace("%1", FeatureName(controllerContext));
			return path;
		}
	}
}