﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DirectSaleNet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddHttpContextAccessor();//通过这个中间件才能得到httpContext
            services.AddSession();//要在服务中也添加session
            services.AddDbContext<Models.DirectSaleContext>();//这些服务都可以在控制器中调用
            services.AddTransient<DirectSaleNet.Services.OptionList>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<DirectSaleNet.Authorize.LoginActionFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();//发生异常的时候给用户展示完整的异常信息
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");  //不是路径 而是操作 ：控制器/action/id(id可以省略)
                app.UseHsts();
            }
            //app.Use... --> 依赖注入 DI  
            app.UseHttpsRedirection();
            app.UseStaticFiles();  //很重要  wwwroot里面的都是静态文件
            app.UseCookiePolicy();
            app.UseSession();//启用session中间件注入
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
