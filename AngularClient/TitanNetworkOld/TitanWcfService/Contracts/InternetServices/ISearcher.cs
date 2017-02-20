using System.Collections.Generic;

namespace TitanWcfService.Contracts.InternetServices
{
    public interface ISearcher
    {
        void SetSearcherEngine(string searcherURL);
        void SetKeyWords(string keyWords);
        List<string> GetLinks();
    }
}