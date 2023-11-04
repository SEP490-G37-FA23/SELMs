global using SELMs.Api.DTOs;
global using Xunit;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;

public static class ApiControllerSetup
{
	public static void SetupController(ApiController apiController)
	{
		var routeData = new HttpRouteData(new HttpRoute());
		apiController.Configuration = new HttpConfiguration();
		apiController.Request = new HttpRequestMessage();
		apiController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = apiController.Configuration;
		apiController.ControllerContext = new HttpControllerContext(apiController.Configuration, routeData, apiController.Request)
		{
			Controller = apiController
		};
	}
}