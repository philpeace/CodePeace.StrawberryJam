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
	
###JavaScript

To add a .js file to a bundle is pretty much the same as a .css file:

    <!-- This will add the jquery library .cjs file to a JavaScript bundle called 'head' -->
    @Html.AddJavaScript(Url.Content("~/scripts/jquery-1.7.1.js"), "head")
	
To add inline JavaScript to a page:

    <!--
	We use templated Razor views to allow is to write inline script.
    In this case we are adding the inline script to the 'head' bundle like before
	-->
    @Html.AddScriptSource(@<text>
		alert('ALL YOUR INLINE SCRIPTS ARE BELONG TO US');
	</text>, "test", area: "head")
	
To write the bundle to the page:

    <!-- This will output the JavaScript bundle called 'head' to the page -->
    @Html.WriteScripts("head")
	
###Settings

By default nothing is minified or bundled, we write conventional `<script>` and `<link>` elements to the page. To actually bundle and minify the output you need to set 2 appsettings in the `web.config`:

    <appSettings>
	    <add key="SJ.Concatenate" value="true" />
        <add key="SJ.Compress" value="true" />
    </appSettings>

The settings are pretty self explanatory, you can choose to just bundle everything up (`SJ.Concatenate = true`) or bundle *AND* minify (`SJ.Compress = true && SJ.Concatenate = true`)
