using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaneSoftWebsiteBot.Models
{
    public class TopScoringIntent
    {
        public string Intent { get; set; }
        public double Score { get; set; }
    }

    public class Intent
    {
        public string intent { get; set; }
        public double Score { get; set; }
    }

    public class Entity
    {
        public string entity { get; set; }
        public string Type { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public double Score { get; set; }
    }

    public class LuisResponseMessage
    {
        public string Query { get; set; }
        public TopScoringIntent TopScoringIntent { get; set; }
        public List<Intent> Intents { get; set; }
        public List<Entity> Entities { get; set; }
    }
}