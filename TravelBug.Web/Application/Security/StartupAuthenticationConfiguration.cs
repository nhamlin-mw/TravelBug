#region header

// Copyright (c) 2018
// Author:         Nicholas Hamlin
// Created Date:  07/10/2018
// Filename: TravelBug:TravelBug.Web:AuthenticationConfiguration.cs
// Usage:

#endregion header

using System;
using EPiServer.Cms.UI.AspNetIdentity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using TravelBug.Application.Security;
using TravelBug.Core.Utility.Nansen.Encore;

[assembly:OwinStartup(typeof(StartupAuthenticationConfiguration))]

namespace TravelBug.Application.Security
{
	public class StartupAuthenticationConfiguration
	{
		public void Configuration(IAppBuilder app)
		{
			// Add CMS integration for ASP.NET Identify
			app.AddCmsAspNetIdentity<ApplicationUser>();
			// Use cookie authentication
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString(ConfigurationUtility.GetAppSetting("LoginUrl", "/Util/login.aspx").Trim('~')),
				Provider = new CookieAuthenticationProvider
				{
					OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager<ApplicationUser>, ApplicationUser>(
						validateInterval: TimeSpan.FromMinutes(30), regenerateIdentity: (manager, user) => manager.GenerateUserIdentityAsync(user))
				}
			});
		}
	}
}