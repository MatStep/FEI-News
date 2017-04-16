using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEI_News.Model
{
    public class Post
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("date")]
        public String Date { get; set; }
        [JsonProperty("title")]
        public TitleObject Title { get; set; }
        [JsonProperty("content")]
        public ContentObject Content { get; set; }

        public String TitleString { get; set; }

        public String ContentString { get; set; }

        public String ImgUrl { get; set; }
    }

    public class ContentObject
    {
        [JsonProperty("rendered")]
        public String ContentText { get; set; }
    }

    public class TitleObject
    {
        [JsonProperty("rendered")]
        public String TitleText { get; set; }
    }
}
