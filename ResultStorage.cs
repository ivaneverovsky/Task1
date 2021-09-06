using System.Collections.Generic;

namespace Task1
{
    class ResultStorage
    {
        private List<ResultBuilder> _resultList = new List<ResultBuilder>();
        public List<ResultBuilder> AllResults
        {
            get
            {
                return _resultList;
            }
        }
        public void AddResult(ResultBuilder rb)
        {
            _resultList.Add(rb);
        }
    }
}