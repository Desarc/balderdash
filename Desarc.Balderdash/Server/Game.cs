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
        private readonly List<Question> m_unusedQuestions;
        private readonly List<Question> m_usedQuestions;

        private readonly Random random = new Random();

        public Game()
        {
            m_unusedQuestions = LoadQuestions();
            m_usedQuestions = new List<Question>();
        }

        public Question GetRandomQuestion()
        {
            if (m_unusedQuestions.Count == 0)
            {
                return null;
            }

            var index = random.Next(m_unusedQuestions.Count);
            var question = m_unusedQuestions.ElementAt(index);
            m_unusedQuestions.RemoveAt(index);
            m_usedQuestions.Add(question);
            return question;
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