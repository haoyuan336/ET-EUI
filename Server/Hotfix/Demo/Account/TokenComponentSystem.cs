namespace ET
{
    public static class TokenComponentSystem
    {
        public static void Add(this TokenComponent self, long key, string token)
        {
            self.TokenDictionary.Add(key, token);
            self.TimeoutRemoveKey(key, token).Coroutine();
        }

        public static string Get(this TokenComponent self, long key)
        {
            string token;

            self.TokenDictionary.TryGetValue(key, out token);
            return token;
        }

        public static void Remove(this TokenComponent self, long key)
        {
            if (self.TokenDictionary.ContainsKey(key))
            {
                self.TokenDictionary.Remove(key);
            }
        }

        private static async ETTask TimeoutRemoveKey(this TokenComponent self, long key, string token)
        {
            await TimerComponent.Instance.WaitAsync(60000);
            string onlineToken = self.Get(key);
            if (!string.IsNullOrEmpty(onlineToken) && onlineToken == token)
            {
                self.Remove(key);
            }
        }
    }
}