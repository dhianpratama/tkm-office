using System.Collections.Generic;

namespace Core.Models
{
    public class GraphDataModel
    {
        public List<string> Labels { get; set; }
        public List<string> Series { get; set; }
        public List<List<int>> Data { get; set; }
        public int Total { get; set; }

        public GraphDataModel()
        {
            Labels = new List<string>();
            Series = new List<string>();
            Data = new List<List<int>>();
        }
    }
}
