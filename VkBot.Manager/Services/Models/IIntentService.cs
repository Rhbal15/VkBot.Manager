using System.Collections.Generic;
using VkBot.Manager.Data;

namespace VkBot.Manager.Services.Models
{
    public interface IIntentService
    {
        Intent Get(int id);
        Intent GetByName(string intentName);
        IEnumerable<Intent> Get(IEnumerable<int> ids);
        IEnumerable<Intent> GetAll();
        IEnumerable<Intent> GetAllSortedByCreateDate();
        IntentSentence GetSentence(int id);
        IEnumerable<IntentSentence> GetSentences(IEnumerable<int> ids);
        IEnumerable<IntentSentence> GetAllSentences();
        IEnumerable<IntentSentence> GetAllSentencesOfIntent(int intentId);
        void Create(IntentInputModel inputModel);
        void Edit(IntentInputModel inputModel);
        void Delete(int id);
        IntentSentence AddSentence(int intentId, IntentSentenceInputModel inputModel);
        void AddSentences(int intentId, IEnumerable<IntentSentenceInputModel> inputModels);
        void UpdateSentences(int intentId, IEnumerable<IntentSentenceInputModel> inputModels);
        void DeleteSentence(int sentenceId);
        void DeleteSentences(IEnumerable<int> sentenceIds);
    }
}