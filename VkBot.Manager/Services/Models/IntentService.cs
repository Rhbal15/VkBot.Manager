using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VkBot.Manager.Data;
using VkBot.Manager.Exceptions;

namespace VkBot.Manager.Services.Models
{
    public class IntentService : IIntentService
    {
        private readonly ApplicationDbContext _context;

        public IntentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Intent Get(int id)
        {
            return GetAll()
                .FirstOrDefault(p => p.Id == id);
        }

        public Intent GetByName(string intentName)
        {
            return GetAll().FirstOrDefault(p => p.Name == intentName);
        }

        public IEnumerable<Intent> Get(IEnumerable<int> ids)
        {
            return GetAll()
                .Where(p => ids.Contains(p.Id));
        }

        public IntentSentence GetSentence(int id)
        {
            return GetAllSentences()
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<IntentSentence> GetSentences(IEnumerable<int> ids)
        {
            return GetAllSentences()
                .Where(p => ids.Contains(p.Id));
        }

        public IEnumerable<IntentSentence> GetAllSentences()
        {
            return _context.IntentSentences
                .Include(p => p.Intent);
        }

        public IEnumerable<IntentSentence> GetAllSentencesOfIntent(int intentId)
        {
            return GetAllSentences().Where(p => p.Intent.Id == intentId);
        }

        public IEnumerable<Intent> GetAll()
        {
            return _context.Intents
                .Include(p => p.IntentSentences);
        }

        public IEnumerable<Intent> GetAllSortedByCreateDate()
        {
            return GetAll().OrderByDescending(p => p.CreateDate);
        }

        public void Create(IntentInputModel inputModel)
        {
            var existIntent = GetByName(inputModel.Name);

            if (existIntent != null)
            {
                throw new SuchIntentNameAlreadyExists();
            }

            var now = DateTime.Now;

            var intent = new Intent
            {
                Name = inputModel.Name,
                CreateDate = now
            };

            if (inputModel.Sentences != null)
            {
                var sentences = inputModel.Sentences.Select(p => new IntentSentence
                {
                    Text = p.Text,
                    CreateDate = now,
                    Intent = intent
                });

                _context.AddRange(sentences);
            }

            _context.Add(intent);
            _context.SaveChanges();
        }

        public void Edit(IntentInputModel inputModel)
        {
            var intent = Get(inputModel.Id);

            if (intent.Name != inputModel.Name)
            {
                var existIntent = GetByName(inputModel.Name);

                if (existIntent != null)
                {
                    throw new SuchIntentNameAlreadyExists();
                }

                intent.Name = inputModel.Name;
            }

            UpdateSentences(inputModel.Id, inputModel.Sentences);
        }

        public IntentSentence AddSentence(int intentId, IntentSentenceInputModel inputModel)
        {
            var intent = Get(intentId);

            if (intent.IntentSentences.Any(p => p.Text == inputModel.Text))
            {
                throw new SuchIntentSentenceAlreadyExists();
            }

            var sentence = new IntentSentence
            {
                Text = inputModel.Text,
                CreateDate = DateTime.Now,
                Intent = intent
            };

            _context.Add(sentence);
            _context.SaveChanges();

            return sentence;
        }

        public void AddSentences(int intentId, IEnumerable<IntentSentenceInputModel> inputModels)
        {
            var intent = Get(intentId);
            var now = DateTime.Now;

            var sentences = inputModels.Select(p => new IntentSentence
            {
                Text = p.Text,
                CreateDate = now,
                Intent = intent
            });

            _context.AddRange(sentences);
            _context.SaveChanges();
        }

        public void UpdateSentences(int intentId, IEnumerable<IntentSentenceInputModel> inputModels)
        {
            var existSentences = GetAllSentencesOfIntent(intentId);

            _context.RemoveRange(existSentences);

            if (inputModels != null)
            {
                AddSentences(intentId, inputModels);
            }
            else
            {
                _context.SaveChanges();
            }
        }

        public void DeleteSentence(int sentenceId)
        {
            var sentence = GetSentence(sentenceId);

            _context.Remove(sentence);
            _context.SaveChanges();
        }

        public void DeleteSentences(IEnumerable<int> sentenceIds)
        {
            var sentences = GetSentences(sentenceIds);

            _context.RemoveRange(sentences);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var intent = Get(id);

            if (intent == null)
            {
                return;
            }

            if (intent.IntentSentences.Any())
            {
                _context.Remove(intent.IntentSentences);
            }
            
            _context.Remove(intent);
            _context.SaveChanges();
        }
    }
}