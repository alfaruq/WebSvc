using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using WebSvc.DTOs;
using WebSvc.Providers;

namespace WebSvc.Services
{
    /// <summary>
    /// This is a sample web service with scripting enabled. Basically this is to allow parameter passing via json format instead of soap.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PeopleServices : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string InsertPeople(string FullName, int Age)
        {
            var objPeople = new People();
            objPeople.FullName = FullName;
            objPeople.Age = Age;
            objPeople.CreatedDate = DateTime.Now;
            objPeople.ModifiedDate = DateTime.Now;

            PeopleProvider.InsertPeople(objPeople);

            return JsonConvert.SerializeObject(objPeople);
        }
    }
}
