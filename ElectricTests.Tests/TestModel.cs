using ElectricTests.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTests.Tests
{
    class TestModel
    {
        public Question question1 = new Question
        {
            Id = 1,
            AnswerDescription = "Description for answer",
            Option1 = "Correct option",
            Option2 = "Second option",
            Option3 = "Another option",
            Option4 = "Last option",
            Title = "First question",
        };

        public Question question2 = new Question
        {
            Id = 2,
            Option1 = "2 Correct option",
            Option2 = "2 Second option",
            Option3 = "2 Another option",
            Option4 = "2 Last option",
            Title = "Second question",
        };

        public Question question3 = new Question
        {
            Id = 3,
            Option1 = "3 Correct option",
            Option2 = "3 Second option",
            Option3 = "3 Another option",
            Option4 = "3 Last option",
            Title = "Third question",
        };
        
        public Question question4 = new Question
        {
            Id = 4,
            AnswerDescription = "4 Description for answer",
            Option1 = "4 Correct option",
            Option2 = "4 Second option",
            Option3 = "4 Another option",
            Option4 = "4 Last option",
            Title = "Four question",
        };

        public Question question5 = new Question
        {
            Id = 5,
            AnswerDescription = "5 Description for answer",
            Option1 = "5 Correct option",
            Option2 = "5 Second option",
            Option3 = "5 Another option",
            Option4 = "5 Last option",
            Title = "Five question",
        };

        public Test test = new Test
        {
            Id = 1,
            Title = "Test Nr. 1",
            AllQuestionsNumber = 300,
            IsAvaible = true,
            OneTestQuestionsNumber = 20
        };
    }
}
