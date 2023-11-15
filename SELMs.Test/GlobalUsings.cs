global using SELMs.Api.Controllers;
global using SELMs.Api.DTOs;
global using SELMs.Models;
global using SELMs.Models.BusinessModel;
global using SELMs.Services;
global using SELMs.Services.Implements;
global using Xunit;
global using Xunit.Abstractions;
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