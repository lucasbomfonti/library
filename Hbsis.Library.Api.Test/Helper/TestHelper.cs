using AutoMapper;
using Hbsis.Library.Application.Mapper;
using Hbsis.Library.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace Hbsis.Library.Api.Test.Helper
{
    public static class TestHelper
    {

        public static IMapper Mapper => GetMapper();

        private static IMapper GetMapper()
        {
            var map1 = new MapperViewModel2Domain();
            var map2 = new Domain2Dto();

            var config = new MapperConfiguration(c =>
            {
                c.AddProfile(map1);
                c.AddProfile(map2);
            });
            var configuration = config;
            return new Mapper(configuration);
        }

        public static T ConvertFromActionResult<T>(ActionResult actionResult) where T : class
        {
            var text = JsonConvert.SerializeObject(actionResult);
            try
            {
                var converted = JsonConvert.DeserializeObject<ResponseDto>(text);
                var parssed = JsonConvert.DeserializeObject<T>(converted.Value.Data.ToString());
                return parssed;
            }
            catch (Exception)
            {
                var converted = JsonConvert.DeserializeObject<ResponseTypeDto<T>>(text);
                return converted.Value;
            }
        }

        public static void PrepareDatabase<T>() where T : DataContext, new()
        {
            var context = new T();
            context.UpdateDatabase();
        }
    }

    public class ResponseTypeDto<T> where T : class
    {
        public T Value { get; set; }
    }

    public class ResponseDto
    {
        public ResponseData Value { get; set; }
    }

    public class ResponseData
    {
        public object Data { get; set; }
    }
}
