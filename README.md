CoreWeb
=======
[![Build status](https://ci.appveyor.com/api/projects/status/an3ae5b27guf47iu)](https://ci.appveyor.com/project/benmccallum/coreweb)

A library of core helpers, extensions, constants, enums and other useful things for .NET Web projects.

## Get it on NuGet!

    Install-Package CoreWeb
    
## Additional setup

### Static file cache busting

I like to use a @madskristensen inspired cache-busting technique for my static assets. See his original post [here](http://madskristensen.net/post/cache-busting-in-aspnet). Mine has a slight variation though so relative URLs in CSS still work. The corresponding URL Rewrite rule to my Fingerprint helper (that resolves the versioned path to the original, physical file path) needs to be added to your web.config, like so:

    <rule name="Resolve Fingerprinted URL" stopProcessing="true">
      <match url="([\S]+)(-ver-[0-9]+)([\S]+)" />
      <action type="Rewrite" url="{R:1}{R:3}" />
    </rule>
