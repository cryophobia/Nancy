namespace Nancy.Demo
{
    using Nancy.Demo.Models;
    using Nancy.Routing;


    public class MainModule : NancyModule
    {
        public MainModule(IRouteCacheProvider routeCacheProvider)
        {
//		    Issues with routeCacheProvider
//			Need to sort this out
//            Get["/"] = x => {
//                return View["routes.cshtml", routeCacheProvider.GetCache()];
//            };
			
//		    Issues with routeCacheProvider
//			Need to sort this out
//			Get["/"] = x => {
//				var model = routeCacheProvider.GetCache();
//                return View["~/views/routes.spark", model];
//            };

            // TODO - implement filtering at the RouteDictionary GetRoute level
            Get["/filtered", r => true] = x => {
                return "This is a route with a filter that always returns true.";
            };

            Get["/filtered", r => false] = x => {
                return "This is also a route, but filtered out so should never be hit.";
            };
			
			Get["/redirect"] = x => {
				return Response.AsRedirect("http://www.google.com"); 
			};
			
            Get["/test"] = x => {
                return "Test";
            };

            Get["/static"] = x => {
                return View["~/views/static.htm"];
            };
			
            Get["/razor"] = x => {
                var model = new RatPack { FirstName = "Frank" };
                return View["~/views/razor.cshtml", model];
            };
			
			//Due to Monodevelop 2.6 Add-in structure changes this needs additional config.
			//Removed for now
//            Get["/ndjango"] = x => {
//                var model = new RatPack { FirstName = "Michael" };
//                return View.Django("~/views/ndjango.django", model);
//            };

            Get["/spark"] = x => {
                var model = new RatPack { FirstName = "Bright" };
                return View["~/views/spark.spark", model];
            };

            Get["/json"] = x => {
                var model = new RatPack { FirstName = "Andy" };
                return Response.AsJson(model);
            };

            Get["/xml"] = x => {
                var model = new RatPack { FirstName = "Andy" };
                return Response.AsXml(model);
            };
			
			//Call the following url to test
			//http://127.0.0.1:8080/access?oauth_token=11111111111111&oauth_verifier=2222222222222222
			//Dynamic cast is for Mono 2.8 only - Fixed in Mono 2.10 Preview
			Get["/access"] = x => {
				try{
					return "Success: " + Request.Query.oauth_token + "; " + Request.Query.oauth_verifier;
				}
				catch {
					return "Call as: /access?oauth_token=11111111111111&oauth_verifier=2222222222222222";
				}
			};
        }
    }
}