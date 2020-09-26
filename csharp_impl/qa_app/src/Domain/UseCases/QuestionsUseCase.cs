using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Domain.UseCases
{
    public class QuestionsUseCase
    {
        private IQuestionRepository QuestionRepository;
        private IAnswerRepository AnswerRepository;

        public QuestionsUseCase(IQuestionRepository questionRepository, IAnswerRepository answerRepository)
        {
            this.QuestionRepository = questionRepository;
            this.AnswerRepository = answerRepository;
        }

        public Task<Question> Create(Question question)
        {
            question.Validate();
            return this.QuestionRepository.Create(question);
        }

        public Task<List<Question>> List()
        {
            return this.QuestionRepository.Get();
        }

        public Task<Question?> View(long questionId)
        {
            return this.QuestionRepository.FindComplete(questionId);
        }

        public async Task WriteAnswer(long questionId, Answer answer)
        {
            var question = await this.QuestionRepository.Find(questionId);
            if (question == null) {
                throw new Exception("Question not found.");
            }

            await this.AnswerRepository.Create(questionId, answer);
        }

        public async Task PickCorrectAnswer(User user, long questionId, long answerId)
        {
            var question = await this.QuestionRepository.FindComplete(questionId);
            if (question == null)
                throw new Exception("Question not found");

            if (!question.CreatedBy.Equals(user))
                throw new Exception("Invalid user.");

            var answer = question.Answers.Find((answer) => answer.AnswerId == answerId);
            if (answer == null) {
                
            }
        }
    }
}