using Microsoft.AspNet.Identity;
using System;
using System.Web;
using System.Web.Caching;

namespace mortuary.App_Start
{
    public class AutomaticBillsConfig
    {
        public static void RegisterAutomaticBilling(int seconds)
        {
            string key = "automaticBillingCron";
            var OnCacheRemove = new CacheItemRemovedCallback(CacheItemRemoved);
            HttpRuntime.Cache.Insert(key, seconds, null, DateTime.Now.AddSeconds(seconds), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, OnCacheRemove);
        }

        private static void CacheItemRemoved(string key, object value, CacheItemRemovedReason reason)
        {
            EmailService emailService = new EmailService();
            IdentityMessage message = new IdentityMessage { Destination = "alvaroscelza@gmail.com", Subject = "Pruebas Cron Billing", Body = "Success!" };
            emailService.SendAsync(message);
            RegisterAutomaticBilling(Convert.ToInt32(value));
        }
    }
}