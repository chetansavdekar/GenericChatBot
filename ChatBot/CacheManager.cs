using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TogetherChatBot
{
    public class CacheManager
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect("chatbotcache.redis.cache.windows.net:6379,password=OIBwqlyi0d/hieixkBfZITpqnY4x2CbFMEjXh1EBWN0=,ssl=false,abortConnect=False");
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}