using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using MvcSample.Controllers;
using NSubstitute;
using Xunit;

namespace MvcSampleTests
{
  public class HomeControllerTests
  {
    private HomeController _sut;
    private HttpContextBase _subHttpContext;
   

    public HomeControllerTests()
    {
      _sut = new HomeController();
      _subHttpContext = Substitute.For<HttpContextBase>();
      _sut.ControllerContext = new ControllerContext(_subHttpContext, new RouteData(), _sut);
    }

    [Fact]
    public void Index_Called_ReturnsView()
    {
      var actionResult = _sut.Index();

      actionResult.Should().BeOfType<ViewResult>();
    }

    [Fact]
    public void Index_Called_IncrementsSessionViewCount()
    {
      _sut.Index();

      var x = _subHttpContext.Received().Session["ViewCount"];
    }

    [Fact]
    public void Index_ViewCountNull_SetsToOne()
    {
      _subHttpContext.Session["ViewCount"].Returns(null);

      _sut.Index();

      _subHttpContext.Session.Received()["ViewCount"] = 1;
    }

    [Fact]
    public void Index_HasValue_Increments()
    {
      _subHttpContext.Session["ViewCount"].Returns(1);

      _sut.Index();

      _subHttpContext.Session.Received()["ViewCount"] = 2;
    }

    [Fact]
    public void Index_Count3_Redirect()
    {
      _subHttpContext.Session["ViewCount"].Returns(3);

      var result = _sut.Index() as RedirectResult;

      result.Url.ShouldAllBeEquivalentTo("/home/about");
    }
  }
}
