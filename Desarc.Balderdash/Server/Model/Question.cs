using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desarc.Balderdash.Server
{
    public class Question
    {
        private readonly List<string> m_fakeAnswers;
        private List<string> m_alreadySelectedFakeAnswers = new List<string>();

        private readonly Random random = new Random();

        public Question(string questionText, string correctAnswer, string fakeAnswers)
        {
            Id = new Guid();
            QuestionText = questionText;
            CorrectAnswer = correctAnswer;
            if (fakeAnswers != null && fakeAnswers.Length > 0)
            {
                m_fakeAnswers = fakeAnswers.Split(',').ToList();
            }
        }

        public Guid Id { get; private set; }

        public string QuestionText { get; private set; }

        public string CorrectAnswer { get; private set; }

        public string GetRandomFakeAnswer()
        {
            var index = random.Next(0, m_fakeAnswers.Count);
            var fakeAnswer = m_fakeAnswers.ElementAt(index);
            m_fakeAnswers.RemoveAt(index);
            m_alreadySelectedFakeAnswers.Add(fakeAnswer);
            return fakeAnswer;
        }

        public List<string> GetManyRandomFakeAnswers(int number)
        {
            var fakeAnswerList = new List<string>();
            for (int i = 0; i < number; i++)
            {
                fakeAnswerList.Add(GetRandomFakeAnswer());
            }

            return fakeAnswerList;
        }

        public void Reset()
        {
            m_fakeAnswers.AddRange(m_alreadySelectedFakeAnswers);
            m_alreadySelectedFakeAnswers = new List<string>();
        }
    }
}