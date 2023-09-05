using System.Xml.Linq;

namespace WebApp.Models
{
    public class Firebase
    {
        public class AboutOld
        {
            public Desc? Desc { get; set; }
            public List<Role>? Role { get; set; }
            public List<Skill>? Skill { get; set; }
            public List<SkillLogo>? SkillLogo { get; set; }
        }
        public class About
        {
            public Desc? Desc { get; set; }
            public List<Socialmedia>? Sosmed { get; set; }
            public List<Role>? Role { get; set; }
            public List<Skill>? Skill { get; set; }
            public List<Project>? Project { get; set; }
        }
        public class Socialmedia
        {
            public string? Id { get; set; } // firebase unique id
            public string? name { get; set; }
            public string? url { get; set; }
            public string? detail { get; set; }
        }
        public class Desc
        {
            public string? Id { get; set; } // firebase unique id
            public string? name { get; set; }
            public string? moto { get; set; }
            public string? detail { get; set; }
        }
        public class Role
        {
            public string? Id { get; set; } // firebase unique id
            public string? name { get; set; }
        }
        public class Skill
        {
            public string? Id { get; set; } // firebase unique id
            public string? name { get; set; }
            public string? type { get; set; }
        }

        public class SkillLogo
        {
            public string? Id { get; set; } // firebase unique id
            public int? nomor { get; set; }
            public string? name { get; set; }
            public string? src { get; set; }
            public string? warna { get; set; }
            public string? part { get; set; }
        }
        public class Log
        {
            public string? Id { get; set; } // firebase unique id
            public string? email { get; set; }
            public string? token { get; set; }
            public string? page { get; set; }
            public string? info { get; set; }
            public string? jam { get; set; }
        }
        public class Project
        {
            public string? Id { get; set; } // firebase unique id
            public string? Name { get; set; }
            public string? Duration { get; set; }
            public string? Type { get; set; }
            public string? Detail { get; set; }
            public List<string>? FrontEnd { get; set; }
            public List<string>? BackEnd { get; set; }
            public List<string>? Database { get; set; }
            public List<string>? Programming { get; set; }
            public List<Demo>? Demo { get; set; }

        }
        public class NewProject
        {
            public string? Id { get; set; } // firebase unique id
            public string? Role { get; set; }
            public string? Name { get; set; }
            public string? SimpleName { get; set; }
            public string? Duration { get; set; }
            public string? Type { get; set; }
            public string? Detail { get; set; }
            public string? Achievement { get; set; }
            public string? TechStack { get; set; }

            public List<Demo>? Demo { get; set; }
        }

        public class Demo
        {
            public string? ProjekName { get; set; }
            public string? Name { get; set; }
            public string? Type { get; set; }
            public string? link { get; set; }
        }
    }
}
