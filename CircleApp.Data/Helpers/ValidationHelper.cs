using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleApp.Data.Helpers
{
    public class ValidationHelper
    {
        public static void RemoveUserFromModelStateKey(ModelStateDictionary ModelState)
        {
            foreach (var key in ModelState.Keys.Where(k => k.StartsWith("User.")).ToList())
            {
                ModelState.Remove(key);
            }
        }


    }
}
