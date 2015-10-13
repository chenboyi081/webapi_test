using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Collections.Specialized;

namespace 命名空间System.Web.Http__2015_10_13
{
    public class ApiControllerBase:System.Web.Http.ApiController
    {

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            string postContentData = string.Empty;

            if (controllerContext.Request.Method != System.Net.Http.HttpMethod.Get)     //非get请求
            {
                NameValueCollection postParam = null;

                if (controllerContext.Request.Content.IsFormData())     //IsFormData()是来自System.Net.Http的扩展方法，【处理】content-type:application/x-www-form-urlencoded
                {
                    postParam = controllerContext.Request.Content.ReadAsFormDataAsync().Result;
                    postContentData = postParam.ToString();

                }
                else
                {
                    if (!controllerContext.Request.Content.IsMimeMultipartContent("form-data"))     //【处理】 content-type:application/json
                    {
                        //获取请求的所有参数值
                        postContentData = controllerContext.Request.Content.ReadAsStringAsync().Result;
                    }
                    else            //【处理】 multipart/form-data
                    {
                        var collPostParamData = controllerContext.Request.RequestUri.ParseQueryString();

                    }
                }
            }
            else
            { 
                
            }
        }
    }
}