namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        private static Uri[] uris = new Uri[]
        {
            new Uri("https://open.feishu.cn/open-apis/bot/v2/hook/e5a04a8a-7b6a-49cd-85f5-d3ad10837e69"),
            
        };

        private static string[] foods = new[]
        {
            "麦当劳", "老盛昌", "热干面", "兰州拉面", "三百块", "日料", "小青龙", "便利蜂", "小杨生煎", "烧腊", "面十八", "新三百块"
        };
        private static HttpClient client = new HttpClient();
        private static Random random = new Random((int)DateTime.Now.Ticks);
        private static bool upload = false;
        static async Task Main(string[] args)
        {
            Console.WriteLine("琳噶酱机器人启动！");
            try
            {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromMinutes(1));
                    Console.WriteLine(DateTime.Now.Hour);
                    if (DateTime.Now.Hour == 12)
                    {
                        if (upload)
                            UploadFeiShu("中午");
                    }
                    else if (DateTime.Now.Hour == 18)
                    {
                        if (upload)
                            UploadFeiShu("晚上");
                    }
                    else
                        upload = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("琳噶酱机器人关闭！");
                throw;
            }
        }

        static void UploadFeiShu(string time)
        {
            var foodIndex = random.Next(0, foods.Length);
            string context = $"琳噶酱今天{time}想吃：{foods[foodIndex]}!";
            HttpContent content = new StringContent($"{{\"msg_type\":\"text\",\"content\":{{\"text\":\"{context}\"}}}}");
            for (var i = 0; i < uris.Length; i++)
            {
                client.PostAsync(uris[i],content);
            }
            upload = false;
            Console.WriteLine(context);
        }
    }
}