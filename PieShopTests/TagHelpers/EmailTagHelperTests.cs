using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using PieShop.TagHelpers;

namespace PieShopTests.TagHelpers;

public class EmailTagHelperTests
{
    [Fact]
    public void Generates_Email_Link()
    {
        // arrange
        EmailTagHelper emailTagHelper = new EmailTagHelper
        {
            Address = "test@pieshop.com", Content = "Email"
        };

        var tagHelperContext =
            new TagHelperContext(
                new TagHelperAttributeList(), 
                new Dictionary<object, object>(), 
                string.Empty);

        var content = new Mock<TagHelperContent>();
        var tagHelperOutput = new TagHelperOutput(
            "a", new TagHelperAttributeList(),
            (cache, encoder) => Task.FromResult(content.Object));
        
        // act 
        emailTagHelper.Process(tagHelperContext, tagHelperOutput);
        
        // assert
        Assert.Equal("Email", tagHelperOutput.Content.GetContent());
        Assert.Equal("a", tagHelperOutput.TagName);
        var tagHelperAttribute = tagHelperOutput.Attributes[0];
        Assert.Equal("mailto:test@pieshop.com", tagHelperOutput.Attributes[0].Value);
    }
}