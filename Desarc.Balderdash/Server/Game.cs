using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Desarc.Balderdash.Server
{
    public class Game
    {
        private readonly List<Question> m_questions;

        public Game()
        {
            m_questions = LoadQuestions();
        }

        private List<Question> LoadQuestions()
        {

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Desarc.Balderdash.Server.Resources.QuestionsEnglish.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Question>>(json, new JsonSerializerSettings());
            }
        }
    }
}