StrawberryJam
=============
StrawberryJam is a JavaScript and CSS minimisation and bundling library for ASP.NET MVC.

Why StrawberryJam?
------------------

Minifying CSS and JavaScript is much like making Jam. You take strawberries (the code) and mash it together (bundling it) and then boil it down so it's really just the pure essence of fruit (minify it).

What makes this bundling-minifier different from others is that it will not just bundle external scripts and stylesheets, but also inline ones too. This is especially useful if you need to generate script on the fly within your views, becuase now that code will get bundle-minified too.

Show me the code
----------------

All code that you need to write is done in the Views.

###Stylesheets

To add a stylesheet to a bundle:

    <!-- This will add the site.css file to a stylesheet bundle called 'head' -->
    @Html.AddCss(Url.Content("~/Content/Site.css"), "head")
	
To write the bundle to the page:

    <!-- This will output the stylesheet bundle called 'head' to the page -->
    @Html.WriteStyles("head")


